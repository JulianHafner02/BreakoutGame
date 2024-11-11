using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleControl : MonoBehaviour
{
    [SerializeField]
    private float paddleSpeed = 2f;

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0f){
            return;
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            float horizontalAxis = Input.GetAxis("Horizontal");
            Vector3 position = transform.position + new Vector3(horizontalAxis * paddleSpeed * Time.deltaTime, 0, 0);
            transform.position = position;
        }        
    }

    private void OnCollisonEnter(Collision collision)
    {
        Rigidbody ballRb = collision.rigidbody;

        if (ballRb != null)
        {
            Vector3 hitPoint = collision.GetContact(0).point;
            float hitFactor = (hitPoint.x - transform.position.x) / transform.localScale.x;
            Vector3 newDirection = new Vector3(hitFactor, 1, 0).normalized;

            ballRb.velocity = newDirection * ballRb.velocity.magnitude;
        }
    }
}
