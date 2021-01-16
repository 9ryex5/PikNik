using UnityEngine;
using UnityEngine.UI;

public class ItemPlayer : MonoBehaviour
{

    public static Players P;

    public Text textName;
    public Text textNumber;

    private Player player;

    public void StartThis(Player _player)
    {
        player = _player;
        Refresh();
        GetComponent<RectTransform>().sizeDelta = new Vector2(0, Screen.height * 0.1f);
    }

    public void Refresh()
    {
        textName.text = player.name;
        textNumber.text = "#" + player.number;
    }

    public void ButtonOpenPlayer()
    {
        P.OpenPlayer(this);
    }

    public Player GetPlayer()
    {
        return player;
    }
}
