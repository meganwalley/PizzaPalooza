using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameManager manager;
    //                                        x       y
    public Vector2 velocity = new Vector2(-1.9f, 1.097f);
    public float speed = 0.75f;
    public bool pause = false;
    public int health = 1;
    public int damage = 1;
    public float worth = 2.5f;
    bool wall = false;
    // Update is called once per frame
    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (pause)
            return;
        else if (wall)
        {
            // damage health
            return;
        }
        rb.MovePosition(rb.position + speed * velocity * Time.deltaTime);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("PizzaProjectile"))
        {
            --health;
            if (health <= 0)
            {
                manager.DeleteGameObject(collision.gameObject);
                manager.DeleteGameObject(this.gameObject, true);
                // delete the world
            }
        } else if (collision.gameObject.name.Contains("Wall"))
        {
            wall = true;
        }
        // 1, 2
    }

    public void SetGameManager(GameManager manager)
    {
        this.manager = manager;
    }
    public int GetDamage()
    {
        return damage;
    }
    public float GetPoints()
    {
        return worth;
    }
}
