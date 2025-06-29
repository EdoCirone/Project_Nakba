using UnityEngine;

public class EventCleanupController : MonoBehaviour
{
    [Header("Tag degli oggetti da pulire")]
    public string eventTag = "OggettiEvento";

    [Header("Pulizia automatica al cambio fase")]
    public bool cleanupOnPhaseChange = true;

    private void OnEnable()
    {
        if (cleanupOnPhaseChange)
            DailyCicleTime.OnPhaseChanged += HandlePhaseChanged;
    }

    private void OnDisable()
    {
        if (cleanupOnPhaseChange)
            DailyCicleTime.OnPhaseChanged -= HandlePhaseChanged;
    }

    private void HandlePhaseChanged(DayPhase newPhase)
    {
        CleanupEventObjects();
    }

    public void CleanupEventObjects()
    {
        GameObject[] allEventObjects = GameObject.FindGameObjectsWithTag(eventTag);
        foreach (GameObject obj in allEventObjects)
        {
            Destroy(obj);
        }

        Debug.Log($"[Cleanup] Rimossi {allEventObjects.Length} oggetti con tag '{eventTag}'.");
    }
}

