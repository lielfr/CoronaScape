using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager
{
    private static GameManager instance = null;
    private int levelNum;

    private GameManager()
    {
        levelNum = 1;
    }

    public static GameManager GetInstance()
    {
        if (instance == null)
            instance = new GameManager();
        return instance;
    }

    public void NextLevel()
    {
        levelNum++;
    }

    public void RestartGame()
    {
        levelNum = 1;
    }

    public int GetLevel()
    {
        return levelNum;
    }
}