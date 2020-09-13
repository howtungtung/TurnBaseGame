using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
public class DataManager : MonoBehaviour
{
    public static DataManager Instance { private set; get; }
    public TextAsset enemiesSettingFile;
    public TextAsset[] quizzesSettingFile;
    public CharacterData[] enemiesData;
    public Dictionary<int, Quiz[]> levelQuizzesData = new Dictionary<int, Quiz[]>();
    public CharacterData playerData;
    public Dictionary<int, CharacterData> levelEnemiesData = new Dictionary<int, CharacterData>();
    public Dictionary<int, bool> levelItemsActive = new Dictionary<int, bool>();
    public CharacterData battleEnemyData;
    public Vector3 playerLevelPosition;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        enemiesData = JsonConvert.DeserializeObject<CharacterData[]>(enemiesSettingFile.text);
        for (int i = 0; i < quizzesSettingFile.Length; i++)
        {
            levelQuizzesData[i] = JsonConvert.DeserializeObject<Quiz[]>(quizzesSettingFile[i].text);
        }
    }

    public void SetLevelItemStatus(int serialID, bool active)
    {
        levelItemsActive[serialID] = active;
    }

    public CharacterData GetEnemyData(int id, int serialID)
    {
        if (levelEnemiesData.ContainsKey(serialID))
        {
            return levelEnemiesData[serialID];
        }
        else
        {
            CharacterData enemyDefinition = Array.Find(enemiesData, data => data.id == id);
            if (enemyDefinition == null)
                throw new UnityException($"Enemy {id} had not been defined!");
            CharacterData newEnemyData = enemyDefinition.Clone();
            levelEnemiesData[serialID] = newEnemyData;
            return newEnemyData;
        }
    }

    public bool GetLevelItemActive(int serialID)
    {
        if (levelItemsActive.ContainsKey(serialID))
        {
            return levelItemsActive[serialID];
        }
        else
        {
            levelItemsActive[serialID] = true;
            return levelItemsActive[serialID];
        }
    }

    public void TriggerBattle(CharacterData enemyData, Vector3 playerPos)
    {
        battleEnemyData = enemyData;
        playerLevelPosition = playerPos;
        TransitionController.Instance.DoTransition("BattleScene");
    }
}
