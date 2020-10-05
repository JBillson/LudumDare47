using PulledTogether;
using UnityEngine;

namespace Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float     speed;
        [SerializeField] private float     lifeSpan;

        private HitMarker _hitMarker;
        public void Init(HitMarker hitMarker)
        {
            _hitMarker = hitMarker;
        }
        
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

            var hasHit = Physics.Raycast(ray, out var hit, movDistance, layerMask, QueryTriggerInteraction.Collide);

            if (hasHit)
            {
                HitObject(hit);
            }
        }

        private void HitObject(RaycastHit hit)
        {
            var damageable = hit.collider.GetComponent<IDamageable>();
        
            if (damageable != null)
            {
               _hitMarker.Trigger(); 
                damageable.TakeDamage(1);
            }

            Destroy(gameObject);
        }
    }
}