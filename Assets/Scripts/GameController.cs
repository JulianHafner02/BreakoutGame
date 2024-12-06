using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private int lives = 3; 
    private float elapsedTime = 0f;
    [SerializeField]
    private TMP_Text timerTextInfo;
    [SerializeField]
    public Image[] heartImages; 
    [SerializeField]
    public Sprite fullHeart;    
    [SerializeField]
    public Sprite emptyHeart;   
    private int currentScore = 0;
    [SerializeField]
    private TMP_Text scoreTextInfo;
    [SerializeField]
    private GameObject ballPrefab;
    [SerializeField]
    private Vector3 ballStartPosition;
    [SerializeField]
    private Vector3 paddleStartPosition;
    [SerializeField]
    private PaddleControl paddle;
    [SerializeField]
    private AudioClip backgroundMusic;
    private string filePath = "Assets/Level1.txt";
    private string filePath2 = "Assets/Level2.txt";
    private string filePath3 = "Assets/Level3.txt";
    private string currentSceneName;

    private string level;
    
    public Equation[] equations = new Equation[10];

    // Start is called before the first frame update
    void Start() {
        level = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        elapsedTime = 0f;
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.volume = 0.5f;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        timerTextInfo.text = $"{minutes:00}:{seconds:00}";
        scoreTextInfo.text = currentScore.ToString();
        CheckForEndOfGame();

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

    public Equation[] GetEquations() {
        return equations;
    }

    public struct gameData {
        public string level;
        public string sumScore;
        public string sumTime;
        public string gameConclusion;
        

        public gameData(string level, string sumScore, string sumTime, string gameConclusion) {
            this.level = level;
            this.sumScore = sumScore;
            this.sumTime = sumTime;
            this.gameConclusion = gameConclusion;
        }

        public override string ToString()
        {
            return $"Level:{level},Score :{sumScore},Time: {sumTime},GameConclusion:{gameConclusion}";
        }
    }

    public static void WriteArrayToFile(gameData scoreAndTimeAndLevel,string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath, append: true))
        {
            writer.WriteLine(scoreAndTimeAndLevel.ToString());
        }
    }

    public static void WriteArrayIndexToFile(Equation equation,string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath, append: true))
        {
            writer.WriteLine(equation.ToString());
        }
    }

     public void WriteEquationsToFile(Equation[] equations,string filePath) {
        for (int i = 0; i < equations.Length; i++) { 
            if (equations[i].equationString == "" || equations[i].equationString == null) {
                break;
            } 
            if(level == "Level1") {
                WriteArrayIndexToFile(equations[i],filePath);
            }
            else if(level == "Level2") {
                WriteArrayIndexToFile(equations[i],filePath2);
            }
            else if(level == "Level3") {
                WriteArrayIndexToFile(equations[i],filePath3);
            } 
        }
    }

    public void LoseALife() {
        
        lives--;
        if (lives >= 0) {
            heartImages[lives].sprite = emptyHeart;
        }
        ResetBallAndPaddle();
    }

    public void ResetBallAndPaddle() {
        if(GameObject.FindGameObjectWithTag("Ball")) {
            GameObject.FindGameObjectWithTag("Ball").transform.position = ballStartPosition;
        }
        else {
            Instantiate(ballPrefab,ballStartPosition,Quaternion.identity);
        }
        GameObject.FindGameObjectWithTag("Paddle").transform.position = paddleStartPosition;
        paddle.GetComponent<MeshRenderer>().enabled = true;
        paddle.SetNewBallRigidBody();
    }


    public void SpawnNewBall() {
        Instantiate(ballPrefab,ballStartPosition,Quaternion.identity);
        paddle.SetNewBallRigidBody();
    }
    
    public void AddScore(int score) {
        currentScore += score;
    }

    
   
    
    public void CheckForEndOfGame () {
        if (lives == -1) {
            currentSceneName = SceneManager.GetActiveScene().name;
            gameData sat = new gameData(currentSceneName,currentScore.ToString(),timerTextInfo.text,"Game Over");
            if(level == "Level1") {
                WriteArrayToFile(sat, filePath);
                WriteEquationsToFile(equations, filePath);
            }
            else if(level == "Level2") {
                WriteArrayToFile(sat, filePath2);
                WriteEquationsToFile(equations, filePath2);
            }
            else if(level == "Level3") {
                WriteArrayToFile(sat, filePath3);
                WriteEquationsToFile(equations, filePath3);
            }
            
            SceneManager.LoadScene("GameOver");
        }

        if (GameObject.FindGameObjectsWithTag("Brick").Length == 0) {
            currentSceneName = SceneManager.GetActiveScene().name;
            gameData sat = new gameData(currentSceneName,currentScore.ToString(),timerTextInfo.text,"Cleared");
            if(level == "Level1") {
                WriteArrayToFile(sat, filePath);
                WriteEquationsToFile(equations, filePath);
            }
            else if(level == "Level2") {
                WriteArrayToFile(sat, filePath2);
                WriteEquationsToFile(equations, filePath2);
            }
            else if(level == "Level3") {
                WriteArrayToFile(sat, filePath3);
                WriteEquationsToFile(equations, filePath3);
            }
            SceneManager.LoadScene("WinScreen");
        }
    }
}
