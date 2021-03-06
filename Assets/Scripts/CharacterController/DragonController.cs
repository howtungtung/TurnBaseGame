﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : CharacterControl
{
    public Bullet bullect;
    public Transform firePos;
  
    public override void Attack(CharacterControl target)
    {
        bullect.transform.position = firePos.position;
        bullect.attack = characterData.atk;
        bullect.target = target.transform;
        animator.SetTrigger("Attack");
    }

    public override void OnAttackFramed()
    {
        bullect.gameObject.SetActive(true);
    }
}
