using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float turnSpeed = 100.0f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Move forward and backward with W and S keys
        float moveDirection = 0.0f;
        if (Input.GetKey(KeyCode.W)) moveDirection = 1.0f;
        if (Input.GetKey(KeyCode.S)) moveDirection = -1.0f;

        Vector3 forwardMovement = transform.forward * moveDirection * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + forwardMovement);

        // Turn right and left with D and A keys
        float turnDirection = 0.0f;
        if (Input.GetKey(KeyCode.D)) turnDirection = 1.0f;
        if (Input.GetKey(KeyCode.A)) turnDirection = -1.0f;

        float rotation = turnDirection * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0, rotation, 0);
        rb.MoveRotation(rb.rotation * turnRotation);

        
    }
}
