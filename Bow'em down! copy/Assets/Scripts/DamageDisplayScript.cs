using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDisplayScript : MonoBehaviour
{
    GameObject DamageDisplayHolder;
    // Start is called before the first frame update
    void Start()
    {
        DamageDisplayHolder = GameObject.FindGameObjectWithTag("DamageDisplayHolder");
        transform.SetParent(DamageDisplayHolder.transform);
        Destroy(gameObject, 0.35f);
    }

}
