using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementComponent
{
    public Character Owner { get; private set; }
    CharacterController controller;

    public MovementComponent(Character owner)
    {
        Owner = owner;

        controller = Owner.gameObject.AddComponent<CharacterController>();
    }

    // To execute move caller must own a stats object and must contain a MOVEMENT_SPEED stat.
    public Vector3 Move(Vector3 movementDirection) 
    {
        Vector3 move = movementDirection * Owner.GetStats().TryGetStatValue(Stat.MOVEMENT_SPEED) * Time.fixedDeltaTime;
        controller.Move(move);
        return move;
    }
}
