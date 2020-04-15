using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public List<GameObject> spawnPoints;
    public List<GameObject> enemyPrefabs;
    public GameObject main;
    public GameManager manager;
    public float waveTime;
    public int maxSpawnAtOnce;


    // Start is called before the first frame update
    void Start()
    {
        manager = main.GetComponent<GameManager>();
    }
    private void Awake()
    {
        StartCoroutine(Spawner(waveTime));
    }

    IEnumerator Spawner(float del)
    {
        yield return new WaitForSeconds(del);
        Debug.Log("Spawn");
        int temp = Random.Range(1, maxSpawnAtOnce - 1);
        for (int i = 0; i <= temp; ++i)
        {
            StartCoroutine(Spawn(Random.Range(0f, 1f)));
        }
        StartCoroutine(Spawner(waveTime));
    }

    IEnumerator Spawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (!manager.IsPaused())
        {
            GameObject temp = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)]);
            temp.transform.position = (spawnPoints[Random.Range(0, spawnPoints.Count)]).transform.position;
            temp.GetComponent<EnemyScript>().SetGameManager(manager);
            Debug.Log("Spawning " + temp.name);
            manager.AddEnemy(temp);
        }
    }
}