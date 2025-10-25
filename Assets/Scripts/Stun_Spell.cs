using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Stun_Spell : MonoBehaviour
{
    float stun_time = 0.0f;

    public void Initialize_Stun_Time(float time)
    {
        stun_time = time;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
      if ((col.GetComponent<Player>() != null))
      {
            Player player = col.GetComponent<Player>();
            player.Set_If_Stunned(true);
            player.enabled = false;
            StartCoroutine(Stun_Timer(stun_time, player));
      }
    }

    IEnumerator Stun_Timer(float stun_time, Player player)
    {
        yield return new WaitForSeconds(stun_time);
        player.Set_If_Stunned(false);
        Destroy(this.GameObject());
    }
}
