using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 2.0f; // Geschwindigkeit des Fallens
    [SerializeField]
    private GameController gameController;
    [SerializeField] private AudioClip PowerUp;
    [SerializeField] private AudioSource audioSource;

    
    void Update()
    {
        // Bewege das Power-up konstant nach unten
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Überprüfe, ob das Power-up das Paddle trifft
        if (other.CompareTag("Paddle"))
        {

            Debug.Log("Power-up hat das Paddle getroffen!");
            audioSource?.PlayOneShot(PowerUp);
            
            // Deaktiviere das Objekt sofort, um weitere Trigger zu vermeiden
            gameObject.SetActive(false);

            // Überprüfe, ob dieses Power-up ein Herz ist (angenommen, es hat den Tag "Heart")
            if (gameObject.CompareTag("Heart"))
            {
                Debug.Log("Es ist ein Herz-Power-up!");

                // Zugriff auf den GameController, um ein Leben hinzuzufügen
               
                if (gameController != null)
                {
                    Debug.Log("GameController gefunden, füge ein Leben hinzu.");
                    gameController.AddLife();
                }
                
            }
            if (gameObject.CompareTag("HourGlass"))
            {
                Debug.Log("Es ist ein HourGlass-Power-up! Geschwindigkeit des Balls wird für 5 Sekunden halbiert.");

                // Finde den Ball und passe die Geschwindigkeit für 5 Sekunden an
                GameObject ball = GameObject.FindGameObjectWithTag("Ball");
                if (ball != null)
                {
                    FixedSpeed fixedSpeedScript = ball.GetComponent<FixedSpeed>();
                    if (fixedSpeedScript != null)
                    {
                        // Halbiere die aktuelle Geschwindigkeit für 5 Sekunden
                        float newSpeed = fixedSpeedScript.speed / 2;
                        fixedSpeedScript.SetSpeed(newSpeed, 5f);
                    }
                    if (gameController != null)
                    {
                        gameController.ChangeBackgroundMusicForDuration(gameController.slowMotionMusic, 5f);
                    }
                }
            }

            // Power-up zerstören, nachdem es eingesammelt wurde
            Destroy(gameObject);
        }
    }

}
