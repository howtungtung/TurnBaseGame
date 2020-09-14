using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int damage = 1;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var characterController = other.GetComponent<CharacterControl>();
            characterController.DoDamage(damage);
        }
    }
}
