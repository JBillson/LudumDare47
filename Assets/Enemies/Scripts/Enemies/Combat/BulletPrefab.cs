using System;
using UnityEngine;

namespace Enemies.Scripts.Enemies.Combat
{
    [RequireComponent(typeof(Rigidbody))]
    public class BulletPrefab : MonoBehaviour
    {
        public float bulletLifetime = 2f;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _rb.AddForce(new Vector3(0, 0, 2000));
            Invoke(nameof(DestroyBullet), bulletLifetime);
        }

        private void DestroyBullet()
        {
            Destroy(gameObject);
        }
    }
}