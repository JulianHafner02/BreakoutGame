using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    [SerializeField] private float kickForce = 2f;
    [SerializeField] private ParticleSystem ballHit;         

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            ContactPoint contact = collision.contacts[0];
            Vector3 contactPosition = contact.point;

            ballHit.transform.position = contactPosition;
            ballHit.Play();

            Rigidbody ballRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 ballVelocity = ballRb.velocity;
            Vector3 wallNormal = contact.normal;
            
            float dotProduct = Vector3.Dot(ballVelocity.normalized, wallNormal);

            if (Mathf.Abs(dotProduct) >= 0.95)
            {
                Vector3 kickDirection = Vector3.down;
                ballRb.AddForce(kickDirection * kickForce, ForceMode.Impulse);

            }
        }
    }
}