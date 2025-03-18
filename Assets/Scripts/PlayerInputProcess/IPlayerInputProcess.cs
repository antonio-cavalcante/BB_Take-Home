using System;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IPlayerInputProcess
{
    public bool GetLookAt(ref Vector3 lookAtPoint);
    public Vector2 GetMoveInput();
    
    //Event to be triggered when the player attacks
    public event Action OnAttack;
}

