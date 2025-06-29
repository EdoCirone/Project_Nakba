using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Infestation : MonoBehaviour
{
    public float radius = 3f;
    public float propagationDelay = 5f;
    public int damagePerSecond = 1;
    public GameObject infestationZonePrefab;

    private CircleCollider2D areaCollider;
    private Dictionary<Collider2D, float> damageTimers = new();

    void Start()
    {
        areaCollider = gameObject.AddComponent<CircleCollider2D>();
        areaCollider.isTrigger = true;
        areaCollider.radius = radius;

        StartCoroutine(PropagateAfterDelay());
    }

    IEnumerator PropagateAfterDelay()
    {
        yield return new WaitForSeconds(propagationDelay);

        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector2 spawnPosition = (Vector2)transform.position + randomDirection * radius * 2f;

        Instantiate(infestationZonePrefab, spawnPosition, Quaternion.identity);
    }

    private void Update()
    {
      
        List<Collider2D> keys = new List<Collider2D>(damageTimers.Keys);
        foreach (var col in keys)
        {
            damageTimers[col] += Time.deltaTime;
            if (damageTimers[col] >= 1f)
            {
                ApplyDamage(col);
                damageTimers[col] = 0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Player") || other.CompareTag("NPC")) && !damageTimers.ContainsKey(other))
        {
            damageTimers.Add(other, 0f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (damageTimers.ContainsKey(other))
        {
            damageTimers.Remove(other);
        }
    }

    private void ApplyDamage(Collider2D other)
    {
        LifeController health = other.GetComponent<LifeController>();
        if (health != null)
        {
            health.AddHp(-damagePerSecond);
        }
    }
}
