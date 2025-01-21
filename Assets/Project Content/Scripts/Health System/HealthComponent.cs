using UnityEngine;
using System;

public class HealthComponent : MonoBehaviour, IHealth
{
    [SerializeField] 
    private float maxHealth = 100f;

    private float currentHealth;

    public float Health => currentHealth;
    public float MaxHealth => maxHealth;

    public event Action OnDie;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public void SetHealth(float value)
    {
        currentHealth = Mathf.Clamp(value, 0, maxHealth);
        if (currentHealth == 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        OnDie?.Invoke();
        Debug.Log($"{gameObject.name} has died.");
    }
}