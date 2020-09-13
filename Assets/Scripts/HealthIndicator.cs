using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIndicator : MonoBehaviour
{
    public GameObject[] hpPoints;


    public void SetHP(int hp)
    {
        for (int i = 0; i < hpPoints.Length; i++)
        {
            if (i < hp)
                hpPoints[i].SetActive(true);
            else
                hpPoints[i].SetActive(false);
        }
    }
}
