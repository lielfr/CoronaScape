using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float dangerRadius = 500.0f;

    private Transform target;
    private NavMeshAgent agent;

    public GameObject player;

    private bool freezeMovement = false;
    private bool isResumeRunning = false;

    private void Start()
    {
        switch (GameManager.Instance.Difficulty)
        {
            case GameEnums.Difficulty.NONE:
                dangerRadius = 200.0f;
                break;
            case GameEnums.Difficulty.EASY:
                dangerRadius = 200.0f;
                break;
            case GameEnums.Difficulty.MEDIUM:
                dangerRadius = 250.0f;
                break;
            case GameEnums.Difficulty.HARD:
                dangerRadius = 300.0f;
                break;
            case GameEnums.Difficulty.EXTREME:
                dangerRadius = 400.0f;
                break;
            default:
                break;
        }

        player = PlayerManager.instance.player;
        agent = GetComponent<NavMeshAgent>();
    }

    public void Update()
    {
        target = player.transform;
        float distance = Vector3.Distance(transform.position, target.position);
        if (!freezeMovement && distance <= dangerRadius)
        {
            SendMessageUpwards("DisplayDanger", null, SendMessageOptions.DontRequireReceiver);
            agent.SetDestination(new Vector3(target.position.x, 0, target.position.z));
            FacePlayer();
        } else
        {
            SendMessageUpwards("HideDanger", null, SendMessageOptions.DontRequireReceiver);
        }
    }

    void FacePlayer()
    {
        Vector3 dir = (target.position - transform.position).normalized;
        Quaternion lookAt = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.y));
        transform.rotation = lookAt;
        Quaternion.Slerp(transform.rotation, lookAt, Time.deltaTime * 300.0f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, dangerRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        SendMessageUpwards("TakeDamage");
        stopMovement();
    }

    private IEnumerator resumeMovement()
    {
        isResumeRunning = true;
        yield return new WaitForSecondsRealtime(3);
        freezeMovement = false;
        isResumeRunning = false;
        agent.isStopped = false;
    }

    public void stopMovement()
    {
        freezeMovement = true;
        agent.isStopped = true;

        if (!isResumeRunning)
            StartCoroutine(resumeMovement());
    }

}
