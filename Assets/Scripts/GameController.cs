using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private int lives = 3; 
    [SerializeField]
    public Image[] heartImages; // Drag your heart Image objects here
    [SerializeField]
    public Sprite fullHeart;    // Drag your full heart sprite here
    [SerializeField]
    public Sprite emptyHeart;   // Drag your empty heart sprite here
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

    // Start is called before the first frame update
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.volume = 0.5f;
        audioSource.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreTextInfo.text = "Score: " + currentScore.ToString();

        CheckForEndOfGame();

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
            SceneManager.LoadScene("GameOver");
        }

        if (GameObject.FindGameObjectsWithTag("Brick").Length == 0) {
            SceneManager.LoadScene("WinScreen");
        }
    }
}
