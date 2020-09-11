using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour
{
    public bool IsDead
    {
        private set;
        get;
    }
    public CharacterData characterData;
    public Transform frontPos;
    public Animator animator;
    public ParticleSystem hitEffect;
    private CharacterController target;
    private Vector3 startPos;

    public event UnityAction onHurt;


    public void Setup(CharacterData data)
    {
        characterData = data;
        startPos = transform.position;
    }

    public void Attack(CharacterController target)
    {
        this.target = target;
        transform.position = target.frontPos.position;
        SetTrigger("Attack");
    }

    public void OnAttackFramed()
    {
        if (target == null)
            return;
        target.DoDamage(characterData.atk);
    }

    public void OnAttackComplete()
    {
        transform.position = startPos;
    }

    public void DoDamage(int attack)
    {
        characterData.hp -= attack;
        SetTrigger("Hit");
        hitEffect.Play();
        onHurt?.Invoke();
        if (characterData.hp <= 0)
        {
            IsDead = true;
            SetTrigger("Die");
        }
    }

    public void SetTrigger(string trigger)
    {
        animator.SetTrigger(trigger);
    }
}
