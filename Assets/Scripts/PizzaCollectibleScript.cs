using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaCollectibleScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 velocity = new Vector2(1.9f, -1.097f);
    public float speed = 1f;
    Vector2 rotation;
    bool rotated = false;
    public bool pause = false;
    public string pizzaType = "cheese";
    // Update is called once per frame
    private void Start()
    {
        rotation = new Vector2(-velocity.x, velocity.y);
        rb = this.GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if (pause)
            return;
        if (rotated)
            rb.MovePosition(rb.position + speed * rotation * Time.deltaTime);
        else
            rb.MovePosition(rb.position + speed * velocity * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            rotated = true;
        }
    }
}
