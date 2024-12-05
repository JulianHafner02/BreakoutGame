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
    [SerializeField]private int health;

    [SerializeField]private float reflectingForce = 0.2f;
    [SerializeField]private BoxCollider boxCollider;
    [SerializeField]private AudioClip destructionSound;
    [SerializeField]private PlayableDirector director;
    [SerializeField]private bool isMathBrick = false;
    [SerializeField]private GameController gameController;
    [SerializeField]private UI_Input input;
    [SerializeField]private ParticleSystem explosion;
    [SerializeField]private Material lowHealthMaterial;
    [SerializeField]private Material invulnerableMaterial;
    [SerializeField]private Material mathBrickMaterial;
    [SerializeField]private MeshRenderer meshRenderer;

    public int mathResult;

    private Boolean isInvulnerable = false;
    private float timer = 0f;



    //private Fields
    private int currentHealth;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        
        currentHealth = health;
        audioSource = GetComponent<AudioSource>();

    }

    private void ReflectBall(Collision collision){

        Rigidbody ballRb = collision.gameObject.GetComponent<Rigidbody>();
        ballRb.AddForce(ballRb.velocity * reflectingForce, ForceMode.VelocityChange);
    }

    private int GenerateRandomNumber() {
        int number = UnityEngine.Random.Range(1, 31);
        return number;
    }


    public void TakeDamage(int damage){

        currentHealth -= damage;

        if(currentHealth == 1 && health == 2){
            meshRenderer.material = lowHealthMaterial; 
            explosion.Play();
        }
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
        
       
        
       
        Destroy(gameObject, 5f);
    }
    
    private void MathEvent(){
        input.InvokeMathEvent();
        // Find all math bricks in the scene
        BrickScript[] allBricks = GameObject.FindObjectsOfType<BrickScript>();

        foreach (BrickScript brick in allBricks)
        {
            if (brick.isMathBrick)
            {
                brick.isInvulnerable = true;
                brick.timer = 5f;
                brick.meshRenderer.material = invulnerableMaterial;
            }
        }
    }


    private void Update() {
        if(isInvulnerable){
            timer -= Time.deltaTime;
            if(timer <= 0f){
                isInvulnerable = false;
                meshRenderer.material = mathBrickMaterial;
            }
        }
    }

    private void OnCollisionEnter(Collision collision){

        if(collision.gameObject.CompareTag("Ball")){
            ReflectBall(collision);
            if(!isInvulnerable){
                TakeDamage(1);
            }                
           
        }

    }
    
    public int GetMathResult(){
        return mathResult;
    }
}


