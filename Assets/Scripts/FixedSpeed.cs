using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedSpeed : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public float m_Thrust = 20f;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.AddForce(Vector3.down * m_Thrust, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        float fixedSpeed = 10f;
        rb.velocity = rb.velocity.normalized * fixedSpeed;

        
    }
}
