using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D rb;

    //particle
    private ParticleSystem particle;
    public float emissionRate;
    ParticleSystem.EmissionModule emissionModule;

    ArrowHead arrowHead;

    //torque
    [HideInInspector]
    public bool hasCollidedWithGround = false;
    
    private float angle;

    //damage
    float damage;
    

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        particle = GetComponent<ParticleSystem>();
        GameObject arrowParent = GameObject.FindGameObjectWithTag("arrowParentTag");
        arrowHead = GetComponentInChildren<ArrowHead>();


        // Set the instantiated arrow as a child of the arrow parent
        transform.parent = arrowParent.transform;
        if (rb == null)
        {
            print("no rigidbody found");
        }
    }
    
    private void Update()
    {
        //standart particle sys 
        emissionModule = particle.emission;
        emissionModule.rateOverTime = rb.velocity.x + Mathf.Abs(rb.velocity.y) * emissionRate;

        
    }
    
    IEnumerator CalculateRotation()
    {
        
        while (!hasCollidedWithGround)
        {
            angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            yield return null;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            hasCollidedWithGround = true;
            particle.Stop();
        }
    }
    
    public IEnumerator StartFlying(float power)
    {

        // Apply force to the right
        Vector2 force = transform.right * power;
        rb.AddForce(force, ForceMode2D.Impulse);
        StartCoroutine(CalculateRotation());
        arrowHead.SetDamage(power);
        yield return new WaitForSeconds(20f);
        Destroy(this.gameObject);
    }
}
