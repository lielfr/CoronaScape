using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DangerTextController : MonoBehaviour
{
    public TextMeshProUGUI tm;
    private bool isShown = false;
    void Start()
    {
        tm.enabled = isShown;
    }

    void Update()
    {
        tm.enabled = isShown;
        tm.color = new Color(tm.color.r, tm.color.g, tm.color.b, (tm.color.a + 0.01f) % 1);
    }

    public void ShowDangerText()
    {
        isShown = true;
    }

    public void HideDangerText()
    {
        isShown = false;
    }
}
