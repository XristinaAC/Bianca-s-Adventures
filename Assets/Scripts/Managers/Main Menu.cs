using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject stats_panel;

    private void Awake()
    {
        SaveManager.save_instance.Load_Data();
    }

    private void Start()
    {
        AudioManager.Instance.PlayMusic(AudioManager.Instance.mainMenuMusic);
    }

    public void Start_Game()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonEffecct);
        SceneManager.LoadSceneAsync("VampireSurvivors");
    }

    public void Open_Settings_Menu()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonEffecct);
        SettingsMenu.sm_instance.gameObject.SetActive(true);
    }

    public void Open_Stats_Menu()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonEffecct);
        stats_panel.SetActive(true);
    }

    public void Quit_Game()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonEffecct);
        Application.Quit();
    }
}
