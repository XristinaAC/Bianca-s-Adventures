using UnityEngine;

public class FrostSpikeAbility : MonoBehaviour
{
    [SerializeField] float angle = 0.0f;
    [SerializeField] float f_orbit_speed = 2.0f;
    [SerializeField] float f_orbit_radius = 0.15f;
    [SerializeField] float f_s_speed = 0.0f;
    [SerializeField] float f_shooting_cooldown = 0.0f;
    [SerializeField] GameObject frost_spike_prefab = null;

    float f_last_shooting = 1;
    GameObject user = null;
    Transform towards_direction = null;

    public void Initialize_User(GameObject player, Transform direction)
    {
        user = player;
        towards_direction = direction;
    }

    void Update()
    {
        Set_Frost_Orbit_Position();
    }

    public void Set_Frost_Orbit_Position()
    {
        angle += Time.deltaTime * f_orbit_speed;
        Vector3 f_orbit = new Vector3(0.0f, Mathf.Sin(angle), 0) * f_orbit_radius;

        Vector3 new_pos = new Vector3(user.transform.position.x, f_orbit.y + (user.transform.position.y + 1.0f), 0);
        this.transform.position = new_pos;
    }

    public void Frost_Spike_Creation()
    {
        if (frost_spike_prefab != null)
        {
            if (Time.time - f_last_shooting >= f_shooting_cooldown)
            {
                GameObject new_frost_spike = Instantiate(frost_spike_prefab, transform.position, Quaternion.identity);

                Vector3 direction = new Vector3(new_frost_spike.transform.position.x - towards_direction.transform.position.x, 0, 0).normalized;
                new_frost_spike.GetComponent<ProjectileBehavior>().Initialize_Velocity(direction, f_s_speed);

                f_last_shooting = Time.time;
                Destroy(new_frost_spike, 2.0f);
            }
        }
    }
}
