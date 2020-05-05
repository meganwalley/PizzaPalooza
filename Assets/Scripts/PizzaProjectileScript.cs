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
    void FixedUpdate()
    {
        if (pause)
            return;
        rb.MovePosition(rb.position + speed * velocity * Time.deltaTime);
    }

    public void BBQ()
    {
        velocity = new Vector2(velocity.y, velocity.x);
        switch (Random.Range(0, 4))
        {
            case 0:
                // pos y, pos x
                velocity.y = Mathf.Abs(velocity.y);
                velocity.x = Mathf.Abs(velocity.x);
                break;
            case 1:
                // pos y, neg x
                velocity.y = Mathf.Abs(velocity.y);
                velocity.x = 0 - Mathf.Abs(velocity.x);
                break;
            case 2:
                // neg y, pos x
                velocity.y = 0 - Mathf.Abs(velocity.y);
                velocity.x = Mathf.Abs(velocity.x);
                break;
            case 3:
            default:
                // neg y, neg x
                velocity.y = 0 - Mathf.Abs(velocity.y);
                velocity.x = 0 - Mathf.Abs(velocity.x);
                break;
        }
    }

    // 1, 2

}
