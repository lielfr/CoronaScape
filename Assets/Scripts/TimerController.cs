using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public float levelTime = 1 * 60 + 30;
    public Text timerText;

    void Start()
    {

    }

    void Update()
    {
        float leftTime = levelTime - Time.time;

        if (leftTime <= 0)
        {
            Debug.Log("Game Over");
            return;
        }
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
