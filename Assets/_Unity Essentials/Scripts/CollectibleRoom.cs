using UnityEngine;

public class CollectibleRoom : MonoBehaviour
{
    public float rotationSpeed;
    public GameObject onCollectEffect;


    // Hover settings
    public float hoverAmplitude = 0.1f; // The height of the hover
    public float hoverFrequency = 1f; // The speed of the hover

    // Original position of the object
    private Vector3 startPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed);

        // Calculate the new Y position
        float newY = startPos.y + hoverAmplitude * Mathf.Sin(Time.time * hoverFrequency);

        // Set the new position
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Instantiate the particle effect
            Instantiate(onCollectEffect, transform.position, transform.rotation);

            // Increment the score using the GameManager
            //GameManager.instance.AddScore(1);

            // Destroy the collectible
            Destroy(gameObject);

            //Debug.Log("Player collected an item. Total Score: " + GameManager.instance.score);
        }
    }
}
