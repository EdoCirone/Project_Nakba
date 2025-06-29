using UnityEngine;

public class RaidEvent : Event
{
    [Header("Prefab del RaidSpawner da inserire")]
    public GameObject raidSpawner;
    public override void Trigger()
    {

        base.Trigger();

        if (raidSpawner == null)
        {
            Debug.LogWarning(" prefab non assegnato in aidEvent.");
            return;
        }

        GameObject instance = Instantiate(raidSpawner);
        instance.name = "RaidSpawner_EventSpawned";

        Debug.Log($"[RaidEvent] RaidSpawner istanziato: {instance.name}");
    }
}
