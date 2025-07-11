using UnityEngine;

public class RunAnimation : MonoBehaviour
{
    [SerializeField] private float speed = 12f;
    [SerializeField] private float height = 0.2f;

    private Vector3 _startPos;

    private void Start()
    {
        _startPos = transform.localPosition;
    }

    private void Update()
    {
        var y = Mathf.Sin(Time.time * speed) * height;
        transform.localPosition = _startPos + Vector3.up * y;
    }
}