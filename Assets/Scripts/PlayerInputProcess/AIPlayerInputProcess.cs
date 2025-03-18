using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class AIPlayerInputProcess : MonoBehaviour, IPlayerInputProcess
{
    public event Action OnAttack;

    private AIBlackboard blackboard;

    public bool GetLookAt(ref Vector3 lookAtPoint)
    {
        if (blackboard.IsPlayerVisible())
        {
            lookAtPoint = blackboard.Player.transform.position;
            return true;
        }
            
        return false;
    }

    public Vector2 GetMoveInput()
    {
        if (blackboard.IsPlayerVisible())
        {
            Vector3 direction = blackboard.Player.transform.position - transform.position;
            return new Vector2(direction.x, direction.z).normalized;
        }
        
        return Vector2.zero;
    }

    void Start()
    {
        blackboard = AIBlackboard.FindAIBlackboard();
    }

    private void Attack()
    {
        OnAttack?.Invoke();
    }
}
