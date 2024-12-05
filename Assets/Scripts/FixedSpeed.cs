using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FixedSpeed : MonoBehaviour
{
    Rigidbody rigidBody;
    public float thrust = 20f;
    [SerializeField] private float speed = 20f;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddForce(Vector3.down);
    }


    private void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = rb.velocity.normalized * speed;
    }
}
