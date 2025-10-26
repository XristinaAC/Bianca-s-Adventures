using UnityEngine;
using UnityEngine.Rendering;

public class OrbitingEnemy : Enemy
{
    [SerializeField] float orbit_speed = 10f;
    [SerializeField] float orbit_radius = 4.0f;
    [SerializeField] GameObject stun_spell_prefab = null;
    [SerializeField] float stun_time = 1.0f;
    [SerializeField] float stun_spell_time = 3.0f;

    float orbit_timer = 0;
    float orbit_time = 0.1f;
    float stun_spell_timer = 0;
    float current_angle = 0;
    
    public override void Enemy_Movement()
    {
        if (!Get_Target())
        {
            return;
        }

        Vector3 distance = (Get_Target().transform.position - transform.position).normalized;

        if (Vector3.Distance(Get_Target().transform.position, transform.position) > 1.0f)
        {
            Orbit();
            orbit_timer += Time.deltaTime * 1.5f;
            if (orbit_timer > orbit_time)
            {
                Move(distance);
                orbit_timer = 0;
            }
        }
    }

    void Orbit()
    {
        current_angle += Time.deltaTime * orbit_speed;
        float rad = current_angle * Mathf.Deg2Rad;

        Vector3 orbitPos = Get_Target().transform.position + new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * orbit_radius;
        transform.RotateAround(Get_Target().transform.position, -Vector3.forward, rad * Time.deltaTime);
        transform.Rotate(0, 180, 0);
    }

    void Orbiting_Stun_Attack()
    {
        Vector3 old_pos = Get_Target().transform.position;
        stun_spell_timer += Time.deltaTime * 0.5f;
        if (stun_spell_timer > stun_spell_time)
        {
            GameObject stun_circle = Instantiate(stun_spell_prefab, old_pos, Quaternion.identity);
            stun_circle.GetComponent<Stun_Spell>().Initialize_Stun_Time(stun_time);
            stun_spell_timer = 0.0f;
        }
    }

    public override void Attack(float attack_distance)
    {
        //If close to player the enemy uses the simple enemy attack
        if (Vector3.Distance(Get_Target().transform.position, transform.position) < attack_distance)
        {
            base.Attack(attack_distance);
        }
        else
        {
            Orbiting_Stun_Attack();
        }
    }
}