using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update
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
