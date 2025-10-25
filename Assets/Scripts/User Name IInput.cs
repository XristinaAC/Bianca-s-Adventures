using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserNameIInput : MonoBehaviour
{
    [SerializeField] TMP_Text user_name = null;
    [SerializeField] TMP_Text input_name_text = null;

    private void Start()
    {
        this.gameObject.SetActive(false);
        if (SaveManager.save_instance.Get_Player_Name() == "")
        {
            this.gameObject.SetActive(true);
        }
    }

    public void Close_Input_Stream()
    {
        if(SaveManager.save_instance.Get_Player_Name() == "")
        {
            SaveManager.save_instance.Set_Player_Name(input_name_text.text);
            user_name.text = input_name_text.text;
        }

        this.gameObject.SetActive(false);
    }
}
