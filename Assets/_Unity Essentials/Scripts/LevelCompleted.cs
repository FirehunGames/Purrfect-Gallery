using UnityEngine;

public class LevelCompleted : MonoBehaviour
{
    private Animator doorAnimator;

    void Start()
    {
        // Get the Animator component attached to the same GameObject as this script
        doorAnimator = GetComponent<Animator>();
        if (doorAnimator == null)
        {
            Debug.LogError("Animator component not found.");
        }
    }

    public void OpenDoor()
    {
        if (doorAnimator != null)
        {
            // Trigger the Door_Open animation
            doorAnimator.SetTrigger("Door_Open");
        }
        else
        {
            Debug.LogError("Animator component is null.");
        }
    }
}
