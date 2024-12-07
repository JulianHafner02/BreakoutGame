using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField]
    private AudioClip winScreenMusic;
    // Start is called before the first frame update
    void Start()
    {
        var audioSource = GetComponent<AudioSource>();
        audioSource.clip = winScreenMusic;
        audioSource.loop = true;
        audioSource.Play();
        var selectLevelButton = GameObject.Find("SelectLevelButton");
        selectLevelButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => LoadLevel("LevelSelection"));
    }

  void LoadLevel(string levelName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }
}
