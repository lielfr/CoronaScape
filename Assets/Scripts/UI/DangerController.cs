using UnityEngine;
using UnityEngine.UI;

public class DangerController : MonoBehaviour
{
    public Image image;
    private bool display;
    void Start()
    {
        image.enabled = display = false;
    }

    void Update()
    {
        image.enabled = display;
    }

    public void DisplayDanger()
    {
        display = true;
    }

    public void HideDanger()
    {
        display = false;
    }
}
