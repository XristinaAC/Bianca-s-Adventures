using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyManager : MonoBehaviour
{
    [Serializable]
    public struct Enemies_Type
    {
        [SerializeField] public GameObject enemy_to_pool;
        [SerializeField] public int times_to_pool;
    };

    [SerializeField] float current_spawn_time = 1;
    [SerializeField] float spawn_time = 1;
    [SerializeField] private GameObject[] spawning_pos = null;
    [SerializeField] AudioClip s_enemy_death = null;
    [SerializeField] List<Enemies_Type> Basic_Enemies = null;
    [SerializeField] List<Enemies_Type> Bosses = null;

    [SerializeField] ParticleSystem death_particle_effect;

    List<List<GameObject>> pooled_enemies = new();
    List<List<GameObject>> pooled_bosses = new();
    List<GameObject> enemies = new();
    
    AudioSource enemy_audio = null;

    int boss_level= 5;
    int target_level = 1;
    bool boss_alive = false;

    void Initializing_Enemy_Pools()
    {
        pooled_enemies.Add(ObjectPool.instance.Initializing_Pool_Objects(Basic_Enemies[0].enemy_to_pool, Basic_Enemies[0].times_to_pool));
        pooled_bosses.Add(ObjectPool.instance.Initializing_Pool_Objects(Bosses[0].enemy_to_pool, Bosses[0].times_to_pool));
        Basic_Enemies.RemoveAt(0);
        Bosses.RemoveAt(0);
    }

    private void Start()
    {
        Initializing_Enemy_Pools();
        enemy_audio = GetComponent<AudioSource>();
    }

    public void Handle_Enemy_Movement()
    {
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i] != null)
            {
                enemies[i].GetComponent<Enemy>().Enemy_Movement();
            }
        }
    }

    void Play_Enemy_Death_Effects(GameObject enemy)
    {
        //enemy_audio.clip = s_enemy_death;
        //enemy_audio.Play();
        Transform particle_position = enemy.transform;
        ParticleSystem enemy_death = Instantiate(death_particle_effect, particle_position.position, Quaternion.identity);
        enemy_death.Play();
        AudioManager.Instance.PlaySFX(s_enemy_death);
        Destroy(enemy_death, 2f);
    }
  
    void Updating_Boss_Level_Spawn(GameObject enemy)
    {
        if(boss_alive)
        {
            Debug.Log("boss kill");
            boss_level += 5;
            boss_alive = false;
        }
    }
    
    void Enemy_Death_Actions(GameObject enemy,int position)
    {
        enemy.GetComponent<Enemy>().Level_Up();
        enemy.GetComponent<Enemy>().Replenish_Health();
        enemy.GetComponent<Enemy>().Get_Target().GetComponent<Player>().Update_Kills();
        enemy.GetComponent<Enemy>().Get_Target().GetComponent<Player>().Update_XP(enemy.GetComponent<Enemy>().Get_Killed_XP());
        target_level = enemy.GetComponent<Enemy>().Get_Target().GetComponent<Player>().Get_Level();
        enemy.gameObject.SetActive(false);
        enemies.RemoveAt(position);
    }

    public void Handle_Enemy_Death(List<GameObject> enemies)
    {
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i] != null && enemies[i].GetComponent<Enemy>().Is_Dead())
            {
                Updating_Boss_Level_Spawn(enemies[i]);
                Play_Enemy_Death_Effects(enemies[i]);
                Enemy_Death_Actions(enemies[i], i);
            }
        }
    }   

    public void Spawn()
    {
        current_spawn_time += 0.5f * Time.deltaTime;
        if (current_spawn_time >= spawn_time)
        {
            Create_Enemies();
            current_spawn_time = 0;
        }
    }

    void New_Enemy_Creation(int pool_index, Vector3 enemy_spawning_position, List<List<GameObject>> pool)
    {
        GameObject new_enemy = ObjectPool.instance.Activating_Pool_Objects(pool[pool_index], pool[pool_index].Count);
        if (new_enemy)
        {
            new_enemy.transform.position = enemy_spawning_position;
            new_enemy.SetActive(true);
            enemies.Add(new_enemy);
        }

       if(pool == pooled_bosses)
        {
            boss_alive = true;
        }
    }

    void Create_Enemies()
    {
        int index = UnityEngine.Random.Range(0, spawning_pos.Length - 1);
        Vector3 enemy_spawn_pos = spawning_pos[index].transform.position;
        int index_p = UnityEngine.Random.Range(0, pooled_enemies.Count);

        if(boss_level > target_level && !boss_alive)
        {
            New_Enemy_Creation(index_p, enemy_spawn_pos, pooled_enemies);
        }
        else
        {
            New_Enemy_Creation(0, enemy_spawn_pos, pooled_bosses);
        }
    }

    public void Expand_Enemy_Pool(bool expand_pool)
    {
        if(expand_pool)
        {
            pooled_enemies.Add(ObjectPool.instance.Initializing_Pool_Objects(Basic_Enemies[0].enemy_to_pool, Basic_Enemies[0].times_to_pool));
            Basic_Enemies.RemoveAt(0);
        }
    }

    public void Attack_Player(GameObject target)
    {
        if (!target || enemies.Count == 0)
        {
            return;
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            float attack_distance = 1.0f;
            enemies[i].GetComponent<Enemy>().Attack(attack_distance);
           
        }
    }

    public List<Enemies_Type> Get_Enemies_Pool()
    {
        return Basic_Enemies;
    }

    public List<GameObject> Get_Enemies()
    {
        return enemies;
    }
}
