using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Movimento libero")]
    [SerializeField] float moveSpeed = 5f;

    [Header("Follow")]
    [SerializeField] float snapSpeed = 5f; // Valore consigliato: 3–7
    private Transform targetToFollow = null;

    private float clickTimer = 0f;
    private const float doubleClickTime = 0.3f;
    private GameObject lastClicked = null;

    void Update()
    {
        HandleFreeMovement();
        clickTimer += Time.deltaTime;
    }

    void LateUpdate()
    {
        HandleCameraFollow(); // si muove dopo tutto
    }

    void HandleCameraFollow()
    {
        if (targetToFollow != null)
        {
            Vector3 targetPos;

            Rigidbody2D rb = targetToFollow.GetComponent<Rigidbody2D>();
            targetPos = rb != null ? (Vector3)rb.position : targetToFollow.position;

            targetPos.z = transform.position.z;

            transform.position = Vector3.Lerp(transform.position, targetPos, snapSpeed * Time.deltaTime);
        }
    }

    void HandleFreeMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (moveX != 0 || moveY != 0)
        {
            targetToFollow = null;
            Vector3 direction = new Vector3(moveX, moveY, 0f).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    void OnSelectableClicked(Selectable clicked)
    {
        GameObject clickedObj = clicked.gameObject;

        if (clickedObj == lastClicked && clickTimer < doubleClickTime)
        {
            targetToFollow = clickedObj.transform;
            Debug.Log("Snap-to: " + clickedObj.name);

            // Snap iniziale (opzionale)
            Vector3 startPos;
            Rigidbody2D rb = targetToFollow.GetComponent<Rigidbody2D>();
            startPos = rb != null ? (Vector3)rb.position : targetToFollow.position;

            startPos.z = transform.position.z;
            transform.position = startPos;
        }

        lastClicked = clickedObj;
        clickTimer = 0f;
    }

    void OnEnable()
    {
        Selectable.OnSelectableClicked += OnSelectableClicked;
    }

    void OnDisable()
    {
        Selectable.OnSelectableClicked -= OnSelectableClicked;
    }
}
