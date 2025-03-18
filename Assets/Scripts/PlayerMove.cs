using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private UnityEvent<GameObject> onDeath;

    [Tooltip("Player states that can be applied to the player;\nFirst state is the inital state")]
    [SerializeField] private PlayerStateMachine playerStates;

    public Action<PlayerStateScriptableObject> OnStateChange { get; set; }

    private IPlayerInputProcess playerInput;
    private Gun gun;
    private Health health;


    void Start()
    {
        Assert.Greater(playerStates.GetStateCount() , 0, "Player states must have at least one state");

        playerInput = GetComponent<IPlayerInputProcess>();
        gun = GetComponent<Gun>();
        health = GetComponent<Health>();
        health.OnDeath += Die;

        playerStates.OnStateChange += ApplyState;
        playerStates.ChangeState(playerStates.GetCurrentState().name);
    }

    private void Die()
    {
        playerRigidbody.constraints = RigidbodyConstraints.None;
        onDeath?.Invoke(this.gameObject);
    }

    private void ApplyState(PlayerState playerState)
    {
        health.ScaleMaxHealth(playerState.playerStateScriptableObject.MaxHealth);
        gun.SetDamage(playerState.playerStateScriptableObject.AttackDamage);
        moveSpeed = playerState.playerStateScriptableObject.MovementSpeed;
        playerRigidbody.mass = playerState.playerStateScriptableObject.Mass;
        transform.localScale = playerState.playerStateScriptableObject.Size * Vector3.one;

        if (playerState.playerStateScriptableObject.StealthMode)
        {
            playerRigidbody.excludeLayers = LayerMask.GetMask("Enemy");
        } else {
            playerRigidbody.excludeLayers = 0;
        }

        if (playerState.playerStateScriptableObject.CanAttack)
        {
            playerInput.OnAttack -= Attack;
            playerInput.OnAttack += Attack;
        } else {
            playerInput.OnAttack -= Attack;
        }


        OnStateChange?.Invoke(playerState.playerStateScriptableObject);
    }

    void Attack()
    {
        if (playerStates.GetCurrentState().playerStateScriptableObject.RadialAttack)
        {
            gun.FireCone(3, 15);
            return;
        } else {
            gun.Fire();
        }
    }

    void FixedUpdate()
    {
        if (health.isDead) return;

        Vector2 moveInput = playerInput.GetMoveInput();
        playerRigidbody.position += moveSpeed * Time.fixedDeltaTime * new Vector3(moveInput.x, 0, moveInput.y);

        Vector3 targetPosition = Vector3.zero;
        if (playerInput.GetLookAt(ref targetPosition)) {
            targetPosition.y = playerRigidbody.position.y; // Keep character looking at same height
            playerRigidbody.transform.LookAt(targetPosition);
        }
    }

    void Update()
    {
        // Key 1 to change state
        if(Keyboard.current[Key.Digit1].wasPressedThisFrame)
        {
            playerStates.ChangeState(0);
        }        
        // Key 2 to change state
        if(Keyboard.current[Key.Digit2].wasPressedThisFrame)
        {
            playerStates.ChangeState(1);
        }        
        // Key 3 to change state
        if(Keyboard.current[Key.Digit3].wasPressedThisFrame)
        {
            playerStates.ChangeState(2);
        }
    }

    public void ChangeState(int stateIndex)
    {
        playerStates.ChangeState(stateIndex);
    }

    public int GetStateCount()
    {
        return playerStates.GetStateCount();
    }

    public string GetStateName(int i)
    {
        return playerStates.GetStateName(i);
    }

    public bool IsStealth()
    {
        return playerRigidbody.excludeLayers != 0;
    }
}
