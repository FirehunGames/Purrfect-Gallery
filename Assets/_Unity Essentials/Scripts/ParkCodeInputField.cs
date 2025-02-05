using UnityEngine;
using TMPro; // For TextMeshPro
using UnityEngine.UI; // For Image
using System.Collections;

public class ParkCodeInputField : MonoBehaviour
{
    public TMP_InputField codeInputField; // Use TMP_InputField instead of InputField
    public AudioSource correctSound; // Reference to the Audio Source
    public AudioSource incorrectSound; // Reference to the Audio Source
    public Image siblingImage;
    public bool gameOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HideInputFieldAndImage();
        // Ensure the input field is not null
        if (codeInputField != null)
        {
            codeInputField.onValueChanged.AddListener(OnInputFieldChanged);
        }
    }

    private void OnInputFieldChanged(string text)
    {
        // Check if the input field has exactly 4 characters
        if (text.Length == 4)
        {
            ValidateCode();
            if (gameOver == false)
            {
                // Start the coroutine to clear the input field after a delay
                StartCoroutine(ClearInputFieldAfterDelay(0.5f));
            }
            else
            {
                Debug.Log("gg");
            }
            
            
            
        }
        else if (text.Length > 4)
        {
            // Temporarily remove the listener to avoid recursion
            codeInputField.onValueChanged.RemoveListener(OnInputFieldChanged);

            // If more than 4 characters are entered, truncate the text
            codeInputField.text = text.Substring(0, 4);

            // Re-add the listener
            codeInputField.onValueChanged.AddListener(OnInputFieldChanged);
        }
    }

    private IEnumerator ClearInputFieldAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Clear the input field and reset it for new input
        codeInputField.text = "";
        codeInputField.ActivateInputField();
    }

    public void ValidateCode()
    {
        if (codeInputField != null)
        {
            string code = codeInputField.text;
            if (code == "1337")
            {
                // Play correct sound
                if (correctSound != null)
                {
                    correctSound.Play();
                    gameOver = true;
                    HideInputFieldAndImage();
                }
            }
            else
            {
                // Play incorrect sound
                if (incorrectSound != null)
                {
                    incorrectSound.Play();
                }

                // Start the shake animation
                StartCoroutine(ShakeInputField(0.5f, 0.1f));
            }
        }
    }

    private IEnumerator ShakeInputField(float duration, float magnitude)
    {
        Vector3 originalPosition = codeInputField.transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = originalPosition.x + Random.Range(-50f, 50f) * magnitude;
            codeInputField.transform.localPosition = new Vector3(x, originalPosition.y, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        codeInputField.transform.localPosition = originalPosition;
    }

    public void ShowInputFieldAndImage()
    {
        if (codeInputField != null)
        {
            codeInputField.gameObject.SetActive(true); // Enable the input field
        }

        if (siblingImage != null)
        {
            siblingImage.gameObject.SetActive(true); // Enable the sibling image
        }
    }

    public void HideInputFieldAndImage()
    {
        // Hide the input field and sibling image by default
        if (codeInputField != null)
        {
            codeInputField.gameObject.SetActive(false); // Disable the input field
        }

        if (siblingImage != null)
        {
            siblingImage.gameObject.SetActive(false); // Disable the sibling image
        }
    }
}