using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{
    /* Player score stats */
    private ScoreController score;
    private int coinScore = 5;
    private int boxMaxScore = 25;
    private int patientKill = 25;
    private int nurseKill = 50;
    private int doctorKill = 100;
    private int guardKill = 200;
    /* Player health stats */
    public HealthBarController healthBar;
    private float maxHealth = 100;
    private float healingPotion = 20;
    private float punchDamage = 5;
    /* Player time stats */
    private TimerController timer;
    private float levelTime = 60 * 5 + 30;
    private float timePotion = 30;
    /* Player teleportation stats */
    private int initialTeleportation = 10;
    private int currentTeleportation;

    void Start()
    {
        score = GetComponent<ScoreController>();
        score.SetCoinScore(coinScore);
        score.SetBoxMaxScore(boxMaxScore);
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

    public void PickCoin()
    {
        score.Coin();
    }

    public void PickBox()
    {
        score.Box();
    }

    public void TakePunchDamage()
    {
        healthBar.PunchDamage();
    }
}
