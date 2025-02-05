using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour
{
    public TMP_Text counterText; // Reference to the TextMeshPro UI element
    private int count; // Variable to hold the count

    void Start()
    {
        // If counterText is not set in the Inspector, find it by name
        if (counterText == null)
        {
            counterText = GameObject.Find("CounterText").GetComponent<TMP_Text>();
        }

        if (counterText == null)
        {
            Debug.LogError("Counter Text UI element not found!");
            return;
        }

        count = 0; // Initialize the count
        UpdateCounterText(); // Update the counter text at the start

        // Call the IncrementCounter method every second
        InvokeRepeating("IncrementCounter", 1f, 1f);
    }

    void IncrementCounter()
    {
        count++; // Increment the count
        UpdateCounterText(); // Update the counter text
    }

    void UpdateCounterText()
    {
        counterText.text = "Counter: " + count.ToString();
    }
}
