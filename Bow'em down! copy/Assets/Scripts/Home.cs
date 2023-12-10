using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    public float maxHealth = 100;
    public float regenRate = 1;
    float health;
    public GameObject HealthBar;
    bool isAbleToRegen = true;
    private void Start()
    {
        health = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) Die();
        UpdateHealthBar();
    }
    void Die()
    {
        print("Game has ended!!!!");
        Time.timeScale = 0;
    }
    public void UpdateHealthBar()
    {
        HealthBar.transform.localScale = new Vector3(health / maxHealth, 1,1);
    }
    IEnumerator Regen()
    {
        while (isAbleToRegen && health < maxHealth)
        {
            
            health += regenRate;
            if (health > maxHealth) health = maxHealth;
            UpdateHealthBar();
            yield return new WaitForSeconds(1);
        }
    }
}
