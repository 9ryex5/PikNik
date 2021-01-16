using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemPlayerHistory : MonoBehaviour
{
    public static ManagerHistory H;

    public Text textNumber;
    public Text textName;
    public Text textDataShown;

    private DataPlayerMatch player;

    public void StartThis(DataPlayerMatch _player)
    {
        player = _player;

        textNumber.text = "#" + _player.GetPlayer().number;
        textName.text = _player.GetPlayer().name;
        ShowTimePlayed();
        GetComponent<RectTransform>().sizeDelta = new Vector2(0, Screen.height * 0.1f);
    }

    public void ShowTimePlayed()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(player.GetTimePlayed());
        textDataShown.text = timeSpan.Minutes.ToString("00") + ":" + timeSpan.Seconds.ToString("00");
    }

    public DataPlayerMatch GetPlayer()
    {
        return player;
    }
}
