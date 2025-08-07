using UnityEngine;

public class MovementCamera : MonoBehaviour
{
    [SerializeField] private Transform targetPosition;
    [SerializeField] private Vector3 offset; 
    public float SmoothCam;
    private Vector3 velocity = Vector3.zero;


    private void LateUpdate()
    {
        Vector3 TargetPos = targetPosition.position + offset;
        TargetPos.z = transform.position.z; 
        transform.position = Vector3.SmoothDamp(transform.position, TargetPos, ref velocity, SmoothCam);
    }
}
