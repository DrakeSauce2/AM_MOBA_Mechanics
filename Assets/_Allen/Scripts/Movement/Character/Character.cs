using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable, IHealth
{
    public MovementComponent _MoveComponent { get; private set; }

    [Header("Stats")]
    [SerializeField] private Stats characterStats;
    private Stats stats;

    public float health { get; set; }

    protected void Initialize(Character character)
    {
        _MoveComponent = new MovementComponent(character);

        stats = Instantiate(characterStats);
        stats.Initialize();
    }

    public void ApplyDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            // Start Death
        }
    }

    public Stats GetStats() => stats;

}
