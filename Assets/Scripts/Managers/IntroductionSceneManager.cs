using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroductionSceneManager : MonoBehaviour
{

    public string firstScene = "TutorialScene";
    public string menuScene = "MenuScene";
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
        Debug.Log("Note: Moving from IntroductionScene to " + firstScene);
        SceneManager.LoadScene(firstScene);
    }

    public void onQuit()
    {
        Debug.Log("Note: Moving from IntroductionScene to " + menuScene);
        SceneManager.LoadScene(menuScene);
    }
}
