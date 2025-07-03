using System.Collections.Generic;
using UnityEngine;


public abstract class Event: MonoBehaviour
{
    [SerializeField] public string eventName { get; set; }
    [SerializeField] string description;
    [SerializeField] Sprite icon;
    public DayPhase associatedPhase; 
    public virtual void Trigger()
    {
        Debug.Log(eventName);
    }

}
