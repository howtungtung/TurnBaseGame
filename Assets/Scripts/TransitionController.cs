using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{
    public static TransitionController Instance { private set; get; }
    private Animator animator;
    private void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
    }

    public void DoTransition(string nextScene)
    {
        StartCoroutine(Processing(nextScene));
    }
    
    private IEnumerator Processing(string nextScene)
    {
        animator.SetTrigger("Show");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nextScene);
        animator.SetTrigger("Hide");
    }
}
