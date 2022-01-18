using GameEnums;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    #region Singleton
    public static GameplayManager instance;
    public void Awake()
    {
        instance = this;
    }
    #endregion

    #region Required References
    public Text greenPotionText;
    public Text redPotionText;
    public Text bluePotionText;
    public Text keyText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI youWonText;
    public TextMeshProUGUI countdownText;
    public PlayerMovement movementController;
    public ProceduralFloorGenerator proceduralGenerator;
    private TimerController timerController;
    private HealthBarController healthBarController;
    private ScoreController scoreController;
    private MenuController menuController;
    #endregion

    #region Fields
    private int greenPotionCount = 0;
    private int redPotionCount = 0;
    private int bluePotionCount = 0;
    private int keyCount = 0;
    private bool handlingPotion = false;
    private bool isGameOver = false;
    private bool playerWon = false;
    private bool handlingMessage = false;
    private static int coinScore;
    private static int boxScore;
    private static float health;
    private static float maxHeal;
    private static float maxDamage;
    public static float levelTime;
    private static float timePotion;
    #endregion

    #region Difficulty
    private static Difficulty difficuly = Difficulty.EASY;
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

    void Start()
    {
        if (SceneChanger.Difficulty == 0)
            Difficulty = Difficulty.EASY;
        if (SceneChanger.Difficulty == 1)
            Difficulty = Difficulty.MEDIUM;
        if (SceneChanger.Difficulty == 2)
            Difficulty = Difficulty.HARD;
        if (SceneChanger.Difficulty == 3)
            Difficulty = Difficulty.EXTREME;

        timerController = TimerController.instance;
        healthBarController = HealthBarController.instance;
        scoreController = ScoreController.instance;
        menuController = MenuController.instance;

        StartNewGame();
    }

    public void StartNewGame()
    {
        movementController.enabled = false;
        menuController.IsDisabled = true;
        gameOverText.enabled = false;
        youWonText.enabled = false;
        timerController.ResetTimer(levelTime);
        healthBarController.ResetHealth(health);
        healthBarController.HideBar();
        redPotionCount = 0;
        greenPotionCount = 0;
        bluePotionCount = 0;
        redPotionText.text = "0";
        greenPotionText.text = "0";
        bluePotionText.text = "0";
        keyText.text = $"0/{proceduralGenerator.numRooms}";
        handlingPotion = false;

        StartCoroutine(CountdownAndRun(3));
    }

    public void PauseGame()
    {
        movementController.enabled = false;
        healthBarController.HideBar();
        timerController.StopTimer();
    }

    public void ContinueGame()
    {
        menuController.IsDisabled = true;
        StartCoroutine(CountdownAndRun(3));
    }

    private IEnumerator CountdownAndRun(int seconds)
    {
        countdownText.enabled = true;
        int count = seconds;
        while (count > 0)
        {
            countdownText.text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }
        countdownText.enabled = false;
        menuController.IsDisabled = false;
        movementController.enabled = true;
        healthBarController.DisplayBar();
        timerController.StartTimer();
    }

    public void Collect(CollectableItems type)
    {
        switch (type)
        {
            case CollectableItems.Coin:
                scoreController.AddScore(coinScore);
                break;
            case CollectableItems.Box:
                scoreController.AddScore(Random.Range(0, boxScore + 1));
                break;
            case CollectableItems.RedPotion:
                redPotionCount++;
                redPotionText.text = redPotionCount.ToString();
                break;
            case CollectableItems.GreenPotion:
                greenPotionCount++;
                greenPotionText.text = greenPotionCount.ToString();
                break;
            case CollectableItems.BluePotion:
                bluePotionCount++;
                bluePotionText.text = bluePotionCount.ToString();
                break;
            case CollectableItems.Key:
                keyCount++;
                keyText.text = $"{keyCount}/{proceduralGenerator.numRooms}";
                if (keyCount == proceduralGenerator.numRooms)
                {
                    PlayerWon();
                }
                break;
            default:
                break;
        }
    }

    public void UseGreenPotion()
    {
        if (greenPotionCount > 0 && !handlingPotion)
            StartCoroutine(TakePotionWithCooldown(1, CollectableItems.GreenPotion));
    }

    public void UseRedPotion()
    {
        if (redPotionCount > 0 && !handlingPotion)
            StartCoroutine(TakePotionWithCooldown(1, CollectableItems.RedPotion));
    }

    public void UseBluePotion()
    {
        if (bluePotionCount > 0 && !handlingPotion)
            StartCoroutine(TakePotionWithCooldown(1, CollectableItems.BluePotion));
    }

    private IEnumerator TakePotionWithCooldown(float seconds, CollectableItems type)
    {
        handlingPotion = true;
        ApplyPotionEffect(type);
        yield return new WaitForSeconds(seconds);
        handlingPotion = false;
    }

    private void ApplyPotionEffect(CollectableItems type)
    {
        switch (type)
        {
            case CollectableItems.RedPotion:
                redPotionCount--;
                redPotionText.text = redPotionCount.ToString();
                timerController.AddTime(timePotion);
                break;
            case CollectableItems.GreenPotion:
                greenPotionCount--;
                greenPotionText.text = greenPotionCount.ToString();
                healthBarController.AddHealth(Random.Range(0f, maxHeal));
                break;
            case CollectableItems.BluePotion:
                bluePotionCount--;
                bluePotionText.text = bluePotionCount.ToString();
                break;
            default:
                break;
        }
    }

    private void GameEnd()
    {
        movementController.enabled = false;
        timerController.enabled = false;
        scoreController.enabled = false;
        healthBarController.HideBar();
        healthBarController.enabled = false;
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            gameOverText.enabled = true;
            GameEnd();
            isGameOver = true;
        }
    }

    public void TakeDamage()
    {
        if (!handlingMessage)
        {
            handlingMessage = true;
            StartCoroutine(healthBarController.PlayerDamaged(Random.Range(0f, maxDamage)));
            handlingMessage = false;
        }
    }

    public void PlayerWon()
    {
        if (!playerWon)
        {
            youWonText.enabled = true;
            GameEnd();
            playerWon = true;
        }
    }


}
