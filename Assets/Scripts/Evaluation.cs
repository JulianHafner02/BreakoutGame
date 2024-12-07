using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

public class Evaluation : MonoBehaviour
{
    private string filePath1;
    private string filePath2;
    private string filePath3;

    [SerializeField]
    private TextMeshProUGUI gamedatarow;
    private int correctEquationCount = 0;
    private int sumEquations = 0;

    // Start is called before the first frame update
    void Start()
    {        
        filePath2 = "Assets/Level2.txt";
        filePath3 = "Assets/Level3.txt";
        filePath1 = "Assets/Level1.txt";
        GameObject returnButton = GameObject.Find("Return");
        returnButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnClickReturn);
        GameObject level1button = GameObject.Find("Level1");
        level1button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => onClickLevel("Level1"));
        GameObject level2button = GameObject.Find("Level2");    
        level2button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => onClickLevel("Level2"));
        GameObject level3button = GameObject.Find("Level3");
        level3button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => onClickLevel("Level3"));
    }

    void OnClickReturn()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelection");
    }

    public void writeLevelEquations(string filePath) {
        Boolean isCorrect = false;
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] parts = line.Split(',');
                if (parts.Length == 4)
                {
                    string equation = parts[0];
                    string answer = parts[1];
                    string correctAnswer = parts[2];
                    string correctlyAnswered = parts[3];
                    if (correctlyAnswered == "True") {
                        sumEquations++;
                        correctEquationCount++;
                        isCorrect = true;
                    }  if (correctlyAnswered == "False") {
                        sumEquations++;
                        isCorrect = false;
                    }
                    GameData gameData = new GameData(equation, answer, correctAnswer, correctlyAnswered);
                    if (lines[i].Contains("Level"))    
                    {
                        gamedatarow.text += "<b>" + gameData.ToString() + "</b>\n";
                    } else {
                        if (isCorrect) {
                            gamedatarow.text += "<color=#0d7527>" + gameData.ToString() + "</color>\n";
                        } else {
                            gamedatarow.text += "<color=red>" + gameData.ToString() + "</color>\n";
                        }
                    }
                    isCorrect = false;
                }
                else
                {
                    Debug.LogError($"Invalid line format: {line}");
                }
            }
        }
    }

    public void evaluationOutput() {
        gamedatarow.text += "<b>Correct equations :</b>" + correctEquationCount + "\n" + "<b>Sum of Equations:</b>" + sumEquations + "\n";
        gamedatarow.text += "<b>Correct Equations%:</b>" + string.Format("{0:0.##}", ((float)correctEquationCount / (float)sumEquations) * 100) + "<b>%</b>";
    }


    private struct GameData
    {
        public string level;
        public string sumScore;
        public string sumTime;
        public string gameConc;

        public GameData(string level, string sumScore, string sumTime, string gameConc)
        {
            this.level = level;
            this.sumScore = sumScore;
            this.sumTime = sumTime;
            this.gameConc = gameConc;
        }

        public override string ToString()
        {
            return $"{level},  {sumScore},  {sumTime},  {gameConc}";
        }
    }
    
    public void onClickLevel(string level) {    
        if (level == "Level1") {
            gamedatarow.text = "";
            sumEquations = 0;   
            correctEquationCount = 0;
            writeLevelEquations(filePath1);
            evaluationOutput();
        }
        else if (level == "Level2") {
            gamedatarow.text = "";
            sumEquations = 0;   
            correctEquationCount = 0;
            writeLevelEquations(filePath2);
            evaluationOutput();
        }
        else if (level == "Level3") {
            gamedatarow.text = "";
            sumEquations = 0;   
            correctEquationCount = 0;
            writeLevelEquations(filePath3);
            evaluationOutput();
        }
    }
}
