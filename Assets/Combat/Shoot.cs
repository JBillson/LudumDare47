using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Combat
{
    public class Shoot : MonoBehaviour
    {
        [SerializeField] private Transform  firePoint;
        [SerializeField] private Projectile projectile;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                Instantiate(projectile, firePoint.position, firePoint.rotation);
        }
    }
}