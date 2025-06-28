using UnityEngine;

public class AidEvent : Event
{
    [Header("Prefab del AidSpawner da inserire")]
    public GameObject aidSpawner;
    public override void Trigger()
    {

        base.Trigger();

        if (aidSpawner == null)
        {
            Debug.LogWarning(" prefab non assegnato in aidEvent.");
            return;
        }

        GameObject instance = Instantiate(aidSpawner);
        instance.name = "AidSpawner_EventSpawned";

        Debug.Log($"[AidEvent] AidSpawner istanziato: {instance.name}");
    }
}
