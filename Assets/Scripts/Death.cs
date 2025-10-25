using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    [SerializeField] GameObject loader = null;
 
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(this.gameObject.activeInHierarchy)
        {
            loader.GetComponent<AudioSource>().Stop();
            this.GetComponent<AudioSource>().Play();
        }
    }

    public void Exit_Game()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
