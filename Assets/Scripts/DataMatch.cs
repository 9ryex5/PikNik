using System;
using System.Collections.Generic;

[Serializable]
public class DataMatch
{

    public DateTime date;
    public TimeSpan duration;
    public List<DataPlayerMatch> players;
    public List<EventMatch> events;

    public DataMatch(DateTime _date, TimeSpan _duration, DataPlayerMatch[] _players, List<EventMatch> _events)
    {
        date = _date;
        duration = _duration;
        players = new List<DataPlayerMatch>(_players);
        events = new List<EventMatch>(_events);
    }

    public int GetNPlayers()
    {
        return players.Count;
    }

    public DataPlayerMatch GetPlayer(int _index)
    {
        return players[_index];
    }

    public int GetNGoalsAlly()
    {
        int goals = 0;

        for (int i = 0; i < events.Count; i++)
        {
            if (events[i].type == EventMatch.MatchEventType.GOAL_ALLY)
                goals++;
        }

        return goals;
    }

    public int GetNGoalsEnemy()
    {
        int goals = 0;

        for (int i = 0; i < events.Count; i++)
        {
            if (events[i].type == EventMatch.MatchEventType.GOAL_ENEMY)
                goals++;
        }

        return goals;
    }

    public int GetNFouls()
    {
        int fouls = 0;

        for (int i = 0; i < events.Count; i++)
        {
            if (events[i].type == EventMatch.MatchEventType.FOUL_ALLY)
                fouls++;
        }

        return fouls;
    }

    public int GetNYellows()
    {
        int yellows = 0;

        for (int i = 0; i < events.Count; i++)
        {
            if (events[i].type == EventMatch.MatchEventType.YELLOW)
                yellows++;
        }

        return yellows;
    }

    public int GetNReds()
    {
        int reds = 0;

        for (int i = 0; i < events.Count; i++)
        {
            if (events[i].type == EventMatch.MatchEventType.RED)
                reds++;
        }

        return reds;
    }
}

[Serializable]
public struct EventMatch
{
    public Player player;
    public List<Player> team;
    public MatchEventType type;
    public TimeSpan time;

    public enum MatchEventType
    {
        HALF_TIME,
        JOIN,
        LEAVE,
        GOAL_ALLY,
        GOAL_ENEMY,
        ASSIST,
        FOUL_ALLY,
        FOUL_ENEMY,
        YELLOW,
        RED
    }
}
