using TMPro;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    private TextMeshProUGUI gameOver;

    private void Awake()
    {
        gameOver = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        gameOver.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        gameOver.gameObject.SetActive(true);
    }
}
