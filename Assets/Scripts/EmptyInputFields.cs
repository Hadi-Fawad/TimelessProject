using UnityEngine;
using UnityEngine.UI;

public class EmptyInputFields : MonoBehaviour
{
    public InputField[] inputFields; // Array of input fields
    public Button button; // Button to be disabled

    void Update()
    {
        bool anyInputFieldEmpty = false;

        // Check if any input field is empty
        foreach (InputField inputField in inputFields)
        {
            if (string.IsNullOrEmpty(inputField.text))
            {
                anyInputFieldEmpty = true;
                break;
            }
        }

        // Disable the button if any input field is empty, enable it otherwise
        button.gameObject.SetActive(!anyInputFieldEmpty);
    }
}
