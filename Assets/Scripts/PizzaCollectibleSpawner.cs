using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaCollectibleSpawner : MonoBehaviour
{
    public GameObject main;
    public GameManager manager;
    public GameObject PizzaCollectiblePrefab;
    public float delay = 2f;

    // Start is called before the first frame update
    void Awake()
    {
        manager = main.GetComponent<GameManager>();
        //manager.GetComponent<GameManager>().DeleteGameObject(collision.gameObject);
        StartCoroutine(Spawner(delay));
    }

    void Spawn()
    {
        if (manager.IsPaused())
            return;
        GameObject temp = Instantiate(PizzaCollectiblePrefab);
        temp.transform.position = this.gameObject.transform.position;
        manager.AddPizzaConveyer(temp);
    }

    IEnumerator Spawner(float del)
    {
        yield return new WaitForSeconds(del);
        Spawn();
        StartCoroutine(Spawner(del));
    }
    IEnumerator Wait(float del)
    {
        yield return new WaitForSeconds(del);
    }
}
