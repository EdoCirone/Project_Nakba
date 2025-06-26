using System;
using UnityEngine;

public class DailyCicleTime : MonoBehaviour
{
    public float timePerPhase = 10f;
    private float timer;

    public DayPhase CurrentPhase { get; private set; } = DayPhase.Dawn;

    public static event Action<DayPhase> OnPhaseChanged;

    void Start()
    {
        NotifyPhaseChange();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timePerPhase)
        {
            AdvancePhase();
            timer = 0f;
        }
    }

    void AdvancePhase()
    {
        CurrentPhase = (DayPhase)(((int)CurrentPhase + 1) % Enum.GetNames(typeof(DayPhase)).Length);
        NotifyPhaseChange();
    }

    void NotifyPhaseChange()
    {
        Debug.Log("Nuova fase della giornata: " + CurrentPhase);
        OnPhaseChanged?.Invoke(CurrentPhase);
    }
}

