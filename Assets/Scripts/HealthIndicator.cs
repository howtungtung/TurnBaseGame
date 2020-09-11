using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIndicator : MonoBehaviour
{
    public GameObject[] hpPoints;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetHP(int hp)
    {
        for (int i = 0; i < hpPoints.Length; i++)
        {
            if (i <= hp)
                hpPoints[i].SetActive(true);
            else
                hpPoints[i].SetActive(false);
        }
    }
}
