using System.Collections;
using Enemies.Scripts.Base;
using Enemies.Scripts.Movement;
using UnityEngine;

namespace Enemies.Scripts.Combat
{
    [RequireComponent(typeof(EnemyMovement))]
    public class EnemyRangedAttack : MonoBehaviour, IEnemyAttack
    {
        [Header("Prefabs")] public GameObject bulletPrefab;
        [Header("Ranged Attack Settings")] public float attackSpeedMultiplier = 1;

        [Header("References")] public Transform firePoint;
        private EnemyMovement _enemyMovement;
        private Enemy _thisEnemy;
        private Coroutine _attackCoroutine;
        private bool _isAttacking;


        private void Awake()
        {
            _enemyMovement = GetComponent<EnemyMovement>();
            _thisEnemy = GetComponent<Enemy>();
        }

        private void Start()
        {
            _enemyMovement.onReadyToAttack += Attack;
        }

        public void Attack()
        {
            if (_attackCoroutine != null) return;
            _attackCoroutine = StartCoroutine(DoAttack());
        }

        public bool IsAttacking()
        {
            return _isAttacking;
        }

        private IEnumerator DoAttack()
        {
            StartCoroutine(Shoot());
            var seconds = 2 / (attackSpeedMultiplier * _thisEnemy.attackSpeed);
            yield return new WaitForSeconds(seconds);
            _attackCoroutine = null;
        }

        private IEnumerator Shoot()
        {
            _isAttacking = true;
            Instantiate(bulletPrefab, firePoint.transform.position, firePoint.rotation);
            // wait for 0.2 seconds - gonna be a dotween animation here
            yield return new WaitForSeconds(0.2f);
            _isAttacking = false;
        }
    }
}