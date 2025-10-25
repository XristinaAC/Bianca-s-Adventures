using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm_instance = null;

    [SerializeField] GameObject player = null;
    [SerializeField] GameObject battle_Manager = null;
    [SerializeField] GameObject death_menu = null;
    [SerializeField] TMP_Text death_menu_score_text = null;
    [SerializeField] TMP_Text fps_text = null;

    public int score = 0;
    bool playing_state;
    string active_scene_name = "";
    bool player_is_dead = false;

    float time = 0.0f;
    float frame_counter = 0.0f;
    float polling_time = 2.0f;

    private void Awake()
    {
        if (gm_instance == null)
        {
            gm_instance = this;
        }
        else if (gm_instance != this)
        {
            Destroy(this.gameObject);
        }

        active_scene_name = SceneManager.GetActiveScene().name;
        if(active_scene_name == "VampireSurvivors")
        {
            playing_state = true;
        }
        else
        {
            playing_state = false;
        }
           
    }

    public void Handle_Player_Death()
    {
        if (player && player.GetComponent<Player>().Is_Dead())
        {
            playing_state = false;
            player.SetActive(false);
            player_is_dead = true;
            SaveManager.save_instance.Set_Score(player.GetComponent<Player>().Get_Score());

            death_menu_score_text.text = player.GetComponent<Player>().Get_Score().ToString();
            death_menu.gameObject.SetActive(true);
        }
    }

    void FPS_Count()
    {
        time += Time.deltaTime;
        frame_counter++;

        if (time >= polling_time)
        {
            int frame_rate = Mathf.RoundToInt(frame_counter / time);
            fps_text.text = "FPS: " + frame_rate.ToString();

            time -= polling_time;
            frame_counter = 0;
        }
    }

    private void Update()
    {
        FPS_Count();

       if (playing_state && player && battle_Manager)
       {
            player.GetComponent<Player>().Player_Controller();
            battle_Manager.GetComponent<EnemyManager>().Handle_Enemy_Movement();
            battle_Manager.GetComponent<BattleManager>().Battle();
       }

        Handle_Player_Death();
    }

    public void Playing_Game(bool is_playing)
    {
        playing_state = is_playing;
    }

    public bool Get_Playing_State()
    {
        return playing_state;
    }

    public string Get_Active_Scene()
    {
        return active_scene_name;
    }

    public bool Get_Is_Player_Dead()
    {
        return player_is_dead;
    }
}
