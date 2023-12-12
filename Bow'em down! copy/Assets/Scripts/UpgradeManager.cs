using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UpgradeManager : MonoBehaviour
{
    public Bow bow;
    public CoinMaster coinMaster;
    public Home home;

    //power Upgrades
    float totalPowerUpgrades = 10;
    float powerUpgradesAquired = 0;
    public GameObject PowerProgressImage;
    float powerCoins = 20;
    public TextMeshProUGUI PowerText;

    //ChargeTime Upgrades
    float totalChargeTimeUpgrades = 5;
    float chargeTimeUpgradesAquired = 0;
    public GameObject ChargeTimeUpgradesAquired;
    float chargeTimeCoins = 10;
    public TextMeshProUGUI chargeTimeText;

    //health upgrades
    float totalHealthUpgrades = 15;
    float healthUpgradesAquired = 0;
    public GameObject HealthUpgradesAquired;
    float healthUpgradeCoins = 10;
    public TextMeshProUGUI healthText;

    // Start is called before the first frame update
    void Start()
    {
        PowerProgressImage.transform.localScale = new Vector3(0, PowerProgressImage.transform.localScale.y, PowerProgressImage.transform.localScale.z);
        PowerText.text = "Power (" + powerCoins + ")";
        ChargeTimeUpgradesAquired.transform.localScale = new Vector3(0, 1, 1);
        chargeTimeText.text = "Charge Time (" + chargeTimeCoins + ")";
        HealthUpgradesAquired.transform.localScale = new Vector3(healthUpgradesAquired / totalHealthUpgrades, 1, 1);
        healthText.text = "Health (" + healthUpgradeCoins + ")";
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
    public void IncreasePower()
    {
        if(powerUpgradesAquired < totalPowerUpgrades && coinMaster.coins >= powerCoins) { 
            bow.power += 1.5f;
            powerUpgradesAquired++;
            PowerProgressImage.transform.localScale = new Vector3(powerUpgradesAquired / totalPowerUpgrades, PowerProgressImage.transform.localScale.y, PowerProgressImage.transform.localScale.z);
            coinMaster.assignCoins(-powerCoins);
            powerCoins *= 3f;
            PowerText.text = "Power (" + powerCoins + ")";
        }
    }
    public void DecreaseTotalChargeTime()
    {
        if(chargeTimeUpgradesAquired < totalChargeTimeUpgrades && coinMaster.coins >= chargeTimeCoins)
        {
            bow.totalChargeTime -= 0.2f;
            chargeTimeUpgradesAquired++;
            ChargeTimeUpgradesAquired.transform.localScale = new Vector3(chargeTimeUpgradesAquired / totalChargeTimeUpgrades, 1, 1);
            coinMaster.assignCoins(-chargeTimeCoins);
            chargeTimeCoins *= 1.5f;
            chargeTimeText.text = "Charge Time (" + chargeTimeCoins + ")";
        }
    }
    public void IncreaseHealth()
    {
        if(healthUpgradesAquired < totalHealthUpgrades && coinMaster.coins >= healthUpgradeCoins)
        {
            home.maxHealth *= 1.1f;
            home.UpdateHealthBar();
            healthUpgradesAquired++;
            HealthUpgradesAquired.transform.localScale = new Vector3(healthUpgradesAquired / totalHealthUpgrades, 1, 1);
            coinMaster.assignCoins(-healthUpgradeCoins);
            healthUpgradeCoins *= 1.4f;
            healthText.text = "Health (" + healthUpgradeCoins + ")";
        }
    }
}
