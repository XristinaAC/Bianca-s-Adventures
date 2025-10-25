using System.Collections;
using UnityEngine;

public class PopUpText : MonoBehaviour
{
    [SerializeField] int coroutine_time = 2;
    void Start()
    {
        StartCoroutine(Pop_Up());
    }

    IEnumerator Pop_Up()
    {
        for(float i = 0; i < coroutine_time; i += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(transform.localScale,Vector3.zero,i);
            yield return null;
        }

        Destroy(this.gameObject,4);
    }
}
