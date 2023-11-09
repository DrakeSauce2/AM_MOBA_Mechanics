using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Character : MonoBehaviour, IDamageable
{
    public MovementComponent _MoveComponent { get; private set; }

    [Header("Stats")]
    [SerializeField] private Stats characterStats;
    private Stats stats;

    [Header("Attack Type")]
    [SerializeField] private BasicAttack basicAttack;
    public BasicAttack BasicAttackType { get { return basicAttack; } }

    protected Animator animator;
    protected Transform cameraTransform;

    protected void BaseInitialize(Character character)
    {
        _MoveComponent = new MovementComponent(character);

        animator = GetComponent<Animator>();
        cameraTransform = Camera.main.transform;
 
        stats = Instantiate(characterStats);
        stats.Initialize();

        stats.TrySetStatValue(Stat.HEALTH, stats.TryGetStatValue(Stat.MAXHEALTH));
    }

    public void SetBasicAttack(GameObject ownerAttackObject)
    {
        basicAttack.Init(ownerAttackObject);

    }

    public void ApplyDamage(GameObject instigator, float damage)
    {
        float currentHealth = stats.TryGetStatValue(Stat.HEALTH);
        stats.TrySetStatValue(Stat.HEALTH, currentHealth - damage);

        if (currentHealth <= 0)
        {
            Debug.Log($"Character Is Dead");
            // Start Death
            // GameManager.Instance.Kill(gameObject, instigator)
        }
    }

    public Stats GetStats() => stats;

}
