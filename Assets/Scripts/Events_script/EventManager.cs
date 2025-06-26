using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [Header("Tutti gli eventi disponibili")]
    public List<Event> allEvents;

    [Header("Eventi del giorno (uno per fase)")]
    public Dictionary<DayPhase, Event> EventsOfTheDay { get; private set; } = new();

    private void OnEnable()
    {
        DailyCicleTime.OnPhaseChanged += OnPhaseChanged;
    }

    private void OnDisable()
    {
        DailyCicleTime.OnPhaseChanged -= OnPhaseChanged;
    }

    void Start()
    {
        GenerateDailyEvents();
    }

    public void GenerateDailyEvents()
    {
        EventsOfTheDay.Clear();

        List<Event> copy = new(allEvents);

        foreach (DayPhase phase in System.Enum.GetValues(typeof(DayPhase)))
        {
            if (copy.Count == 0) break;

            int index = Random.Range(0, copy.Count);
            Event selected = copy[index];
            selected.associatedPhase = phase;
            EventsOfTheDay[phase] = selected;
            copy.RemoveAt(index);
        }

        Debug.Log("Eventi assegnati alle fasi del giorno:");
        foreach (var kv in EventsOfTheDay)
            Debug.Log($" - {kv.Key}: {kv.Value.eventName}");
    }

    private void OnPhaseChanged(DayPhase newPhase)
    {
        if (EventsOfTheDay.TryGetValue(newPhase, out Event evt))
        {
            evt.Trigger();
        }
    }
}
