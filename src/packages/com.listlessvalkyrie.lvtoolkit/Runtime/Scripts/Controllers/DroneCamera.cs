using UnityEngine;

public class DroneCamera : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private Rigidbody rigidBody;

    private float force = 20f;

    private float maxSpeed = 10f;

    [Header("Position")]
    [SerializeField]
    private Vector3 offset = new Vector3(0f, 15f, 0f);

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();

        if (rigidBody == null)
            throw new MissingComponentException("DroneCamera requires a Rigidbody.");

        rigidBody.useGravity = false;
        rigidBody.linearDamping = 3f;
    }

    private void FixedUpdate()
    {
        if (target == null)
            return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 direction = desiredPosition - rigidBody.position;

        rigidBody.AddForce(direction * force, ForceMode.Acceleration);

        if (rigidBody.linearVelocity.magnitude > maxSpeed)
            rigidBody.linearVelocity = rigidBody.linearVelocity.normalized * maxSpeed;
    }

    private void LateUpdate()
    {
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2f * Time.deltaTime);
    }
}
