using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventorySlot> slots = new();

    public virtual void AddItem(ItemData item, int amount)
    {
        InventorySlot existing = slots.Find(s => s.item == item);
        if (existing != null)
        {
            existing.quantity += amount;
        }
        else
        {
            slots.Add(new InventorySlot(item, amount));
        }
    }

    public virtual void RemoveItem(ItemData item, int amount)
    {
        InventorySlot slot = slots.Find(s => s.item == item);
        if (slot != null)
        {
            slot.quantity -= amount;
            if (slot.quantity <= 0)
                slots.Remove(slot);
        }
    }
}
