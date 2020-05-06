using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaCollectibleSpawner : MonoBehaviour
{
    PlayerData data;
    public GameObject main;
    public GameManager manager;
    public GameObject PizzaCollectibleCheesePrefab;
    public GameObject PizzaCollectiblePepperoniPrefab;
    public GameObject PizzaCollectibleBBQPrefab;
    public GameObject PizzaCollectibleSupremePrefab;
    public GameObject PizzaCollectibleHawaiianPrefab;
    public float delay = 2f;

    // Start is called before the first frame update
    void Awake()
    {
        data = GameObject.FindObjectOfType<PlayerData>();
        manager = main.GetComponent<GameManager>();
        //manager.GetComponent<GameManager>().DeleteGameObject(collision.gameObject);
        StartCoroutine(Spawner(delay));
    }

    void Spawn()
    {
        if (manager.IsPaused())
            return;
        GameObject temp = Instantiate(getPizza());
        temp.transform.position = this.gameObject.transform.position;
        manager.AddPizzaConveyer(temp);
    }

    private GameObject getPizza()
    {
        /**
        int i = 0;
        if (data.unlockPizzaBBQ)
            i += 3;
        if (data.unlockPizzaCheese)
            i += 1;
        if (data.unlockPizzaSupreme)
            i += 6;
        if (data.unlockPizzaPepperoni)
            i += 2;
        // 9 = all
        // 1 = cheese
        // 3 = cheese + pepperoni
        // 4 = cheese + bbq
        // 6 = cheese + pepperoni + bbq
        // 7 = cheese + supreme
        // 8 = not possible
        // 9 = cheese + pepperoni + supreme
        // 10 = cheese + bbq + supreme
        // 11 = impossible
        // 12 = cheese + pepperoni + bbq + supreme
    **/
        // pepperoni = 25% chance
        // bbq = 15% chance
        // supreme = 5% chance
        // cheese is whatever % left
        int pizza = Random.Range(0, 100);
        if (data.unlockPizzaSupreme && pizza >= 95)
        {
            return PizzaCollectibleSupremePrefab;
        } else if (data.unlockPizzaBBQ && pizza >= 82)
        {
            return PizzaCollectibleBBQPrefab;
        } else if (data.unlockPizzaPepperoni && pizza >= 65)
        {
            return PizzaCollectiblePepperoniPrefab;
        } else if (data.unlockPizzaHawaiian && pizza < 10)
        {
            return PizzaCollectibleHawaiianPrefab;
        } else
        {
            return PizzaCollectibleCheesePrefab;
        }
    }

    IEnumerator Spawner(float del)
    {
        yield return new WaitForSeconds(del);
        Spawn();
        StartCoroutine(Spawner(NewDelayTime(delay)));
    }
    IEnumerator Wait(float del)
    {
        yield return new WaitForSeconds(del);
    }

    float NewDelayTime(float delay)
    {
        if (data.unlockBeltOil)
        {
            delay -= 0.1f;
        }
        if (data.unlockBeltGears) {
            delay -= 0.1f;
        }
        if (data.unlockBeltReplace)
        {
            delay -= 0.2f;
        }
        if (data.unlockBeltFire) {
            delay -= 0.2f;
        }
        return delay;
    }
}
