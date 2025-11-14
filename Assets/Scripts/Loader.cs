using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField] GameObject settings_menu;
    [SerializeField] GameObject save_manager;
    [SerializeField] GameObject player;

    private void Awake()
    {
        if (SettingsMenu.sm_instance == null)
        {
            Instantiate(settings_menu);
        }

        if (SaveManager.save_instance == null)
        {
            Instantiate(save_manager);
        }

        if (GetComponent<AudioSource>())
        {
            //SettingsMenu.sm_instance.Set_Audio_Source(this.GetComponent<AudioSource>());
            //this.GetComponent<AudioSource>().volume = SettingsMenu.sm_instance.Get_Audio_Value();
        }
    }
}
