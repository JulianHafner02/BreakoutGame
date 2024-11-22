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
        InvokeRepeating("CheckForEndOfGame",20,3); // 20 bricks destroyed or 3 lives wasted
    }

    // Update is called once per frame
    void Update()
    {
        livesTextInfo.text = "Balls: " + lives.ToString();

        if (lives < 0) { //checks if lives are gone and gameover if so
            GameOverScreen.GetComponent<Canvas>().enabled = true;
            Time.timeScale = 0;
        }
    }

    public void LoseALife() {
        if (lives == 0) {
            lives = 3;
            ResetBallAndPaddle();
        } 
        else {
            lives--;
            ResetBallAndPaddle();
        }
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
    /*
    public void CheckForEndOfGame () {
        if (GameObject.Find("BorderBottom").transform.childCount == 0) {
            SceneManager.LoadScene(0);
        }
    }
    */

    public void SpawnNewBall() {
        Instantiate(ballPrefab,ballStartPosition,Quaternion.identity);
        paddle.SetNewBallRigidBody();
    }
    
    public void AddScore(int score) {
        currentScore += score;
        scoreTextInfo.text = "Score: " + currentScore.ToString();
    }
    
}
