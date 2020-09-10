using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class LevelStartDirector : MonoBehaviour
{
    public Transform[] allEnemys;
    public int focusIndex;
    public CinemachineVirtualCamera cam;
    public void FocusNext()
    {
        cam.Follow = allEnemys[focusIndex];
        focusIndex++;
    }
}
