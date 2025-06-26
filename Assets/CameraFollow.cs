using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float offset = 10;
    void LateUpdate()
    {
        transform.position = new Vector3 (target.position.x, target.position.y, target.position.z - offset);   
    }
}
