using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    PlayerData data;
    public Button PlayButton;
    public Button QuitButton;
    public string firstScene;

    // Start is called before the first frame update
    void Start()
    {
        data = GameObject.FindObjectOfType<PlayerData>();
        PlayButton.onClick.AddListener(onPlay);
        QuitButton.onClick.AddListener(onQuit);
        PlayerPrefs.SetString("PickUp", "space");
        PlayerPrefs.SetString("Throw", "space");
        PlayerPrefs.SetString("Up", "w");
        PlayerPrefs.SetString("Down", "s");
        PlayerPrefs.SetString("Escape", "escape");
        PlayerPrefs.SetString("Quit", "q");
        // reset all difficulty values.
        data.difficulty = 1;
        data.maxWaves = data.baseWaves;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            onQuit();
        }
        else if (Input.GetKey("space"))
        {
            onPlay();
        }
    }

    public void onPlay()
    {
        Debug.Log("Note: Moving from MenuScene to " + firstScene);
        SceneManager.LoadScene(firstScene);
    }

    public void onQuit()
    {
        Debug.Log("Note: Quitting application.");
        Application.Quit();
    }
}
