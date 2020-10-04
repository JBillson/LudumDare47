using System.Collections;
using Enemies.Scripts.Enemies.Base;
using Enemies.Scripts.Enemies.Movement;
using UnityEngine;

namespace Enemies.Scripts.Enemies.Combat
{
    [RequireComponent(typeof(EnemyMovement))]
    public class RangedAttack : MonoBehaviour
    {
        [Header("Prefabs")] public GameObject bulletPrefab;
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
            Shoot();
            var seconds = 2 / (attackSpeedMultiplier * _thisEnemy.attackSpeed);
            yield return new WaitForSeconds(seconds);
            _attackCoroutine = null;
        }

        private void Shoot()
        {
            Instantiate(bulletPrefab, firePoint.transform.position, firePoint.rotation);
        }
    }
}