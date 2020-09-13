using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class LevelManager : MonoBehaviour
{
    public CinemachineVirtualCamera followPlayerCam;
    public HealthIndicator playerHealthIndicator;
    public GameObject gameOverUI;
    public GameObject victoryUI;
    private CharacterData playerData;
    public EnemyInfo[] allEnemies;
    public CharacterController player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        playerData = DataManager.Instance.playerData;
        playerData.onHPChanged += OnPlayerHPChanged;
        player.characterData = playerData;
        playerHealthIndicator.SetHP(playerData.hp);
        allEnemies = FindObjectsOfType<EnemyInfo>();
        for(int i = 0; i < allEnemies.Length; i++)
        {
            CharacterData enemyData = DataManager.Instance.GetEnemyData(allEnemies[i].id, allEnemies[i].serialID);
            allEnemies[i].CharacterController.characterData = enemyData;
            if (enemyData.hp <= 0)
            {
                allEnemies[i].gameObject.SetActive(false);
            }
        }
        player.agent.Warp(DataManager.Instance.playerLevelPosition);
       // player.transform.position =;
        CheckGameOver();
    }

    private void OnDisable()
    {
        playerData.onHPChanged -= OnPlayerHPChanged;
    }

    public void ToggleTopView()
    {
        followPlayerCam.Priority = followPlayerCam.Priority == 11 ? 9 : 11;
    }

    private void OnPlayerHPChanged()
    {
        playerHealthIndicator.SetHP(playerData.hp);
        if(playerData.hp <= 0)
        {
            StartCoroutine(EndingLevel(gameOverUI));
        }
    }

    private IEnumerator EndingLevel(GameObject showUI)
    {
        yield return new WaitForSeconds(1f);
        showUI.SetActive(true);
        yield return new WaitForSeconds(3f);
        TransitionController.Instance.DoTransition("LevelSelectScene");
    }

    private void CheckGameOver()
    {
        if(playerData.hp <= 0)
        {
            StartCoroutine(EndingLevel(gameOverUI));
            return;
        }
        for (int i = 0; i < allEnemies.Length; i++)
        {
            if (allEnemies[i].gameObject.activeInHierarchy)
                return;
        }
        StartCoroutine(EndingLevel(victoryUI));
    }
}
