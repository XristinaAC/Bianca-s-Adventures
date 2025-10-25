using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject stats_panel;

    private void Awake()
    {
        SaveManager.save_instance.Load_Data();
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
