using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : BattleStatsManager
{
    [SerializeField] float speed = 2.0f;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] Transform battle_pos;

    [SerializeField] Image level_xp_bar = null;
    [SerializeField] GameObject enemy_damage_text = null;
    [SerializeField] AudioClip s_sword_swing = null;
    [SerializeField] AudioClip s_level_up = null;
    [SerializeField] AudioClip s_upgrading = null;
    [SerializeField] AudioClip s_hitting_enemy = null;

    [SerializeField] TMP_Text ui_score_text = null;
    [SerializeField] TMP_Text ui_level_text = null;


    Animator animator = null;
    AudioSource audio = null;
    int level = 1;
    int current_XP = 0;
    int LevelingUp_XP = 10;
    int enemies_killed = 0;
    bool is_stunned = false;

    bool has_ability;
    string ability;

    void Set_Up_Variables()
    {
        ui_level_text.text = "Level: " + level.ToString();
        ui_score_text.text = "Score: " + enemies_killed.ToString();
        animator = GetComponent<Animator>();
        level_xp_bar.fillAmount = 0;
        has_ability = false;
        ability = "";
        audio = GetComponent<AudioSource>();
    }

    void Start()
    {
        Set_Up_Variables();
    }

    public void Player_Controller()
    {
        if (is_stunned == false)
        {
            Vector3 movement = Set_Player_Movement();

            Set_Player_Rotation(movement.x);
            Melee_Attack();
        }
    }

    Vector3 Set_Player_Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(x, y, 0);

        transform.position += movement.normalized * Time.deltaTime * speed;

        return movement;
    }

    void Set_Player_Rotation(float x_axis_movement)
    {
        if (x_axis_movement != Vector3.zero.x)
        {
            float angle = Mathf.Atan2(0.0f, x_axis_movement) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        }
    }

    public void Set_If_Stunned(bool stunned)
    {
        is_stunned = stunned;
    }

    //Animation methonds ---------------------------------
    void Melee_Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("isAttacking", true);
            Play_Sound_Effect(s_sword_swing);
        }
    }

    public void Upgrading(bool upgrading)
    {
        animator.SetBool("isUpgrading", upgrading);
        Play_Sound_Effect(s_upgrading);
    }

    void Finish_Upgrading()
    {
        animator.SetBool("isUpgrading", false);
    }

    void Finish_Slashing()
    {
        animator.SetBool("isSlashing", false);
    }

    void Set_Damage_Text(Collider2D col)
    {
        int damage = (int)Get_Damage();
        enemy_damage_text.GetComponent<TextMeshPro>().text = damage.ToString();
        GameObject text = enemy_damage_text;
        Vector3 text_pos = col.GameObject().transform.position + new Vector3(0, 1, 0);
        Instantiate(text, text_pos, Quaternion.identity);
    }

    void Attack()
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(battle_pos.position, 0.3f, enemyLayer);
        if (col.Length > 0)
        {
            for(int i=0; i < col.Length;++i)
            {
                if (enemy_damage_text != null)
                {
                    Play_Sound_Effect(s_hitting_enemy);
                    Set_Damage_Text(col[i]);
                    col[i].GetComponent<Enemy>().Take_Damage(Get_Damage());
                }
            }
        }   
    }

    void Finish_Melee_Attack()
    {
        animator.SetBool("isAttacking", false);
    }

    //-------------------------------------------------------


    public bool Level_Up()
    {
        if(current_XP >= LevelingUp_XP)
        {
            Debug.Log("Hi");
            level++;
            current_XP = 0;
            LevelingUp_XP += 5;
            Play_Sound_Effect(s_level_up);
            ui_level_text.text = "Level: " + level.ToString();
            return true;
        }

        return false;
    }

    public void Update_XP(int enemy_xp)
    {
        current_XP += enemy_xp;
        level_xp_bar.fillAmount = (level_xp_bar.fillAmount + current_XP) / LevelingUp_XP;
    }

    public void Set_XP_Bar_Zero()
    {
        level_xp_bar.fillAmount = 0; ;
    }

    public void Update_Kills()
    {
        enemies_killed++;
        ui_score_text.text = "Score: " + enemies_killed.ToString();
    }
   
    public void Set_XP_Bar(int fill)
    {
        level_xp_bar.fillAmount = 0;
    }

    //public void Set_Ability(bool has_ability, string ability)
    //{
    //    this.has_ability = has_ability;
    //    this.ability = ability;
    //}

    public bool Has_Ability()
    {
        return has_ability;
    }    

    public void Upgrade_Speed(float upgrade)
    {
        speed += upgrade;
    }

    public void Play_Sound_Effect(AudioClip sound_effect)
    {
        audio.clip = sound_effect;
        audio.Play();
    }

    public int Get_Level()
    {
        return level;
    }

    public string Get_Gained_Ability()
    {
        return ability;
    }

    public Animator Get_Animator()
    {
        return animator;
    }

    public int Get_Score()
    {
        return enemies_killed;
    }
}
