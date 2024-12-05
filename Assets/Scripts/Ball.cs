using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private AudioSource audiosource;
    [SerializeField]
    private List<AudioClip> clips;
    [SerializeField]
    private CinemachineImpulseSource mainImpulseSource;
    [SerializeField]
    private CinemachineImpulseSource rightImpulseSource;
    [SerializeField]
    private CinemachineImpulseSource leftImpulseSource;
    [SerializeField]
    private PlayableDirector director;

    private void Start() {
        
    }

    
    private void OnCollisionEnter (Collision collision) {
        if (collision.gameObject.CompareTag("Paddle")) {
            director.Play();
            audiosource.clip = clips[0];
            audiosource.Play(); 
        }
        if (collision.gameObject.CompareTag("Brick")) {
            director.Play();
            mainImpulseSource.GenerateImpulse();
            audiosource.clip = clips[1];
            audiosource.Play();
        }
        if (collision.gameObject.CompareTag("Border")) {
            audiosource.clip = clips[2];    
            audiosource.Play();
            if(collision.gameObject.name == "BorderRight") {
                rightImpulseSource.GenerateImpulse();
            }
            else if(collision.gameObject.name == "BorderLeft") {
                leftImpulseSource.GenerateImpulse();
            }
            else {
                director.Play();
                mainImpulseSource.GenerateImpulse();
            }
        }
        if (collision.gameObject.CompareTag("BottomBorder")) {
            gameController.LoseALife();
        }
    }

    private void OnTriggerEvent(Collider other) {
        if( other.gameObject.CompareTag("DeathZone")) {
            gameController.LoseALife();
        }
    }
}
