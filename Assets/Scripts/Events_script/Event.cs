using UnityEngine;


public abstract class Event: MonoBehaviour
{
    public string eventName;
    public string description;
    public Sprite icon;
    public DayPhase associatedPhase;
    public virtual void Trigger()
    {
        Debug.Log(eventName);
    }
}
