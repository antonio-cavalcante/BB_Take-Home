using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputProcess : MonoBehaviour, IPlayerInputProcess
{
    public event Action OnAttack;

    private Vector2 moveInput;
    private Vector2 pointInput;
    private int groundLayerMask;

    void Start()
    {
        groundLayerMask = LayerMask.GetMask("Ground");
    }

    public void Look(InputAction.CallbackContext context)
    {
        pointInput = context.ReadValue<Vector2>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed) {
            OnAttack?.Invoke();
        }
    }

    public bool GetLookAt(ref Vector3 lookAtPoint)
    {
        Ray ray = Camera.main.ScreenPointToRay(pointInput);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayerMask))
        {
            lookAtPoint = hit.point;
            return true;
        }
        return false;
    }

    public Vector2 GetMoveInput()
    {
        return moveInput;
    }
}
