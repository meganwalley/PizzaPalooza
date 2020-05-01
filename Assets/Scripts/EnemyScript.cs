using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameManager manager;
    public Animator anim;
    //                                        x       y
    public Vector2 velocity = new Vector2(-1.9f, 1.097f);
    public float speed = 0.75f;
    public bool pause = false;
    public int health = 1;
    public int damage = 1;
    public float worth = 2.5f;
    bool dead = false;
    bool wall = false;
    // Update is called once per frame
    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
    }
    void Update()
    {
        if (pause || wall || dead)
            return;
        //else if (wall)
            // damage health when hitting a wall?
        rb.MovePosition(rb.position + speed * velocity * Time.deltaTime);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("PizzaProjectile"))
        {
            --health;
            anim.SetInteger("health", health);
            manager.DeleteGameObject(collision.gameObject);
            if (health <= 0)
            {
                this.GetComponent<CapsuleCollider2D>().enabled = false;
                dead = true;
                StartCoroutine(DelayDeath());
                // delete the world
            }
        } else if (collision.gameObject.name.Contains("Wall"))
        {
            wall = true;
            anim.SetBool("hitting", true);
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

    IEnumerator DelayDeath()
    {
        damage = 0;
        yield return new WaitForSeconds(1.1f);
        manager.DeleteGameObject(this.gameObject, true);
    }
}
