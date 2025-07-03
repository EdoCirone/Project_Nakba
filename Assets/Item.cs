using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Item : MonoBehaviour, IPointerClickHandler
{
    public string nome;
    public int ID;
    public abstract void OnPointerClick(PointerEventData eventData);
  
}
