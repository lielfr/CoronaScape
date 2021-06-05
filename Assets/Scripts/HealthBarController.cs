using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    /* HealthBar characteristics*/
    public Slider healthSlider;
    public Image currentHealthFill;
    private float maxHealth;
    private float currentHealth;
    private bool isEmpty;
    /* Game influence on HealthBar */
    private float healingPotion;
    private float punchDamage;

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
    }
    public void SetPotionHeal(float healingPotion)
    {
        this.healingPotion = healingPotion;
    }
    public void SetPunchDamage(float punchDamage)
    {
        this.punchDamage = punchDamage;
    }
    public void Init()
    {
        currentHealth = maxHealth;
        SetCurrentHealthFill();
    }
    public void HealingPotion()
    {
        currentHealth += healingPotion;
        if(currentHealth > maxHealth)
            currentHealth = maxHealth;
        SetCurrentHealthFill();
    }
    public void PunchDamage()
    {
        currentHealth -= punchDamage;
        SetCurrentHealthFill();
    }
    private void SetCurrentHealthFill()
    {
        if(isEmpty) // Think about a better way...
            return;

        healthSlider.value = currentHealth;
        if(healthSlider.value > 0)
        {
            if(currentHealth <= maxHealth && currentHealth > maxHealth / 2)
                currentHealthFill.color = Color.green;
            else
            {
                if(currentHealth <= maxHealth / 2 && currentHealth > maxHealth / 4)
                    currentHealthFill.color = Color.yellow;
                else
                    currentHealthFill.color = Color.red;
            }
        }
        else {
            healthSlider.value = maxHealth;
            currentHealthFill.color = Color.black;
            isEmpty = true;
        }
    }
}
