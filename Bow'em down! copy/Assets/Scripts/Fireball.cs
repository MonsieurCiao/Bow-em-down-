using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    Transform burg;
    Rigidbody2D rb;
    public float speed = 1;
    public float damage;
    Home home;
    Animator animator;
    bool hasCollided = false;
    // Start is called before the first frame update
    void Start()
    {
        //need to CHANGE this if changing WIZARD SCALE
        transform.localScale = new Vector3(1f / 3f, 1f / 3f, 1);


        burg = GameObject.FindGameObjectWithTag("Home").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        home = GameObject.FindGameObjectWithTag("Home").GetComponent<Home>();
        animator = GetComponent<Animator>();

        //setting arrow Holder as Transform Parent
        GameObject arrowParent = GameObject.FindGameObjectWithTag("arrowParentTag");
        transform.parent = arrowParent.transform;
        if (rb == null)
        {
            print("no rigidbody found");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!hasCollided)
        {
            Vector3 direction = burg.position - transform.position;
            direction.Normalize();
            rb.velocity = direction * speed;
        }
        else rb.velocity = Vector2.zero;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Home"))
        {
            MakePain();
            hasCollided = true;
            print("collided with burg");
        }
        if (collision.CompareTag("Arrow"))
        {
            animator.SetTrigger("explode");
        }
    }
    void MakePain()
    {
        home.TakeDamage(damage);
        animator.SetTrigger("explode");
        //call animator "explode" here
        //on finish of animation delete gameobject
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
