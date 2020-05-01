using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData : MonoBehaviour
{
    private static PlayerData inst;
    public static PlayerData Instance {
        get { return inst; }
    }

    void Awake()
    {
        if (inst != null && inst != this)
        {
            Destroy(this.gameObject);
            return;
        }
        inst = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // data
    public bool zenMode = false;
    public bool playedTutorial = false;
    public bool winStatus = false;
    public string lastScene = "menuScene";
    public string nextScene = "PlayScene";
    public string playScene = "PlayScene";
    public string menuScene = "menuScene";
    public string gameoverScene = "GameOverScene";
    public int baseWaves = 10;
    public int nextLevelWavesAdd = 5;
    public int wavesCap = 25;

    // data that might change regularly
    public float score = 0f;
    public int difficulty = 1;
    public int maxWaves = 10;
    public string pizzaHeld = "none";

    // upgrades
    public bool unlockPizzaPepperoni = true;
    public bool unlockPizzaBBQ = true;
    public bool unlockPizzaSupreme = true;
    public void GameReset()
    {
        score = 0f;
        difficulty = 1;
        maxWaves = baseWaves;
        pizzaHeld = "none";
    }
    public void LevelReset()
    {

    }
}
