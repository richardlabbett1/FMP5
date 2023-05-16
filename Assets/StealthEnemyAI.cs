using UnityEngine;
using UnityEngine.AI;

public class StealthEnemyAI : MonoBehaviour
{
    public Transform[] waypoints;       // Array of patrol waypoints
    public Transform player;            // Reference to the player object
    public float fieldOfViewAngle = 90f; // AI's field of view angle
    public float detectionTime = 2f;     // Time taken to detect the player
    public float chaseSpeed = 3.5f;      // AI's speed when chasing the player
    public float patrolSpeed = 2f;       // AI's patrol speed
    public float attackRange = 1.5f;     // Range for attacking the player

    private int currentWaypointIndex = 0;    // Current waypoint index
    private NavMeshAgent agent;              // Reference to the NavMeshAgent component
    private bool playerDetected = false;     // Flag indicating if player is detected
    private Vector3 lastKnownPlayerPosition; // Last known player position
    private float detectionTimer = 0f;       // Timer for detection time

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Set initial destination to the first waypoint
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    void Update()
    {
        // If the player is detected, chase the player
        if (playerDetected)
        {
            ChasePlayer();
        }
        else
        {
            // Otherwise, patrol between waypoints
            Patrol();
        }
    }

    void Patrol()
    {
        // If the AI is close enough to the current waypoint, move to the next waypoint
        if (agent.remainingDistance < agent.stoppingDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    void ChasePlayer()
    {
        // If the player is in the AI's field of view
        Vector3 directionToPlayer = player.position - transform.position;
        if (Vector3.Angle(transform.forward, directionToPlayer) < fieldOfViewAngle * 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer, out hit))
            {
                if (hit.collider.gameObject == player.gameObject)
                {
                    // Player is in sight
                    lastKnownPlayerPosition = player.position;
                    agent.SetDestination(player.position);
                    agent.speed = chaseSpeed;

                    // If the AI is within attacking range, attack the player
                    if (Vector3.Distance(transform.position, player.position) <= attackRange)
                    {
                        AttackPlayer();
                    }
                    return;
                }
            }
        }

        // Player is not in sight, go to the last known position
        agent.SetDestination(lastKnownPlayerPosition);
        agent.speed = patrolSpeed;

        // If the AI reaches the last known position, stop chasing
        if (agent.remainingDistance < agent.stoppingDistance)
        {
            playerDetected = false;
        }
    }

    void AttackPlayer()
    {
        // Implement your attack behavior here
        Debug.Log("Attacking player!");
    }

    void OnTriggerEnter(Collider other)
    {
        // If the player enters the AI's trigger zone, start detection process
        if (other.gameObject == player.gameObject)
        {
            detectionTimer = detectionTime;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            Vector3 directionToPlayer = other.transform.position - transform.position;
            float angle = Vector3.Angle(transform.forward, directionToPlayer); if (angle < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToPlayer, out hit))
                {
                    if (hit.collider.gameObject == player.gameObject)
                    {
                        // Start detection process
                        detectionTimer -= Time.deltaTime;
                        if (detectionTimer <= 0f)
                        {
                            playerDetected = true;
                            agent.speed = chaseSpeed;
                        }
                    }
                }
            }
        }
    }
}