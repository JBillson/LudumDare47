using System;
using System.Collections;
using Enemies.Scripts.Enemies.Base;
using FpsController;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Enemies.Scripts.Enemies.Movement
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Enemy))]
    public class EnemyMovement : MonoBehaviour
    {
        [Header("Reposition Settings")] public float repositionRadius = 5;
        [Range(0, 1)] public float repositionHealthPercentage = 0.2f;
        public float repositionMinDistToPlayer = 2;
        private NavMeshAgent _agent;
        private PlayerController _playerController;
        private Enemy _thisEnemy;
        private bool _isMoving;
        private bool _isRepositioning;
        private Transform _target;

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
            _agent.destination = _target.position;
            transform.LookAt(_playerController.transform);

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

        [Button]
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