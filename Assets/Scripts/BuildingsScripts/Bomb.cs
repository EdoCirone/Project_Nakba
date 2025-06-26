using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explosionRadius = 5f;
    public LayerMask bombardabileLayer;
    public GameObject explosionEffect;
    public Vector2 drop = Vector2.down;
    private Rigidbody2D rb;
    public float dropSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 10); // per testare, va modificato.
    }

    private void Explode()
    {
        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius, bombardabileLayer);

        foreach (Collider2D hit in hits)
        {
            Bombardabile target = hit.GetComponentInParent<Bombardabile>();
            if (target != null)
            {
                target.TriggerBombardamento();
            }
        }

        Destroy(gameObject);
    }

    private void Update()
    {
        rb.MovePosition(rb.position + drop.normalized * dropSpeed * Time.deltaTime);

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & bombardabileLayer) != 0)
        {
            Explode();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
