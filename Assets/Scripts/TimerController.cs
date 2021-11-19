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
    private bool isEnabled = false;

    [SerializeField]
    private float remaining;
    #endregion

    public bool IsEnabled { get => isEnabled; }

    void Update()
    {
        if (isEnabled)
        {
            remaining -= Time.deltaTime;
            if (remaining <= 0)
            {
                remaining = 0;
                isEnabled = false;
                GameManager.Instance.IsGameOver = true;
            }
            SetText(remaining);
        }
    }

    public void ResetTimer(float value)
    {
        remaining = ++value;
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
