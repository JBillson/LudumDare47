using System;
using UnityEngine;

namespace Clock
{
    public class Clock : MonoBehaviour
    {
        const float DegreesPerHour = 30f, DegreesPerMinute = 6f, DegreesPerSecond = 6f;
        public Transform hoursTransform, minutesTransform, secondsTransform;
        public bool continuous;
        private DateTime _time;

        private void Awake()
        {
            _time = new DateTime(2020, 10, 20, 0, 10, 0);
            Debug.Log(_time);
            InvokeRepeating(nameof(UpdateClock), 0, 1);
        }

        public DateTime Time()
        {
            return _time;
        }

        private void UpdateClock()
        {
            if (_time.Second < 1)
            {
                var x = _time.AddMinutes(-1);
                var y = x.AddSeconds(59);
                _time = y;
            }
            else
            {
                var x = _time.AddSeconds(-1);
                _time = x;
            }

            if (continuous)
            {
                hoursTransform.localRotation =
                    Quaternion.Euler(0f, (float) _time.TimeOfDay.TotalHours * DegreesPerHour, 0f);
                minutesTransform.localRotation =
                    Quaternion.Euler(0f, (float) _time.TimeOfDay.TotalMinutes * DegreesPerMinute, 0f);
                secondsTransform.localRotation =
                    Quaternion.Euler(0f, (float) _time.TimeOfDay.TotalSeconds * DegreesPerSecond, 0f);
            }
            else
            {
                hoursTransform.localRotation =
                    Quaternion.Euler(0f, _time.Hour * DegreesPerHour,
                        0f); // Rotation storage. Multiply hour by 30 to get correct rotation around center
                minutesTransform.localRotation = Quaternion.Euler(0f, _time.Minute * DegreesPerMinute, 0f);
                secondsTransform.localRotation = Quaternion.Euler(0f, _time.Second * DegreesPerSecond, 0f);
            }
        }
    }
}