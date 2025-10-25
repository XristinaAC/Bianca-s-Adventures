using UnityEngine;

public class Ranged_Enemy : Enemy
{
    [SerializeField] float shoot_time = 0.5f;
    [SerializeField] float r_speed = 1.0f;
    [SerializeField] float projectile_speed = 0.9f;
    [SerializeField] Transform spawn_projectile_position;
    
    Vector3 distance = Vector3.zero;
    [SerializeField] GameObject energy_projectile_prefab = null;
    float shoot_timer = 0;
  
    void Update()
    {
        Enemy_Movement();
    }

    //Here is checking whether the player is close or somewhat far but is coming towards the player. The enemy's movement is set to always go 
    //away from the player.

    void Set_Position()
    {
        float step = r_speed * Time.deltaTime;
        transform.position -= distance * step;
    }
    void Move_Back_Or_Forth()
    {
        if (Vector3.Dot(Get_Target().transform.TransformDirection(Vector3.right), transform.TransformDirection(Vector3.right)) >= 0)
        {
            Set_Position();
        }
        else if (Vector3.Dot(Get_Target().transform.TransformDirection(Vector3.right), transform.TransformDirection(Vector3.right)) <= 0)
        {
            Set_Position();
        }
    }

    public override void Enemy_Movement()
    {
        if(!Get_Target())
        {
            return;
        }

        distance = (Get_Target().transform.position - transform.position).normalized;
        if (Vector3.Distance(Get_Target().transform.position, transform.position) > 6)
        {
            Move(distance);
        }
        else if(Vector3.Distance(Get_Target().transform.position, transform.position) < 4)
        {
            Move_Back_Or_Forth();
        }
        Rotate(distance);
    }

    public override void Attack(float attack_distance)
    {
        shoot_timer += Time.deltaTime * 0.2f;
        if (shoot_timer > shoot_time)
        {
            GameObject new_energy_projectile = Instantiate(energy_projectile_prefab, spawn_projectile_position.position, Quaternion.identity);

            //The projectile's rotation is set in way to work for all kind's of projectiles. Even arrows. If I don't set it 
            //and arrows are used thay don't point at the player. 
            Quaternion rotation = Quaternion.LookRotation(distance);
            new_energy_projectile.transform.rotation = rotation * Quaternion.Euler(0, 90, 0);
            Vector3 distance2 = (Get_Target().transform.position - transform.position).normalized;
            new_energy_projectile.GetComponent<ProjectileBehavior>().Initialize_Velocity(distance2, projectile_speed);

            Destroy(new_energy_projectile, 2.0f);
            shoot_timer = 0;
        }
    }
}

