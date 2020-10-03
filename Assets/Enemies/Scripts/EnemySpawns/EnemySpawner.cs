using System;
using System.Collections.Generic;
using System.Linq;
using Dungeons;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies.Scripts.EnemySpawns
{
    [RequireComponent(typeof(Dungeon))]
    public class EnemySpawner : SerializedMonoBehaviour
    {
        [Header("Enemies & Spawns")] public Dictionary<Enemy, int> enemies = new Dictionary<Enemy, int>();
        public List<Transform> spawnPoints;

        [Header("Settings")] public int minPackSize;
        public int maxPackSize;
        [Range(0, 1)] public float percentageToSpawnNextEnemies;

        [Header("References")] public Transform enemyHolder;

        private Dungeon _dungeon;
        private List<Enemy> _enemyCollection = new List<Enemy>();
        private int _numberOfEnemiesAliveWhenSpawningNextWave;
        private bool _spawnsStarted;

        private void Awake()
        {
            _dungeon = GetComponent<Dungeon>();
        }

        public void Start()
        {
            _dungeon.onDungeonEntered += StartSpawns;
        }

        private void Update()
        {
            if (!_spawnsStarted || _enemyCollection.Count <= 0) return;
            if (enemyHolder.childCount > _numberOfEnemiesAliveWhenSpawningNextWave) return;
            SpawnNextPack();
        }

        private void StartSpawns()
        {
            // init enemy collection
            foreach (var enemy in enemies)
            {
                for (var i = 0; i < enemy.Value; i++)
                {
                    _enemyCollection.Add(enemy.Key);
                }
            }

            // Shuffle enemy collection to get variation in packs
            _enemyCollection = _enemyCollection.OrderBy(a => Guid.NewGuid()).ToList();
            _numberOfEnemiesAliveWhenSpawningNextWave = (int) (percentageToSpawnNextEnemies * _enemyCollection.Count);
            _spawnsStarted = true;
            SpawnNextPack();
        }

        private void SpawnNextPack()
        {
            var packSize = Random.Range(minPackSize, maxPackSize);
            if (packSize > _enemyCollection.Count)
                packSize = _enemyCollection.Count;

            var enemiesToSpawn = new List<Enemy>();
            for (var i = 0; i < packSize; i++)
            {
                enemiesToSpawn.Add(_enemyCollection[i]);
            }

            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            DoSpawn(enemiesToSpawn, spawnPoint);

            if (_enemyCollection.Count >= minPackSize || _enemyCollection.Count == 0) return;
            Debug.Log($"{_enemyCollection.Count} enemies remaining");
            Debug.Log("Enemies remaining < minPackSize.  Spawning remaining enemies");
            enemiesToSpawn.Clear();
            enemiesToSpawn.AddRange(_enemyCollection);
            DoSpawn(enemiesToSpawn, spawnPoint);
        }

        private void DoSpawn(List<Enemy> enemiesToSpawn, Transform spawnPoint)
        {
            if (enemiesToSpawn == null) return;
            foreach (var enemy in enemiesToSpawn)
            {
                _enemyCollection.Remove(enemy);
            }


            foreach (var enemy in enemiesToSpawn)
            {
                var point = spawnPoint.transform.position + Random.insideUnitSphere * 2;
                point = new Vector3(point.x, 0, point.z);
                Instantiate(enemy, point, Quaternion.identity, enemyHolder.transform);
            }

            Debug.Log($"{enemiesToSpawn.Count} enemies spawned");
        }
    }
}