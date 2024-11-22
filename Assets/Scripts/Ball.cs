using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private AudioSource audiosource;
    [SerializeField]
    private List<AudioClip> clips;
    // Start is called before the first frame 

    
    private void OnCollisionEnter (Collision collision) {
        if (collision.gameObject.CompareTag("Paddle")) {
            audiosource.clip = clips[0];
            audiosource.Play(); 
        }
        if (collision.gameObject.CompareTag("Brick")) {
            gameController.AddScore(10);
        }
        if (collision.gameObject.CompareTag("Border")) {
            audiosource.clip = clips[2];    
            audiosource.Play();
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
