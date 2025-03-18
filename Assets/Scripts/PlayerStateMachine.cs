using System;
using UnityEngine;

[System.Serializable]
public struct PlayerState
{
    public string name;
    public PlayerStateScriptableObject playerStateScriptableObject;
}


[System.Serializable]
public struct PlayerStateMachine
{
    [SerializeField] private PlayerState[] playerStates;

    private int currentStateIndex;

    public Action<PlayerState> OnStateChange { get; set; }

    public void Initialize(PlayerState[] playerStates)
    {
        this.playerStates = playerStates;
        currentStateIndex = 0;
    }

    public void ChangeState(string stateName)
    {
        for (int i = 0; i < playerStates.Length; i++)
        {
            if (playerStates[i].name == stateName)
            {
                currentStateIndex = i;
                ChangeState(i);
                break;
            }
        }
    }

    public void ChangeState(int stateIndex)
    {
        if (stateIndex >= 0 && stateIndex < playerStates.Length)
        {
            currentStateIndex = stateIndex;
            OnStateChange?.Invoke(playerStates[currentStateIndex]);
        }
    }

    public int GetCurrentStateIndex()
    {
        return currentStateIndex;
    }

    public int GetStateCount()
    {
        return playerStates.Length;
    }

    public PlayerState GetCurrentState()
    {
        return playerStates[currentStateIndex];
    }

    public string GetStateName(int index)
    {
        return playerStates[index].name;
    }
}
