using System.Collections;
using UnityEngine;
using static Wave;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public Home home;
    public Wave[] waves;
    private int currentWaveIndex = 0;
    private bool waveHasBeenCleared = false;

    public TextMeshProUGUI WaveText;

    //time between waves
    bool waitUntilNextWave;
    public GameObject skipToNextWaveButton;
    public GameObject UpgradeCanvas;

    void Start()
    {
        StartCoroutine(SpawnWaves());
        skipToNextWaveButton.SetActive(false);
        UpgradeCanvas.SetActive(false);
        UpdateWaveText();
    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") == null) waveHasBeenCleared = true;
    }

    IEnumerator SpawnWaves()
    {
        while (currentWaveIndex < waves.Length)
        {
            waveHasBeenCleared = false;
            Wave currentWave = waves[currentWaveIndex];

            foreach (EnemySpawnInfo enemyInfo in currentWave.enemies)
            {
                for (int i = 0; i < enemyInfo.count; i++)
                {
                    SpawnEnemy(enemyInfo.enemyPrefab);
                    yield return new WaitForSeconds(enemyInfo.timeBetweenSpawns);
                }
            }
            
            //wait until the enemies have been killed
            while (!waveHasBeenCleared)
            {
                yield return new WaitForSeconds(1);
            }
            
            //make button appear to skip to next wave but till then wait..
            Bow bow = GameObject.FindGameObjectWithTag("Bow").GetComponent<Bow>();
            bow.inputAllowed = false;
            waitUntilNextWave = true;
            home.isAbleToRegen = false;
            skipToNextWaveButton.SetActive(true);
            UpgradeCanvas.SetActive(true);
            //Destroy all Fireballs
            GameObject[] fireballs = GameObject.FindGameObjectsWithTag("Fireball");
            foreach (GameObject fireball in fireballs)
            {
                fireball.GetComponent<Animator>().SetTrigger("explode");
            }

            while (waitUntilNextWave)
            {
                yield return new WaitForSeconds(1);
            }

            bow.inputAllowed = true;
            home.isAbleToRegen = true;
            skipToNextWaveButton.SetActive(false);
            UpgradeCanvas.SetActive(false);
            currentWaveIndex++;
            UpdateWaveText();
        }
    }

    void UpdateWaveText()
    {
        WaveText.text = "Wave " + (currentWaveIndex + 1).ToString();
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        // Implement your logic to spawn enemies here
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    public void SkipToNextWave()
    {
        waitUntilNextWave = false;
        
    }
}

[System.Serializable]
public class Wave
{
    [System.Serializable]
    public class EnemySpawnInfo
    {
        public GameObject enemyPrefab;
        public int count;
        public float timeBetweenSpawns;
    }

    public EnemySpawnInfo[] enemies;
    public float timeBetweenWaves;
}
