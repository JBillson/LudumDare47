using System;
using Combat;
using UnityEngine;

public class EnemyLife : MonoBehaviour, IDamageable
{
    public float  Life;
    public Action EnemyKilled;

    public void TakeDamage(float damage)
    {
        Life -= damage;

        if (Life <= 0)
        {
            EnemyKilled?.Invoke();
            Destroy(gameObject);
        }
    }
}