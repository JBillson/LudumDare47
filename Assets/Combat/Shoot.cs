using System;
using PulledTogether;
using UnityEngine;
using UnityEngine.Serialization;

namespace Combat
{
    public class Shoot : MonoBehaviour
    {
        [SerializeField] private Transform   firePoint;
        [SerializeField] private Projectile  projectile;
        [SerializeField] private HitMarker   _hitMarker;
        [SerializeField] private AudioSource AudioSource;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                var proj = Instantiate(projectile, firePoint.position, firePoint.rotation);
                proj.Init(_hitMarker);
                AudioSource.Play();
            }
        }
    }
}