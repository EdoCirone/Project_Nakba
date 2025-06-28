using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class Aid : MonoBehaviour
{
    public GameObject[] aidItems;
    public float fallSpeed = 2f;
    public float minStopTime = 1f;
    public float maxStopTime = 5f;

    private bool isFalling = true;

    void Start()
    {
        float stopTime = Random.Range(minStopTime, maxStopTime);
        Invoke(nameof(StopFalling), stopTime);
    }

    void Update()
    {   if (isFalling)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        }
    }
    void StopFalling()
    {
        isFalling = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryController playerInventory = other.GetComponent<InventoryController>();

            if (playerInventory != null)
            {
                foreach (GameObject item in aidItems)
                {
                    playerInventory.AddItem(item);
                }
            }

            Destroy(gameObject);
        }
    }
}
