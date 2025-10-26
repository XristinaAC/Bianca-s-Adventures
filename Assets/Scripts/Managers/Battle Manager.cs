using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] GameObject player = null;
    [SerializeField] GameObject frost_orb = null;
    [SerializeField] GameObject slash = null;
    [SerializeField] Transform ability_to_direction = null;
    [SerializeField] GameObject frost_wave = null;
    [SerializeField] float casting_cooldown = 2.5f;
    [SerializeField] float expand_enemy_pool_time = 3.0f;
    [SerializeField] AudioClip s_slash = null;

    GameObject frost_orb_ability = null;
    Dictionary<string, Action> sub;
    float w_last_cast = 0.0f;
    float basic_enemies_time = 0;
    int expand_pool_level = 5;

    public void Battle()
    {
        
        this.GetComponent<Upgrade_Manager>().Execute_Ability();
        Enemy_Handler();
        Handle_Enemy_Attack();

        Expand_Pool_Faster();
    }


    void Expand_Enemy_Pool()
    {
        basic_enemies_time += Time.deltaTime * 0.1f;
        if (basic_enemies_time >= expand_enemy_pool_time)
        {
            GetComponent<EnemyManager>().Expand_Enemy_Pool(true);
            basic_enemies_time = 0.0f;
        }
    }

    void Expand_Pool_Faster()
    {
        if (GetComponent<EnemyManager>().Get_Enemies_Pool().Count > 0)
        {
            if (expand_pool_level <= player.GetComponent<Player>().Get_Level())
            {
                expand_pool_level += 5;
                expand_enemy_pool_time = expand_enemy_pool_time - 0.3f;
            }
            Expand_Enemy_Pool();
        }
    }

    void Enemy_Handler()
    {
        GetComponent<EnemyManager>().Spawn();
        GetComponent<EnemyManager>().Handle_Enemy_Death(GetComponent<EnemyManager>().Get_Enemies());
    }

    void Handle_Enemy_Attack()
    {
        GetComponent<EnemyManager>().Attack_Player(player);
    }
    
   
   //--------------------------------------------------------------------------------------------------- 
    
    
    
    
    
    
    
    void Frost_Ability()
    {
       
        if (Input.GetMouseButtonDown(1))
        {
            if (frost_orb_ability == null)
            {
                Vector3 orb_pos = new Vector3(player.transform.position.x, player.transform.position.y + 1f, 0);
                frost_orb_ability = Instantiate(frost_orb, orb_pos, Quaternion.identity);
            }

            frost_orb_ability.GetComponent<FrostSpikeAbility>().Initialize_User(player, ability_to_direction);
            Frost_Spike_Shoot();
            Destroy(frost_orb_ability, 5.0f);
        }
    } 

    void Frost_Spike_Shoot()
    {
        frost_orb_ability.GetComponent<FrostSpikeAbility>().Frost_Spike_Creation();
    }

    void Wave_Ability()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if(Time.time - w_last_cast >= casting_cooldown)
            {
                GameObject new_wave = Instantiate(frost_wave, player.transform.position, Quaternion.identity);
                Destroy(new_wave, 1.0f);
                w_last_cast = Time.time;
            }
        }
    }
}
