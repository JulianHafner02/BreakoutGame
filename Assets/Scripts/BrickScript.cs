using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;  // Füge diesen Namespace hinzu




public class BrickScript : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private float reflectingForce = 0.2f;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private AudioClip destructionSound;
    [SerializeField] private PlayableDirector director;
    [SerializeField] private bool isMathBrick = false;
    [SerializeField] private GameController gameController;
    [SerializeField] private UI_Input input;
    [SerializeField] private ParticleSystem explosion;

    // Referenz auf mehrere Power-Up Prefabs
    [SerializeField] private GameObject[] powerUpPrefabs;

    // Referenz zur gespeicherten Timeline (Playable Asset)
    [SerializeField] private PlayableAsset hourGlassTimeline;

    public int mathResult;

    private int currentHealth;
    private AudioSource audioSource;

    void Start()
    {
        currentHealth = health;
        audioSource = GetComponent<AudioSource>();
    }

    private void ReflectBall(Collision collision, Action<int> callback)
    {
        Rigidbody ballRb = collision.gameObject.GetComponent<Rigidbody>();
        ballRb.AddForce(ballRb.velocity * reflectingForce, ForceMode.VelocityChange);
        callback?.Invoke(1);
    }

    private int GenerateRandomNumber()
    {
        int number = UnityEngine.Random.Range(1, 31);
        return number;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            gameController.AddScore(100);
            HandleDestruction();
        }
    }

    private void HandleDestruction()
    {
        if (isMathBrick)
        {
            MathEvent();
            SpawnPowerUp();
        }
        director.Play();
        audioSource?.PlayOneShot(destructionSound);
        explosion.Play();
        boxCollider.enabled = false;

        if(!isMathBrick)
        {
            float dropChance = UnityEngine.Random.Range(0f, 1f);
            if (dropChance <= 0.3f && powerUpPrefabs.Length > 0)
            {
                SpawnPowerUp();
            }
        }
        

        Destroy(gameObject, 4f);
    }

    private void MathEvent()
    {
        input.InvokeMathEvent();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            ReflectBall(collision, (result) => {
                TakeDamage(result);
            });
        }
    }

    public int GetMathResult()
    {
        return mathResult;
    }

    private void SpawnPowerUp()
    {
        if (powerUpPrefabs.Length > 0)
        {
            // Zufällig ein Power-Up aus dem Array auswählen
            int randomIndex = UnityEngine.Random.Range(0, powerUpPrefabs.Length);
            GameObject selectedPowerUpPrefab = powerUpPrefabs[randomIndex];

            if (selectedPowerUpPrefab != null)
            {
                // Instantiate the selected power-up prefab at the brick's position
                GameObject powerUpInstance = Instantiate(selectedPowerUpPrefab, transform.position, Quaternion.identity);
                powerUpInstance.transform.SetParent(null);
                powerUpInstance.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);

                if (selectedPowerUpPrefab.CompareTag("XL_Pad"))
                {
                    powerUpInstance.transform.Rotate(0, 180, 0); // XL_Pad um 180 Grad drehen
                    powerUpInstance.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                }
                else
                {
                    powerUpInstance.transform.Rotate(0, 90, 0); // Andere Power-Ups um 90 Grad drehen
                    powerUpInstance.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                }
                // Add the PowerUpScript to handle movement and collision
                PowerUpScript powerUpScript = powerUpInstance.AddComponent<PowerUpScript>();

                // Get the PlayableDirector from the instantiated object, if applicable
                PlayableDirector playableDirector = powerUpInstance.GetComponentInChildren<PlayableDirector>();
                if (playableDirector != null && hourGlassTimeline != null)
                {
                    playableDirector.playableAsset = hourGlassTimeline;

                    // Update the bindings to use the new instance
                    foreach (var output in hourGlassTimeline.outputs)
                    {
                        if (output.sourceObject is AnimationTrack animationTrack)
                        {
                            playableDirector.SetGenericBinding(animationTrack, powerUpInstance);
                        }
                    }

                    playableDirector.Play();
                }

                // Set Rigidbody to kinematic to avoid physics interactions
                Rigidbody rb = powerUpInstance.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                }
            }
        }
    }

}