using UnityEngine;

public class BombEvent : Event
{
    [Header("Prefab del BombManager da spawnare")]
    public GameObject bombManagerPrefab;
    public override void Trigger()
    {

        base.Trigger();

        if (bombManagerPrefab == null)
        {
            Debug.LogWarning("BombManager prefab non assegnato in BombEvent.");
            return;
        }

        GameObject instance = Instantiate(bombManagerPrefab);   
        instance.name = "BombManager_EventSpawned";

        Debug.Log($"[BombEvent] BombManager istanziato: {instance.name}");
    }
}
