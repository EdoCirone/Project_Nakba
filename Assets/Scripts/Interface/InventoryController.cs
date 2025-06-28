using UnityEditor.UIElements;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject[] itemPrefab;

    
    void Start()
    {
        for ( int i = 0; i < slotCount; i ++)
        {
            Slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();
            if (i < itemPrefab.Length)
            {
                GameObject item = Instantiate(itemPrefab[i], slot.transform);
                item.GetComponent<RectTransform>().anchorMin = Vector2.zero;
                slot.currentItem = item;
            }
        }
    }
}
