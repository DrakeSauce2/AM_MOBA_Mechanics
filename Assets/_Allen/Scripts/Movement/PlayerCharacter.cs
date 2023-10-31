using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerCharacter : Character
{
    PlayerInput playerInput;
    PlayerInputSystem playerInputActions; 

    private void Awake()
    {
        Initialize(this);

        InitializeMovementComponents();
    }

    private void InitializeMovementComponents()
    {
        playerInput = GetComponent<PlayerInput>();

        playerInputActions = new PlayerInputSystem();
        playerInputActions.MnK.Enable();
    }

    private void FixedUpdate()
    {
        Vector2 moveInput = playerInputActions.MnK.WASD.ReadValue<Vector2>();

        if (_MoveComponent == null) return;

        _MoveComponent.Move(new Vector3(moveInput.x, 0, moveInput.y));
    }
}
