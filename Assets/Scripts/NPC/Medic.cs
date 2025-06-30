using UnityEngine;

public class Medic : NPC
{
    public float healingRadius = 3f;
    public float healingAmount = 10f;
    public float healingCooldown = 5f;

    private float lastHealTime = 0f;

    protected override void Update()
    {
        base.Update();

        if (Time.time >= lastHealTime + healingCooldown)
        {
            CuraVicini();
            lastHealTime = Time.time;
        }
    }

    void CuraVicini()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, healingRadius);

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == this.gameObject) continue;

            Bisogni bisogni = hit.GetComponent<Bisogni>();
            if (bisogni != null)
            {
                LifeController vita = hit.GetComponent<LifeController>();
                if (vita != null)
                {
                    vita.AddHp(healingAmount);
                    Debug.Log($"Medico ha curato {hit.name} di {healingAmount} HP.");
                }
            }
        }
    }
}
