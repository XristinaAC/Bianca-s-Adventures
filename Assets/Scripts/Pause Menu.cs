using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject quit_panel;
    [SerializeField] GameObject pause_menu;
   
    void Start()
    {
        if(pause_menu && quit_panel)
        {
            pause_menu.gameObject.SetActive(false);
            quit_panel.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.gm_instance.Get_Is_Player_Dead() == false && GameManager.gm_instance.Get_Playing_State())
        {
            pause_menu.gameObject.SetActive(true);
            GameManager.gm_instance.Playing_Game(false);
        }
    }

    public void Continue_Game()
    {
        pause_menu.gameObject.SetActive(false);
        GameManager.gm_instance.Playing_Game(true);
    }

    public void Open_Settings_Menu()
    {
        SettingsMenu.sm_instance.GameObject().SetActive(true);
    }

    public void Exit_Game()
    {
        quit_panel.SetActive(true);
    }

    public void Exit_Panel_Yes()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Exit_Panel_No()
    {
        quit_panel.SetActive(false);
    }
}

