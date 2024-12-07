using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FixedSpeed : MonoBehaviour
{
    private Rigidbody rigidBody;
    public float thrust = 20f;
    [SerializeField] public float speed = 20f;
    private float originalSpeed;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        originalSpeed = speed; 
        rigidBody.AddForce(Vector3.down);
    }

    private void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = rb.velocity.normalized * speed;
    }

   
    public void SetSpeed(float newSpeed, float duration)
    {
        StopAllCoroutines(); 
        StartCoroutine(AdjustSpeedTemporarily(newSpeed, duration));
    }

    
    private IEnumerator AdjustSpeedTemporarily(float newSpeed, float duration)
    {
        speed = newSpeed; 
        yield return new WaitForSeconds(duration); 
        speed = originalSpeed; 
        Debug.Log("Geschwindigkeit zurückgesetzt auf ursprüngliche Geschwindigkeit.");
    }
}
