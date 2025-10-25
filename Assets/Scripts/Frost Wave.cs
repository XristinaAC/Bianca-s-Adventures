using TMPro;
using UnityEngine;

public class FrostWave : MonoBehaviour
{
    [SerializeField] float damage = 5.0f;
    [SerializeField] GameObject damage_text;

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.GetComponent<Enemy>() != null))
        {
           Enemy enemy = col.GetComponent<Enemy>();
           Text_Damage_Setup(enemy.gameObject);
           enemy.GetComponent<Enemy>().Take_Damage(damage);
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
