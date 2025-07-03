using UnityEngine;
using UnityEngine.EventSystems;

public class Bread : Item
{
    public float satisfaction = 10f;
    Bisogni eat;

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == 0)
        {
            Bisogni eat = Selectable.Selected.GetComponent<Bisogni>();
            if (eat != null)
            {
                eat.Mangia(satisfaction);
                Debug.Log("Pane usato su " + eat.gameObject.name);
            }
        }
    }

}
  
