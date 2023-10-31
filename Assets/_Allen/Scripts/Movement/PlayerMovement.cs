using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;
    PlayerInputSystem playerInputActions;
    MovementComponent _MoveComponent;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    private float currentSpeed;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        playerInputActions = new PlayerInputSystem();
        playerInputActions.MnK.Enable();

        _MoveComponent = gameObject.AddComponent<MovementComponent>();

        currentSpeed = moveSpeed;
    }

    private void FixedUpdate()
    {
        Vector2 moveInput = playerInputActions.MnK.WASD.ReadValue<Vector2>();
        _MoveComponent.Move(new Vector3(moveInput.x, 0, moveInput.y), currentSpeed);
    }
}
