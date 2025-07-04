using UnityEngine;
using UnityEngine.EventSystems;

public class Bandage : Item, IPointerClickHandler
{
    private StatusAilments status;
   
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == 0)
        {
            status = Object.FindFirstObjectByType<StatusAilments>();
            status.isInjured = false;
            Debug.Log("Injury healed!");
        }
    }
}
