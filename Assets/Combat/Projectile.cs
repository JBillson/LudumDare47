using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float     speed;
        [SerializeField] private float     lifeSpan;

        private void Update()
        {
            lifeSpan -= Time.deltaTime;

            if (lifeSpan <= 0)
                Destroy(gameObject);

            var movDistance = speed * Time.deltaTime;
            CheckCollisions(movDistance);
            transform.Translate(Vector3.forward * movDistance);
        }

        private void CheckCollisions(float movDistance)
        {
            var ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            var hasHit = Physics.Raycast(ray, out hit, movDistance, layerMask, QueryTriggerInteraction.Collide);

            if (hasHit)
            {
                HitObject(hit);
            }
        }

        private void HitObject(RaycastHit hit)
        {
            Debug.Log(hit.collider.name);
            Destroy(gameObject);
        }
    }
}