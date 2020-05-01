using System.Collections;
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
        LastSceneButton.onClick.AddListener(onNextLevel);
        NextSceneButton.onClick.AddListener(onRedo);
        MenuButton.onClick.AddListener(onQuit);
        ScoreText.text = data.score.ToString("$ 0.00");
        if (data.winStatus)
            TitleText.text = "Well done!";
        else
            TitleText.text = "Time to be quarantined for two weeks. :(";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            onQuit();
        }
        else if (Input.GetKey("shift"))
        {
            onRedo();
        }
        else if (Input.GetKey("space"))
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
        data.playedTutorial = true;
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
