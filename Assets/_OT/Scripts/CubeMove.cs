using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class CubeMove : MonoBehaviour
{
    [SerializeField] private Rigidbody _cubeRB;

    [SerializeField] Transform _cameraTransform;

    [SerializeField] float _moveForce = 10.0f;

    private float _jumpForce = 10.0f;

    private Vector3 _moveDirection;


    private float _horizontal;
    private float _vertical;


    private void Awake()
    {
        if (_cubeRB == null)
        {
            _cubeRB = GetComponent<Rigidbody>();
        }


        _cameraTransform = Camera.main.transform;
    }


    private void Update()
    {
         _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        _moveDirection = new Vector3(_horizontal, 0f, _vertical);
    }
    private void FixedUpdate()
    {
        Movement();

    }

    private void Movement()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _cubeRB.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);

        _cubeRB.AddForce(_moveDirection * _moveForce, ForceMode.Force);
    }
}
