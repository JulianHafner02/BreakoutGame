using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 2.0f; 
    [SerializeField]
    private GameController gameController;
    [SerializeField] private AudioClip PowerUp;
    [SerializeField] private AudioSource audioSource;

    
    void Update()
    {
  
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Paddle"))
        {

            Debug.Log("Power-up hat das Paddle getroffen!");
            audioSource?.PlayOneShot(PowerUp);
            
           
            gameObject.SetActive(false);

            
            if (gameObject.CompareTag("Heart"))
            {
                Debug.Log("Es ist ein Herz-Power-up!");

                
               
                if (gameController != null)
                {
                    Debug.Log("GameController gefunden, f�ge ein Leben hinzu.");
                    gameController.AddLife();
                }
                
            }
            else if (gameObject.CompareTag("HourGlass"))
            {
                Debug.Log("Es ist ein HourGlass-Power-up! Geschwindigkeit des Balls wird f�r 3 Sekunden halbiert.");

               
                GameObject ball = GameObject.FindGameObjectWithTag("Ball");
                if (ball != null)
                {
                    FixedSpeed fixedSpeedScript = ball.GetComponent<FixedSpeed>();
                    if (fixedSpeedScript != null)
                    {
                        
                        float newSpeed = fixedSpeedScript.speed / 2;
                        fixedSpeedScript.SetSpeed(newSpeed, 3f);
                    }
                    if (gameController != null)
                    {
                        gameController.ChangeBackgroundMusicForDuration(gameController.slowMotionMusic, 3f);
                    }
                }
                
            }
            else if (gameObject.CompareTag("XL_Pad"))
            {
                Debug.Log("Es ist ein XL_Pad-Power-up! Paddle wird f�r 7 Sekunden verl�ngert.");

              
                GameObject paddle = GameObject.FindGameObjectWithTag("Paddle");
                if (paddle != null)
                {
                    PaddleControl paddleControlScript = paddle.GetComponent<PaddleControl>();
                    if (paddleControlScript != null)
                    {
                       
                        paddleControlScript.SetPaddleScaleTemporarily(2f, 7f);
                    }
                }
            }

          
            Destroy(gameObject);
        }
    }

}
