using UnityEngine;
using UnityEngine.UI;

public class ItemEventPlaying : MonoBehaviour
{
    public Image imageEvent;
    public Text textPlayer;

    public Sprite[] spritesEvents;

    public void StartThis(int _event, ItemPlayerPlaying _player)
    {
        imageEvent.sprite = spritesEvents[_event];
        textPlayer.text = _player.GetPlayer().name;
    }

    public void Clicked()
    {

    }
}
