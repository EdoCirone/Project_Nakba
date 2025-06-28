using System.Collections.Generic;
using UnityEngine;

public class AidSpawner : MonoBehaviour
{

    public GameObject[] aidPrefab;
    public float spawnInterval = 2f;
    public AudioClip spawnSound;
    public int maxAid = 10;

    [Header("Area di spawn")]
    public Vector2 spawnAreaMin = new Vector2(-10, -5);
    public Vector2 spawnAreaMax = new Vector2(10, 5);

    private float timer = 0f;
    private List<GameObject> spawnedAid = new List<GameObject>();

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && spawnedAid.Count < maxAid)
        {
            SpawnAid();
            AudioSource.PlayClipAtPoint(spawnSound, transform.position, 1);
            timer = 0f;
        }
    }

    void SpawnAid()
    {
        if (aidPrefab == null || aidPrefab.Length == 0) return;

        Vector2 randomPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        GameObject randomAid = aidPrefab[Random.Range(0, aidPrefab.Length)];
        GameObject aid = Instantiate(randomAid, randomPosition, Quaternion.identity);
        spawnedAid.Add(aid);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 center = (spawnAreaMin + spawnAreaMax) / 2f;
        Vector3 size = spawnAreaMax - spawnAreaMin;
        Gizmos.DrawWireCube(center, size);
    }
}
