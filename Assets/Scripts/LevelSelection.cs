using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
        var level1Button = GameObject.Find("Level1Button");
        var level2Button = GameObject.Find("Level2Button");
        var level3Button = GameObject.Find("Level3Button");

        level1Button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => LoadLevel("Level1"));
        level2Button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => LoadLevel("Level2"));
        level3Button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => LoadLevel("Level3"));
    }

    void LoadLevel(string levelName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }
    

}
