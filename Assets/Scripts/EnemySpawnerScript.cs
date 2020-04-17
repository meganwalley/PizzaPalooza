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
    public int wave = 0;


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
        wave++;
        Debug.Log("Spawning wave #" + wave);
        int temp = Random.Range(0, maxSpawnAtOnce);
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
            manager.AddEnemy(temp);
        }
    }
}