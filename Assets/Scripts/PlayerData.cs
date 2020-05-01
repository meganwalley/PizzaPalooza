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

    public float score = 0f;
    public bool zenMode = false;
    public bool playedTutorial = false;
    public bool winStatus = false;
    public String lastScene = "menuScene";
    public String nextScene = "PlayScene";
    public String playScene = "PlayScene";
    public String menuScene = "menuScene";
    public String gameoverScene = "GameOverScene";
    public int difficulty = 1;
    public int maxWaves = 10;
    public int baseWaves = 10;
    public int nextLevelWavesAdd = 10;
    public int wavesCap = 50;
}
