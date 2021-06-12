using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreController : MonoBehaviour
{
    public Text scoreText;
    private int currentScore;
    private int coinScore;
    private int boxMaxScore;
    private void Start()
    {
        currentScore = 0;
        scoreText.text = "Score: 0";
    }
    public void SetCoinScore(int coinScore)
    {
        this.coinScore = coinScore;
    }
    public void SetBoxMaxScore(int boxMaxScore)
    {
        this.boxMaxScore = boxMaxScore;
    }
    public void Coin()
    {
        currentScore += coinScore;
        scoreText.text = "Score: " + currentScore.ToString();
    }
    public void Box()
    {
        int randomScore = Random.Range(0, boxMaxScore + 1);
        currentScore += randomScore;
        scoreText.text = "Score: " + currentScore.ToString();
    }
}
