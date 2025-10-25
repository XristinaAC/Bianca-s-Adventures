using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class BattleStatsManager : MonoBehaviour
{
    [SerializeField] float max_health = 10.0f;
    [SerializeField] float current_health = 10.0f;
    [SerializeField] float damage = 0.0f;

    [SerializeField] Image health_bar = null;
    [SerializeField] TMP_Text health_bar_text = null;
    [SerializeField] ParticleSystem target_damage_effect;

    private void Awake()
    {
        current_health = max_health;
        health_bar_text.text = current_health.ToString() + " / " + max_health.ToString();
    }

    void Enemy_Death_Effect()
    {
        if (target_damage_effect)
        {
            ParticleSystem target_damage = Instantiate(target_damage_effect, this.transform.position, Quaternion.identity);
            target_damage.Play();
            Destroy(target_damage, 0.5f);
        }
    }

    void Update_Health_Bar()
    {
        if (health_bar && health_bar_text)
        {
            health_bar.fillAmount = current_health / max_health;
            health_bar_text.text = current_health.ToString() + " / " + max_health.ToString();
        }
    }

    public void Take_Damage(float opponent_damage)
    {
        if(current_health > 0)
        {
            if(opponent_damage > current_health)
            {
                current_health = 0;
            }
            else
            {
                current_health -= opponent_damage;
                Enemy_Death_Effect();
            }

            Update_Health_Bar();
        }
    }

    public bool Is_Dead()
    {
        if (current_health <= 0)
        {
            return true;
        }
        return false;
    }
    
    public void Heal(float heal)
    {
        current_health += heal;
    }

    public void Replenish_Health()
    {
        current_health = max_health;
    }

    public void Upgrade_Health(float new_health)
    {
        max_health += new_health;
        if(current_health < max_health)
        {
            current_health += new_health;
        }
        else
        {
            current_health = max_health;
        }

        Update_Health_Bar();
    }

    public void Upgrade_Damage(float upgrade)
    {
        damage += upgrade;
    }


    public void Set_Damage(float new_damage)
    {
        damage += new_damage;
    }

    public void Set_Health_Bar()
    {
        if (health_bar)
        {
            health_bar.fillAmount = current_health / max_health;
        }
    }

    public float Get_Damage()
    {
        return damage;
    }

    public float Get_Health()
    {
        return current_health;
    }

    public float GetMaxHealth()
    {
        return max_health;
    }
}
