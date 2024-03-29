﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroductionSceneManager : MonoBehaviour
{

    public string firstScene = "PlayScene";
    public string menuScene = "MenuScene";
    private void Awake()
    {
        StartCoroutine(AutoMoveOn(15f));
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            onQuit();
        }
        else if (Input.GetKey(KeyCode.Return))
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

    IEnumerator AutoMoveOn(float delay)
    {
        yield return new WaitForSeconds(delay);
        onPlay();
    }
}
