using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedSpeed : MonoBehaviour
{
    Rigidbody rigidBody;
    public float thrust = 20f;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddForce(Vector3.down);
    }


    private void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        float fixedSpeed = 10f;
        rb.velocity = rb.velocity.normalized * fixedSpeed;
    }
}
