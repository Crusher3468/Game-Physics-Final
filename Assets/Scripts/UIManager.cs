using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    //[SerializeField] Slider healthMeter;
    //[SerializeField] Slider shieldMeter;
    [SerializeField] TMP_Text ballCount;
    [SerializeField] TMP_Text scoreUI;
    [SerializeField] TMP_Text goalUI;
    [SerializeField] GameObject WinUI;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject TitleUI;

    public void ShowTitle(bool show)
    {
        TitleUI.SetActive(show);
    }

    public void ShowGameOver(bool show)
    {
        gameOverUI.SetActive(show);
    }

    public void ShowWinUI(bool show)
    {
        WinUI.SetActive(show);
    }

    public void SetHealth(int health)
    {
        //healthMeter.value = Mathf.Clamp(health, 0, 100);
    }

    public void SetShield(int shield)
    {
        //shieldMeter.value = Mathf.Clamp(shield, 0, 100);
    }

    public void SetScore(int score)
    {
        scoreUI.text = score.ToString();
    }

    public void SetGoal(int goal)
    {
        goalUI.text = goal.ToString();
    }
    public void SetBallCount(int balls)
    {
        ballCount.text = balls.ToString();
    }

}
