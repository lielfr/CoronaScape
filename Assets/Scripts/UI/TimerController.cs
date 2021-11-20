using GameEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    #region Fields
    private Text timerText;
    private float remaining;
    private bool isEnabled;
    #endregion

    #region Properties
    public bool IsEnabled { get => isEnabled; }
    #endregion

    private void Awake()
    {
        remaining = GameplayManager.LevelTime;
        isEnabled = false;
    }

    private void Start()
    {
        timerText = GetComponent<Text>();
        SetText(remaining);
    }

    void Update()
    {
        if (isEnabled)
        {
            remaining -= Time.deltaTime;
            if (remaining <= 0f)
            {
                remaining = 0f;
                isEnabled = false;
                gameObject.SendMessageUpwards(Messages.GameOver.ToString());
            }
            SetText(remaining);
        }
    }

    public void ResetTimer(float value)
    {
        remaining = value;
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

    public void GameOver()
    {
        isEnabled = false;
        gameObject.SetActive(false);
    }
}
