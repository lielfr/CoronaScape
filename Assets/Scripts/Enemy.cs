using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float dangerRadius = 200.0f;

    private Transform target;
    private NavMeshAgent agent;

    private float health = 100f;
    private float laserDamage = 30f;

    private void Start()
    {
        switch (GameManager.Instance.Difficulty)
        {
            case GameEnums.Difficulty.NONE:
                laserDamage = 30f;
                dangerRadius = 200.0f;
                break;
            case GameEnums.Difficulty.EASY:
                laserDamage = 30f;
                dangerRadius = 200.0f;
                break;
            case GameEnums.Difficulty.MEDIUM:
                laserDamage = 20f;
                dangerRadius = 250.0f;
                break;
            case GameEnums.Difficulty.HARD:
                laserDamage = 20f;
                dangerRadius = 300.0f;
                break;
            case GameEnums.Difficulty.EXTREME:
                laserDamage = 10f;
                dangerRadius = 400.0f;
                break;
            default:
                break;
        }

        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    public void Update()
    {
        /*
        float distance = Vector3.Distance(transform.position, target.position);
        if(distance < dangerRadius)
        {
            agent.SetDestination(target.position);
        } 
        */
            
    }

    public void TakeDamage()
    {
        health -= laserDamage;
        if(health <= 0)
            Destroy(this);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, dangerRadius);
    }
}
