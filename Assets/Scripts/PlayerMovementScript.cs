using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    Vector2 speed;
    public float moveSpeed = 2.0f;
    bool leftWall = false;
    bool rightWall = false;
    List<GameObject> pizzas;
    bool touchingPizza = false;
    // Start is called before the first frame update
    void Start()
    {
        pizzas = new List<GameObject>();
        rb = this.GetComponent<Rigidbody2D>();
    //    anim = this.GetComponent<Animator>();
        speed = new Vector2(9.5f, 5.485f); // 19, 10.97
    }

    public void MoveUp()
    {
        if (rightWall)
            return;
        rb.MovePosition(rb.position + speed * Time.deltaTime);
    }

    public void MoveDown()
    {
        if (leftWall)
            return;
        rb.MovePosition(rb.position - speed * Time.deltaTime);
    }

    public void PickUp(GameObject grabbedPizza)
    {
        pizzas.Remove(grabbedPizza);
        Destroy(grabbedPizza);

    }

    public void Throw()
    {
        // needs to generate a pizza or is this just for the animation?
        // I'm thinking JUST for the animation.
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "LeftPlayerWall")
            leftWall = true;
        else if(collision.gameObject.name == "RightPlayerWall")
            rightWall = true;
        else if(collision.gameObject.name.Contains("PizzaCollectible")) {
            pizzas.Add(collision.gameObject);
            touchingPizza = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "LeftPlayerWall")
            leftWall = false;
        else if (collision.gameObject.name == "RightPlayerWall")
            rightWall = false;
        else if (collision.gameObject.name.Contains("PizzaCollectible")) {
            if (collision.gameObject == null)
            {
                foreach(GameObject pizza in pizzas)
                {
                    if (pizza == null)
                        pizzas.Remove(pizza);
                }
            }
            pizzas.Remove(collision.gameObject);
            touchingPizza = false;
        }
    }

    public GameObject GetPizza()
    {
        GameObject closestPizza = null;
        foreach(GameObject pizza in pizzas)
        {
            closestPizza = Closer(closestPizza, pizza);
        }

        return closestPizza;
    }

    public GameObject Closer(GameObject a, GameObject b)
    {
        if (a == null && b == null)
            return a;
        else if (a == null)
            return b;
        else if (b == null)
            return a;
        else
        {
            float distA = Vector2.Distance(a.transform.position, this.transform.position);
            float distB = Vector2.Distance(b.transform.position, this.transform.position);
            if (distA > distB)
                return b;
            return a;
        }
    }
}