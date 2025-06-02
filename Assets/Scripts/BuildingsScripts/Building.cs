using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public abstract class Building : MonoBehaviour
{
    [SerializeField] protected float recoveryTime = 100f;
    [SerializeField] protected Vector2 exitOffset = new Vector2(1f, 0f);

    protected List<GameObject> occupants = new List<GameObject>();

    public bool IsOccupied => occupants.Count > 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnPlayerEnter(collision.gameObject);
            StartCoroutine(TemporarilyDisablePlayer(collision.gameObject));
        }
    }

    IEnumerator TemporarilyDisablePlayer(GameObject player)
    {
        occupants.Add(player);
        player.SetActive(false);

        yield return new WaitForSeconds(recoveryTime);

        if (player != null && !IsDestroyedByBomb())
        {
            player.transform.position += (Vector3)exitOffset;
            player.SetActive(true);
            occupants.Remove(player);
        }
    }

    public virtual void OnBuildingDestroyedByBomb()
    {
        foreach (var p in occupants)
        {
            if (p != null)
                Destroy(p); // O p.GetComponent<Vita>().Muori();
        }

        occupants.Clear();
    }

    protected virtual bool IsDestroyedByBomb()
    {
        Bombardabile bomb = GetComponent<Bombardabile>();
        return bomb != null && bomb.isBombed;
    }

    public abstract void OnPlayerEnter(GameObject player);
}
