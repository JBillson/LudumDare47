using System;
using UnityEngine;

namespace PulledTogether
{
    public class HitMarker : MonoBehaviour
    {
        public AudioSource AudioSource;
        
        private void Awake()
        {
           Invoke(nameof(Disable), 0.05f); 
        }

        public void Trigger()
        {
            AudioSource.Play();
            gameObject.SetActive(true);
           Invoke(nameof(Disable), 0.1f); 
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}