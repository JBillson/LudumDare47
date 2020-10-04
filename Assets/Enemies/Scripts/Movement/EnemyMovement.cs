using System;
using System.Collections;
using Enemies.Scripts.Base;
using Enemies.Scripts.Combat;
using FpsController;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Enemies.Scripts.Movement
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Enemy))]
    public class EnemyMovement : MonoBehaviour
    {
        [Header("Reposition Settings")] public float repositionRadius = 5;
        [Range(0, 1)] public float repositionHealthPercentage = 0.2f;
        public float repositionMinDistToPlayer = 2;

        [Header("References")] public Transform enemyModel;
        private NavMeshAgent _agent;
        private PlayerController _playerController;
        private Enemy _thisEnemy;
        private bool _isMoving;
        private bool _isRepositioning;
        private Transform _target;
        private IEnemyAttack _enemyAttack;

        public Action onReadyToAttack;

        public bool IsMoving()
        {
            return _isMoving;
        }

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _thisEnemy = GetComponent<Enemy>();
            _playerController = FindObjectOfType<PlayerController>();
            _enemyAttack = GetComponent<IEnemyAttack>();
        }

        private void Start()
        {
            InitEnemyMovement();
        }

        private void InitEnemyMovement()
        {
            SetTarget(_playerController.transform);
            _agent.stoppingDistance = _thisEnemy.attackRange;
            _agent.speed = _thisEnemy.moveSpeed;
        }

        private void Update()
        {
            _isMoving = !(_agent.remainingDistance <= _agent.stoppingDistance);
            // Can't move when attacking
            if (_enemyAttack.IsAttacking()) return;

            _agent.destination = _target.position;

            // enemy model to look at player while moving
            var position = _playerController.transform.position;
            enemyModel.transform.LookAt(new Vector3(position.x, 0, position.z));

            if (_isRepositioning) return;

            if (_thisEnemy.EnemyLife().Life < _thisEnemy.EnemyLife().maxLife * repositionHealthPercentage ||
                DistanceToPlayer() <= repositionMinDistToPlayer)
            {
                Reposition();
            }

            if (!_isMoving && DistanceToPlayer() < _thisEnemy.attackRange)
            {
                onReadyToAttack?.Invoke();
            }
        }

        private float DistanceToPlayer()
        {
            return Vector3.Distance(_agent.transform.position, _playerController.transform.position);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        private void Reposition()
        {
            StartCoroutine(DoReposition());
        }

        private IEnumerator DoReposition()
        {
            _isRepositioning = true;
            var newPos = Random.insideUnitSphere * repositionRadius + transform.position;
            newPos = new Vector3(newPos.x, 0, newPos.z);
            var target = new GameObject("<NavMeshTarget>");
            target.transform.position = newPos;
            _agent.stoppingDistance = 0;
            SetTarget(target.transform);
            while (_agent.remainingDistance >= _agent.stoppingDistance + 0.01f)
            {
                yield return null;
            }

            _isRepositioning = false;
            _agent.stoppingDistance = _thisEnemy.attackRange;
            SetTarget(_playerController.transform);
            Destroy(target);
        }
    }
}