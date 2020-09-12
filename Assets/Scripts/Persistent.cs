using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Persistent : MonoBehaviour
{
    public string firstScene;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(firstScene);
    }
}
