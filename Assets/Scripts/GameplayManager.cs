using GameEnums;
using TMPro;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    #region Required References
    public TextMeshProUGUI gameOverText;
    private PlayerMovement movementController;
    private TimerController timerController;
    private HealthBarController healthBarController;
    private ScoreController scoreController;
    #endregion


    #region Fields
    private bool isGameOver = false;
    private bool handlingMessage = false;
    private static int coinScore;
    private static int boxScore;
    private static float health;
    private static float maxHeal;
    private static float maxDamage;
    private static float levelTime;
    private static float timePotion;
    #endregion

    #region Difficulty
    private static Difficulty difficuly;
    public static Difficulty Difficulty
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
            }
        }
    }
    #endregion

    #region Properties
    public static float LevelTime { get => levelTime; }
    public static float Health { get => health; }
    #endregion

    private void Awake()
    {
        if (SceneChanger.Difficulty == 0)
            Difficulty = Difficulty.EASY;
        if (SceneChanger.Difficulty == 1)
            Difficulty = Difficulty.MEDIUM;
        if (SceneChanger.Difficulty == 2)
            Difficulty = Difficulty.HARD;
        if (SceneChanger.Difficulty == 3)
            Difficulty = Difficulty.EXTREME;
        movementController = FindObjectOfType<PlayerMovement>();
        timerController = FindObjectOfType<TimerController>();
        healthBarController = FindObjectOfType<HealthBarController>();
        scoreController = FindObjectOfType<ScoreController>();
        gameOverText.enabled = false;
    }

    public void Collect(CollectableItems type)
    {
        switch (type)
        {
            case CollectableItems.Coin:
                gameObject.BroadcastMessage("AddScore", coinScore);
                break;
            case CollectableItems.Box:
                gameObject.BroadcastMessage("AddScore", Random.Range(0, boxScore + 1));
                break;
            case CollectableItems.RedPotion:
                gameObject.BroadcastMessage("AddTime", timePotion);
                break;
            case CollectableItems.GreenPotion:
                gameObject.BroadcastMessage("AddHealth", Random.Range(0f, maxHeal));
                break;
            case CollectableItems.BluePotion: // not implemented
                break;
            default:
                break;
        }
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            gameOverText.enabled = true;
            movementController.enabled = false;
            timerController.enabled = false;
            scoreController.enabled = false;
            healthBarController.HideBar();
            healthBarController.enabled = false;
            isGameOver = true;
        }
    }

    public void TakeDamage()
    {
        if (!handlingMessage)
        {
            handlingMessage = true;
            gameObject.BroadcastMessage("PlayerDamaged", /*-Random.Range(0f, maxDamage)*/ 100);
            handlingMessage = false;
        }
    }



}
