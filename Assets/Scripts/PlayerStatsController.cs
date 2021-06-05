using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{
    /* Player health stats */
    public HealthBarController healthBar;
    private float maxHealth = 100;
    private float healingPotion = 20;
    private float punchDamage = 5;
    /* Player time stats */
    public TimerController timer;
    private float levelTime = 60 * 5 + 30;
    private float timePotion = 30;

    /* Player teleportation stats */
    public int initialTeleportation = 10;
    private int currentTeleportation;

    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetPotionHeal(healingPotion);
        healthBar.SetPunchDamage(punchDamage);
        healthBar.Init();
        timer = GetComponent<TimerController>();
        timer.SetLevelTime(levelTime);
        timer.SetPotionTime(timePotion);
        currentTeleportation = initialTeleportation;
    }

    public void TakeHealingPotion()
    {
        healthBar.HealingPotion();
    }

    public void TakeTimePotion()
    {
        timer.TimePotion();
    }

    public void TakeTeleportationPotion()
    {
        currentTeleportation += 5;
        Debug.Log(currentTeleportation);
    }

    public void TakePunchDamage()
    {
        healthBar.PunchDamage();
    }
}
