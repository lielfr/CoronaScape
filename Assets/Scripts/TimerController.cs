using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    #region Private Fields
    [SerializeField]
    private Text timerText;

    [SerializeField]
    private bool isRunning = false;

    [SerializeField]
    private float remaining;
    #endregion


    void Update()
    {
        if(isRunning)
        {
            remaining -= Time.deltaTime;
        }
        if (remaining <= 0) {
            remaining = 0;
            isRunning = false;
        }
        SetText(remaining);
    }

    public void Init(float value) 
    {
        remaining = value + 1;
        isRunning = true;
    }

    public void AddTime(float value)
    {
        remaining += value;
    }

    void SetText(float value)
    {
        float mins = Mathf.FloorToInt(value / 60);
        float secs = Mathf.FloorToInt(value % 60);
        timerText.text = $"{mins:00}" + ":" + $"{secs:00}";
    }
}
