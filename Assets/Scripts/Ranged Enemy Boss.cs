using UnityEngine;

public class RangedEnemyBoss : Ranged_Enemy
{
    [SerializeField] Transform spawn_projectile_pos_2 = null;
    [SerializeField] Transform spawn_projectile_pos_3 = null;

    [SerializeField] GameObject projectile = null;

    float p_shoot_timer = 0;
  
    public override void Attack(float attack_distance)
    {
        base.Attack(attack_distance);
        Vector3 distance = (Get_Target().transform.position - transform.position).normalized;
        p_shoot_timer += Time.deltaTime * 0.2f;
        if (p_shoot_timer > 0.5f)
        {
            Create_Extra_Projectile(distance, spawn_projectile_pos_2);
            Create_Extra_Projectile(distance, spawn_projectile_pos_3);
            p_shoot_timer = 0;
        }
    }

    void Create_Extra_Projectile(Vector3 distance, Transform projectile_position)
    {
        GameObject new_energy_projectile2 = Instantiate(projectile, projectile_position.position, Quaternion.identity);
        Quaternion rotation2 = Quaternion.LookRotation(distance);
        new_energy_projectile2.transform.rotation = rotation2 * Quaternion.Euler(0, 90, 0);
        new_energy_projectile2.GetComponent<ProjectileBehavior>().Initialize_Velocity(distance, 0.05f);

        Destroy(new_energy_projectile2, 2.0f);
    }
}
