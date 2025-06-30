using UnityEngine;

public class Teacher : NPC
{
    public float teachingRadius = 3f;
    public float moraleBoost = 8f;
    public float teachingCooldown = 6f;

    private float lastTeachTime = 0f;

    protected override void Update()
    {
        base.Update();

        if (Time.time >= lastTeachTime + teachingCooldown)
        {
            Insegna();
            lastTeachTime = Time.time;
        }
    }

    void Insegna()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, teachingRadius);

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == this.gameObject) continue;

            Bisogni bisogni = hit.GetComponent<Bisogni>();
            if (bisogni != null)
            {
                bisogni.Conforta(moraleBoost);
                Debug.Log($"Teacher ha aumentato il morale di {hit.name} di {moraleBoost}.");
            }
        }
    }
}
