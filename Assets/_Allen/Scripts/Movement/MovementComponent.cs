using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementComponent : MonoBehaviour
{
    CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public void Move(Vector3 movementDirection, float targetSpeed)
    {
        controller.Move(movementDirection * targetSpeed * Time.fixedDeltaTime);
    }

}
