using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public Text timerText;
    private float levelTime;
    private float leftTime;
    private float potionTime;
    public void SetLevelTime(float levelTime)
    {
        this.levelTime = levelTime;
        leftTime = levelTime;
    }
    public void SetPotionTime(float potionTime)
    {
        this.potionTime = potionTime;
    }
    public void TimePotion()
    {
        levelTime += potionTime;
    }

    void Update()
    {
        if (leftTime <= 0) { // Need to end the game
            timerText.text = "00:00";
            return;
        }


        leftTime = levelTime - Time.time;
        string currentTime = "";
        int mins = (int)leftTime / 60;
        int secs = (int)leftTime % 60;
        if (mins < 10)
            currentTime += "0";

        currentTime += mins.ToString() + ":";

        if (secs < 10)
            currentTime += "0";

        currentTime += secs.ToString();
        timerText.text = currentTime;
    }
}
