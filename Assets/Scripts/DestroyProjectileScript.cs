using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectileScript : MonoBehaviour
{
    public GameObject manager;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Zombie" && collision.tag != "Player")
        {
            manager.GetComponent<GameManager>().DeleteGameObject(collision.gameObject);
        }
    }
}
