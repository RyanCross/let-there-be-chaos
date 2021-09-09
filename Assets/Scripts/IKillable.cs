using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A  killable entity is something that can 1. Take Damage and 2. Die
public interface IKillable
{
    void TakeDamage(int damage);
    void Die();
}