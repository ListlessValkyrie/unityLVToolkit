using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private float moveSpeed = 2f;

    [SerializeField]
    private float sprintMultiplier = 1.5f;

    [SerializeField] 
    private float acceleration = 20f;

    [SerializeField] 
    private float deceleration = 25f;

    private Vector2 moveInput = Vector2.zero;

    private Rigidbody rigidbody;

    private bool isSprinting = false;

    private void Awake()
    {
        rigidbody = GetComponentInChildren<Rigidbody>();
        rigidbody.freezeRotation = true;
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        isSprinting = context.performed;
        Debug.Log($"Sprinting: {isSprinting}");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log($"Moving: {moveInput}");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Jumping");
        }
    }

    private void FixedUpdate()
    {
        float currentMoveSpeed = isSprinting ? moveSpeed * sprintMultiplier : moveSpeed;

        Vector3 targetVelocity = new(moveInput.x * currentMoveSpeed, rigidbody.linearVelocity.y, moveInput.y * currentMoveSpeed);

        float rate = moveInput.sqrMagnitude > 0.01f ? acceleration : deceleration;

        rigidbody.linearVelocity = Vector3.MoveTowards(
            rigidbody.linearVelocity,
            targetVelocity,
            rate * Time.fixedDeltaTime);
    }
}
