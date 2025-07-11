using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [Space, SerializeField] private float laneOffset = 2f;
    [SerializeField] private float sideMoveSpeed = 10f;
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;

    private int _currentLane;
    private bool _isGrounded = true;

    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosition;

    private void Start()
    {
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        HandleInput();
        MoveToLane();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, forwardSpeed);
    }

    private void HandleInput()
    {
        if (Input.touchCount != 1)
            return;

        var touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            _startTouchPosition = touch.position;
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            _endTouchPosition = touch.position;
            var delta = _endTouchPosition - _startTouchPosition;

            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                if (delta.x > 50) MoveRight();
                else if (delta.x < -50) MoveLeft();
            }
            else
            {
                if (delta.y > 50) Jump();
            }
        }
    }

    private void MoveLeft()
    {
        if (_currentLane > -1)
            _currentLane--;
    }

    private void MoveRight()
    {
        if (_currentLane < 1)
            _currentLane++;
    }

    private void MoveToLane()
    {
        var targetPosition = new Vector3(_currentLane * laneOffset, transform.position.y, transform.position.z);
        var moveDirection = Vector3.MoveTowards(transform.position, targetPosition, sideMoveSpeed * Time.deltaTime);
        transform.position = new Vector3(moveDirection.x, moveDirection.y, transform.position.z);
    }

    private void Jump()
    {
        if (!_isGrounded)
            return;

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        _isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
            _isGrounded = true;
    }
}