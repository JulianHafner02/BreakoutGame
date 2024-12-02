using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private int lives = 3; 
    private int currentScore = 0;
    [SerializeField]
    private TMP_Text livesTextInfo;
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
    private Canvas GameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        livesTextInfo.text = "Balls: " + lives.ToString();
        scoreTextInfo.text = "Score: " + currentScore.ToString();

        CheckForEndOfGame();

    }

    public void LoseALife() {
       
        lives--;
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
        if (lives == 0) {
            SceneManager.LoadScene("GameOver");
        }

        if (GameObject.FindGameObjectsWithTag("Brick").Length == 0) {
            SceneManager.LoadScene("WinScreen");
        }
    }
}
