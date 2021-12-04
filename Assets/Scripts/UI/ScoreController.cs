using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    #region Singleton
    public static ScoreController instance;
    public void Awake()
    {
        instance = this;
    }
    #endregion

    #region Fields
    [SerializeField]
    private TextMeshProUGUI scoreText;
    private int currentScore;
    #endregion

    private void Start()
    {
        ResetScore();
    }

    public void ResetScore()
    {
        currentScore = 0;
        scoreText.text = "GAME SCORE\n0";
    }

    public void AddScore(int value)
    {
        currentScore += value;
        scoreText.text = $"GAME SCORE\n{currentScore}";
    }
}
