using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerPlaying : MonoBehaviour
{

    private SaveFile SF;
    private ManagerGame G;

    //PLAYING UI
    public Transform parentItemPlayers;
    public GameObject prefabItemPlayer;
    public Transform prefabSpacer;
    private Transform spacer;
    public Transform parentItemEvents;
    public GameObject prefabItemEvent;
    public GameObject layoutMenu;
    public GameObject buttonHalfTime;
    public Text textTimer;
    public GameObject textSecondHalf;
    public Image imagePlayPause;
    public Text textTimeOfDay;
    public Sprite spritePlay;
    public Sprite spritePause;

    public GameObject panelNoPlayers;

    //SCOREBOARD
    public Text textScore;
    public Text textFouls;

    private List<ItemPlayerPlaying> allPlayers;
    private List<ItemPlayerPlaying> active;
    private List<EventMatch> events;
    private TimeSpan timeFirstHalf;
    private float timer;
    private int scoreAlly;
    private int scoreEnemy;
    private int currentFoulsAlly;
    private int currentFoulsEnemy;
    private bool running;
    private int reds;
    public GameObject[] selectionsEvents;
    private int selectedEvent;

    private void Awake()
    {
        SF = SaveFile.SF;
        G = ManagerGame.G;
        ItemPlayerPlaying.P = this;
    }

    private void OnEnable()
    {
        imagePlayPause.sprite = spritePlay;
        running = false;
        timer = 0;
        textTimer.text = "00:00";
        textSecondHalf.SetActive(false);
        buttonHalfTime.SetActive(true);
        scoreAlly = 0;
        scoreEnemy = 0;
        currentFoulsAlly = 0;
        currentFoulsEnemy = 0;
        reds = 0;
        selectedEvent = -1;

        UpdateScoreboard();

        layoutMenu.SetActive(false);

        for (int i = 0; i < parentItemPlayers.childCount; i++)
            Destroy(parentItemPlayers.GetChild(i).gameObject);

        allPlayers = new List<ItemPlayerPlaying>();
        active = new List<ItemPlayerPlaying>();
        events = new List<EventMatch>();

        List<Player> plantel = G.GetPlantel();

        for (int i = 0; i < plantel.Count; i++)
        {
            ItemPlayerPlaying ipp = Instantiate(prefabItemPlayer, parentItemPlayers).GetComponent<ItemPlayerPlaying>();
            ipp.StartThis(plantel[i]);
            allPlayers.Add(ipp);
        }

        spacer = Instantiate(prefabSpacer, parentItemPlayers);
    }

    private void Update()
    {
        if (running)
        {
            timer += Time.deltaTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(timer);
            textTimer.text = timeSpan.Minutes.ToString("00") + ":" + timeSpan.Seconds.ToString("00");

            for (int i = 0; i < active.Count; i++)
                active[i].Refresh(Time.deltaTime);
        }

        textTimeOfDay.text = DateTime.Now.ToShortTimeString();
    }

    public void ButtonSave()
    {
        if (events.Count == 0)
            return;

        for (int i = 0; i < events.Count; i++)
        {
            switch (events[i].type)
            {
                case EventMatch.MatchEventType.GOAL_ALLY:
                    events[i].player.goals++;
                    break;
                case EventMatch.MatchEventType.ASSIST:
                    events[i].player.assists++;
                    break;
                case EventMatch.MatchEventType.FOUL_ALLY:
                    events[i].player.fouls++;
                    break;
                case EventMatch.MatchEventType.YELLOW:
                    events[i].player.yellows++;
                    break;
                case EventMatch.MatchEventType.RED:
                    events[i].player.reds++;
                    break;
            }
        }

        DataPlayerMatch[] playersMatch = new DataPlayerMatch[allPlayers.Count];

        for (int i = 0; i < allPlayers.Count; i++)
            playersMatch[i] = new DataPlayerMatch(allPlayers[i]);

        SF.AddMatch(new DataMatch(DateTime.Now, timeFirstHalf + TimeSpan.FromSeconds(timer), playersMatch, events));
        SF.SavePlayersData();
    }

    public void ButtonHalfTime()
    {
        currentFoulsAlly = 0;
        currentFoulsEnemy = 0;

        EventMatch em;

        em.player = null;
        em.team = null;
        em.type = EventMatch.MatchEventType.HALF_TIME;
        em.time = TimeSpan.FromSeconds(timer);

        events.Add(em);

        for (int i = 0; i < allPlayers.Count; i++)
            allPlayers[i].HalfTime();

        if (running) ButtonPlayPause();

        timeFirstHalf = TimeSpan.FromSeconds(timer);
        timer = 0;
        textSecondHalf.SetActive(true);
        buttonHalfTime.SetActive(false);

        UpdateScoreboard();
    }

    public void ButtonSelectEvent(int _event)
    {
        if (selectedEvent > -1)
            selectionsEvents[selectedEvent].SetActive(false);

        if (selectedEvent == _event)
            selectedEvent = -1;
        else
        {
            selectedEvent = _event;
            selectionsEvents[selectedEvent].SetActive(true);
        }
    }

    public void ButtonGoalEnemy()
    {
        EventMatch em;

        em.player = null;
        em.team = ToTeam(active);
        em.type = EventMatch.MatchEventType.GOAL_ENEMY;
        em.time = TimeSpan.FromSeconds(timer);

        events.Add(em);

        scoreEnemy++;

        UpdateScoreboard();
    }

    public void ButtonFoulEnemy()
    {
        EventMatch em;

        em.player = null;
        em.team = null;
        em.type = EventMatch.MatchEventType.FOUL_ENEMY;
        em.time = TimeSpan.FromSeconds(timer);

        events.Add(em);

        currentFoulsEnemy++;

        UpdateScoreboard();
    }

    private void UpdateScoreboard()
    {
        textScore.text = scoreAlly + " - " + scoreEnemy;
        textFouls.text = currentFoulsAlly + " - " + currentFoulsEnemy;
    }

    public void ButtonPlayPause()
    {
        if (!running && active.Count == 0)
        {
            panelNoPlayers.SetActive(true);
            return;
        }

        running = !running;
        imagePlayPause.sprite = running ? spritePause : spritePlay;
    }

    public void ClickedPlayer(ItemPlayerPlaying _player, bool _playing)
    {
        if (selectedEvent > -1 && !_playing)
            return;

        switch (selectedEvent)
        {
            case -1:
                if (_playing)
                    PlayerLeave(_player);
                else
                    PlayerJoin(_player);

                spacer.SetSiblingIndex(active.Count);
                break;
            case 0:
                ButtonGoal(_player);
                break;
            case 1:
                ButtonAssist(_player);
                break;
            case 3:
                ButtonFoul(_player);
                break;
            case 4:
                ButtonFoulEnemy();
                break;
            case 5:
                ButtonYellow(_player);
                break;
            case 6:
                ButtonRed(_player);
                break;
        }

        AddEvent(_player, _playing);
    }

    private void AddEvent(ItemPlayerPlaying _player, bool _playing)
    {
        int _event = -1;

        if (selectedEvent == -1)
        {
            if (!_playing) _event = 0;
            if (_playing) _event = 1;
        }
        else
            _event = selectedEvent + 2;

        ItemEventPlaying iep = Instantiate(prefabItemEvent, parentItemEvents).GetComponent<ItemEventPlaying>();
        iep.transform.SetAsFirstSibling();
        iep.StartThis(_event, _player);
    }

    private void PlayerJoin(ItemPlayerPlaying _player)
    {
        EventMatch em;

        em.player = _player.GetPlayer();
        em.team = null;
        em.type = EventMatch.MatchEventType.JOIN;
        em.time = TimeSpan.FromSeconds(timer);

        events.Add(em);
        _player.AddEvent(em);

        active.Add(_player);
        _player.transform.SetSiblingIndex(active.Count - 1);
        _player.Joined();
    }

    private void PlayerLeave(ItemPlayerPlaying _player)
    {
        EventMatch em;

        em.player = _player.GetPlayer();
        em.team = null;
        em.type = EventMatch.MatchEventType.LEAVE;
        em.time = TimeSpan.FromSeconds(timer);

        events.Add(em);
        _player.AddEvent(em);

        active.Remove(_player);
        _player.transform.SetSiblingIndex(allPlayers.Count - (reds + 1));
        _player.Left();
    }

    private void ButtonGoal(ItemPlayerPlaying _player)
    {
        EventMatch em;

        em.player = _player.GetPlayer();
        em.team = ToTeam(active);
        em.type = EventMatch.MatchEventType.GOAL_ALLY;
        em.time = TimeSpan.FromSeconds(timer);

        events.Add(em);
        _player.AddEvent(em);

        _player.Goal();

        scoreAlly++;

        UpdateScoreboard();
    }

    private void ButtonAssist(ItemPlayerPlaying _player)
    {
        EventMatch em;

        em.player = _player.GetPlayer();
        em.team = null;
        em.type = EventMatch.MatchEventType.ASSIST;
        em.time = TimeSpan.FromSeconds(timer);

        events.Add(em);
        _player.AddEvent(em);
    }

    private void ButtonFoul(ItemPlayerPlaying _player)
    {
        EventMatch em;

        em.player = _player.GetPlayer();
        em.team = null;
        em.type = EventMatch.MatchEventType.FOUL_ALLY;
        em.time = TimeSpan.FromSeconds(timer);

        events.Add(em);
        _player.AddEvent(em);

        currentFoulsAlly++;

        UpdateScoreboard();
    }

    private void ButtonYellow(ItemPlayerPlaying _player)
    {
        if (_player.GetYellow())
        {
            ButtonRed(_player);
            return;
        }

        EventMatch em;

        em.player = _player.GetPlayer();
        em.team = null;
        em.type = EventMatch.MatchEventType.YELLOW;
        em.time = TimeSpan.FromSeconds(timer);

        events.Add(em);
        _player.AddEvent(em);

        _player.Yellow();
    }

    private void ButtonRed(ItemPlayerPlaying _player)
    {
        EventMatch em;

        em.player = _player.GetPlayer();
        em.team = null;
        em.type = EventMatch.MatchEventType.RED;
        em.time = TimeSpan.FromSeconds(timer);

        events.Add(em);
        _player.AddEvent(em);

        _player.Red();

        active.Remove(_player);
        _player.transform.SetAsLastSibling();

        reds++;
    }

    private List<Player> ToTeam(List<ItemPlayerPlaying> _players)
    {
        List<Player> team = new List<Player>();

        for (int i = 0; i < _players.Count; i++)
            team.Add(_players[i].GetPlayer());

        return team;
    }
}
