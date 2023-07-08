using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _playerRigidbody;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    private bool _isGrounded;
    private Transform _cameraTransform;
    private float _mouseSensitivity = 2f;
    private float _rotationX = 0f;
    public bool Immobilized;

    void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _cameraTransform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Start()
    {
        Immobilized = true;
    }

    void Update()
    {
        if (Immobilized)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(x, 0f, y) * _speed * Time.deltaTime;
            _playerRigidbody.MovePosition(transform.position + movement);

            if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            {
                _isGrounded = false;
                _playerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            }
        }
    }

    private void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        _rotationX -= mouseY;
        _rotationX = Mathf.Clamp(_rotationX, -70f, 90f);

        _cameraTransform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        _isGrounded = true;
    }
    
}