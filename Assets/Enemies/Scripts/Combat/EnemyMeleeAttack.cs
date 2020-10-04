using System.Collections;
using Enemies.Scripts.Base;
using Enemies.Scripts.Movement;
using UnityEngine;

namespace Enemies.Scripts.Combat
{
    [RequireComponent(typeof(EnemyMovement))]
    public class EnemyMeleeAttack : MonoBehaviour
    {
        [Header("Ranged Attack Settings")] public float attackSpeedMultiplier = 1;

        [Header("References")] public Transform firePoint;
        private EnemyMovement _enemyMovement;
        private Enemy _thisEnemy;
        private Coroutine _attackCoroutine;

        private void Awake()
        {
            _enemyMovement = GetComponent<EnemyMovement>();
            _thisEnemy = GetComponent<Enemy>();
        }

        private void Start()
        {
            _enemyMovement.onReadyToAttack += Attack;
        }

        private void Attack()
        {
            if (_attackCoroutine != null) return;
            _attackCoroutine = StartCoroutine(DoAttack());
        }

        private IEnumerator DoAttack()
        {
            SlashAttack();
            var seconds = 2 / (attackSpeedMultiplier * _thisEnemy.attackSpeed);
            yield return new WaitForSeconds(seconds);
            _attackCoroutine = null;
        }

        private void SlashAttack()
        {
        }
    }
}