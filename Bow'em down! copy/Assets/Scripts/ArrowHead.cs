using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using TMPro;

public class ArrowHead : MonoBehaviour
{
    Rigidbody2D rbParent;
    BoxCollider2D box;
    BoxCollider2D ownBox;
    Arrow arrow;
    Bow bow;

    public TextMeshProUGUI damageDisplay;
    public TextMeshProUGUI damageDisplaySmall;
    public TextMeshProUGUI damageDisplayBig;

    public ParticleSystem arrowHitEnemyParticle;
    float damage;


    private void Start()
    {
        rbParent = GetComponentInParent<Rigidbody2D>();
        box = rbParent.GetComponent<BoxCollider2D>();
        ownBox = GetComponent<BoxCollider2D>();
        arrow = GetComponentInParent<Arrow>();
        bow = GameObject.FindGameObjectWithTag("Bow").GetComponent<Bow>();
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
            
            //INSTANTIATING DAMAGE DISPLAY
            if (bow.power == damage)
            {
                Vector3 damageDisplayLocation = transform.position;
                damageDisplay.text = Mathf.Round((damage * 10) / 10).ToString();
                Instantiate(damageDisplay, damageDisplayLocation, GameObject.FindGameObjectWithTag("Home").transform.rotation);
            }
            else if(bow.power >= damage)
            {
                //instantiate smaller
                Vector3 damageDisplayLocation = transform.position;
                damageDisplaySmall.text = Mathf.Round((damage * 10) / 10).ToString();
                Instantiate(damageDisplaySmall, damageDisplayLocation, GameObject.FindGameObjectWithTag("Home").transform.rotation);
            }
            else
            {
                //instantiate bigger
                Vector3 damageDisplayLocation = transform.position;
                damageDisplayBig.text = Mathf.Round((damage * 10) / 10).ToString();
                Instantiate(damageDisplayBig, damageDisplayLocation, GameObject.FindGameObjectWithTag("Home").transform.rotation);
            }

            Destroy(box);
            Destroy(ownBox);
            if(collision.GetComponent<Enemy>() != null)
            {
                collision.GetComponent<Enemy>().Hit(damage);
            }
            else if(collision.GetComponent<WizardScript>() != null)
            {
                collision.GetComponent<WizardScript>().Hit(damage);
            }
            
            ParticleSystem particleInstance = Instantiate(arrowHitEnemyParticle, transform.position, transform.rotation);
            particleInstance.Play();

            SpriteRenderer parentSpriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
            parentSpriteRenderer.enabled = false;
        }
    }
    
    public void SetDamage(float power)
    {
        damage = power;
    }
}
