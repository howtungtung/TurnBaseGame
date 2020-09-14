using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
public class CharacterControl : MonoBehaviour
{
    public bool isStatic;
    public CharacterData characterData;
    public Animator animator;
    public NavMeshAgent agent;
    public ParticleSystem hitEffect;
    private CharacterControl target;
    private Vector3 startPos;

    private void Update()
    {
        if (isStatic)
            return;
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    public virtual void Attack(CharacterControl target)
    {
        startPos = transform.position;
        this.target = target;
        transform.position = target.transform.position + target.transform.forward;
        animator.SetTrigger("Attack");
    }

    public virtual void OnAttackFramed()
    {
        if (target == null)
            return;
        target.DoDamage(characterData.atk);
    }

    public virtual void OnAttackComplete()
    {
        transform.position = startPos;
    }

    public void DoDamage(int attack)
    {
        characterData.ReduceHP(attack);
        animator.SetTrigger("Hit");
        hitEffect.Play();
        if (characterData.hp <= 0)
        {
            animator.SetTrigger("Die");
            if (!isStatic)
                agent.isStopped = true;
        }
    }
}
