using UnityEngine;
using UnityEngine.UI;

public class ControlsChange : MonoBehaviour
{
    public Image imageKey; // Image component to be changed
    public Button imageBtn; // Button to trigger the key press check
    private bool isWaitingForKey = false;

    void changeKey()
    {
        isWaitingForKey = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        imageBtn.onClick.AddListener(changeKey);
    }

    // Update is called once per frame
    void Update()
    {
        if (isWaitingForKey && Input.anyKeyDown)
        {
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    Debug.Log("Key pressed: " + key);
                    isWaitingForKey = false;

                    // Construct the sprite name based on the key pressed
                    string spriteName = key.ToString() + "-Key";

                    // Load the sprite by name from the Resources folder
                    Sprite newSprite = Resources.Load<Sprite>(spriteName);
                    if (newSprite != null)
                    {
                        imageKey.sprite = newSprite;
                    }
                    else
                    {
                        Debug.LogError("Sprite not found: " + spriteName);
                    }

                    break;
                }
            }
        }
    }
}
