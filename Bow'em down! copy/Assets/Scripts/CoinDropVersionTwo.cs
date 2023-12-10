using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDropVersionTwo : MonoBehaviour
{
    public GameObject Coin;
    GameObject[] coins;

    public float speed = 5;
    Vector2 targetPosition;


    void Start()
    {
        Vector2 targetPosition = new Vector2(GameObject.FindGameObjectWithTag("Home").transform.position.x, GameObject.FindGameObjectWithTag("Home").transform.position.y);

    }

    
    void FixedUpdate()
    {
        
        if (coins.Length == 0) Destroy(gameObject);
        
    }


    public void InstantiateCoins(int numberOfCoins)
    {
        coins = new GameObject[numberOfCoins]; // Initialize the array with the specified size
        for(int i = 0; i < numberOfCoins; i++)
        {
            coins[i] = Instantiate(Coin, transform.position, transform.rotation);
            Rigidbody2D coinRb = coins[i].GetComponent<Rigidbody2D>();
            coinRb.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(1.25f, 1.75f));
           
        }
        
    }
    
}
