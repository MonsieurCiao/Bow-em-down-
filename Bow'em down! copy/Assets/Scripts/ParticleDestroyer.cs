using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    public float time;

    void Start()
    {
        StartCoroutine(DestroyDis());
    }

    IEnumerator DestroyDis()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
