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
    public int health;

    // upgrades
    public  float costPizzaPepperoni = 100f;
    public  float costPizzaBBQ = 200f;
    public  float costPizzaHawaiian = 300f;
    public  float costPizzaSupreme = 500f;
    public  float costZombieCautionTape = 400f;
    public  float costZombieClosedSign = 150f;
    public  float costBeltOil = 100f;
    public  float costBeltGears = 100f;
    public  float costBeltFire = 400f;
    public  float costBeltReplace = 300f;
    public  float costHealthMop = 100f;
    public  float costHealthSoap = 200f;
    public  float costHealthGloves = 500f;
    public  float costHealthMask = 300f;
    public  float costHealth = 50f;

    public bool unlockPizzaPepperoni = true;
    public bool unlockPizzaBBQ = false;
    public bool unlockPizzaSupreme = false;
    public bool unlockPizzaHawaiian = false;
    public bool unlockZombieCautionTape = false;
    public bool unlockZombieClosedSign = false;
    public bool unlockBeltOil = false;
    public bool unlockBeltGears = false;
    public bool unlockBeltFire = false;
    public bool unlockBeltReplace = false;
    public bool unlockHealthMop = false;
    public bool unlockHealthSoap = false;
    public bool unlockHealthGloves = false;
    public bool unlockHealthMask = false;

    public float purchaseBatteryTimer = 30f;
    public float purchaseHealth;

    // slows down how often you get hit?
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
