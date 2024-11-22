using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleControl : MonoBehaviour
{
    [SerializeField]
    private float paddleSpeed = 10f;
    private Rigidbody ballRb;
    void Update()
    {
        if (Time.timeScale == 0f)
            return;

        if (Input.GetKey(KeyCode.LeftArrow)  || Input.GetKey(KeyCode.RightArrow))
        {
            float horizontal = Input.GetAxis("Horizontal");
            Vector3 newPosition = transform.position + new Vector3(horizontal * paddleSpeed * Time.deltaTime, 0, 0);
            transform.position = newPosition;
            
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            float horizontal = Input.GetAxis("Horizontal");
            Vector3 newPosition = transform.position + new Vector3(horizontal * paddleSpeed * Time.deltaTime, 0, 0);
            transform.position = newPosition;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ballRb = collision.gameObject.GetComponent<Rigidbody>();
        if (ballRb != null)
        {
            //Get the exact point where the ball hit the paddle
            Vector3 hitpoint = collision.contacts[0].point;

            float hitfactor = (hitpoint.x - transform.position.x) / transform.localScale.x;

            Vector3 newDirection = new Vector3(hitfactor, 1, 0).normalized;
            ballRb.velocity = newDirection; 
        }
    }

    public void SetNewBallRigidBody() 
    {
        ballRb = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>();
        ballRb.interpolation = RigidbodyInterpolation.Interpolate;
        ballRb.velocity = Vector3.zero;
        ballRb.useGravity = false;
        Invoke(nameof(InitialDownPushToBall),2f);
    }

    public void InitialDownPushToBall () {
        ballRb.useGravity = true;
        ballRb.AddForce(new Vector2(Random.Range(-0.5f,0.5f), -1f).normalized * 5f,ForceMode.Impulse);
        ballRb.useGravity = false;
    }
}

   

