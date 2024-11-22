using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class UI_Input : MonoBehaviour
{
    private TMP_InputField inputField;
    private TextMeshProUGUI output;

    public void UserInput() {
        inputField.text = output.text;
    }
}
