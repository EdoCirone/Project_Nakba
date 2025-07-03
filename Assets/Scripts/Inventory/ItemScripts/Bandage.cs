using UnityEngine;
using UnityEngine.EventSystems;

public class Bandage : Item, IPointerClickHandler
{
    StatusAilments status;

    public float satisfaction = 10f;
    // lascio così per non avere i problemi in Unity, più avanti lo aggiusto 
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
