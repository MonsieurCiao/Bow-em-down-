using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    CoinMaster coinMaster;
    Vector3 targetPosition;
    public float speed = 5f;
    public float arrivalThreshold = 0.1f; // Define a threshold for reaching the target

    bool hasHitBase = false;
    Vector3 startingPosition;
    float totalDistance;
    void Start()
    {
        
        coinMaster = GameObject.FindGameObjectWithTag("CoinMaster").GetComponent<CoinMaster>();

        targetPosition = new Vector3(-5.83900023f, -3.37800002f, 0);


        StartCoroutine(FlyTowardsBase());
    }

    IEnumerator FlyTowardsBase()
    {
        yield return new WaitForSeconds(1f);
        startingPosition = transform.position;
        totalDistance = Vector3.Distance(startingPosition, targetPosition);
        Destroy(GetComponent<CircleCollider2D>());
        Destroy(GetComponent<Rigidbody2D>());
        GetComponent<TrailRenderer>().enabled = true;
        while (!hasHitBase)
        {
            // Calculate the distance to the target
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
            float distanceToTargetTravelled = totalDistance - distanceToTarget;           
            float speedToDistance = speed * distanceToTargetTravelled / totalDistance;
            if (speedToDistance < 10f) speedToDistance = 10f;
            // Calculate the direction to move towards the target position
            Vector3 direction = (targetPosition - transform.position).normalized;
                       
            // Move the object towards the target at the specified speed
            transform.position += direction * Mathf.Min(speedToDistance * Time.deltaTime, distanceToTarget);

            // Check if we're close enough to the target position
            if (distanceToTarget < arrivalThreshold)
            {
                hasHitBase = true;
            }
            
            yield return null;
        }

        coinMaster.assignCoins(1);
        Destroy(gameObject);
    }
}
