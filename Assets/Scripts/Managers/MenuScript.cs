using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    PlayerData data;
    public Button PlayButton;
    public Button ZenButton;
    public Button NormalButton;
    public Button ZenExtremeButton;
    public Button ZenHardButton;
    public Button ZenMediumButton;
    public Button ZenEasyButton;
    public Button QuitButton;
    public GameObject MainChoices;
    public GameObject PlayChoices;
    public GameObject ZenChoices;
    public string firstScene;

    // Start is called before the first frame update
    void Start()
    {
        data = GameObject.FindObjectOfType<PlayerData>();
        NormalButton.onClick.AddListener(onPlay);
        QuitButton.onClick.AddListener(onQuit);

        PlayButton.onClick.AddListener(playButton);
        ZenButton.onClick.AddListener(zenButton);

        ZenEasyButton.onClick.AddListener(zenEasy);
        ZenMediumButton.onClick.AddListener(zenMedium);
        ZenHardButton.onClick.AddListener(zenHard);
        ZenExtremeButton.onClick.AddListener(zenExtreme);

        PlayerPrefs.SetString("PickUp", "space");
        PlayerPrefs.SetString("Throw", "space");
        PlayerPrefs.SetString("Up", "w");
        PlayerPrefs.SetString("Down", "s");
        PlayerPrefs.SetString("Escape", "escape");
        PlayerPrefs.SetString("Quit", "q");

        // reset all difficulty values.
        data.difficulty = 1;
        data.maxWaves = data.baseWaves;

        menuButton();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            if (ZenChoices.active)
            {
                playButton();
            } else if (PlayChoices.active)
            {
                menuButton();
            } else
            {
                onQuit();
            }
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

    public void playButton()
    {
        MainChoices.SetActive(false);
        ZenChoices.SetActive(false);
        PlayChoices.SetActive(true);
    }

    public void onNormal()
    {
        data.zenMode = false;
        Debug.Log("Note: Moving from MenuScene to " + data.playScene);
        SceneManager.LoadScene(data.playScene);
    }

    public void zenButton()
    {
        MainChoices.SetActive(false);
        ZenChoices.SetActive(true);
        PlayChoices.SetActive(false);
    }

    public void menuButton()
    {
        MainChoices.SetActive(true);
        ZenChoices.SetActive(false);
        PlayChoices.SetActive(false);
    }

    public void zenEasy()
    {
        onZen(1);
    }

    public void zenMedium()
    {
        onZen(2);
    }

    public void zenHard()
    {
        onZen(3);
    }

    public void zenExtreme()
    {
        onZen(4);
    }

    public void onZen(int i)
    {
        data.zenMode = true;
        data.difficulty = i;
        Debug.Log("Note: Moving from MenuScene to " + data.playScene);
        SceneManager.LoadScene(data.playScene);
    }
}
