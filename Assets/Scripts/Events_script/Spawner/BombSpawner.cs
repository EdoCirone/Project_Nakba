using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{

    public GameObject[] bombPrefab;
    public float spawnInterval = 2f;
    public AudioClip spawnSound;
    public int maxBomb = 10;

    [Header("Area di spawn")]
    public Vector2 spawnAreaMin = new Vector2(-10, -5);
    public Vector2 spawnAreaMax = new Vector2(10, 5);

    private float timer = 0f;
    private List<GameObject> spawnedBomb = new List<GameObject>();

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && spawnedBomb.Count < maxBomb)
        {
            SpawnBomb();
            AudioSource.PlayClipAtPoint(spawnSound, transform.position, 1);
            timer = 0f;
        }
    }

    void SpawnBomb()
    {
        if (bombPrefab == null || bombPrefab.Length == 0) return;

        Vector2 randomPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        GameObject randomEnemy = bombPrefab[Random.Range(0, bombPrefab.Length)];
        GameObject enemy = Instantiate(randomEnemy, randomPosition, Quaternion.identity);
        spawnedBomb.Add(enemy);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 center = (spawnAreaMin + spawnAreaMax) / 2f;
        Vector3 size = spawnAreaMax - spawnAreaMin;
        Gizmos.DrawWireCube(center, size);
    }
}

