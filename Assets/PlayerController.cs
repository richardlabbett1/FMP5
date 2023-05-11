using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public PlayerMovement movement;
    public Transform cameraTransform;

    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // Rotate the player to face the camera's forward direction
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
        transform.rotation = targetRotation;

        // Update the animator with movement parameters
        float moveSpeed = movement.isCrouching ? movement.crouchSpeed : movement.speed;
        float moveAmount = Mathf.Clamp01(Mathf.Abs(movement.moveDirection.x) + Mathf.Abs(movement.moveDirection.z));
        animator.SetFloat("MoveSpeed", moveSpeed * moveAmount);
        animator.SetBool("IsCrouching", movement.isCrouching);
        animator.SetBool("IsGrounded", movement.isGrounded);
    }
}






