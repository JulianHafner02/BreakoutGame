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
    private string filePath = "Assets/equations.txt";


    private Equation[] equations = new Equation[10];

    void Start() {
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
                mathq.text = "Correct";
                mathq.color = Color.green;
                gameController.AddScore(500);
                equations[index].isCorrect = true;
                WriteArrayToFile(equations[index], filePath);
            } else {
                mathq.text = "Incorrect";
                mathq.color = Color.red;
                gameController.AddScore(-250);
                equations[index].isCorrect = false;
                WriteArrayToFile(equations[index], filePath);
            }
            Debug.Log(equations[index].equationString +" "+ equations[index].result +" "+ equations[index].answer + " " + equations[index].isCorrect); 
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

    public struct Equation {
        public string equationString;
        public int result;
        public int answer;
        public Boolean isCorrect;

        public Equation(string equationString, int result, int answer, Boolean isCorrect) {

            this.equationString = equationString;
            this.result = result;
            this.answer = answer;
            this.isCorrect = isCorrect;
        }

        public override string ToString()
        {
            return $"{equationString},{result},{answer},{isCorrect}";
        }
    }

    public static void WriteArrayToFile(Equation equation, string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath, append: true))
        {
            writer.WriteLine(equation.ToString());
        }
    }

    public void InvokeMathEvent() {
        TextMeshProUGUI mathq = GameObject.Find("Math").GetComponent<TextMeshProUGUI>();
        mathq.color = Color.black;
        int number1 = GenerateRandomNumber();
        int number2 = GenerateRandomNumber();
        mathq.text = number1 + " + " + number2 + " =";
        mathresult = number1 + number2;
        index += 1;
        equations[index] = new Equation(number1 + " + " + number2, mathresult, 0, false);
    }
}
