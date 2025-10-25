using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class StatsMenu : MonoBehaviour
{
    [SerializeField] TMP_Text score_text = null;
    [SerializeField] TMP_Text user_name_text = null;

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    private void Start()
    {
        if(score_text && user_name_text && SaveManager.save_instance)
        {
            score_text.text = SaveManager.save_instance.Get_Score().ToString();
            user_name_text.text = SaveManager.save_instance.Get_Player_Name();
        }
    }

    public void Close_Stats_Menu()
    {
        this.gameObject.SetActive(false);
    }
}
