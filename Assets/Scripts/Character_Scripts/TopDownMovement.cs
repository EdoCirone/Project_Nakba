using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    Rigidbody2D _rb;
    Vector2 _target;
    public Vector2 Dir { get; private set; }
    bool _isMoving = false;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void MoveTo(Vector2 target)
    {
        _target = target;
        _isMoving = true;
    }

    void FixedUpdate()
    {
        if (_isMoving)
        {
            Vector2 currentPosition = _rb.position;
            Dir = (_target - currentPosition).normalized;
            _rb.linearVelocity = Dir * speed;

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
