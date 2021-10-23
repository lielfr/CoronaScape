using GameEnums;
using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{
    #region Score
    private ScoreController score;

    [SerializeField]
    private int coinScore = 5;
    [SerializeField]
    private int boxScore = 25;
    #endregion

    #region Health
    private HealthBarController healthBar;

    [SerializeField]
    private float health = 100f;
    [SerializeField]
    private float maxHeal = 15f;
    [SerializeField]
    private float maxDamage = 10f;
    #endregion

    #region Time
    private TimerController timer;

    [SerializeField]
    private float levelTime = 60 * 5 + 30;
    [SerializeField]
    private float timePotion = 30;
    #endregion

    private void Start()
    {
        score = GetComponent<ScoreController>();
        score.Init();
        healthBar = GetComponent<HealthBarController>();
        healthBar.Init(health);
        timer = GetComponent<TimerController>();
        timer.Init(levelTime);
    }


    public void PickItem(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.COIN:
                score.AddScore(coinScore);
                break;
            case ItemType.BOX:
                score.AddScore(Random.Range(0, boxScore + 1));
                break;
            default: break;
        }
    }

    public void TakePotion(PotionType potionType)
    {
        switch (potionType)
        {
            case PotionType.RED:
                timer.AddTime(timePotion);
                break;
            case PotionType.GREEN:
                healthBar.AddHealth(Random.Range(0f, maxHeal));
                break;
            case PotionType.BLUE: break;
            default: break;
        }
    }

    public void TakeDamage()
    {
        float damage = Random.Range(0f, maxDamage);
        healthBar.AddHealth(-damage);
    }
}
