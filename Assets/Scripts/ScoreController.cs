using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    #region Private Fields
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private int currentScore;
    #endregion

    public void Init()
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
