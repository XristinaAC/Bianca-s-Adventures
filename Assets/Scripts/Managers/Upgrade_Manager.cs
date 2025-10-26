using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Playables;
using UnityEngine;

public class Upgrade_Manager : MonoBehaviour
{
    [SerializeField] GameObject player = null;
    [SerializeField] GameObject upgrades_menu = null;
    [SerializeField] GameObject abilities_menu = null;
    [SerializeField] TMP_Text level_up_text = null;
    [SerializeField] Transform ability_to_direction = null;
    [SerializeField] GameObject slash_prefab = null;
    [SerializeField] AudioClip s_slash = null;

    [SerializeField] List<GameObject> abilities_upgrades_menu = new();
    GameObject menu_to_activate = null;

    [SerializeField] int upgrade_health = 2;
    [SerializeField] int upgrade_speed = 1;
    [SerializeField] int upgrade_damage = 2;

    Dictionary<int, Action> abilities;

    bool fade = true;
    int ability = -1;

    void Initialize_Abilities_List()
    {
        abilities = new Dictionary<int, Action>();
        //abilities.Add("Frost Spike", Frost_Ability);
        abilities.Add(0, Slash_Ability);
        //abilitiesb.Add("Frost Wave", Wave_Ability);
    }

    private void Awake()
    {
        Initialize_Abilities_List();
    }


    void Start()
    { 
        for (int i = 0; i < abilities_upgrades_menu.Count; ++i)
        {
            abilities_upgrades_menu[i].SetActive(false);
        }
        level_up_text.gameObject.SetActive(false);
    }

    void Update()
    {
        if (player && player.GetComponent<Player>().Level_Up())
        {
            Enabling_Upgrade_State();
        }
    }

    //Activating level text and upgrade menu
    void Enabling_Upgrade_State()
    {
        if (level_up_text != null && !fade)
        {
            level_up_text.CrossFadeAlpha(1, 2, false);
            fade = true;
        }

        int upgrades_choise = UnityEngine.Random.Range(0, abilities_upgrades_menu.Count);
        menu_to_activate = abilities_upgrades_menu[upgrades_choise];

        level_up_text.gameObject.SetActive(true);
        menu_to_activate.gameObject.SetActive(true); 
        GameManager.gm_instance.Playing_Game(false);
        if (level_up_text != null && fade)
        {
            level_up_text.CrossFadeAlpha(0, 2, false);
            fade = false;
        }
    }

    public void Speed_Upgrade()
    {
        player.GetComponent<Player>().Upgrade_Speed(upgrade_speed++);

        Disable_Level_Up_Menu();
    }

    public void Health_Upgrade()
    {
        player.GetComponent<Player>().Upgrade_Health(upgrade_health++);

        Disable_Level_Up_Menu();
    }

    public void Damage_Upgrade()
    {
        player.GetComponent<Player>().Upgrade_Damage(upgrade_damage++);

        Disable_Level_Up_Menu();
    }

    public void Get_Ability(int ability_id)
    {
        ability = ability_id;
        Disable_Level_Up_Menu();
    }

    public void Execute_Ability()
    {
        if(ability == -1)
        {
            return;
        }
        abilities[ability]();
    }

    void Disable_Level_Up_Menu()
    {
        if (menu_to_activate == abilities_menu)
        {
            abilities_menu.gameObject.SetActive(false);
        }
        else
        {
            upgrades_menu.gameObject.SetActive(false);
        }
        level_up_text.gameObject.SetActive(false);
        player.GetComponent<Player>().Set_XP_Bar(0);
        player.GetComponent<Player>().Upgrading(true);
        GameManager.gm_instance.Playing_Game(true);
    }

    
    
    
    


    //-------Slash
    
    void Slash_Ability()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Play_Abikity_Effects();
            Slash_Creation();
        }
    }

    void Slash_Creation()
    {
        Vector3 new_pos = new Vector3(player.transform.position.x + 1.0f, player.transform.position.y, 0);
        GameObject new_slash = Instantiate(slash_prefab, new_pos, Quaternion.identity);
        Vector3 direction = new Vector3(new_pos.x - ability_to_direction.transform.position.x, 0, 0).normalized;
        new_slash.transform.rotation = player.transform.rotation;
        new_slash.GetComponent<SlashAbility>().Initialize_Velocity(direction, 0.1f, player);
    }

    void Play_Abikity_Effects()
    {
        player.GetComponent<Player>().Play_Sound_Effect(s_slash);
        player.GetComponent<Player>().Get_Animator().SetBool("isSlashing", true);
    }
}
