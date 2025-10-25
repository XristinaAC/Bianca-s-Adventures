using UnityEngine;

public class CameraControler : MonoBehaviour
{
    //Set for following the player
    [SerializeField] GameObject player = null;

    Vector3 offset = new();
 
    void Start()
    {
        offset = transform.position - player.transform.position;

    }

    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
