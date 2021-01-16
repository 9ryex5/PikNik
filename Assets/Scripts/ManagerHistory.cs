using UnityEngine;
using UnityEngine.UI;

public class ManagerHistory : MonoBehaviour
{
    private SaveFile SF;
    private ManagerUI MUI;

    public Transform parentItemMatches;
    public GameObject prefabItemMatch;

    public Image buttonGeneral;
    public Image buttonEvents;
    public Image buttonPlayers;
    public GameObject panelGeneral;
    public GameObject panelEvents;
    public GameObject panelPlayers;
    private Image currentActiveButton;
    private GameObject currentActivePanel;

    //MATCH PROFILE
    public Animator panelMatchProfile;
    public Text textMatchTitle;
    private ItemMatch currentItemMatch;

    //GENERAL
    public GameObject buttonDelete;
    public GameObject buttonFilter;
    public GameObject buttonSort;
    public Text textDuration;
    public Text textGoalsAlly;
    public Text textGoalsEnemy;
    public Text textFouls;
    public Text textYellows;
    public Text textReds;

    //EVENTS
    public Transform parentItemEvents;
    public ItemEvent prefabItemEvent;

    //PLAYERS
    public Transform parentItemPlayers;
    public ItemPlayerHistory prefabItemPlayer;

    private void Awake()
    {
        SF = SaveFile.SF;
        MUI = ManagerUI.MUI;
        ItemMatch.H = this;
        ItemPlayerHistory.H = this;
    }

    private void Start()
    {
        currentActiveButton = buttonGeneral;
        currentActivePanel = panelGeneral;
    }

    private void OnEnable()
    {
        for (int i = 0; i < parentItemMatches.childCount; i++)
            Destroy(parentItemMatches.GetChild(i).gameObject);

        for (int i = 0; i < SF.GetAllMatches().Count; i++)
        {
            ItemMatch im = Instantiate(prefabItemMatch, parentItemMatches).GetComponent<ItemMatch>();
            im.StartThis(SF.GetMatch(i));
        }
    }

    public void OpenMatch(ItemMatch _itemMatch)
    {
        currentItemMatch = _itemMatch;
        textMatchTitle.text = _itemMatch.GetMatch().date.ToShortDateString();

        //GENERAL
        textDuration.text = _itemMatch.GetMatch().duration.Minutes.ToString("00") + ":" + _itemMatch.GetMatch().duration.Seconds.ToString("00");
        textGoalsAlly.text = _itemMatch.GetMatch().GetNGoalsAlly().ToString();
        textGoalsEnemy.text = _itemMatch.GetMatch().GetNGoalsEnemy().ToString();
        textFouls.text = _itemMatch.GetMatch().GetNFouls().ToString();
        textYellows.text = _itemMatch.GetMatch().GetNYellows().ToString();
        textReds.text = _itemMatch.GetMatch().GetNReds().ToString();

        //EVENTS
        for (int i = 0; i < parentItemEvents.childCount; i++)
            Destroy(parentItemEvents.GetChild(i).gameObject);

        for (int i = 0; i < _itemMatch.GetMatch().events.Count; i++)
        {
            ItemEvent ie = Instantiate(prefabItemEvent, parentItemEvents);
            ie.StartThis(_itemMatch.GetMatch().events[i]);
        }

        //PLAYERS
        for (int i = 0; i < parentItemPlayers.childCount; i++)
            Destroy(parentItemPlayers.GetChild(i).gameObject);

        for (int i = 0; i < _itemMatch.GetMatch().GetNPlayers(); i++)
        {
            ItemPlayerHistory ie = Instantiate(prefabItemPlayer, parentItemPlayers);
            ie.StartThis(_itemMatch.GetMatch().GetPlayer(i));
        }

        ButtonGeneral();

        MUI.GoRight(panelMatchProfile);
    }

    public void ButtonGeneral()
    {
        buttonDelete.SetActive(true);
        buttonFilter.SetActive(false);
        buttonSort.SetActive(false);
        currentActiveButton.color = new Color(1, 1, 1, 190 / 255f);
        currentActivePanel.SetActive(false);
        currentActiveButton = buttonGeneral;
        currentActivePanel = panelGeneral;
        currentActiveButton.color = new Color(1, 1, 1, 100 / 255f);
        currentActivePanel.SetActive(true);
    }

    public void ButtonConfirmDelete()
    {
        SF.RemoveMatch(currentItemMatch.GetMatch());
        Destroy(currentItemMatch.gameObject);
    }

    public void ButtonEvents()
    {
        buttonDelete.SetActive(false);
        buttonFilter.SetActive(true);
        buttonSort.SetActive(false);
        currentActiveButton.color = new Color(1, 1, 1, 190 / 255f);
        currentActivePanel.SetActive(false);
        currentActiveButton = buttonEvents;
        currentActivePanel = panelEvents;
        currentActiveButton.color = new Color(1, 1, 1, 100 / 255f);
        currentActivePanel.SetActive(true);
    }

    public void ButtonPlayers()
    {
        buttonDelete.SetActive(false);
        buttonFilter.SetActive(false);
        buttonSort.SetActive(true);
        currentActiveButton.color = new Color(1, 1, 1, 190 / 255f);
        currentActivePanel.SetActive(false);
        currentActiveButton = buttonPlayers;
        currentActivePanel = panelPlayers;
        currentActiveButton.color = new Color(1, 1, 1, 100 / 255f);
        currentActivePanel.SetActive(true);
    }
}
