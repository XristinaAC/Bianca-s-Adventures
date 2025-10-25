using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SlashAbility : MonoBehaviour
{
    [SerializeField] float damage = 5.0f;
    [SerializeField] GameObject damage_text;

    Vector3 slash_velocity = Vector3.zero;
    GameObject player = null;
 
    public void Initialize_Velocity(Vector3 direction, float speed,GameObject player)
    {
        slash_velocity = direction * speed;
        this.player = player;
    }

    void Update()
    {
        this.transform.position += slash_velocity;
    }

    void Enemy_Knockback(GameObject player, float knockback_force, GameObject enemy)
    {
        Vector3 direction = (enemy.transform.position - player.transform.position).normalized;
        Vector3 new_pos = new Vector3(enemy.transform.position.x + direction.x, enemy.transform.position.y + direction.y, 0);
        enemy.transform.position += direction * knockback_force;
        enemy.transform.position += direction;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.GetComponent<Enemy>() != null)
        {
            Text_Damage_Setup(col.gameObject);
            col.GetComponent<Enemy>().Take_Damage(damage);
            Enemy_Knockback(player, 1, col.gameObject);
            Destroy(this.gameObject);
        }
    }

    void Text_Damage_Setup(GameObject enemy)
    {
        damage_text.GetComponent<TextMeshPro>().text = damage.ToString();
        GameObject text = damage_text;
        Vector3 text_pos = enemy.transform.position + new Vector3(0, 1, 0);
        Instantiate(text, text_pos, Quaternion.identity);
    }
}
