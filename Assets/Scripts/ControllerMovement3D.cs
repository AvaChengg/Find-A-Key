using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMovement3D : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _turnSpeed = 10f;

    [Header("SFX")]
    [SerializeField] private AudioSource _footstepLeft;
    [SerializeField] private AudioSource _footstepRight;
    
    private float _speed = 0f;
    private bool _hasMoveInput;
    private Vector3 _moveInput;
    private Vector3 _lookDirection;


    private GameObject _mainCamera;
    private CharacterController _characterController;
    private Animator _animator;

    private void Start()
    {
        _mainCamera = GameObject.Find("Main Camera");
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    public void SetMoveInput(Vector3 input)
    {
        // check if the player press the key or not
        _hasMoveInput = input.magnitude > 0.1f;
        _moveInput = _hasMoveInput ? input : Vector3.zero;
        
    }

    public void SetLookDirection(Vector3 direction)
    {
        // rotate the player
        _lookDirection = new Vector3(direction.x, 0f, direction.z).normalized;
    }


    private void FixedUpdate()
    {
        _speed = 0;
        float targetRotation = 0f;

        if (_moveInput.magnitude < 0.1f)
        {
            _moveInput = Vector3.zero;
            _animator.SetFloat("Speed", 0);
            return;
        }

        // move character
        if (_moveInput != Vector3.zero)
        {
            _speed = _moveSpeed;
        }

        targetRotation = Quaternion.LookRotation(_lookDirection).eulerAngles.y + _mainCamera.transform.rotation.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, targetRotation, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _turnSpeed * Time.fixedDeltaTime); // smooth the rotation

        _moveInput = rotation * Vector3.forward;
        _characterController.Move(_moveInput * _speed * Time.fixedDeltaTime);
        _animator.SetFloat("Speed", _moveInput.magnitude);
    }

    public void PlayLeftSFX()
    {
        _footstepLeft.Play();
    }
    public void PlayRightSFX()
    {
        _footstepRight.Play();
    }
}
