using UnityEngine;

public class Merchant : NPC
{
    public float interactionRadius = 3f;
    public float tradeCooldown = 10f;
    public GameObject aidPrefabToGive;

    private float lastTradeTime = 0f;

    protected override void Update()
    {
        base.Update();

        if (Time.time >= lastTradeTime + tradeCooldown)
        {
            Scambia();
            lastTradeTime = Time.time;
        }
    }

    void Scambia()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactionRadius);

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == this.gameObject) continue;

            if (hit.CompareTag("Player") || hit.GetComponent<Bisogni>() != null)
            {
                if (aidPrefabToGive != null)
                {
                    Instantiate(aidPrefabToGive, hit.transform.position + Vector3.up * 0.5f, Quaternion.identity);
                    Debug.Log($"Merchant ha distribuito un Aid a {hit.name}.");
                }
            }
        }
    }
}