using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float viewDistance = 10f;
    public float viewAngle = 45f;
    public float moveSpeed = 5f;

    private Transform player;
    private Vector3 lastKnownPlayerPosition;
    private bool playerInSight;

    public enum State { Idle, Searching, Attacking };
    public State currentState;

    private void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = State.Idle;
       
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                Idle();
                break;
            case State.Searching:
                Search();
                break;
            case State.Attacking:
                Attack();
                break;
        }
    }

    private void Idle()
    {
        if (CanSeePlayer())
        {
            
            lastKnownPlayerPosition = player.position;
            playerInSight = true;
            currentState = State.Attacking;
            Debug.Log("lol");
        }
    }

    private void Search()
    {
        transform.position = Vector3.MoveTowards(transform.position, lastKnownPlayerPosition, moveSpeed * Time.deltaTime);

        if (transform.position == lastKnownPlayerPosition)
        {
            currentState = State.Idle;
        }

        if (CanSeePlayer())
        {
            lastKnownPlayerPosition = player.position;
            playerInSight = true;
            currentState = State.Attacking;
        }
    }

    private void Attack()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

        if (!CanSeePlayer() && !playerInSight)
        {
            currentState = State.Searching;
        }

        if (Vector3.Distance(transform.position, player.position) <= 2f)
        {
            // Attack the player
        }
    }

    private bool CanSeePlayer()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        if (angle < viewAngle / 2f)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, direction.normalized, out hit, viewDistance))
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