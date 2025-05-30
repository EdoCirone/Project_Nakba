using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    Rigidbody2D _rb;
    Vector2 _target;
    float speed = 5f;
    bool _isMoving = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Converte la posizione del mouse da schermo a mondo
            Vector3 mouseScreenPos = Input.mousePosition;
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            _target = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);
            _isMoving = true;
        }
    }

    void FixedUpdate()
    {
        if (_isMoving)
        {
            Vector2 currentPosition = _rb.position;
            Vector2 dir = (_target - currentPosition).normalized;

            _rb.linearVelocity = dir * speed;

            // Fermati se sei vicino abbastanza
            if (Vector2.Distance(currentPosition, _target) < 0.1f)
            {
                _rb.linearVelocity = Vector2.zero;
                _isMoving = false;
            }
        }
        else
        {
            _rb.linearVelocity = Vector2.zero;
        }
    }
}
