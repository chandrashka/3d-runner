using UnityEngine;

[RequireComponent(typeof(Transform))]
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new(0, 1, -2);
    [SerializeField] private float followSpeed = 5f;

    private void LateUpdate()
    {
        var desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
    }
}