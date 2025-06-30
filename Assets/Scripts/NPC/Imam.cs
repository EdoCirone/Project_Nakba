using UnityEngine;

public class Imam : NPC
{
    public float preachingRadius = 3f;
    public float faithBoost = 10f;
    public float preachingCooldown = 8f;

    private float lastPreachTime = 0f;

    protected override void Update()
    {
        base.Update();

        if (Time.time >= lastPreachTime + preachingCooldown)
        {
            Predica();
            lastPreachTime = Time.time;
        }
    }

    void Predica()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, preachingRadius);

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == this.gameObject) continue;

            Bisogni bisogni = hit.GetComponent<Bisogni>();
            if (bisogni != null)
            {
                bisogni.Prega(faithBoost);
                Debug.Log($"Imam ha aumentato la fede di {hit.name} di {faithBoost}.");
            }
        }
    }

}
