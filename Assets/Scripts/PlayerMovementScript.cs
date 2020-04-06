using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    Vector2 speed;
    public float moveSpeed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    //    anim = this.GetComponent<Animator>();
        speed = new Vector2(0, moveSpeed);
    }

    public void MoveUp()
    {
        rb.MovePosition(rb.position + speed * Time.deltaTime);
    }

    public void MoveDown()
    {
        rb.MovePosition(rb.position - speed * Time.deltaTime);
    }

    public void PickUp()
    {

    }

    public void Throw()
    {

    }


}
