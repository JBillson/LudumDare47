using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    public Image       image;
    public TMP_Text TextMeshPro;
    
    public void CooldownCountdown(float timer, float maxTimer, float currentValue)
    {
        timer -= Time.deltaTime;

        var precentage = timer / maxTimer;
        image.fillAmount = precentage;
        
        if(currentValue <= 0 ) return;
        
        TextMeshPro.text = currentValue.ToString("0.0");
    }
}
