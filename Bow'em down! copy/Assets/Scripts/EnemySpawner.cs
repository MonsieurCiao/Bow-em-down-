using System.Collections;
using UnityEngine;
using static Wave;

public class EnemySpawner : MonoBehaviour
{
    public Wave[] waves;
    private int currentWaveIndex = 0;
    private bool waveHasBeenCleared = false;

    //time between waves
    bool waitUntilNextWave;
    public GameObject skipToNextWaveButton;
    public GameObject UpgradeCanvas;
    void Start()
    {
        StartCoroutine(SpawnWaves());
        skipToNextWaveButton.SetActive(false);
        UpgradeCanvas.SetActive(false);
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
            skipToNextWaveButton.SetActive(true);
            UpgradeCanvas.SetActive(true);
            while (waitUntilNextWave)
            {
                yield return new WaitForSeconds(1);
            }
            bow.inputAllowed = true;
            skipToNextWaveButton.SetActive(false);
            UpgradeCanvas.SetActive(false);
            currentWaveIndex++;
        }
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