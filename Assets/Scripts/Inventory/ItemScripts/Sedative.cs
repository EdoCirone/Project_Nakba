using Unity.Jobs;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sedative : Item, IPointerClickHandler
{
   
    public float satisfaction = 10f;
   
    Bisogni sleep;

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == 0)
        {
            Bisogni eat = Selectable.Selected.GetComponent<Bisogni>();
            if (sleep != null)
            {
                sleep.Dormi(satisfaction);
                Debug.Log("Pane usato su " + sleep.gameObject.name);
            }
        }
    }
}
