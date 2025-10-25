using Unity.VisualScripting;
using UnityEngine;

public class Enemy : BattleStatsManager
{
    [SerializeField] float speed = 1.0f;
    [SerializeField] int killed_xp;

    GameObject target = null;
    float angle = 180.0f;
    float damage_time = 0.0f;
    int level = 1;
    int next_level = 6;

    private void Awake()
    {
        target = GameObject.FindAnyObjectByType<Player>().GameObject();
    }
  
    public virtual void Enemy_Movement()
    {
        if (target && Vector3.Distance(target.transform.position, transform.position) > 1.0f )
        {
            Vector3 distance = (target.transform.position - transform.position).normalized;
            Move(distance);
            Rotate(distance);
        }
    }

    public void Move(Vector3 distance)
    {
        var step = speed * Time.deltaTime;
        transform.position += distance * step;
    }

    public void Rotate(Vector3 distance)
    {
        if (Vector3.Dot(distance, transform.TransformDirection(Vector3.right)) > 0)
        {
            transform.Rotate(0, angle, 0);
        }
    }

    public virtual void Attack(float attack_distance)
    {
        if (Vector3.Distance(transform.position, target.transform.position) < attack_distance)
        {
            damage_time += Time.deltaTime;
            if (damage_time >= 1.0f)
            {
                target.GetComponent<Player>().Take_Damage(Get_Damage());
                damage_time = 0.0f;
            }
        }
    }

    public void Level_Up()
    {
        if (level == next_level)
        {
            float new_damage = Get_Damage() + 2;
            Set_Damage(new_damage);
            next_level += 6;
            return;
        }

        level += 1;
    }

    public int Get_Killed_XP()
    {
        return killed_xp;
    }

    public GameObject Get_Target()
    {
        return target;
    }
}
