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

    [Header("Shop Component")]
    [SerializeField] private ShopComponent shopComponent;

    private AbilityComponent abilityComponent;

    public InventoryComponent inventoryComponent { get; private set; }

    private StatsPerSecond statsPerSecond;

    Vector2 animInput = Vector2.zero;
    Vector2 refVel = Vector2.zero;
    bool isInitialized = false;
    bool attack = false;


    public void Initialize()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        inventoryComponent = gameObject.AddComponent<InventoryComponent>();

        BaseInitialize(this);
        InitializeInputSystem();

        SetBasicAttack(gameObject);
        inventoryComponent.Init(GetStats());

        statsPerSecond = GetComponent<StatsPerSecond>();
        statsPerSecond.Init(GetStats());

        abilityComponent = gameObject.GetComponent<AbilityComponent>();
        abilityComponent.Init(this.gameObject);

        isInitialized = true;
    }

    public void InitializePlayerInfo(PlayerInfo playerInfo)
    {
        this.playerInfo = playerInfo;

        

        playerInfo._HUDbar.SetCharacterIcon(CharacterSpriteIcon);
    }

    private void InitializeInputSystem()
    {
        playerInputActions = new PlayerInputSystem();
        playerInputActions.MnK.Enable();

        playerInputActions.MnK.BasicAttack.performed += BasicAttack;

        playerInputActions.MnK.StopAbility.performed += CancelAbility;
        playerInputActions.MnK.Ability1.performed += StartCastAbility_1;
        playerInputActions.MnK.Ability2.performed += StartCastAbility_2;
        playerInputActions.MnK.Ability3.performed += StartCastAbility_3;
        playerInputActions.MnK.Ability4.performed += StartCastAbility_4;

        playerInputActions.MnK.Shop.performed += Shop;

        playerInputActions.MnK.BasicAttack.performed += Look;
    }

    private void Shop(InputAction.CallbackContext context)
    {
        shopComponent.ToggleShop();
    }

    #region Abilities

    private void CancelAbility(InputAction.CallbackContext context)
    {
        foreach (ActiveAbility ability in abilityComponent.ActiveAbilities)
        {
            ability.StopCast();
        }
    }

    private void StartCastAbility_1(InputAction.CallbackContext context)
    {
        Debug.Log(abilityComponent);
        Debug.Log(abilityComponent.ActiveAbilities.Count);
        abilityComponent.TryUseAbility(0);
        animator.SetTrigger($"Ability1");
    }

    private void StartCastAbility_2(InputAction.CallbackContext context)
    {
        abilityComponent.TryUseAbility(1);
        animator.SetTrigger($"Ability2");
    }

    private void StartCastAbility_3(InputAction.CallbackContext context)
    {
        abilityComponent.TryUseAbility(2);
        animator.SetTrigger($"Ability3");
    }

    private void StartCastAbility_4(InputAction.CallbackContext context)
    {
        abilityComponent.TryUseAbility(3);
        animator.SetTrigger($"Ability4");
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
    }

    private void Attack()
    {
        BasicAttackType.Attack(GetStats().TryGetStatValue(Stat.BASICATTACKDAMAGE) + 
                              (GetStats().TryGetStatValue(Stat.PHYSICAL_POWER)    * GetStats().TryGetStatValue(Stat.PHYSICAL_POWER_SCALE)) + 
                              (GetStats().TryGetStatValue(Stat.MAGICAL_POWER)     * GetStats().TryGetStatValue(Stat.MAGICAL_POWER_SCALE)));
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
