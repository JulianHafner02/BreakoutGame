using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRb = collision.gameObject.GetComponent<Rigidbody>();
            if (ballRb != null)
            {
                Vector3 normal = collision.contacts[0].normal;
                if (Mathf.Abs(Vector3.Dot(normal, Vector3.up)) > 0.95f)
                {
                    ballRb.AddForce(Vector3.down * 50f, ForceMode.Impulse);
                }
            }
        }
    }
}
