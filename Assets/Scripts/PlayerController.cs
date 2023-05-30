using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private ControllerMovement3D _controllerMovement;
    private Vector3 _moveInput;

    private void Awake()
    {
        _controllerMovement = GetComponent<ControllerMovement3D>();
    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        _moveInput = new Vector3(input.x, 0f, input.y);
    }

    private void Update()
    {
        if (_controllerMovement == null) return;

        _controllerMovement.SetMoveInput(_moveInput);
        _controllerMovement.SetLookDirection(_moveInput);
    }
}
