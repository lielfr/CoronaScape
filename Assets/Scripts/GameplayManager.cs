using GameEnums;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    private Canvas canvas;

    #region Fields
    private bool isGameOver = false;
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

    public static float LevelTime { get => levelTime; }
    public static float Health { get => health; }
    public bool IsGameOver { get => IsGameOver; }



    void Start()
    {

    }

    void Update()
    {

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
        gameObject.BroadcastMessage(Messages.GameOver.ToString());
        isGameOver = true;
    }

    
}
