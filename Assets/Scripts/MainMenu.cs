using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        var playButton = GameObject.Find("PlayButton");
        playButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(TaskOnClick);
        var exitButton = GameObject.Find("ExitButton");
        exitButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });
    }

    void TaskOnClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelection");
    }
}
