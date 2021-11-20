using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    #region Fields
    private Text scoreText;
    private int currentScore;
    #endregion

    private void Awake()
    {
        scoreText = GameObject.Find("Score").GetComponent<Text>();
    }

    private void Start()
    {
        ResetScore();
    }

    public void ResetScore()
    {
        currentScore = 0;
        scoreText.text = "Score: 0";
    }

    public void AddScore(int value)
    {
        currentScore += value;
        scoreText.text = $"Score: {currentScore}";
    }
}
