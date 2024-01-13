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

    //regen uprgades
    float totalRegenUpgrades = 15;
    float regenUpgradesAquired = 0;
    public GameObject RegenUpgradesAquired;
    float regenUpgradeCoins = 10;
    public TextMeshProUGUI regenText;

    //chance of critical upgrades
    float totalChanceCriticalUpgrades = 10;
    float chanceCriticalUpgradesAquired = 0;
    public GameObject ChanceCriticalUpgradesAquired;
    float chanceCriticalCoins = 15;
    public TextMeshProUGUI chanceCriticalText;

    //critical Mutliplier upgrades
    float totalCriticalMultiplierUpgrades = 10;
    float criticalMutliplierUpgradesAquired = 0;
    public GameObject CriticalMultiplierUpgradesAquired;
    float criticalMultiplierCoins = 15;
    public TextMeshProUGUI criticalMultiplierText;

    // Start is called before the first frame update
    void Start()
    {
        PowerProgressImage.transform.localScale = new Vector3(0, PowerProgressImage.transform.localScale.y, PowerProgressImage.transform.localScale.z);
        PowerText.text = "Power (" + powerCoins + ")";
        ChargeTimeUpgradesAquired.transform.localScale = new Vector3(0, 1, 1);
        chargeTimeText.text = "Charge Time (" + chargeTimeCoins + ")";
        HealthUpgradesAquired.transform.localScale = new Vector3(healthUpgradesAquired / totalHealthUpgrades, 1, 1);
        healthText.text = "Health (" + healthUpgradeCoins + ")";
        RegenUpgradesAquired.transform.localScale = new Vector3(regenUpgradesAquired / totalRegenUpgrades, 1, 1);
        regenText.text = "Regen (" + regenUpgradeCoins + ")";
        ChanceCriticalUpgradesAquired.transform.localScale = new Vector3(chanceCriticalUpgradesAquired / totalChanceCriticalUpgrades, 1, 1);
        chanceCriticalText.text = "crit Chance (" + chanceCriticalCoins + ")";
        CriticalMultiplierUpgradesAquired.transform.localScale = new Vector3(criticalMutliplierUpgradesAquired / totalCriticalMultiplierUpgrades, 1, 1);
        criticalMultiplierText.text = "crit Multiply (" + criticalMultiplierCoins + ")";
    }
    

    public void IncreasePower()
    {
        if(powerUpgradesAquired < totalPowerUpgrades && coinMaster.coins >= powerCoins) { 
            bow.power += 1.25f;
            powerUpgradesAquired++;
            PowerProgressImage.transform.localScale = new Vector3(powerUpgradesAquired / totalPowerUpgrades, PowerProgressImage.transform.localScale.y, PowerProgressImage.transform.localScale.z);
            coinMaster.assignCoins(-powerCoins);
            powerCoins *= 2f;
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
    public void IncreaseRegen()
    {
        if(regenUpgradesAquired < totalRegenUpgrades && coinMaster.coins >= regenUpgradeCoins)
        {
            home.regenRate *= 1.1f;
            regenUpgradesAquired++;
            RegenUpgradesAquired.transform.localScale = new Vector3(regenUpgradesAquired / totalRegenUpgrades, 1, 1);
            coinMaster.assignCoins(-regenUpgradeCoins);
            regenUpgradeCoins *= 1.4f;
            regenText.text = "Regen (" + regenUpgradeCoins + ")";
        }
    }
    public void ChanceCritical()
    {
        if(chanceCriticalUpgradesAquired < totalChanceCriticalUpgrades && coinMaster.coins >= chanceCriticalCoins)
        {
            bow.chanceOfCritical += 5;
            chanceCriticalUpgradesAquired++;
            ChanceCriticalUpgradesAquired.transform.localScale = new Vector3(chanceCriticalUpgradesAquired / totalChanceCriticalUpgrades, 1, 1);
            coinMaster.assignCoins(-chanceCriticalCoins);
            chanceCriticalCoins *= 2;
            chanceCriticalText.text = "crit Chance (" + chanceCriticalCoins + ")";
        }
    }
    public void criticalMultiplier()
    {
        if (criticalMutliplierUpgradesAquired < totalCriticalMultiplierUpgrades && coinMaster.coins >= criticalMultiplierCoins)
        {
            bow.criticalMultiplier += 0.15f;
            criticalMutliplierUpgradesAquired++;
            CriticalMultiplierUpgradesAquired.transform.localScale = new Vector3(criticalMutliplierUpgradesAquired / totalCriticalMultiplierUpgrades, 1, 1);
            coinMaster.assignCoins(-criticalMultiplierCoins);
            criticalMultiplierCoins *= 2;
            criticalMultiplierText.text = "crit Multiply (" + criticalMultiplierCoins + ")";
        }
    }
}
