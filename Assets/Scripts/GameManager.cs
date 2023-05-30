using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] AudioSource gameMusic;

    [SerializeField] GameObject WinUI;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform playerStart;

    [Header("Events")]

    [SerializeField] EventRouter startGameEvent;
    [SerializeField] EventRouter stopGameEvent;
    [SerializeField] EventRouter winGameEvent;

    int score = 0;

    public enum State
    {
        TITLE,
        START_GAME,
        PLAY_GAME,
        GAME_OVER,
        GAME_WIN
    }

    State state = State.TITLE;
    float statetimer = 0;

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
                Instantiate(playerPrefab, playerStart);
                state = State.PLAY_GAME;
                startGameEvent.Notify();
                gameMusic.Play();
                break;
            case State.PLAY_GAME:
                break;
            case State.GAME_OVER:
                statetimer -= Time.deltaTime;
                if (statetimer <= 0)
                {
                    UIManager.Instance.ShowGameOver(false);
                    DestroyImmediate(playerPrefab);
                    state = State.TITLE;
                }
                break;
            case State.GAME_WIN:
                statetimer -= Time.deltaTime;
                if (statetimer <= 0)
                {
                    WinUI.SetActive(false);
                    state = State.TITLE;
                }
                break;
            default:
                break;
        }
    }

    public void Start()
    {
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
        WinUI.SetActive(true);
        Debug.Log("Win!!!");
        statetimer = 3;

    }

	public void AddPoints(int points)
    {
		score += points;
		UIManager.Instance.SetScore(score);
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
