using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPlayerPlaying : MonoBehaviour
{

    public static ManagerPlaying P;

    private Image myImage;

    public Text textName;
    public Text textNumber;
    public Text textTimerPoint;
    public Text textTimerGame;
    public GameObject imageGoals;
    public Text textGoals;
    public GameObject imageYellow;
    public GameObject imageRed;

    private Player player;
    private List<EventMatch> events;

    private float timerPoint;
    private float timerGame;
    private bool playing;
    private int goals;
    private bool yellow;
    private bool red;

    private float timePlayedFirst;
    private float timePlayedSecond;

    public void StartThis(Player _player)
    {
        myImage = GetComponent<Image>();
        player = _player;
        textName.text = _player.name;
        textNumber.text = "#" + _player.number;
        GetComponent<RectTransform>().sizeDelta = new Vector2(0, Screen.height * 0.1f);

        events = new List<EventMatch>();
    }

    public void Clicked()
    {
        if (red)
            return;

        P.ClickedPlayer(this, playing);
    }

    public void Refresh(float _timePassed)
    {
        timerPoint += _timePassed;
        timerGame += _timePassed;
        TimeSpan timeSpanPoint = TimeSpan.FromSeconds(timerPoint);
        textTimerPoint.text = timeSpanPoint.Minutes.ToString("00") + ":" + timeSpanPoint.Seconds.ToString("00");
        TimeSpan timeSpanGame = TimeSpan.FromSeconds(timerGame);
        textTimerGame.text = timeSpanGame.Minutes.ToString("00") + ":" + timeSpanGame.Seconds.ToString("00");
    }

    public Player GetPlayer()
    {
        return player;
    }

    public float GetPlayedTimeFirst()
    {
        return timePlayedFirst;
    }

    public float GetPlayedTimeSecond()
    {
        return timePlayedSecond;
    }

    public List<EventMatch> GetEvents()
    {
        return events;
    }

    public void AddEvent(EventMatch _event)
    {
        events.Add(_event);
    }

    public void Joined()
    {
        playing = true;
        myImage.color = new Color(0.8f, 1, 0.8f, 190f / 255);
    }

    public void Left()
    {
        playing = false;
        timerPoint = 0;
        textTimerPoint.text = "00:00";
        myImage.color = new Color(1, 1, 0.8f, 190f / 255);
    }

    public void Goal()
    {
        goals++;
        textGoals.text = goals.ToString();
        imageGoals.SetActive(true);
    }

    public bool GetYellow()
    {
        return yellow;
    }

    public void Yellow()
    {
        if (yellow)
        {
            Red();
            return;
        }

        yellow = true;
        imageYellow.SetActive(true);
    }

    public void Red()
    {
        red = true;
        imageYellow.SetActive(false);
        imageRed.SetActive(true);
        myImage.color = new Color(1, 0.8f, 0.8f, 190f / 255);
    }

    public void HalfTime()
    {
        timePlayedFirst = timerGame;
        timerGame = 0;
        timerPoint = 0;
    }

    public void EndGame()
    {
        timePlayedSecond = timerGame;
    }
}
