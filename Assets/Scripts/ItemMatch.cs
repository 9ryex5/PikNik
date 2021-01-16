using UnityEngine;
using UnityEngine.UI;

public class ItemMatch : MonoBehaviour
{

    public static ManagerHistory H;

    public Text textDate;
    public Text textDuration;
    public Text textScore;

    private DataMatch match;

    public void StartThis(DataMatch _match)
    {
        match = _match;
        textDate.text = _match.date.Day + "/" + _match.date.Month + "/" + _match.date.ToString("yy");
        textDuration.text = _match.duration.Minutes.ToString("00") + ":" + _match.duration.Seconds.ToString("00");
        textScore.text = _match.GetNGoalsAlly() + " - " + _match.GetNGoalsEnemy();
        GetComponent<RectTransform>().sizeDelta = new Vector2(0, Screen.height / 10);
    }

    public void ButtonOpenMatch()
    {
        H.OpenMatch(this);
    }

    public DataMatch GetMatch()
    {
        return match;
    }
}
