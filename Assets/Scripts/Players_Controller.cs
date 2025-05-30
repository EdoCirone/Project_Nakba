using UnityEngine;

public class Players_Controller : MonoBehaviour
{
    TopDownMovement _mover;
    public bool _isSelected = false;

    void Awake()
    {
        _mover = GetComponent<TopDownMovement>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _isSelected)
        {
            Vector3 mouseScreenPos = Input.mousePosition;
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            Vector2 target = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
            _mover.MoveTo(target);
        }
    }
}
