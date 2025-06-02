using UnityEngine;

public class Bombardabile : MonoBehaviour
{
    [Header("Prefab sostitutivo")]
    [SerializeField] private GameObject blueprintDanneggiato;

    [HideInInspector] public bool isBombed = false;

    public void TriggerBombardamento()
    {
        if (isBombed || blueprintDanneggiato == null) return;

        isBombed = true;

        // Istanzia il prefab danneggiato nello stesso punto
        Instantiate(
            blueprintDanneggiato,
            transform.position,
            transform.rotation,
            transform.parent
        );

        // Notifica il Building prima della distruzione
        Building building = GetComponent<Building>();
        if (building != null)
            building.OnBuildingDestroyedByBomb();

        // Distrugge l'edificio originale
        Destroy(gameObject);
    }
}
