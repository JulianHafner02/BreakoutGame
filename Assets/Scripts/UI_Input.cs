using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    [SerializeField]
    private GameController gameController;
    private int mathresult;

    private int index = -1;

    [SerializeField] private Animator equationAnimator;

    [SerializeField] private AudioClip correctSound;
    [SerializeField] private AudioClip incorrectSound;
    [SerializeField] private AudioSource audioSource;
    private GameController.Equation[] equations;

    private string level;

    void Start() {
        level = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        equations = gameController.GetEquations();
        inputText = GameObject.FindGameObjectWithTag("Input").GetComponent<TMP_InputField>();
        inputText.ActivateInputField();
        mathq = GameObject.Find("Math").GetComponent<TextMeshProUGUI>();
    }
    
    public void UserInput() {
        if (inputText.text != null && inputText.text != "") {
            inputResult = Convert.ToInt32(inputText.text);
            equations[index].answer = inputResult;
            inputText.text = "";
            inputText.ActivateInputField();
            if (inputResult == mathresult) {
                audioSource.PlayOneShot(correctSound);
                mathq.text = "Correct";
                mathq.color = Color.green;
                gameController.AddScore(500);
                equations[index].isCorrect = true;
                Debug.Log(equations[index].equationString + " "+ equations[index].isCorrect + " " + equations[index].answer + " " + equations[index].result);
            } else {
                audioSource.PlayOneShot(incorrectSound);
                mathq.text = "Incorrect";
                mathq.color = Color.red;
                gameController.AddScore(-250);
                equations[index].isCorrect = false;
                Debug.Log(equations[index].equationString + " "+ equations[index].isCorrect + " " + equations[index].answer + " " + equations[index].result);
            }
        }
        
    }

    public int GetInput() {
        Debug.Log("inputResult: " + inputResult);
        return inputResult;
    }

    private int GenerateRandomNumber() {
        int number = UnityEngine.Random.Range(1, 100);
        return number;
    }

    private int GenerateSmallRandomNumber(){
        int number1 = UnityEngine.Random.Range(1, 10);
        return number1;
    }

    public void InvokeMathEvent() {
        
        if(level == "Level1") {
            TextMeshProUGUI mathq = GameObject.Find("Math").GetComponent<TextMeshProUGUI>();
            mathq.color = Color.black;
            int number1 = GenerateRandomNumber();
            int number2 = GenerateRandomNumber();
            mathq.text = number1 + " + " + number2 + " =";
            mathresult = number1 + number2;
            index += 1;
            equations[index] = new GameController.Equation(number1 + " + " + number2, mathresult, 0, false);
            equationAnimator.SetTrigger("ShowEquation"); 
        }
        else if(level == "Level2") {
            TextMeshProUGUI mathq = GameObject.Find("Math").GetComponent<TextMeshProUGUI>();
            mathq.color = Color.black;
            int number1 = GenerateRandomNumber();
            int number2 = GenerateRandomNumber();
            mathq.text = number1 + " - " + number2 + " =";
            mathresult = number1 - number2;
            index += 1;
            equations[index] = new GameController.Equation(number1 + " - " + number2, mathresult, 0, false);
            equationAnimator.SetTrigger("ShowEquation"); 
        }
        else if(level == "Level3") {
            TextMeshProUGUI mathq = GameObject.Find("Math").GetComponent<TextMeshProUGUI>();
            mathq.color = Color.black;
            int number1 = GenerateSmallRandomNumber();
            int number2 = GenerateSmallRandomNumber();
            mathq.text = number1 + " * " + number2 + " =";
            mathresult = number1 * number2;
            index += 1;
            equations[index] = new GameController.Equation(number1 + " * " + number2, mathresult, 0, false);
            equationAnimator.SetTrigger("ShowEquation"); 
        }
        
    }
}
