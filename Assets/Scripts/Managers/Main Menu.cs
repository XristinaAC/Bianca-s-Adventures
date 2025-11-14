using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject stats_panel;

    Dictionary<int, Action> menu_action;

    private void Awake()
    {
        SaveManager.save_instance.Load_Data();
    }

    private void Start()
    {
        menu_action = new Dictionary<int, Action>();
        menu_action.Add(0, Start_Game);
        menu_action.Add(1, Open_Settings_Menu);
        menu_action.Add(2, Open_Stats_Menu);
        menu_action.Add(3, Quit_Game);
        AudioManager.Instance.PlayMusic(AudioManager.Instance.mainMenuMusic);
    }

    public void Menu_Action(int id)
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonEffecct);
        menu_action[id]();
    }

    public void Start_Game()
    {
        SceneManager.LoadSceneAsync("VampireSurvivors");
    }

    public void Open_Settings_Menu()
    {
        SettingsMenu.sm_instance.gameObject.SetActive(true);
    }

    public void Open_Stats_Menu()
    {
        stats_panel.SetActive(true);
    }

    public void Quit_Game()
    {
        Application.Quit();
    }
}
