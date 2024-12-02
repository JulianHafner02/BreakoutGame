using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class BrickScript : MonoBehaviour
{

    //Brick Properties
    [SerializeField]
    private int health = 3;
    [SerializeField]
    private float reflectingForce = 0.2f;
    [SerializeField]
    private BoxCollider boxCollider;
    //[SerializeField]
    //private ParticleSystem destructionEffect;
    [SerializeField]
    private AudioClip destructionSound;
    [SerializeField]
    private PlayableDirector director;
    [SerializeField]
    private bool isMathBrick = false;
    [SerializeField]
    private int scoreValue = 10;
    [SerializeField]
    private GameController gameController;
    public int mathResult;
    [SerializeField]
    private UI_Input input;


    //private Fields
    private int currentHealth;
    private AudioSource audioSource;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        audioSource = GetComponent<AudioSource>();

    }

    private void ReflectBall(Collision collision, Action<int> callback){

        Rigidbody ballRb = collision.gameObject.GetComponent<Rigidbody>();
        ballRb.AddForce(ballRb.velocity * reflectingForce, ForceMode.VelocityChange);
        callback?.Invoke(1);
    }

    private int GenerateRandomNumber() {
        int number = UnityEngine.Random.Range(1, 31);
        return number;
    }


    public void TakeDamage(int damage){

        currentHealth -= damage;

        if(currentHealth <= 0){
            gameController.AddScore(100);
            HandleDestruction();
        }
        
    }

    private void HandleDestruction(){
         if(isMathBrick){
            MathEvent();
        }
        director.Play();
        audioSource?.PlayOneShot(destructionSound);
        boxCollider.enabled = false;
        
        //destructionEffect?.Play();
        
       
        Destroy(gameObject, 4f);
    }
    
    private void MathEvent(){
        input.InvokeMathEvent();
    }

    private void OnCollisionEnter(Collision collision){

        if(collision.gameObject.CompareTag("Ball")){
            ReflectBall(collision, (result) => {
                TakeDamage(result);
            });
        }

    }
    
    public int GetMathResult(){
        return mathResult;
    }
}
