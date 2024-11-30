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
        
    }

  void LoadLevel(string levelName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }
}
