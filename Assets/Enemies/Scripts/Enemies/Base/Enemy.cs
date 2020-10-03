using UnityEngine;

namespace Enemies.Scripts.Enemies.Base
{
    public class Enemy : MonoBehaviour
    {
        public EnemyType enemyType;
        [Header("Enemy Settings")] public int attackSpeed = 1;
        public float attackRange = 2;
        public float moveSpeed = 3.5f;

        private EnemyLife _enemyLife;

        private void Awake()
        {
            _enemyLife = GetComponent<EnemyLife>();
        }

        public EnemyLife EnemyLife()
        {
            return _enemyLife;
        }
    }
}