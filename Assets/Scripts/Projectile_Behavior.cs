using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField] float damage = 10.0f;
    [SerializeField] string target;
    [SerializeField] GameObject damage_text;

    Vector3 velocity = new();
  
    public void Initialize_Velocity(Vector3 direction, float speed)
    { 
        velocity = direction * speed;
    }

    void Update()
    {
        transform.position += velocity;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.GameObject().tag == target)
        {
            if (col.GetComponent<Enemy>() != null)
            {
                Enemy enemy = col.GetComponent<Enemy>();
                Text_Damage_Setup(enemy);
                enemy.Take_Damage(damage);
                Destroy(this.GameObject()); 
            }
            else if ((col.GetComponent<Player>() != null))
            {
                Player player = col.GetComponent<Player>();
                player.Take_Damage(damage);
                Destroy(this.GameObject());
            }
        }
    }

    void Text_Damage_Setup(Enemy enemy)
    {
        damage_text.GetComponent<TextMeshPro>().text = damage.ToString();
        GameObject text = damage_text;
        Vector3 text_pos = enemy.GameObject().transform.position + new Vector3(0, 1, 0);
        Instantiate(text, text_pos, Quaternion.identity);
    }
}
