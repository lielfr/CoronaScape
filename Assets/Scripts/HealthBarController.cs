using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    #region Private Fields
    private float currentHealth;
    private bool isEmpty;

    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Image fill;

    [SerializeField]
    private float maxHealth;
    #endregion


    public void Init(float value)
    {
        maxHealth = value;
        currentHealth = value;
        isEmpty = currentHealth <= 0;
        SetFill();
    }

    public void AddHealth(float value)
    {
        currentHealth += value;
        SetFill();
    }

    private void SetFill()
    {
        if (isEmpty) // Think about a better way...
            return;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        slider.value = currentHealth;
        if (slider.value > 0)
        {
            if (currentHealth <= maxHealth && currentHealth > maxHealth / 2)
                fill.color = Color.green;
            else
            {
                if (currentHealth <= maxHealth / 2 && currentHealth > maxHealth / 4)
                    fill.color = Color.yellow;
                else
                    fill.color = Color.red;
            }
        }
        else
        {
            slider.value = maxHealth;
            fill.color = Color.black;
            isEmpty = true;
        }
    }
}
