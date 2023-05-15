using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class FollowNavMeshAgent : MonoBehaviour
{
    public Transform player;
    public float maxDistance = 10f; // Maximum distance for enemy visibility
    public LayerMask obstacleLayer; // Layer mask for obstacles

    private NavMeshAgent navMeshAgent;
    private bool isPlayerVisible = true; // Flag to track player visibility
    private Vector3 lastKnownPlayerPosition; // Last known position of the player
    private bool hasSpottedPlayer = false; // Flag to track if the player has been spotted

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        lastKnownPlayerPosition = transform.position;
    }

    private void Update()
    {
        if (IsPlayerVisible())
        {
            // Player is visible, update last known position and set the flag to true
            lastKnownPlayerPosition = player.position;
            hasSpottedPlayer = true;
        }

        if (hasSpottedPlayer)
        {
            // Go to the last known position where the player was spotted
            navMeshAgent.destination = lastKnownPlayerPosition;
        }
    }

    private bool IsPlayerVisible()
    {
        Vector3 direction = player.position - transform.position;
        float distance = Vector3.Distance(transform.position, player.position);

        // Check if player is within maximum distance and there are no obstacles blocking the view
        if (distance <= maxDistance && !Physics.Linecast(transform.position, player.position, obstacleLayer))
        {
            isPlayerVisible = true;
        }
        else
        {
            isPlayerVisible = false;
        }

        return isPlayerVisible;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == player)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
