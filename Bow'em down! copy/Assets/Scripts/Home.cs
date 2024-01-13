using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    public float maxHealth = 100;
    public float regenRate = 1;
    float health;
    public GameObject HealthBar;
    public bool isAbleToRegen = true;
    public GameObject GFX;
    private Animator animator;
    
    private void Start()
    {
        animator = GFX.GetComponent<Animator>();
        health = maxHealth;
        StartCoroutine(Regen());
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) {
            Die();
            health = 0;
        }
        UpdateHealthBar();
    }

    void Die()
    {
        print("Game has ended!!!!");
        Time.timeScale = 0;
    }

    public void UpdateHealthBar()
    {
        print(health);
        HealthBar.transform.localScale = new Vector3(health / maxHealth, 1,1);

        //since update health bar is called every time the health changes in any way
        //we can call updatevisuals here.
        UpdateVisuals();
    }

    IEnumerator Regen()
    {
        while (true)
        {
            while (isAbleToRegen && health < maxHealth)
            {

                health += regenRate;
                if (health > maxHealth)
                {
                    health = maxHealth;
                    print("health was above maxHealth");
                }
                UpdateHealthBar();
                yield return new WaitForSeconds(1);
            }
            yield return null;
        }
    }

    void UpdateVisuals()
    {
        if(health / maxHealth == 1)
        {
            animator.SetInteger("healthState", 0);
        }else if(health/ maxHealth > 0.75f){
            animator.SetInteger("healthState", 1);
        }
        else if(health/maxHealth > 0.5f)
        {
            animator.SetInteger("healthState", 2);
        }
        else if (health / maxHealth > 0.25f)
        {
            animator.SetInteger("healthState", 3);
        }
        else
        {
            animator.SetInteger("healthState", 4);
        }
    }
}
