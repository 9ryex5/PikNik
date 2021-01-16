using UnityEngine;
using UnityEngine.UI;

public class ItemPlayerGame : MonoBehaviour
{
    public static ManagerGame G;

    public Text textNumber;
    public Text textName;
    public Image imageChecked;

    public Sprite spriteChecked;
    public Sprite spriteUnchecked;

    private Player player;

    private bool selected;

    public void StartThis(Player _player)
    {
        player = _player;
        textName.text = _player.name;
        textNumber.text = "#" + _player.number;
        GetComponent<RectTransform>().sizeDelta = new Vector2(0, Screen.height / 10);
    }

    public void Refresh()
    {
        selected = false;
        imageChecked.sprite = spriteUnchecked;
    }

    public void Clicked()
    {
        selected = !selected;

        imageChecked.sprite = selected ? spriteChecked : spriteUnchecked;
        G.ClickedPlayer(selected, player);
    }
}
