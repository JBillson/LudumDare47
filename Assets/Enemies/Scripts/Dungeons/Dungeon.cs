using System;
using UnityEngine;

namespace Dungeons
{
    public class Dungeon : MonoBehaviour
    {
        public Action onDungeonEntered;

        private void Start()
        {
            onDungeonEntered?.Invoke();
        }
    }
}