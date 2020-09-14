using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelEnemyBehavior : MonoBehaviour
{
    private CharacterControl characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterControl>();
    }

    public void OnMouseEnter()
    {
        EnemyDetailUI.Instance.Show(characterController.characterData, transform.position);
    }

    public void OnMouseExit()
    {
        EnemyDetailUI.Instance.Hide();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<NavMeshAgent>().isStopped = true;
            var playerPos = other.transform.position;
            playerPos.x = Mathf.RoundToInt(playerPos.x);
            playerPos.z = Mathf.RoundToInt(playerPos.z);
            DataManager.Instance.TriggerBattle(characterController.characterData, playerPos);
        }
    }
}
