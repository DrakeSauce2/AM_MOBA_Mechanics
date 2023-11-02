using System;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    public MovementComponent _MoveComponent { get; private set; }

    [Header("Stats")]
    [SerializeField] private Stats characterStats;
    private Stats stats;

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

    public void ApplyDamage(GameObject instigator, int damage)
    {
        stats.TrySetStatValue(Stat.HEALTH, damage);

        if (stats.TryGetStatValue(Stat.HEALTH) <= 0)
        {
            // Start Death
            // GameManager.Instance.Kill(gameObject, instigator)
        }
    }

    public Stats GetStats() => stats;

}
