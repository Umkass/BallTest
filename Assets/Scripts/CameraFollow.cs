using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    Vector3 offset;

    [SerializeField] float smoothSpeed = 0.1f;
    Vector3 velocity = Vector3.zero;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        SmoothFollow();
    }

    void SmoothFollow()
    {
        Vector3 targetPos = target.position + offset;
        Vector3 smoothFollow = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothSpeed);
        transform.position = smoothFollow;
    }
}
