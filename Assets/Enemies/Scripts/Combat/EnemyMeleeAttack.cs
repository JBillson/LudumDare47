using System.Collections;
using DG.Tweening;
using Enemies.Scripts.Base;
using Enemies.Scripts.Movement;
using UnityEngine;

namespace Enemies.Scripts.Combat
{
    [RequireComponent(typeof(EnemyMovement))]
    public class EnemyMeleeAttack : MonoBehaviour, IEnemyAttack
    {
        [Header("Melee Attack Settings")] public float attackSpeedMultiplier = 1;

        [Header("Debug Settings")] public Material debugAttackMaterial;

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
            StartCoroutine(SlashAttack());
            var seconds = 2 / (attackSpeedMultiplier * _thisEnemy.attackSpeed);
            yield return new WaitForSeconds(seconds);
            _attackCoroutine = null;
        }

        private IEnumerator SlashAttack()
        {
            _isAttacking = true;
            debugAttackMaterial.color = Color.red;
            // transform.DOLocalMoveZ(1, .1f);
            yield return new WaitForSeconds(0.2f);
            debugAttackMaterial.color = Color.white;
            _isAttacking = false;
        }
    }
}