using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [Space, SerializeField] private float laneOffset = 2f;
    [SerializeField] private float sideMoveSpeed = 10f;
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;

    private int _currentLane;
    private float _speed;

    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosition;

    public Action OnCoinCollected { get; set; }
    public Action OnDeath { get; set; }

    public void StartGame()
    {
        _speed = forwardSpeed;
        rb.isKinematic = false;
    }

    private void Start()
    {
        _speed = 0;
        rb.isKinematic = true;
    }

    private void Update()
    {
        HandleInput();
        MoveToLane();
    }

    private void FixedUpdate()
    {
        if (rb.isKinematic)
            return;

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, _speed);
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
        if (Mathf.Abs(rb.linearVelocity.y) < 0.01f)
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            OnCoinCollected?.Invoke();
        }
        else if (other.CompareTag("Obstacle"))
        {
            OnDeath?.Invoke();
            Start();
        }
    }
}