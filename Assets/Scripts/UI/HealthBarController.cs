using GameEnums;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    #region Fields
    private Slider slider;
    private Image fill;
    private float maxHealth;
    private bool isEmpty;
    private float currentHealth;
    #endregion

    private bool handlingMessage = false;
    private void Awake()
    {
        maxHealth = GameplayManager.Health;
    }

    private void Start()
    {
        slider = GetComponent<UnityEngine.UI.Slider>();
        fill = GameObject.Find("HealthBarFill").GetComponent<Image>();
        ResetHealth(maxHealth);
    }

    public void ResetHealth(float value)
    {
        maxHealth = value;
        currentHealth = value;
        isEmpty = currentHealth <= 0;
        Fill();
    }


    public IEnumerator PlayerDamaged(float value)
    {
        if (!handlingMessage)
        {
            handlingMessage = true;
            currentHealth -= value;
            Fill();
            yield return new WaitForSeconds(2);
            handlingMessage = false;
        }
    }

    public void AddHealth(float value)
    {
        currentHealth += value;
        Fill();
    }

    public void HideBar()
    {
        slider.gameObject.SetActive(false);
        fill.gameObject.SetActive(false);
    }

    public void DisplayBar()
    {
        slider.gameObject.SetActive(true);
        fill.gameObject.SetActive(true);
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
                FindObjectOfType<GameplayManager>().GameOver();
            }
        }
    }
}
