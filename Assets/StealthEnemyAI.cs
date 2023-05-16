using UnityEngine;
using UnityEngine.AI;

public class StealthEnemyAI : MonoBehaviour
{
    public Transform[] waypoints;   // List of waypoints for the AI to patrol
    public float fieldOfView = 90f; // Field of view angle for the AI
    public float detectionTime = 2f; // Time taken by the AI to detect the player
    public float speed = 3.5f;      // Movement speed of the AI

    private NavMeshAgent agent;
    private int currentWaypointIndex;
    private Transform player;
    private Vector3 lastKnownPosition;
    private bool playerDetected;
    private float detectionTimer;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentWaypointIndex = 0;
        lastKnownPosition = transform.position;
        playerDetected = false;
        detectionTimer = 0f;

        PatrolToNextWaypoint();
    }

    private void Update()
    {
        if (!playerDetected)
        {
            if (CanSeePlayer())
            {
                detectionTimer += Time.deltaTime;
                if (detectionTimer >= detectionTime)
                {
                    // Player detected
                    playerDetected = true;
                    detectionTimer = 0f;
                    lastKnownPosition = player.position;
                }
            }
            else
            {
                detectionTimer = 0f;
            }

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                PatrolToNextWaypoint();
            }
        }
        else
        {
            // Move towards the player's last known position
            agent.SetDestination(lastKnownPosition);

            if (CanSeePlayer())
            {
                // Player still visible, update last known position
                lastKnownPosition = player.position;
            }
            else
            {
                detectionTimer += Time.deltaTime;
                if (detectionTimer >= detectionTime)
                {
                    // Player lost
                    playerDetected = false;
                    detectionTimer = 0f;
                    PatrolToNextWaypoint();
                }
            }
        }
    }

    private void PatrolToNextWaypoint()
    {
        agent.SetDestination(waypoints[currentWaypointIndex].position);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    private bool CanSeePlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        if (angle <= fieldOfView * 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
        }

        return false;
    }
}