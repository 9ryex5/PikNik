using System;

[Serializable]
public class Player
{

    public int number;
    public string name;
    public int goals;
    public int assists;
    public int fouls;
    public int yellows;
    public int reds;

    public Player(int _number, string _name)
    {
        number = _number;
        name = _name;
    }

}
