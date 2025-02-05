using UnityEngine;

public class Collectible : MonoBehaviour
{
    public float rotationSpeed;
    public GameObject onCollectEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Optionally rotate the collectible from the start
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Instantiate the particle effect
            Instantiate(onCollectEffect, transform.position, transform.rotation);

            // Increment the score using the GameManager
            GameManager.instance.AddScore(1);

            // Destroy the collectible
            Destroy(gameObject);

            Debug.Log("Player collected an item. Total Score: " + GameManager.instance.score);
        }
    }
}
