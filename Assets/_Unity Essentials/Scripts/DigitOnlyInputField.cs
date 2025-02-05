using TMPro;
using UnityEngine;

public class DigitOnlyInputField : MonoBehaviour
{
    public TMP_InputField inputField;

    void Start()
    {
        // Assign the validation method to the onValidateInput event
        inputField.onValidateInput += ValidateDigitInput;
    }

    private char ValidateDigitInput(string text, int charIndex, char addedChar)
    {
        // Allow only digits (0-9)
        if (char.IsDigit(addedChar))
        {
            return addedChar; // Accept the character
        }
        else
        {
            return '\0'; // Reject the character
        }
    }
}