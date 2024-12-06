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
        originalSpeed = speed; // Speichere die Originalgeschwindigkeit
        rigidBody.AddForce(Vector3.down);
    }

    private void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = rb.velocity.normalized * speed;
    }

    // Methode zum Ändern der Geschwindigkeit
    public void SetSpeed(float newSpeed, float duration)
    {
        StopAllCoroutines(); // Stoppe alle laufenden Coroutines, um Überschneidungen zu vermeiden
        StartCoroutine(AdjustSpeedTemporarily(newSpeed, duration));
    }

    // Coroutine zum temporären Ändern der Geschwindigkeit
    private IEnumerator AdjustSpeedTemporarily(float newSpeed, float duration)
    {
        speed = newSpeed; // Setze die neue Geschwindigkeit
        yield return new WaitForSeconds(duration); // Warte für die angegebene Dauer
        speed = originalSpeed; // Setze die Geschwindigkeit zurück auf den ursprünglichen Wert
        Debug.Log("Geschwindigkeit zurückgesetzt auf ursprüngliche Geschwindigkeit.");
    }
}
