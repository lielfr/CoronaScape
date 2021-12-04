using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class MenuController : MonoBehaviour
{
    #region Singleton
    public static MenuController instance;
    public void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField]
    private GameObject menuPanel;
    public bool IsDisabled { get; set; } = true;

    private void Start()
    {
        menuPanel.SetActive(false);
    }

    public void ShowMenu()
    {
        if (!IsDisabled)
        {
            GameplayManager.instance.PauseGame();
            menuPanel.SetActive(true);
        }
    }

    public void BackButtonClicked()
    {
        if (!IsDisabled)
        {
            menuPanel.SetActive(false);
            GameplayManager.instance.ContinueGame();
        }
    }

    public void RestartButtonClicked()
    {
        if (!IsDisabled)
        {
            menuPanel.SetActive(false);
            GameplayManager.instance.StartNewGame();
        }
    }

    public void ExitButtonClicked()
    {
        if (!IsDisabled)
        {
            Application.Quit();
        }
    }
}
