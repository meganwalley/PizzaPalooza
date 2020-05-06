using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    PlayerData data;

    public List<GameObject> spawnPoints;
    public List<GameObject> enemyEasyPrefabs;
    public List<GameObject> enemyMediumPrefabs;
    public List<GameObject> enemyHardPrefabs;
    public List<GameObject> enemyExtremePrefabs;
    public GameObject main;
    public GameManager manager;
    public float waveTime = 8f;
    public int maxSpawnAtOnce;

    public int wave = 0;
    public int difficulty = 1;
    public int maxWaves = 10;
    public bool zen;


    // Start is called before the first frame update
    void Start()
    {
        data = GameObject.FindObjectOfType<PlayerData>();
        manager = main.GetComponent<GameManager>();
        difficulty = data.difficulty;
        maxWaves = data.maxWaves;
        zen = data.zenMode;
        if (zen)
            wave = 1;
    }
    private void Awake()
    {
        StartCoroutine(Spawner(waveTime));
    }

    IEnumerator Spawner(float del)
    {
        yield return new WaitForSeconds(del);
        if (!zen && !manager.IsPaused())
            wave++;
        Debug.Log("Spawning wave #" + wave);
        manager.currentWave = wave;

        // ZOMBIE CLOSED SIGN IS APPLIED HERE
        if (wave <= maxWaves)
        {
            StartCoroutine(Spawner(waveTime + ((data.unlockZombieClosedSign ? 1 : 0) * 0.5f) - (0.5f * (difficulty - 1))));
            int temp = Random.Range(0, maxSpawnAtOnce);
            for (int i = 0; i <= temp; ++i)
            {
                StartCoroutine(Spawn(Random.Range(0f, 1f)));
            }
        }
    }

    IEnumerator Spawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (!manager.IsPaused())
        {
            GameObject temp = Instantiate(getZombie());
            temp.transform.position = (spawnPoints[Random.Range(0, spawnPoints.Count)]).transform.position;
            temp.GetComponent<EnemyScript>().SetGameManager(manager);
            manager.AddEnemy(temp);
        }
    }

    private GameObject getZombie()
    {
        if (zen)
        {
            int mobDifficulty = Random.Range(0, 100);
            switch (difficulty)
            {
                case 4:
                    {
                        if (mobDifficulty > 50)
                            return enemyExtremePrefabs[Random.Range(0, enemyExtremePrefabs.Count)];
                        else if (mobDifficulty > 25)
                            return enemyHardPrefabs[Random.Range(0, enemyHardPrefabs.Count)];
                        else if (mobDifficulty > 10)
                            return enemyMediumPrefabs[Random.Range(0, enemyMediumPrefabs.Count)];
                        else
                            return enemyEasyPrefabs[Random.Range(0, enemyEasyPrefabs.Count)];
                    }
                case 3:
                    {
                        if (mobDifficulty > 85)
                            return enemyExtremePrefabs[Random.Range(0, enemyExtremePrefabs.Count)];
                        else if (mobDifficulty > 40)
                            return enemyHardPrefabs[Random.Range(0, enemyHardPrefabs.Count)];
                        else if (mobDifficulty > 15)
                            return enemyMediumPrefabs[Random.Range(0, enemyMediumPrefabs.Count)];
                        else
                            return enemyEasyPrefabs[Random.Range(0, enemyEasyPrefabs.Count)];
                    }
                case 2:
                    {
                        if (mobDifficulty > 95)
                            return enemyExtremePrefabs[Random.Range(0, enemyExtremePrefabs.Count)];
                        else if (mobDifficulty > 85)
                            return enemyHardPrefabs[Random.Range(0, enemyHardPrefabs.Count)];
                        else if (mobDifficulty > 25)
                            return enemyMediumPrefabs[Random.Range(0, enemyMediumPrefabs.Count)];
                        else
                            return enemyEasyPrefabs[Random.Range(0, enemyEasyPrefabs.Count)];
                    }
                case 1:
                default:
                    {
                        if (mobDifficulty > 98)
                            return enemyExtremePrefabs[Random.Range(0, enemyExtremePrefabs.Count)];
                        else if (mobDifficulty > 90)
                            return enemyHardPrefabs[Random.Range(0, enemyHardPrefabs.Count)];
                        else if (mobDifficulty > 75)
                            return enemyMediumPrefabs[Random.Range(0, enemyMediumPrefabs.Count)];
                        else
                            return enemyEasyPrefabs[Random.Range(0, enemyEasyPrefabs.Count)];
                    }
            }
        }
        if (difficulty == 1)
        {
            return enemyEasyPrefabs[Random.Range(0, enemyEasyPrefabs.Count)];
            // easy only
        }
        else if (difficulty == 2)
        {
            // easy mobs the first 25%
            if (wave < maxWaves/4)
            {
                return enemyEasyPrefabs[Random.Range(0, enemyEasyPrefabs.Count)];
            } else if (wave < (3*maxWaves)/4)
            {
                // mix of medium and easy mobs the next 50%
                if (Random.Range(0, 2) < 1)
                {
                    return enemyEasyPrefabs[Random.Range(0, enemyEasyPrefabs.Count)];
                } else
                {
                    return enemyMediumPrefabs[Random.Range(0, enemyMediumPrefabs.Count)];
                }
            } else
            {
                // medium mobs the last 25%
                return enemyMediumPrefabs[Random.Range(0, enemyMediumPrefabs.Count)];
            }
        }
        else if (difficulty == 3)
        {
            // easy mobs the first 12%
            if (wave < maxWaves / 6)
            {
                return enemyEasyPrefabs[Random.Range(0, enemyEasyPrefabs.Count)];
            }
            else if (wave < (2 * maxWaves) / 6)
            {
                // mix of medium and easy mobs the next 12%
                if (Random.Range(0, 2) < 1)
                {
                    return enemyEasyPrefabs[Random.Range(0, enemyEasyPrefabs.Count)];
                }
                else
                {
                    return enemyMediumPrefabs[Random.Range(0, enemyMediumPrefabs.Count)];
                }
            }
            else if (wave < (3 * maxWaves) / 6)
            {
                // mix of medium and easy mobs the next 12%
                int mobDifficulty = Random.Range(0, 3);
                switch (mobDifficulty)
                {
                    case 2:
                        return enemyHardPrefabs[Random.Range(0, enemyHardPrefabs.Count)];
                    case 1:
                        return enemyMediumPrefabs[Random.Range(0, enemyMediumPrefabs.Count)];
                    case 0:
                    default:
                        return enemyEasyPrefabs[Random.Range(0, enemyEasyPrefabs.Count)];
                }
            }
            else if (wave < (4 * maxWaves) / 6)
            {
                // mix of medium and hard mobs the next 12%
                if (Random.Range(0, 2) < 1)
                {
                    return enemyHardPrefabs[Random.Range(0, enemyHardPrefabs.Count)];
                }
                else
                {
                    return enemyMediumPrefabs[Random.Range(0, enemyMediumPrefabs.Count)];
                }
            }
            else
            {
                // hard mobs the last 24%
                return enemyHardPrefabs[Random.Range(0, enemyHardPrefabs.Count)];
            }
        }
        else // extreme modes!
        {

            // easy mobs the first 12%
            if (wave < maxWaves / 8)
            {
                // mix of medium and easy mobs the next 12%
                if (Random.Range(0, 2) < 1)
                {
                    return enemyEasyPrefabs[Random.Range(0, enemyEasyPrefabs.Count)];
                }
                else
                {
                    return enemyMediumPrefabs[Random.Range(0, enemyMediumPrefabs.Count)];
                }
            }
            else if (wave < (2 * maxWaves) / 8)
            {
                int mobDifficulty = Random.Range(0, 3);
                switch (mobDifficulty)
                {
                    case 2:
                        return enemyHardPrefabs[Random.Range(0, enemyHardPrefabs.Count)];
                    case 1:
                        return enemyMediumPrefabs[Random.Range(0, enemyMediumPrefabs.Count)];
                    case 0:
                    default:
                        return enemyEasyPrefabs[Random.Range(0, enemyEasyPrefabs.Count)];
                }
            }
            else if (wave < (3 * maxWaves) / 8)
            {
                // mix of medium and easy mobs the next 12%
                if (Random.Range(0, 2) < 1)
                {
                    return enemyHardPrefabs[Random.Range(0, enemyHardPrefabs.Count)];
                }
                else
                {
                    return enemyMediumPrefabs[Random.Range(0, enemyMediumPrefabs.Count)];
                }
            }
            else if (wave < (maxWaves) / 2)
            {
                int mobDifficulty = Random.Range(0, 3);
                switch (mobDifficulty)
                {
                    case 2:
                        return enemyHardPrefabs[Random.Range(0, enemyHardPrefabs.Count)];
                    case 1:
                        return enemyMediumPrefabs[Random.Range(0, enemyMediumPrefabs.Count)];
                    case 0:
                    default:
                        return enemyExtremePrefabs[Random.Range(0, enemyExtremePrefabs.Count)];
                }
            }
            else if (wave < (5 * maxWaves) / 8)
            {
                // mix of medium and hard mobs the next 12%
                if (Random.Range(0, 2) < 1)
                {
                    return enemyHardPrefabs[Random.Range(0, enemyHardPrefabs.Count)];
                }
                else
                {
                    return enemyExtremePrefabs[Random.Range(0, enemyExtremePrefabs.Count)];
                }
            } else
            {
                return enemyExtremePrefabs[Random.Range(0, enemyExtremePrefabs.Count)];
            }
        }
    }

}