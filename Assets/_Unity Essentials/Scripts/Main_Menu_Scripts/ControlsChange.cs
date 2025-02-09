using UnityEngine;
using UnityEngine.UI;

public class ControlsChange : MonoBehaviour
{
    public Button[] imageButtons; // Array of buttons to trigger the key press check
    public Image[] buttonImages; // Array of Image components to be changed
    private bool isWaitingForKey = false;
    public GameObject waitingPanel;
    private int currentButtonIndex = -1;

    void ChangeKey(int buttonIndex)
    {
        isWaitingForKey = true;
        currentButtonIndex = buttonIndex;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < imageButtons.Length; i++)
        {
            int index = i; // Capture the current value of i
            imageButtons[i].onClick.AddListener(() => ChangeKey(index));
        }
    }

    /*public void Safe()
    {
        PlayerPrefs.GetString("controlKeys", key);
    }*/

    // Update is called once per frame
    void Update()
    {
        if (isWaitingForKey && Input.anyKeyDown)
        {
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Debug.Log("ESC pressed: Cancel key change");
                    isWaitingForKey = false;
                    break;
                }

                if (Input.GetKeyDown(key) &&
                    ((key >= KeyCode.A && key <= KeyCode.Z) ||
                     (key >= KeyCode.Alpha0 && key <= KeyCode.Alpha9) ||
                     key == KeyCode.Space ||
                     key == KeyCode.LeftShift))
                {
                    Debug.Log("Key pressed: " + key);
                    isWaitingForKey = false;

                    if (currentButtonIndex >= 0 && currentButtonIndex < buttonImages.Length)
                    {
                        // Construct the sprite name based on the key pressed
                        string spriteName = key.ToString() + "-Key";

                        // Load the sprite by name from the Resources folder
                        Sprite newSprite = Resources.Load<Sprite>(spriteName);
                        if (newSprite != null)
                        {
                            buttonImages[currentButtonIndex].sprite = newSprite;

                            // Adjust the image width based on the key pressed
                            RectTransform rt = buttonImages[currentButtonIndex].GetComponent<RectTransform>();
                            if (key == KeyCode.Space || key == KeyCode.LeftShift)
                            {
                                rt.sizeDelta = new Vector2(100, rt.sizeDelta.y); // Set width to 100
                            }
                            else
                            {
                                rt.sizeDelta = new Vector2(50, rt.sizeDelta.y); // Set width to 50
                            }
                        }
                        else
                        {
                            Debug.LogError("Sprite not found: " + spriteName);
                        }
                    }

                    break;
                }
            }
        }

        waitingPanel.SetActive(isWaitingForKey);
    }
}
