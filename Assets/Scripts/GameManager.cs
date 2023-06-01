using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] AudioSource gameMusic;
    [SerializeField] GameObject playerPrefab;
    GameObject spawn;

    [Header("Events")]

    [SerializeField] EventRouter startGameEvent;
    [SerializeField] EventRouter stopGameEvent;
    [SerializeField] EventRouter winGameEvent;

    [SerializeField] int score = 0;
    [SerializeField] AudioClip impact;
    AudioSource hit;

    public enum State
    {
        TITLE,
        START_GAME,
        START_LEVEL,
        PLAY_GAME,
        GAME_OVER,
        GAME_WIN
    }

    State state = State.TITLE;
    float statetimer = 0;
    float gametimer = 0;
    int goal;
    int level;
    public static int edge;
    public static int balls;


    private void Update()
    {
        switch (state)
        {
            case State.TITLE:
                UIManager.Instance.ShowTitle(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case State.START_GAME:
                score = 0;
                UIManager.Instance.ShowTitle(false);
                Cursor.lockState = CursorLockMode.Locked;
                level = 1;
                //Instantiate(playerPrefab, playerStart);
                state = State.START_LEVEL;
                startGameEvent.Notify();
                gameMusic.Play();
                break;
            case State.START_LEVEL:
                switch (level)
                {
                    case 1:
                        SceneManager.LoadScene(level -1);
                        balls = 6;
                        edge = 4;
                        score = 0;
                        goal = 1000;
                        break;
                    case 2:
                        SceneManager.LoadScene(level-1);
                        balls = 10;
                        edge = 8;
                        score = 0;
                        goal = 2000;
                        break;
                    case 3:
                        SceneManager.LoadScene(level - 1);
                        balls = 13;
                        edge = 8;
                        score = 0;
                        goal = 3000;
                        break;
                    default:
                        break;
                }
                UIManager.Instance.SetScore(score);
                UIManager.Instance.SetGoal(goal);
                UIManager.Instance.SetBallCount(balls);
                state = State.PLAY_GAME;
                break;
            case State.PLAY_GAME:
                UIManager.Instance.SetBallCount(balls);
                gametimer -= Time.deltaTime;

                if (gametimer < 0)
                {
                    if (balls > 0)
                    {
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            spawn = GameObject.Find("PlayerSpawn");
                            Instantiate(playerPrefab, spawn.transform.position, Quaternion.identity);
                            balls--;
                            UIManager.Instance.SetBallCount(balls);
                            gametimer = 1;
                        }
                    }
                }
                break;
            case State.GAME_OVER:
                statetimer -= Time.deltaTime;
                gameMusic.Stop();
                if (statetimer <= 0)
                {
                    UIManager.Instance.ShowGameOver(false);
                    //DestroyImmediate(playerPrefab);
                    state = State.TITLE;
                }
                break;
            case State.GAME_WIN:
                statetimer -= Time.deltaTime;
                gameMusic.Stop();
                if (statetimer <= 0)
                {
                    UIManager.Instance.ShowWinUI(false);
                    state = State.TITLE;
                }
                break;
            default:
                break;
        }
    }

    public void Start()
    {
        hit = GetComponent<AudioSource>();
        winGameEvent.onEvent += SetGameWin;
    }

    public void SetGameOver()
    {
        UIManager.Instance.ShowGameOver(true);
        state = State.GAME_OVER;
        statetimer = 3;
        stopGameEvent.Notify();
    }

    public void OnStartGame()
    {
        state = State.START_GAME;
    }

    public void SetGameWin()
    {
        UIManager.Instance.ShowWinUI(true);
        state = State.GAME_WIN;
        statetimer = 3;
        //winGameEvent.Notify();
    }

    public void SetNextLevel()
    {
        if (level != 3)
        {
            level++;
            state = State.START_LEVEL;
        }
        else 
        {
            score = 0;
            SetGameWin();
        }

    }

    public void AddPoints(int points)
    {
        score += points;
        UIManager.Instance.SetScore(score);

        if (score >= goal)
        {
            GameManager.Instance.SetNextLevel();
        }
        else if (balls == 0 && score < goal)
        {
            GameManager.Instance.SetGameOver();
        }

    }

    public void Sound()
    {
        hit.PlayOneShot(impact);
    }    

	//public void OnDamage()
	//{
	//	UIManager.Instance.SetHealth((int)GetComponent<Health>().health);
	//}

	//public void OnHeal()
	//{
	//	UIManager.Instance.SetHealth((int)GetComponent<Health>().health);
	//}

	//public void OnShieldDamage()
	//{
	//	UIManager.Instance.SetShield((int)GetComponent<Shield>().shield);
	//}

	//public void OnShieldHeal()
	//{
	//	UIManager.Instance.SetShield((int)GetComponent<Shield>().shield);
	//}

	public void OnDeath()
	{
		GameManager.Instance.SetGameOver();
	}
}
