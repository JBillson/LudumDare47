using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    public Image       image;
    public TMP_Text TextMeshPro;
    
    public void CooldownCountdown(float timer, float maxTimer)
    {
        timer -= Time.deltaTime;

        var precentage = timer / maxTimer;
        image.fillAmount = precentage;
        var tmpVal = timer;
        if (tmpVal <= 0)
            tmpVal = 0;
        
        TextMeshPro.text = tmpVal.ToString("0.0");
    }
}
