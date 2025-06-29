using UnityEngine;

public class InfestationEvent : Event
{
    [Header("Prefab del InfestationSpawner da inserire")]
    public GameObject infestationSpawner;
    public override void Trigger()
    {

        base.Trigger();

        if (infestationSpawner == null)
        {
            Debug.LogWarning(" prefab non assegnato in InfestationEvent.");
            return;
        }

        GameObject instance = Instantiate(infestationSpawner);
        instance.name = "AidSpawner_EventSpawned";

        Debug.Log($"[InfestationEvent] InfestationSpawner istanziato: {instance.name}");
    }
}
