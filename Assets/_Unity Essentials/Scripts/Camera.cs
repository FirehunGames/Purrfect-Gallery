using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public Transform player;   // The player or the target the camera follows
    public float distance = 5.0f;   // Default distance from the player
    public float minDistance = 1.0f;   // Minimum distance to keep from the player
    public float maxDistance = 10.0f;  // Maximum distance from the player
    public float height = 2.0f;   // Height offset from the player
    public float verticalOffset = 1.0f;  // Vertical offset to position the player near the bottom of the camera view
    public float smoothSpeed = 10.0f;   // Smooth speed for camera movement

    void LateUpdate()
    {
        // Calculate the desired camera position with the vertical offset
        Vector3 desiredPosition = player.position - player.forward * distance + Vector3.up * (height + verticalOffset);
        RaycastHit hit;

        if (Physics.Linecast(player.position, desiredPosition, out hit))
        {
            // Adjust the camera position to the hit point
            float hitDistance = Vector3.Distance(player.position, hit.point);
            distance = Mathf.Clamp(hitDistance, minDistance, maxDistance);
            transform.position = Vector3.Lerp(transform.position, hit.point + Vector3.up * verticalOffset, smoothSpeed * Time.deltaTime);
        }
        else
        {
            // Smoothly move to the desired position
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }

        //transform.LookAt(player); // Ensure the camera is always looking at the player
    }
}
