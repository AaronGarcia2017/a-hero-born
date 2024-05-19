using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerBehavior : MonoBehaviour
{
    public float DistanceToGround = 0.1f;
    public LayerMask GroundLayer;
    public float MoveSpeed = 10f;
    public float RotateSpeed = 75f;
    private CapsuleCollider _col;
    private float _vInput;
    private float _hInput;
    // 1
    private Rigidbody _rb;
    public float JumpVelocity = 5f;
    private bool _isJumping;
    // 2
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
    }
    void Update()
    {
        _vInput = Input.GetAxis("Vertical") * MoveSpeed;
        _hInput = Input.GetAxis("Horizontal") * RotateSpeed;
        /*
        this.transform.Translate(Vector3.forward * _vInput *
        Time.deltaTime);
        this.transform.Rotate(Vector3.up * _hInput * Time.deltaTime);
        */
        // 2
        _isJumping |= Input.GetKeyDown(KeyCode.J);
        // ... No other changes needed ...

    }
    // 1
    void FixedUpdate()
    {

        Vector3 rotation = Vector3.up * _hInput;

        Quaternion angleRot = Quaternion.Euler(rotation *
        Time.fixedDeltaTime);

        _rb.MovePosition(this.transform.position +
        this.transform.forward * _vInput * Time.fixedDeltaTime);

        _rb.MoveRotation(_rb.rotation * angleRot);


        // 3
        if (_isJumping)
        {
            // 4
            _rb.AddForce(Vector3.up * JumpVelocity, ForceMode.Impulse);
        }
        // 5
        _isJumping = false;
        // ... No other changes needed ...
        if (IsGrounded() && _isJumping)
        {
            _rb.AddForce(Vector3.up * JumpVelocity,
            ForceMode.Impulse);
        }
    }
        private bool IsGrounded()
        {
            // 7
            Vector3 capsuleBottom = new Vector3(_col.bounds.center.x,
            _col.bounds.min.y, _col.bounds.center.z);

            // 8
            bool grounded = Physics.CheckCapsule(_col.bounds.center,
            capsuleBottom, DistanceToGround, GroundLayer, QueryTriggerInteraction.Ignore);

            // 9
            return grounded;

        }
    }
    


