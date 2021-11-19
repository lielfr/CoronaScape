using GameEnums;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager
{
    #region Thread Safe Singleton
    private static readonly System.Lazy<GameManager> instance = new System.Lazy<GameManager>(() => new GameManager());
    public static GameManager Instance => instance.Value;
    private GameManager() { }
    #endregion

    public bool IsGameOver { get; set; } = false;

    #region Level
    public int Level { get; private set; } = 1;
    public void NextLevel() => Level++;
    public void RestartGame() => Level = 1;
    #endregion

    #region Difficulty
    public Difficulty Difficulty { get; private set; } = Difficulty.EASY;
    #endregion
}