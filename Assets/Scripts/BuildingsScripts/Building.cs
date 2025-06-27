using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D))]
public abstract class Building : MonoBehaviour, IContextProvider
{
    [SerializeField] protected float recoveryTime = 100f;
    [SerializeField] protected Vector2 exitOffset = new Vector2(1f, 0f);

    protected List<GameObject> occupants = new List<GameObject>();
    public bool IsOccupied => occupants.Count > 0;

    private Collider2D _collider;
    private Transform _player;

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
        // Non più necessario salvare il riferimento una volta sola
    }

        void Update()
    {
        if (_collider == null) return;

        var selected = Selectable.Selected;
        if (selected != null)
        {
            float playerY = selected.transform.position.y;
            float buildingY = transform.position.y;

            // Se il player è SOPRA (y > building.y), allora passa dietro → disattiva collider
            _collider.enabled = playerY < buildingY;
        }
        else
        {
            _collider.enabled = true; // Nessun personaggio selezionato → abilita collider
        }
    }

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
                Destroy(p); // oppure p.GetComponent<Vita>()?.Muori();
        }

        occupants.Clear();
    }

    protected virtual bool IsDestroyedByBomb()
    {
        Bombardabile bomb = GetComponent<Bombardabile>();
        return bomb != null && bomb.isBombed;
    }

    public abstract void OnPlayerEnter(GameObject player);

    public abstract List<ContextAction> GetContextActions(GameObject player);
}
