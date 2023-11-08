using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerCharacter : Character
{  
    private PlayerInputSystem playerInputActions;
    private PlayerInfo playerInfo;

    Vector2 animInput = Vector2.zero;
    Vector2 refVel = Vector2.zero;

    bool isInitialized = false;
    bool attack = false;

    public void Initialize()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        BaseInitialize(this);
        InitializeInputSystem();

        isInitialized = true;
    }

    public void SetPlayerInfo(PlayerInfo playerInfo)
    {
        this.playerInfo = playerInfo;
    }

    private void InitializeInputSystem()
    {
        playerInputActions = new PlayerInputSystem();
        playerInputActions.MnK.Enable();

        playerInputActions.MnK.BasicAttack.performed += BasicAttack;
    }

    private void BasicAttack(InputAction.CallbackContext context)
    {
        if (attack == true) return;

        attack = true;

        animator.SetTrigger("BasicAttack");
        ApplyDamage(gameObject, 25);
    }

    private void Attack()
    {
        BasicAttackType.Attack();
    }

    private void ResetAttack()
    {
        animator.ResetTrigger("BasicAttack");

        attack = false;
    }

    private void FixedUpdate()
    {
        if (!isInitialized) return;

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
        animator.SetFloat("xVel", animInput.y);
        animator.SetFloat("yVel", animInput.x);

        Quaternion targetRotation = Quaternion.LookRotation(cameraTransform.forward, Vector3.up);
        transform.rotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);
    }

}
