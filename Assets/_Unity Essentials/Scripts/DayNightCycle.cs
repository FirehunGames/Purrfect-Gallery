using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    // Variable to set the duration of a full day in seconds
    [SerializeField]
    private float dayDuration = 86400f; // Default to 86400 seconds (24 hours)

    private float rotationSpeed;

    void Start()
    {
        // Calculate the rotation speed in degrees per second
        rotationSpeed = 360f / dayDuration;
    }

    void Update()
    {
        // Rotate the light around the X-axis to simulate the sun moving across the sky
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }
}
