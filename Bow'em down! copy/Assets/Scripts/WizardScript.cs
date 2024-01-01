using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardScript : MonoBehaviour
{
    public float health;
    public float speed;
    public float coins;
    public ParticleSystem dieParticles;


    Rigidbody2D rb;

    Animator animator;


    public GameObject CoinDrop;


    //changing between attacking and not attacking
    bool hasntCollided = true;
    bool isOnRightHeight = true;
    float initialHeight = -2.38f;
    float zenit;

    //this bool is required to check wether it is the first runthrough of the code
    bool firstRuntime = true;

    //shooting the Fireball
    public Transform fireballSpawnPos;
    public GameObject fireball;

    // Start is called before the first frame update
    void Start()
    {
        //after spawning set the right height for the wizard
        transform.position = new Vector3(transform.position.x, initialHeight, transform.position.z);

        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        zenit = transform.position.y + Random.Range(1f, 1.5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (hasntCollided)
        {
            if (isOnRightHeight) rb.velocity = new Vector2(-speed, rb.velocity.y);

            else goToNotAttack();
        }
        else
        {
            goToAttack();

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WizardAttack"))
        {


            Invoke("ChangeHasCollided", Random.Range(0, 3.5f));


        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("WizardAttack"))
        {
            hasntCollided = true;
        }
    }
    public void Hit(float damage)
    {
        print("got hit");
        health -= damage;
        print("cur health:" + health);
        if (health <= 0)
        {
            Die();
        }
    }


    void ChangeHasCollided()
    {
        hasntCollided = false;
        rb.velocity = new Vector2(0, rb.velocity.y);
    }
    
    void goToAttack()
    {
        animator.SetBool("isAttacking", true);

        if (transform.position.y <= zenit)
        {


            float factor = (zenit - transform.position.y) / (zenit + (initialHeight * -1));
            if (factor < 0.2) factor = 0.2f;

            transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f * factor, transform.position.z);
            isOnRightHeight = false;
            firstRuntime = true;
        }
        else
        {
            if (firstRuntime)
            {
                rb.velocity = new Vector2(0, 0);
                firstRuntime = false;


                StartCoroutine(Attack());
            }
        }
    }
    void goToNotAttack()
    {
        animator.SetBool("isAttacking", false);
        if (transform.position.y > initialHeight)
        {
            print("moving to initial height");
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
        }
        else
        {
            isOnRightHeight = true;
            print("has reached initial height");
        }
    }
    IEnumerator Attack()
    {
        while (!hasntCollided)
        {
            animator.SetBool("fire", true);
            print("attacking");
            yield return new WaitForSeconds(0.7f);
            animator.SetBool("fire", false);
            yield return new WaitForSeconds(Random.Range(5, 10));
        }
    }
    public void Fire()
    {
        print("FIRE");
        Instantiate(fireball, fireballSpawnPos);
    }



    //DIE LOGIC

    void Die()
    {

        //death Particles
        Vector3 particlePos = new Vector3(transform.position.x - 0.056f, transform.position.y - 0.513f, transform.position.y);
        ParticleSystem dieParticlesInstance = Instantiate(dieParticles, particlePos, transform.rotation);
        dieParticlesInstance.Play();

        //drop Coins Particle

        CoinDropVersionTwo coinDropVersionTwo = Instantiate(CoinDrop, particlePos, transform.rotation).GetComponent<CoinDropVersionTwo>();
        coinDropVersionTwo.InstantiateCoins(Mathf.RoundToInt(coins));




        //destroy gameobject
        Destroy(this.gameObject);
    }
}
