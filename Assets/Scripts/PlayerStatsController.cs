using GameEnums;
using TMPro;
using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{
    #region Controllers
    private ScoreController scoreController;
    private HealthBarController healthController;
    private TimerController timerController;
    #endregion

    #region Fields
    [SerializeField]
    private int coinScore = 5;
    [SerializeField]
    private int boxScore = 25;
    [SerializeField]
    private float health = 100f;
    [SerializeField]
    private float maxHeal = 15f;
    [SerializeField]
    private float maxDamage = 10f;
    [SerializeField]
    private float levelTime = 60 * 5 + 30;
    [SerializeField]
    private float timePotion = 30;
    #endregion

    #region Difficulty
    private Difficulty difficuly = Difficulty.NONE;
    public Difficulty Difficulty
    {
        get => difficuly;
        set
        {
            if (difficuly != value)
            {
                switch (value)
                {
                    case Difficulty.EASY:
                        coinScore = 15;
                        boxScore = 25;
                        health = 100f;
                        maxHeal = 15f;
                        maxDamage = 10f;
                        levelTime = 1200f;
                        timePotion = 120f;
                        break;
                    case Difficulty.MEDIUM:
                        coinScore = 10;
                        boxScore = 15;
                        health = 100f;
                        maxHeal = 10f;
                        maxDamage = 10f;
                        levelTime = 900f;
                        timePotion = 60f;
                        break;
                    case Difficulty.HARD:
                        coinScore = 5;
                        boxScore = 15;
                        health = 100f;
                        maxHeal = 10f;
                        maxDamage = 15f;
                        levelTime = 600f;
                        timePotion = 30f;
                        break;
                    case Difficulty.EXTREME:
                        coinScore = 5;
                        boxScore = 10;
                        health = 100f;
                        maxHeal = 5f;
                        maxDamage = 15f;
                        levelTime = 300f;
                        timePotion = 10f;
                        break;
                    default:
                        break;
                }

                difficuly = value;
                ResetStats();
            }
        }
    }
    #endregion

    #region Game Over
    public TextMeshProUGUI gameOver;
    #endregion

    private void Start()
    {
        gameOver.gameObject.SetActive(false);
        scoreController = GetComponent<ScoreController>();
        healthController = GetComponent<HealthBarController>();
        timerController = GetComponent<TimerController>();
        Difficulty = GameManager.Instance.Difficulty;
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver)
            EndGame();
    }

    public void ResetStats()
    {
        scoreController.ResetScore();
        healthController.ResetHealth(health);
        timerController.ResetTimer(levelTime);
        timerController.StartTimer();
    }

    public void PickItem(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.COIN:
                scoreController.AddScore(coinScore);
                break;
            case ItemType.BOX:
                scoreController.AddScore(Random.Range(0, boxScore + 1));
                break;
            default: break;
        }
    }

    public void TakePotion(PotionType potionType)
    {
        switch (potionType)
        {
            case PotionType.RED:
                timerController.AddTime(timePotion);
                break;
            case PotionType.GREEN:
                healthController.AddHealth(Random.Range(0f, maxHeal));
                break;
            case PotionType.BLUE: break;
            default: break;
        }
    }

    public void TakeDamage()
    {
        float damage = Random.Range(0f, maxDamage);
        healthController.AddHealth(-damage);
    }

    public void EndGame()
    {
        timerController.StopTimer();
        healthController.HideBar();

        timerController.gameObject.SetActive(false);
        healthController.gameObject.SetActive(false);
        scoreController.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(true);
    }
}
