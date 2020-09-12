using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
public class DataManager : MonoBehaviour
{
    public TextAsset enemiesSettingFile;
    public TextAsset[] quizzesSettingFile;
    public EnemyData[] enemiesData;
    public Dictionary<int, Quiz[]> levelQuizzesData = new Dictionary<int, Quiz[]>();
    public CharacterData playerData;
    public Dictionary<int, CharacterData> levelEnemiesData = new Dictionary<int, CharacterData>();

    public static DataManager Instance { private set; get; }
    private void Awake()
    {
        Instance = this;
        enemiesData = JsonConvert.DeserializeObject<EnemyData[]>(enemiesSettingFile.text);
        for (int i = 0; i < quizzesSettingFile.Length; i++)
        {
            levelQuizzesData[i] = JsonConvert.DeserializeObject<Quiz[]>(quizzesSettingFile[i].text);
        }
    }

    public void GetEnemyData(int id, int instanceID)
    {

    }
}
