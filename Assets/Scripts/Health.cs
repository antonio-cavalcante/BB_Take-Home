using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    float health = 100f;

    internal Action OnDeath;
    internal Action<float> OnHealthChange;

    internal bool isDead = false;

    void Start()
    {
        Reset();
    }

/// <summary>
/// Changes max health and scales current health accordingly
/// </summary>
/// <param name="newMaxHealth">new Maximum health</param>
    public void ScaleMaxHealth(float newMaxHealth)
    {
        health *= newMaxHealth / maxHealth;
        maxHealth = newMaxHealth;
    }

    public void Reset()
    {
        isDead = false;
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        OnHealthChange?.Invoke(health);

        if (health <= 0 && !isDead)
        {
            health = 0;
            isDead = true;
            OnDeath?.Invoke();
        }
    }
}
