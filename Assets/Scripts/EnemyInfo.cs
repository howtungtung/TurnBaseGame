using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    public int id;
    public int serialID;
    public CharacterControl CharacterController { private set; get; }

    private void Awake()
    {
        CharacterController = GetComponent<CharacterControl>();
    }
}