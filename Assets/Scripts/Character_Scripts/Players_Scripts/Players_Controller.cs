using UnityEngine;

public class Players_Controller : MonoBehaviour
{
    TopDownMovement _mover;

    void Start()
    {
        _mover = GetComponent<TopDownMovement>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = 10f; 
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            Vector2 target = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
            _mover.MoveTo(target);
        }
    }
}
