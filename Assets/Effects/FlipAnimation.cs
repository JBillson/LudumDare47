using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace PulledTogether
{
    public class FlipAnimation : MonoBehaviour
    {
        public bool IsAnimating { get; set; }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
                Animate(transform);
        }
        
        public async void Animate(Transform transform)
        {
            if (IsAnimating) return;
            
            IsAnimating = true;

            var localEulerAngles = transform.localEulerAngles;

            transform.DOLocalRotate(
                new Vector3(360 * -1, localEulerAngles.y, localEulerAngles.z), .4f,
                RotateMode.LocalAxisAdd).SetEase(Ease.OutSine).WaitForCompletion();
            await Task.Delay(TimeSpan.FromSeconds(.4f));

            IsAnimating = false;
        }
    }
}