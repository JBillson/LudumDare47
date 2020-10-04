using System;
using Combat;
using UnityEngine;

public class EnemyLife : MonoBehaviour, IDamageable
{
    [HideInInspector] public float Life;

    public float maxLife;
    public Action EnemyKilled;


    private void Awake()
    {
        Life = maxLife;
    }

    public void TakeDamage(float damage)
    {
        Life -= damage;

        if (Life <= 0)
        {
            Destroy(gameObject);
            EnemyKilled?.Invoke();
        }
    }
}