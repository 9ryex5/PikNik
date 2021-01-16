using UnityEngine;
using UnityEngine.UI;

public class ItemEvent : MonoBehaviour
{
    public Image imageType;
    public Text textPlayer;
    public Text textTime;

    public Sprite halfTime;
    public Sprite join;
    public Sprite leave;
    public Sprite goalAlly;
    public Sprite goalEnemy;
    public Sprite assist;
    public Sprite foulAlly;
    public Sprite foulEnemy;
    public Sprite yellow;
    public Sprite red;

    public void StartThis(EventMatch _event)
    {
        switch (_event.type)
        {
            case EventMatch.MatchEventType.HALF_TIME:
                imageType.sprite = halfTime;
                break;
            case EventMatch.MatchEventType.JOIN:
                imageType.sprite = join;
                break;
            case EventMatch.MatchEventType.LEAVE:
                imageType.sprite = leave;
                break;
            case EventMatch.MatchEventType.GOAL_ALLY:
                imageType.sprite = goalAlly;
                break;
            case EventMatch.MatchEventType.GOAL_ENEMY:
                imageType.sprite = goalEnemy;
                break;
            case EventMatch.MatchEventType.ASSIST:
                imageType.sprite = assist;
                break;
            case EventMatch.MatchEventType.FOUL_ALLY:
                imageType.sprite = foulAlly;
                break;
            case EventMatch.MatchEventType.FOUL_ENEMY:
                imageType.sprite = foulEnemy;
                break;
            case EventMatch.MatchEventType.YELLOW:
                imageType.sprite = yellow;
                break;
            case EventMatch.MatchEventType.RED:
                imageType.sprite = red;
                break;
        }

        textTime.text = _event.time.Minutes.ToString("00") + ":" + _event.time.Seconds.ToString("00");
        textPlayer.text = _event.player == null ? string.Empty : _event.player.name;
        GetComponent<RectTransform>().sizeDelta = new Vector2(0, Screen.height * 0.1f);
    }
}
