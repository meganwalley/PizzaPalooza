using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{

    public float delayTime = 2f;
    public bool pause = false;
    bool canBeDamaged = true;
    public int maxHealth = 10;
    public int currentHealth = 10;
    List<EnemyScript> enemiesTouching;
    public void Start()
    {
        currentHealth = maxHealth;
        enemiesTouching = new List<EnemyScript>();
    }

    public void Awake()
    {
        StartCoroutine(DamageCooldown(delayTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canBeDamaged)
            return;
        if (collision.tag == "Zombie")
        {
            enemiesTouching.Add(collision.gameObject.GetComponent<EnemyScript>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!canBeDamaged)
            return;
        if (collision.tag == "Zombie")
        {
            enemiesTouching.Remove(collision.gameObject.GetComponent<EnemyScript>());
        }
    }

    IEnumerator DamageCooldown(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (!pause)
            Damage();
        StartCoroutine(DamageCooldown(delay));
    }

    void Damage()
    {
        if (enemiesTouching.Count == 0)
            return;
        foreach (EnemyScript es in enemiesTouching)
        {
            if (es != null)
            {
                currentHealth -= es.GetDamage();
            }
        }
    }
}
