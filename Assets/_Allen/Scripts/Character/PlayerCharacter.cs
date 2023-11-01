using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerCharacter : Character
{
    UIManager _UIManager;
    PlayerInputSystem playerInputActions;

    private ValueGauge healthGauge;
    private ValueGauge manaGauge;
    private ValueGauge expGauge;

    [SerializeField] private float rotationSpeed;

    private void Awake()
    {
        Initialize(this);

        _UIManager = UIManager.Instance;

        InitializeMovementComponents();
        InitializeValueGauges();
    }

    private void InitializeValueGauges()
    {
        healthGauge = Instantiate(_UIManager.ValueBarPrefab, _UIManager.PlayerValueGaugesTransform).GetComponent<ValueGauge>();
        manaGauge = Instantiate(_UIManager.ValueBarPrefab, _UIManager.PlayerValueGaugesTransform).GetComponent<ValueGauge>();
        expGauge = Instantiate(_UIManager.ValueBarPrefab, _UIManager.PlayerValueGaugesTransform).GetComponent<ValueGauge>();

        healthGauge.Initialize(GetStats().TryGetStatValue(Stat.MAXHEALTH), Color.green);
        manaGauge.Initialize(GetStats().TryGetStatValue(Stat.MAXMANA), Color.blue);
        expGauge.Initialize(GetStats().TryGetStatValue(Stat.LEVELUP_COST), Color.yellow);
    }

    private void InitializeMovementComponents()
    {
        playerInputActions = new PlayerInputSystem();
        playerInputActions.MnK.Enable();
    }

    Vector2 animInput = Vector2.zero;
    Vector2 refVel = Vector2.zero;
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 moveInput = playerInputActions.MnK.WASD.ReadValue<Vector2>();
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0;

        _MoveComponent.Move(move);

        animInput = Vector2.SmoothDamp(animInput, moveInput, ref refVel, 0.2f);

        animator.SetFloat("xVel", animInput.x);
        animator.SetFloat("yVel", animInput.y);

        Quaternion targetRotation = Quaternion.LookRotation(cameraTransform.forward, Vector3.up);

        transform.rotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);

    }

}
