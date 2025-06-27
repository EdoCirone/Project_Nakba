using UnityEngine;

public class FamilyStatus : MonoBehaviour
{
    public int money = 0;

    public void AddMoney(int amount)
    {
        money += amount;
    }

    public bool Spend(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            return true;
        }

        Debug.LogWarning("Not enough money.");
        return false;
    }
}
