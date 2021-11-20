using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float health = 100f;
    private float laserDamage = 30f;

    private void Start()
    {
        switch (GameManager.Instance.Difficulty)
        {
            case GameEnums.Difficulty.NONE:
                laserDamage = 30f;
                break;
            case GameEnums.Difficulty.EASY:
                laserDamage = 30f;
                break;
            case GameEnums.Difficulty.MEDIUM:
                laserDamage = 20f;
                break;
            case GameEnums.Difficulty.HARD:
                laserDamage = 20f;
                break;
            case GameEnums.Difficulty.EXTREME:
                laserDamage = 10f;
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.CompareTag("Player"))
            gameObject.SendMessageUpwards("TakeDamage");

        if (other.CompareTag("Laser"))
            TakeDamage();
    }

    public void TakeDamage()
    {
        health -= laserDamage;
        if(health <= 0)
            Destroy(this);
    }
}
