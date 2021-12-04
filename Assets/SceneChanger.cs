using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static int Difficulty { get; private set; } = 0;

    public void LoadMainScene()
    {
        SceneManager.LoadScene("ProcedurallyGeneratedLevelDemo");
    }

    public void ChangeDifficulty()
    {
        Difficulty = FindObjectOfType<TMPro.TMP_Dropdown>().value;
    }
}
