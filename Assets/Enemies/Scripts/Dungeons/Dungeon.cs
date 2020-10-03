using System;
using UnityEngine;

namespace Enemies.Scripts.Dungeons
{
    public class Dungeon : MonoBehaviour
    {
        public Action onDungeonEntered;
        public Action onDungeonCompleted;

        private void Start()
        {
            onDungeonEntered?.Invoke();
        }

        public void DungeonCompleted()
        {
            onDungeonCompleted?.Invoke();
            Debug.Log("Dungeon Completed");
        }
    }
}