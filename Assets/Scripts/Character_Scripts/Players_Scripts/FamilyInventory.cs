using UnityEngine;

public class FamilyInventory : MonoBehaviour
{
    public int medicine = 0;
    public int food = 0;
    public int water = 0;

    public void Add(string type, int amount)
    {
        switch (type.ToLower())
        {
            case "medicine": medicine += amount; break;
            case "food": food += amount; break;
            case "water": water += amount; break;
            default: Debug.LogWarning("Invalid resource type: " + type); break;
        }
    }

    public bool Consume(string type, int amount)
    {
        switch (type.ToLower())
        {
            case "medicine":
                if (medicine >= amount) { medicine -= amount; return true; }
                break;
            case "food":
                if (food >= amount) { food -= amount; return true; }
                break;
            case "water":
                if (water >= amount) { water -= amount; return true; }
                break;
        }

        Debug.LogWarning("Not enough resource: " + type);
        return false;
    }
}
