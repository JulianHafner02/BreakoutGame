using System;
using System.Collections;
using System.Collections.Generic;
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
        TextMeshProUGUI mathq = GameObject.Find("Math").GetComponent<TextMeshProUGUI>();
        mathq.text = "2 + 2";
    }

    private void OnCollisionEnter(Collision collision){

        if(collision.gameObject.CompareTag("Ball")){
            ReflectBall(collision, (result) => {
                TakeDamage(result);
            });
        }

    }
            
}
