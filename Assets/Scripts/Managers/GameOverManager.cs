using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Button NextSceneButton;
    public Button MenuButton;
    public Text TitleText;
    public Text ScoreText;
    string menuScene = "MenuScene";
    string playScene = "PlayScene";
    // Start is called before the first frame update

    void Start()
    {
        NextSceneButton.onClick.AddListener(onPlay);
        MenuButton.onClick.AddListener(onQuit);
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
        Debug.Log("Note: Moving from GameOverScene to " + playScene); ;
        SceneManager.LoadScene(playScene);
    }

    public void onQuit()
    {
        Debug.Log("Note: Moving from GameOverScene to MenuScene");
        SceneManager.LoadScene(menuScene);
    }

}
