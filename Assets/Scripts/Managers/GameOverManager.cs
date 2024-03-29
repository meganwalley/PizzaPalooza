﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    PlayerData data;
    public Button NextSceneButton;
    public Button LastSceneButton;
    public Button MenuButton;
    public Text TitleText;
    public Text ScoreText;
    // Start is called before the first frame update

    void Start()
    {
        data = GameObject.FindObjectOfType<PlayerData>();
        NextSceneButton.enabled = data.winStatus;
        LastSceneButton.onClick.AddListener(onRedo);
        NextSceneButton.onClick.AddListener(onNextLevel);
        MenuButton.onClick.AddListener(onQuit);
        ScoreText.text = data.score.ToString("$ 0.00");
        if (data.playedTutorial && data.winStatus)
        {
            data.lastScore = data.score;
            data.playedTutorial = true;
            TitleText.text = "Good job on your first shift! Are you prepared for more?";
        }
        else if (data.playedTutorial && !data.winStatus)
        {
            // didn't win.
            data.score = data.lastScore;
            TitleText.text = "Nice try. Let's do that again to get the basics down!";
            NextSceneButton.gameObject.SetActive(false);
        }
        else if (data.winStatus)
        {
            data.lastScore = data.score;
            TitleText.text = "Well done! But there's more shifts to go...!";
            LastSceneButton.gameObject.SetActive(false);
        }
        else
        {
            data.score = data.lastScore;
            TitleText.text = "Time to be quarantined for two weeks. :(";
            NextSceneButton.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            onQuit();
        }
        else if (Input.GetKey(KeyCode.Return) && !data.winStatus)
        {
            onRedo();
        }
        else if (Input.GetKey(KeyCode.Return) && data.winStatus)
        {
            onNextLevel();
        }
    }
    public void onRedo()
    {
        Debug.Log("Note: Moving from GameOverScene to " + data.lastScene); ;
        SceneManager.LoadScene(data.lastScene);
    }
    public void onNextLevel()
    {
        data.lastScene = data.menuScene;
        // add some difficulty.
        if (data.wavesCap <= data.maxWaves)
        {
            if (data.difficulty == 4)
            {
                // increase regardless of waves cap.
                data.maxWaves += data.nextLevelWavesAdd;
            } else
            {
                data.difficulty++;
                data.maxWaves = data.baseWaves;
            }
        } else
        {
            data.maxWaves += data.nextLevelWavesAdd;
        }

        Debug.Log("Note: Moving from GameOverScene to " + data.nextScene); ;
        SceneManager.LoadScene(data.nextScene);
    }

    public void onQuit()
    {
        data.lastScene = data.menuScene;
        Debug.Log("Note: Moving from GameOverScene to " + data.menuScene);
        SceneManager.LoadScene(data.menuScene);
    }

}
