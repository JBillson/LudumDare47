using System;
using UnityEngine;

namespace Enemies.Scripts.Enemies.Base
{
    public class Enemy : MonoBehaviour
    {
        public EnemyType enemyType;
        [Header("Enemy Settings")] public int attackSpeed = 1;
        public float attackRange = 2;
        public float moveSpeed = 3.5f;
        [SerializeField] private float _maxHealth = 100f;

        private float _health;

        private void Awake()
        {
            _health = _maxHealth;
        }

        public float GetCurrentHealth()
        {
            return _health;
        }

        public float GetMaxHealth()
        {
            return _maxHealth;
        }
    }
}