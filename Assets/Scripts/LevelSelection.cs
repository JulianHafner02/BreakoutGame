using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    [SerializeField]
    private AudioClip levelselectionSound;
    // Start is called before the first frame update
    public void Start()
    {
        var audioSource = GetComponent<AudioSource>();
        audioSource.clip = levelselectionSound;
        audioSource.loop = true;
        audioSource.Play();
        var level1Button = GameObject.Find("Level1Button");
        var level2Button = GameObject.Find("Level2Button");
        var level3Button = GameObject.Find("Level3Button");
        var evaluationButton = GameObject.Find("EvaluationButton");
        GameObject MainMenu = GameObject.Find("MainMenu");

        level1Button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => LoadLevel("Level1"));
        level2Button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => LoadLevel("Level2"));
        level3Button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => LoadLevel("Level3"));
        evaluationButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => LoadLevel("EvaPage"));
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
