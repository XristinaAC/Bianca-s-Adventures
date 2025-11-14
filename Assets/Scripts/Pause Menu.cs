using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject quit_panel;
    [SerializeField] GameObject pause_menu;

    Dictionary<int, Action> menu_action;

    void Start()
    {
        menu_action = new Dictionary<int, Action>();
        menu_action.Add(0, Continue_Game);
        menu_action.Add(1, Open_Settings_Menu);
        menu_action.Add(2, Exit_Game);
        menu_action.Add(3, Exit_Panel_Yes);
        menu_action.Add(4, Exit_Panel_No);

        if (pause_menu && quit_panel)
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

    public void Menu_Action(int id)
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonEffecct);
        menu_action[id]();
    }

    private void Continue_Game()
    {
        pause_menu.gameObject.SetActive(false);
        GameManager.gm_instance.Playing_Game(true);
    }

    private void Open_Settings_Menu()
    {
        SettingsMenu.sm_instance.GameObject().SetActive(true);
    }

    private void Exit_Game()
    {
        quit_panel.SetActive(true);
    }

    private void Exit_Panel_Yes()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private void Exit_Panel_No()
    {
        quit_panel.SetActive(false);
    }
}

