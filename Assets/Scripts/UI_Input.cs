using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Input : MonoBehaviour
{
    private string input;

    void Start() {
        TMP_InputField inputText = GameObject.FindGameObjectWithTag("Input").GetComponent<TMP_InputField>();
        inputText.ActivateInputField();
    }


    public void UserInput(string s) {
        input = s;
        Debug.Log(input);
    }
}
