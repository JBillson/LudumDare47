using System;
using UnityEngine;

public class ChangeValue : MonoBehaviour
{
    public TextMesh minute, second;
    private Clock.Clock _clock;

    private void Awake()
    {
        _clock = FindObjectOfType<Clock.Clock>();
    }


    private void Update()
    {
        var time = _clock.Time();

        int minuteValue = time.Minute;
        int secondValue = time.Second;

        if (minuteValue >= 0 && minuteValue <= 9)
        {
            minute.text = "0" + minuteValue.ToString();
        }
        else
        {
            minute.text = minuteValue.ToString();
        }

        if (secondValue >= 0 && secondValue <= 9)
        {
            second.text = "0" + secondValue.ToString();
        }
        else
        {
            second.text = secondValue.ToString();
        }
    }
}