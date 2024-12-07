using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private AudioClip gameOverSound;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.PlayOneShot(gameOverSound);
        GameObject tryAgainButton = GameObject.Find("TryAgainButton");
       
        tryAgainButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => LoadLevel("LevelSelection"));
        
        GameObject MainMenu = GameObject.Find("MainMenu");
       
        MainMenu.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(loadMainMenu);
        
    }

    void LoadLevel(string levelName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }
    void loadMainMenu() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
