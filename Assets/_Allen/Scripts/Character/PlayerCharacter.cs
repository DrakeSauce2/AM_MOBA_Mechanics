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

    [SerializeField] private PassiveAbility passiveAbility;
    [SerializeField] private List<ActiveAbility> activeAbilities = new List<ActiveAbility>(4);

    bool isInitialized = false;
    bool attack = false;

    public void Initialize()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        BaseInitialize(this);
        InitializeInputSystem();

        SetBasicAttack(gameObject);

        for (int i = 0; i < activeAbilities.Count; i++)
        {
            activeAbilities[i].Init(playerInfo._HUDbar.AbilitySlotList[i]);
        }

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

        playerInputActions.MnK.Ability1.performed += StartCastAbility_1;
        playerInputActions.MnK.Ability2.performed += StartCastAbility_2;
        playerInputActions.MnK.Ability3.performed += StartCastAbility_3;
        playerInputActions.MnK.Ability4.performed += StartCastAbility_4;

        playerInputActions.MnK.BasicAttack.performed += Look;
    }

    #region Abilities

    private void StartCastAbility_1(InputAction.CallbackContext context)
    {
        activeAbilities[0].UseAbility();
    }

    private void StartCastAbility_2(InputAction.CallbackContext context)
    {
        activeAbilities[1].UseAbility();
    }

    private void StartCastAbility_3(InputAction.CallbackContext context)
    {
        activeAbilities[2].UseAbility();
    }

    private void StartCastAbility_4(InputAction.CallbackContext context)
    {
        activeAbilities[3].UseAbility();
    }

    #endregion

    private void Look(InputAction.CallbackContext context)
    {
        Vector2 lookInput = playerInputActions.MnK.Look.ReadValue<Vector2>();
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
