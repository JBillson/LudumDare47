using System;
using Enemies.Scripts.Spawns;
using UnityEngine;

namespace Dungeons.Scripts
{
    public class Dungeon : MonoBehaviour
    {
        public GameObject dungeonDoor;
        private EnemySpawner _enemySpawner;
        public Action onDungeonEntered;
        public Action onDungeonCompleted;

        private void Awake()
        {
            _enemySpawner = GetComponent<EnemySpawner>();
        }

        private void Start()
        {
            _enemySpawner.onFinalEnemyKilled += DungeonCompleted;
            onDungeonEntered?.Invoke();
        }

        private void DungeonCompleted()
        {
            onDungeonCompleted?.Invoke();
            Debug.Log("Dungeon Completed");
            Destroy(dungeonDoor);
        }
    }
}