using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ControlsChange : MonoBehaviour
{
    public Button[] controlBtn; // Array of buttons to trigger the key press check
    public Image[] buttonImages; // Array of Image components to be changed
    private bool isWaitingForKey = false;
    public GameObject waitingPanel;
    private int currentButtonIndex = -1;
    public TMP_Text waitingPanelText;


    void ChangeKey(int buttonIndex)
    {
        isWaitingForKey = true;
        currentButtonIndex = buttonIndex;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadControls();

        GameObject soundManagerObject = GameObject.Find("Volume");

        for (int i = 0; i < controlBtn.Length; i++)
        {
            int index = i; // Capture the current value of i
            controlBtn[i].onClick.AddListener(() => ChangeKey(index));
        }
    }

    public void ResetDefaultValues()
    {
        PlayerPrefs.DeleteAll();
        LoadControls();
    }

    public string ButtonPressed(int btn)
    {
        //func holds the array and depending on the button pressed returns the name of the playerpref
        string btnPressed = btn switch
        {
            0 => "up",
            1 => "left",
            2 => "down",
            3 => "right",
            4 => "jump",
            5 => "run",
            6 => "meow",
            7 => "lookback",
            _ => null
        };
        return btnPressed;
    }

    public string DefaultKeybinds(int btn)
    {
        //func holds the array and depending on the button pressed returns the name of the playerpref
        string defaultKeys = btn switch
        {
            0 => "W",
            1 => "A",
            2 => "S",
            3 => "D",
            4 => "Space",
            5 => "LeftShift",
            6 => "F",
            7 => "Z",
            _ => null
        };
        return defaultKeys;
    }

    public void LoadControls()
    {
        for (int i = 0; i < controlBtn.Length; i++)
        {
            string keys = ButtonPressed(i);
            string defaultValues = DefaultKeybinds(i);
            string value = PlayerPrefs.HasKey(keys) ? PlayerPrefs.GetString(keys) : defaultValues;
            List<string> resizeKeys = new List<string> { "LeftShift-Key", "LeftShift", "SPACE", "Space-Key", "Space" };

            string spriteName = value.EndsWith("-Key") ? value : value + "-Key";
            Sprite newSprite = Resources.Load<Sprite>(spriteName);

            if (newSprite != null)
            {
                buttonImages[i].sprite = newSprite;
                RectTransform rt = buttonImages[i].GetComponent<RectTransform>();

                if (resizeKeys.Contains(value))
                {
                    rt.sizeDelta = new Vector2(100, rt.sizeDelta.y); // Set width to 100
                }
                else
                {
                    rt.sizeDelta = new Vector2(50, rt.sizeDelta.y); // Set width to 50
                }

                //Debug.Log($"Loaded sprite for {keys}: {spriteName}");
            }
            else
            {
                Debug.LogError($"Sprite not found for {keys}: {spriteName}");
            }

            if (!PlayerPrefs.HasKey(keys))
            {
                PlayerPrefs.SetString(keys, defaultValues);
                //Debug.Log($"Default value set for {keys}: {defaultValues}");
            }
        }
    }

    public void SaveControls(int btn, string keybind)
    {
        string btnPressed = ButtonPressed(btn);

        if (btnPressed != null)
        {
            //example (button[0] meaning up, "W")
            PlayerPrefs.SetString(btnPressed, keybind);
        }

        /*Debug.Log("boton presionado: " + btnPressed + "   tecla presionada: " + keybind);
        Debug.Log(PlayerPrefs.GetString(btnPressed, keybind));*/
    }


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
                    waitingPanel.SetActive(false);
                    waitingPanelText.text = "Select a new key <br>A - Z, 0 - 9, Space or Shift<br><br>ESC to exit";
                    break;
                }

                if (Input.GetKeyDown(key) &&
                    ((key >= KeyCode.A && key <= KeyCode.Z) ||
                     (key >= KeyCode.Alpha0 && key <= KeyCode.Alpha9) ||
                     key == KeyCode.Space ||
                     key == KeyCode.LeftShift))
                {

                    bool isDuplicateKey = false;

                    for (int i = 0; i < controlBtn.Length; i++)
                    {
                        string keys = ButtonPressed(i);
                        string thisKey = key.ToString();
                        string assignedKey = PlayerPrefs.GetString(keys);

                        //Debug.Log("Current set key: " + PlayerPrefs.GetString(keys) + " for the input: " + keys);

                        if (assignedKey == thisKey || assignedKey == thisKey + "-Key")
                        {
                            isDuplicateKey = true;
                            Debug.Log("Current set key: " + PlayerPrefs.GetString(keys) + " for the input: " + keys);
                            waitingPanelText.text = thisKey + " is already assigned to the " + keys + " control <br><br> Please select another one";
                            waitingPanel.SetActive(true);
                            break;
                        }
                    }
                    if (!isDuplicateKey)
                    {
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
                                waitingPanelText.text = "Select a new key <br>A - Z, 0 - 9, Space or Shift<br><br>ESC to exit";
                                //call save() to playerprefs
                                SaveControls(currentButtonIndex, spriteName);

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
                                isWaitingForKey = false;
                            }
                        }
                    }
                    

                    break;
                }
            }
        }

        waitingPanel.SetActive(isWaitingForKey);
    }
}
