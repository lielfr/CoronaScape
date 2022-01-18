using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static int Difficulty { get; private set; } = 0;

    public void LoadMainScene()
    {
        ChangeDifficulty();
        SceneManager.LoadScene("ProcedurallyGeneratedLevelDemo");
    }

    public void ChangeDifficulty()
    {
        Difficulty = FindObjectOfType<TMPro.TMP_Dropdown>().value + 1; // 0 Is None, which we don't want.
        GameManager.Instance.Difficulty = (GameEnums.Difficulty)Difficulty;
    }
}
