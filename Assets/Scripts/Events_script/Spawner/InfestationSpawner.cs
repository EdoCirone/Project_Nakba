using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public class InfestationSpawner : MonoBehaviour
{
    [Header("Setup")]
    public GameObject infestationPrefab;      
    public Transform[] spawnPoints;            

    [Header("Ondata")]
    public int initialWaveCount = 3;           
    public float timeBetweenWaves = 10f;       
    public int waveIncreaseAmount = 2;         

    private int currentWave = 0;
    private int infestationsToSpawn;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            currentWave++;
            infestationsToSpawn = initialWaveCount + (currentWave - 1) * waveIncreaseAmount;

            Debug.Log($"Inizio onda {currentWave}: spawn {infestationsToSpawn} infestazioni.");

           
            for (int i = 0; i < infestationsToSpawn; i++)
            {
                SpawnInfestation();
                yield return new WaitForSeconds(0.5f);
            }

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    void SpawnInfestation()
    {
        if (spawnPoints.Length == 0) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(infestationPrefab, spawnPoint.position, Quaternion.identity);
    }
}
