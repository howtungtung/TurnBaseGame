using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.Playables;
using System;

public class BattleManager : MonoBehaviour
{
    public int quizLevel = 0;
    public int maxTurnNumber = 5;
    public PlayableDirector startingTimeline;
    public HealthIndicator playerHealthIndicator;
    public HealthIndicator enemyHealthIndicator;
    public CharacterControl enemy;
    public CharacterControl player;
    public Text enemyNameLabel;
    public EnemyInfo[] enemies;
    public Transform spawnPos;
    private int turnNumber;
    public ActionTimer actionTimer;
    public TurnUI turnUI;
    public CameraShake cameraShake;
    public GameObject gameOverUI;
    public Animator turnStartHintUI;
    public string backLevelScene = "Level1";
    private Quiz[] quizzes;
    public QuizUI quizUI;
    private bool waitPlayerAnswer;
    private bool isAnswerRight;
    private void OnDisable()
    {
        player.characterData.onHPReduce -= OnPlayerHurt;
        enemy.characterData.onHPReduce -= OnEnemyHurt;
    }

    private IEnumerator Start()
    {
        yield return Starting();
        yield return GameLoop();
        yield return Ending();
    }

    private IEnumerator Starting()
    {
        quizzes = DataManager.Instance.levelQuizzesData[quizLevel];
        Shuffle(quizzes);
        //setup enemy
        EnemyInfo battleEnemy = Array.Find(enemies, data => data.id == DataManager.Instance.battleEnemyData.id);
        enemy = Instantiate(battleEnemy, spawnPos.position, spawnPos.rotation).CharacterController;
        Destroy(enemy.GetComponent<LevelEnemyBehavior>());
        enemy.characterData = DataManager.Instance.battleEnemyData;
        enemy.characterData.onHPReduce += OnEnemyHurt;
        enemyNameLabel.text = enemy.characterData.name;
        //setup player
        player.characterData = DataManager.Instance.playerData;
        player.characterData.onHPReduce += OnPlayerHurt;
        startingTimeline.Play();
        yield return new WaitUntil(() => startingTimeline.state == PlayState.Paused);
    }

    private IEnumerator GameLoop()
    {
        turnNumber = 1;
        turnUI.Setup(maxTurnNumber);
        actionTimer.Setup(enemy.characterData.actionTime);
        playerHealthIndicator.SetHP(player.characterData.hp);
        enemyHealthIndicator.SetHP(enemy.characterData.hp);
        playerHealthIndicator.gameObject.SetActive(true);
        enemyHealthIndicator.gameObject.SetActive(true);
        actionTimer.gameObject.SetActive(true);
        turnUI.gameObject.SetActive(true);
        do
        {
            turnStartHintUI.SetTrigger("Play");
            turnUI.UpdateTurn(turnNumber);
            yield return new WaitForSeconds(1f);
            isAnswerRight = false;
            waitPlayerAnswer = true;
            int quizIndex = (int)Mathf.Repeat(turnNumber - 1, quizzes.Length);
            quizUI.Show(quizzes[quizIndex]);
            float timer = enemy.characterData.actionTime;
            while (timer > 0 && waitPlayerAnswer)
            {
                yield return null;
                timer -= Time.deltaTime;
                actionTimer.UpdateTimer(timer);
            }
            yield return new WaitForSeconds(1f);
            quizUI.gameObject.SetActive(false);
            if (isAnswerRight)
            {
                player.Attack(enemy);
            }
            else
            {
                enemy.Attack(player);
            }
            yield return new WaitForSeconds(2f);
            turnNumber++;
        } while (turnNumber <= maxTurnNumber && player.characterData.hp > 0 && enemy.characterData.hp > 0);
    }

    private IEnumerator Ending()
    {
        string nextScene = backLevelScene;
        if (player.characterData.hp <= 0)
        {
            gameOverUI.SetActive(true);
            nextScene = "LevelSelectScene";
        }
        yield return new WaitForSeconds(2f);
        TransitionController.Instance.DoTransition(nextScene);
    }

    private void OnEnemyHurt(int amount)
    {
        enemyHealthIndicator.SetHP(enemy.characterData.hp);
        cameraShake.Shake();
    }

    private void OnPlayerHurt(int amount)
    {
        playerHealthIndicator.SetHP(player.characterData.hp);
        cameraShake.Shake();
    }

    public void OnPlayerAnswer(bool isRight)
    {
        waitPlayerAnswer = false;
        isAnswerRight = isRight;
    }

    private void Shuffle<T>(T[] array)
    {
        var rng = new System.Random();
        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n);
            n--;
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
}
