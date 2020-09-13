using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ActionTimer : MonoBehaviour
{
    public Color normalColor;
    public Color timeUpColor;
    public float maxTime;
    public Text timerText;
    private float colorChangeTime;
    public float colorChangeThreshold = 0.3f;
    public void Setup(float timer)
    {
        maxTime = timer;
        colorChangeTime = maxTime * colorChangeThreshold;
        UpdateTimer(timer);
    }

    public void UpdateTimer(float time)
    {
        timerText.text = Mathf.CeilToInt(time).ToString("00");
        if(time <= colorChangeTime)
        {
            timerText.color = Color.Lerp(timeUpColor, normalColor, time / colorChangeTime);
        }
        else
        {
            timerText.color = normalColor;
        }
    }
}
