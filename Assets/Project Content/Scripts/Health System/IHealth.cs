using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IHealth
{
    float Health { get; }
    float MaxHealth { get; }

    
    void Heal(float amount);

    void TakeDamage(float amount);
    
    void SetHealth(float value);
    
    void Die();
}