using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int attack;
    public Transform target;
    public float speed = 1;
    private Rigidbody rigid;
    public ParticleSystem explosionEffect;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
   
    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.LookAt(target);
        rigid.velocity = direction * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<CharacterController>().DoDamage(attack);
            explosionEffect.transform.position = other.transform.position;
            explosionEffect.Play();
            gameObject.SetActive(false);
        }
    }
}
