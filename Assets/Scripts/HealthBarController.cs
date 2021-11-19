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

    public void ResetHealth(float value)
    {
        maxHealth = value;
        currentHealth = value;
        isEmpty = currentHealth <= 0;
        Fill();
    }

    public void AddHealth(float value)
    {
        currentHealth += value;
        Fill();
    }

    private void Fill()
    {
        if (!isEmpty)
        {
            currentHealth = currentHealth > maxHealth ? maxHealth : currentHealth;
            if (currentHealth > 0)
            {
                slider.value = currentHealth;
                if (maxHealth / 2 < currentHealth && currentHealth <= maxHealth)
                    fill.color = Color.green;
                if (maxHealth / 4 < currentHealth && currentHealth <= maxHealth / 2)
                    fill.color = Color.yellow;
                if (currentHealth <= maxHealth / 4)
                    fill.color = Color.red;
            }

            else
            {
                slider.value = maxHealth;
                fill.color = Color.black;
                isEmpty = true;
            }
        }
    }
}
