using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Teleport : MonoBehaviour
{
    public Teleport exit;
    private bool disableTeleport;

    public void OnTriggerEnter(Collider other)
    {
        if (disableTeleport)
            return;
        if (other.CompareTag("Player"))
        {
            exit.disableTeleport = true;
            other.GetComponent<NavMeshAgent>().Warp(exit.transform.position);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            disableTeleport = false;
        }
    }
}
