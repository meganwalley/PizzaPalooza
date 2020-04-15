using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaProjectileScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 velocity = new Vector2(1.9f, -1.097f);
    public float speed = 1.25f;
    public bool pause = false;
    // Update is called once per frame
    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (pause)
            return;
        rb.MovePosition(rb.position + speed * velocity * Time.deltaTime);
    }

    // 1, 2

}
