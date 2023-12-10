using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float speed;
    public float damage;
    public float attackRate;
    public float coins;
    public ParticleSystem dieParticles;
    GameObject home;
    bool hasntCollided = true;
    Rigidbody2D rb;
    Home homeScript;
    Animator animator;
    CoinMaster coinMaster;
    public GameObject dropCoins;

    public GameObject CoinDrop;
    // Start is called before the first frame update
    void Start()
    {
        coinMaster = GameObject.FindGameObjectWithTag("CoinMaster").GetComponent<CoinMaster>();
        rb = GetComponent<Rigidbody2D>();
        home = GameObject.FindGameObjectWithTag("Home");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speed", -(rb.velocity.x));
        if (hasntCollided)
        {
            
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Home"))
        {
            homeScript = collision.GetComponent<Home>();
            hasntCollided = false;
            StartCoroutine(MakePain());
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Home"))
        {
            hasntCollided = true;
        }
    }
    public void Hit(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        //assign Coins 
        //coinMaster.assignCoins(coins);

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
    IEnumerator MakePain()
    {
        while (!hasntCollided)
        {
            homeScript.TakeDamage(damage);
            yield return new WaitForSeconds(attackRate);
            
        }
    }
}
