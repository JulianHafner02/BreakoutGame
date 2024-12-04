using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Input : MonoBehaviour
{
    private int inputResult;
    private TMP_InputField inputText;

    private TextMeshProUGUI mathq;
    private int mathresult;

    [SerializeField] private GameController gameController;
    [SerializeField] private Animator equationAnimator;




    void Start() {
        inputText = GameObject.FindGameObjectWithTag("Input").GetComponent<TMP_InputField>();
        inputText.ActivateInputField();
        mathq = GameObject.Find("Math").GetComponent<TextMeshProUGUI>();
    }
    
    public void UserInput() {
        inputResult = Convert.ToInt32(inputText.text);
        Debug.Log(inputResult);
        inputText.text = "";
        inputText.ActivateInputField();
        Debug.Log("InputResult" + inputResult + "MathResult" + mathresult);
        if (inputResult == mathresult) {
            mathq.text = "Correct";
            mathq.color = Color.green;
            gameController.AddScore(500);
        } else {
            mathq.text = "Incorrect";
            mathq.color = Color.red;
            gameController.AddScore(-250);
        }
    }

    public int GetInput() {
        Debug.Log("inputResult: " + inputResult);
        return inputResult;
    }

    private int GenerateRandomNumber() {
        int number = UnityEngine.Random.Range(1, 31);
        return number;
    }

    public void InvokeMathEvent() {
        TextMeshProUGUI mathq = GameObject.Find("Math").GetComponent<TextMeshProUGUI>();
        mathq.color = Color.black;
        int number1 = GenerateRandomNumber();
        int number2 = GenerateRandomNumber();
        mathq.text = number1 + " + " + number2 + " =";
        mathresult = number1 + number2;
        Debug.Log("Brickscript:" + mathresult);
        equationAnimator.SetTrigger("ShowEquation");  // Setzt den Trigger im Animator Controller
        
    }
}
