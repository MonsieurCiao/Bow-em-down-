using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ArrowHead : MonoBehaviour
{
    Rigidbody2D rbParent;
    BoxCollider2D box;
    BoxCollider2D ownBox;
    Arrow arrow;
    
    public ParticleSystem arrowHitEnemyParticle;
    float damage;
    private void Start()
    {
        rbParent = GetComponentInParent<Rigidbody2D>();
        box = rbParent.GetComponent<BoxCollider2D>();
        ownBox = GetComponent<BoxCollider2D>();
        arrow = GetComponentInParent<Arrow>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            
            rbParent.constraints = RigidbodyConstraints2D.FreezeAll;
            arrow.hasCollidedWithGround = true;
            Destroy(ownBox);
         
            Destroy(box);
            ParticleSystem particle;
            particle = GetComponentInParent<ParticleSystem>();
            particle.Stop();
        }
        if (collision.CompareTag("Enemy"))
        {
            Destroy(box);
            Destroy(ownBox);
            collision.GetComponent<Enemy>().Hit(damage);
            ParticleSystem particleInstance = Instantiate(arrowHitEnemyParticle, transform.position, transform.rotation);
            particleInstance.Play();
            Destroy(particleInstance, 0.32f);

            SpriteRenderer parentSpriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
            parentSpriteRenderer.enabled = false;
        }
    }
    public void SetDamage(float power)
    {
        damage = power;
    }
}
