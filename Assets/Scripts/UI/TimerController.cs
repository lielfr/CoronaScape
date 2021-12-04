using GameEnums;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    #region Singleton
    public static TimerController instance;
    public void Awake()
    {
        instance = this;
        isEnabled = false;
    }
    #endregion

    #region Fields
    public TextMeshProUGUI timerText;
    private float remaining;
    private bool isEnabled;
    #endregion

    #region Properties
    public bool IsEnabled { get => isEnabled; }
    #endregion

    void Update()
    {
        if (isEnabled)
        {
            remaining -= Time.deltaTime;
            if (remaining <= 0f)
            {
                remaining = 0f;
                FindObjectOfType<GameplayManager>().GameOver();
                isEnabled = false;
            }
            SetText(remaining);
        }
    }

    public void ResetTimer(float value)
    {
        remaining = value;
        SetText(remaining);
    }

    public void StartTimer()
    {
        isEnabled = true;
    }

    public void StopTimer()
    {
        isEnabled = false;
    }

    public void AddTime(float value)
    {
        remaining += value;
    }

    private void SetText(float value)
    {
        float mins = Mathf.FloorToInt(value / 60);
        float secs = Mathf.FloorToInt(value % 60);
        timerText.text = $"{mins:00}" + ":" + $"{secs:00}";
    }
}
