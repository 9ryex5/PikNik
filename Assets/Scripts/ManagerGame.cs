using System.Collections.Generic;
using UnityEngine;

public class ManagerGame : MonoBehaviour
{
    public static ManagerGame G;

    private SaveFile SF;
    private ManagerUI MUI;

    public Transform parentItemPlayers;
    public GameObject prefabItemPlayer;

    private List<ItemPlayerGame> allPlayers;
    private List<Player> plantel;

    public Animator panelPlaying;

    private void Awake()
    {
        G = this;
        SF = SaveFile.SF;
        MUI = ManagerUI.MUI;
        ItemPlayerGame.G = this;

        allPlayers = new List<ItemPlayerGame>();
        plantel = new List<Player>();
    }

    private void OnEnable()
    {
        allPlayers.Clear();
        plantel.Clear();

        for (int i = 0; i < parentItemPlayers.childCount; i++)
            Destroy(parentItemPlayers.GetChild(i).gameObject);

        for (int i = 0; i < SF.GetAllPlayers().Count; i++)
        {
            ItemPlayerGame ipg = Instantiate(prefabItemPlayer, parentItemPlayers).GetComponent<ItemPlayerGame>();
            ipg.StartThis(SF.GetPlayer(i));
            allPlayers.Add(ipg);
        }
    }

    public void ClickedPlayer(bool _select, Player _player)
    {
        if (_select)
            plantel.Add(_player);
        else
            plantel.Remove(_player);
    }

    public void ButtonStartGame()
    {
        if (plantel.Count > 0)
            MUI.GoRight(panelPlaying);
    }

    public List<Player> GetPlantel()
    {
        return plantel;
    }
}
