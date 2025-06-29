using System.Collections.Generic;
using UnityEngine;

public class RaidSpawner : MonoBehaviour
{
    [Header("Punti di spawn disponibili")]
    public Transform[] spawnPoints;

    [Header("Prefab del nemico da spawnare")]
    public GameObject[] soldierPrefab;

    [Header("Intervallo tra gli spawn (secondi)")]
    public float spawnInterval = 2f;

    [Header("Audio di spawn")]
    public AudioClip spawnSound;

    [Header("Numero di soldati massimi")]
    public int maxSoldier;

    private float timer = 0f;

    private List<GameObject> spawnedSoldiers = new List<GameObject>();


    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && spawnedSoldiers.Count < maxSoldier)
        {
            SpawnEnemy();
            //AudioController.Play(spawnSound, transform.position, 1);
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {

        spawnPoints = System.Array.FindAll(spawnPoints, sp => sp != null);
        if (spawnPoints.Length == 0 || soldierPrefab == null) return;

        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject randomSoldier = soldierPrefab[Random.Range(0, soldierPrefab.Length)];
        Instantiate(randomSoldier, randomSpawnPoint.position, Quaternion.identity);
    }
}
