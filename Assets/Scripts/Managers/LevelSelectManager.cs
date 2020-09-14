using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    private void Start()
    {
        DataManager.Instance.ResetAllData();
    }

    public void OnLevelSelect(int level)
    {
        TransitionController.Instance.DoTransition("Level" + level);
    }
}
