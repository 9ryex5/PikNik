using System;
using System.Collections.Generic;

[Serializable]
public class DataPlayerMatch
{

    private Player player;
    private float timePlayedFirst;
    private float timePlayedSecond;
    private List<EventMatch> events;

    public DataPlayerMatch(ItemPlayerPlaying _player)
    {
        player = _player.GetPlayer();
        timePlayedFirst = _player.GetPlayedTimeFirst();
        timePlayedSecond = _player.GetPlayedTimeSecond();
        events = _player.GetEvents();
    }

    public Player GetPlayer()
    {
        return player;
    }

    public float GetTimePlayed()
    {
        return timePlayedFirst + timePlayedSecond;
    }

    public int GetNEvents()
    {
        return events.Count;
    }

    public EventMatch GetEvent(int _index)
    {
        return events[_index];
    }
}
