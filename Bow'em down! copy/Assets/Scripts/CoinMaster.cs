using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinMaster : MonoBehaviour
{
    public float coins;
    public float emeralds;
    public TMP_Text coinDisplay;
    public TMP_Text emeraldDisplay;

    // Start is called before the first frame update
    //void Start()
    //{
    //    coins = 0.2634f;
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
    public void assignCoins(float gold)
    {
        coins += gold;
        if (coins >= 1000000)
        {
            float result = coins / 1000000;
            coinDisplay.text = (Mathf.Round(result * 100) / 100).ToString() + "m";
        }
        else if(coins >= 1000)
        {
            float result = coins / 1000;
            coinDisplay.text = (Mathf.Round(result * 100) / 100).ToString() + "k";
        }
        
        else coinDisplay.text = (Mathf.Round(coins * 10) / 10).ToString(); 

    }
    public void assignEmeralds(float ems)
    {
        emeralds += ems;
        emeraldDisplay.text = emeralds.ToString();
    }
}

