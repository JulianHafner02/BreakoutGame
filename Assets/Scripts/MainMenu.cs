using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private AudioClip backgroundMusic;
    public void Start()
    {
        var audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.Play();
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
