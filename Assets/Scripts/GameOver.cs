using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
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
