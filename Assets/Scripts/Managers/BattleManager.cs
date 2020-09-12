using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.Playables;
using System;

public class BattleManager : MonoBehaviour
{
    public PlayableDirector startingTimeline;
    public PlayableDirector gamingTimeline;
    public HealthIndicator playerHealthIndicator;
    public HealthIndicator enemyHealthIndicator;
    public CharacterController enemy;
    public CharacterController player;
    public CharacterController[] enemys;
    public Transform spawnPos;
    public TextAsset quizFile;
    public TextAsset enemyFile;
    public Quiz[] quizzes;
    public EnemyData[] enemiesData;
    public EnemyData enemyData;
    public int turnNumber;
    public ActionTimer actionTimer;
    public TurnUI turnUI;
    public CameraShake cameraShake;
    private int playerAnswer = -1;
    public GameObject victoryUI;
    public GameObject gameOverUI;

    private void Awake()
    {
        enemy = Instantiate(enemys[0], spawnPos.position, spawnPos.rotation);
        quizzes = JsonConvert.DeserializeObject<Quiz[]>(quizFile.text);
        enemiesData = JsonConvert.DeserializeObject<EnemyData[]>(enemyFile.text);
        enemyData = enemiesData[0];
        enemy.Setup(new CharacterData()
        {
            atk = enemyData.atk,
            maxHP = enemyData.hp,
            hp = enemyData.hp,
        });
        player.Setup(new CharacterData()
        {
            atk = 1,
            maxHP = 5,
            hp = 5,
        });
    }

    private void OnEnable()
    {
        player.onHurt += OnPlayerHurt;
        enemy.onHurt += OnEnemyHurt;
    }

    private void OnDisable()
    {
        player.onHurt -= OnPlayerHurt;
        enemy.onHurt -= OnEnemyHurt;
    }

    private IEnumerator Start()
    {
        yield return Starting();
        yield return GameLoop();
        yield return Ending();
    }

    private IEnumerator Starting()
    {
        playerHealthIndicator.gameObject.SetActive(false);
        enemyHealthIndicator.gameObject.SetActive(false);
        turnUI.gameObject.SetActive(false);
        actionTimer.gameObject.SetActive(false);
        startingTimeline.Play();
        yield return new WaitUntil(() => startingTimeline.state == PlayState.Paused);
    }

    private IEnumerator GameLoop()
    {
        turnNumber = 1;
        turnUI.Setup(quizzes.Length);
        turnUI.UpdateTurn(turnNumber);
        actionTimer.Setup(enemyData.actionTime);
        actionTimer.UpdateTimer(enemyData.actionTime);
        playerHealthIndicator.SetHP(player.characterData.hp);
        enemyHealthIndicator.SetHP(enemy.characterData.hp);
        while (turnNumber <= quizzes.Length && player.IsDead == false && enemy.IsDead == false)
        {
            turnUI.UpdateTurn(turnNumber);
            gamingTimeline.Play();
            yield return new WaitUntil(() => gamingTimeline.state == PlayState.Paused);
            float timer = enemyData.actionTime;
            while (timer > 0 && playerAnswer == -1)
            {
                yield return null;
                timer -= Time.deltaTime;
                actionTimer.UpdateTimer(timer);
            }
            if (quizzes[turnNumber - 1].answer == playerAnswer)
            {
                player.Attack(enemy);
            }
            else
            {
                enemy.Attack(player);
            }
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => player.IsIdle && enemy.IsIdle);
            turnNumber++;
        }
    }

    private IEnumerator Ending()
    {
        if (player.IsDead)
        {
            gameOverUI.SetActive(true);
        }
        else if (enemy.IsDead)
        {
            victoryUI.SetActive(true);
        }
        yield return new WaitForSeconds(3f);
    }

    private void OnEnemyHurt()
    {
        enemyHealthIndicator.SetHP(enemy.characterData.hp);
        cameraShake.Shake();
    }

    private void OnPlayerHurt()
    {
        playerHealthIndicator.SetHP(player.characterData.hp);
        cameraShake.Shake();
    }

    public void TriggerEnemy(string trigger)
    {
        enemy.SetTrigger(trigger);
    }

    public void OnPlayerSelect(int index)
    {
        playerAnswer = index;
    }
}
