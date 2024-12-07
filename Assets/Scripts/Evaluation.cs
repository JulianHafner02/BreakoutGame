using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class Evaluation : MonoBehaviour
{
    private string filePath = "Assets/Level3.txt";
    private string filePath2 = "Assets/Level2.txt";
    private string filePath3 = "Assets/Level3.txt";
    [SerializeField]
    private TextMeshProUGUI gamedatarow;
    private int correctEquationCount = 0;
    private int sumEquations = 0;

    // Start is called before the first frame update
    void Start()
    {        
        writeLevelEquations();
        evaluationOutput();
        GameObject returnButton = GameObject.Find("Return");
        returnButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnClickReturn);
    }

    void OnClickReturn()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelection");
    }

    public void writeLevelEquations() {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            Debug.Log(lines.Length);
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
                    }  if (correctlyAnswered == "False") {
                        sumEquations++;
                    }
                    GameData gameData = new GameData(equation, answer, correctAnswer, correctlyAnswered);
                    gamedatarow.text += gameData.ToString() + "\n";
                }
                else
                {
                    Debug.LogError($"Invalid line format: {line}");
                }
            }
        }
    }

    public void evaluationOutput() {
        gamedatarow.text += "Correct equations:" + correctEquationCount + "\n" + "Sum of Equations:" + sumEquations + "\n";
        gamedatarow.text += "% of Correct Equations:" + ((float)correctEquationCount / (float)sumEquations) * 100 + "%";
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

    public void TaskOnClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelection");
    }
    
}