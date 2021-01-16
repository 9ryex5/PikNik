//Ordenar jogadores por numero nas listas
//Eventos golos ver quem estava em campo
//Intervalo reset tempo mas manter o acumulado dos jogadores
//Alterar botão gravar no jogo
//Refresh ao entrar numa match, aparece a confirmação para eliminar

using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveFile : MonoBehaviour
{
    public static SaveFile SF;

    private List<Player> allPlayers;
    private List<DataMatch> allMatches;

    private void Awake()
    {
        SF = this;
    }

    private void Start()
    {
        LoadPlayersData();
        LoadMatchesData();
    }

    public void AddPlayer(Player _player)
    {
        allPlayers.Add(_player);
        SavePlayersData();
    }

    public void RemovePlayer(Player _player)
    {
        allPlayers.Remove(_player);
        SavePlayersData();
    }

    public List<Player> GetAllPlayers()
    {
        return allPlayers;
    }

    public Player GetPlayer(int _index)
    {
        return allPlayers[_index];
    }

    public void SavePlayersData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/PlayerData.pd");

        PlayerData data = new PlayerData();

        data.allPlayers = allPlayers;

        bf.Serialize(file, data);
        file.Close();
    }

    private void LoadPlayersData()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerData.pd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/PlayerData.pd", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            allPlayers = data.allPlayers;
        }
        else
        {
            allPlayers = new List<Player>();
            SavePlayersData();
        }
    }

    public void AddMatch(DataMatch _match)
    {
        allMatches.Add(_match);
        SaveMatchesData();
    }

    public void RemoveMatch(DataMatch _match)
    {
        allMatches.Remove(_match);
        SaveMatchesData();
    }

    public List<DataMatch> GetAllMatches()
    {
        return allMatches;
    }

    public DataMatch GetMatch(int _index)
    {
        return allMatches[_index];
    }

    private void SaveMatchesData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MatchesData.md");

        MatchesData data = new MatchesData();

        data.allMatches = allMatches;

        bf.Serialize(file, data);
        file.Close();
    }

    private void LoadMatchesData()
    {
        if (File.Exists(Application.persistentDataPath + "/MatchesData.md"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MatchesData.md", FileMode.Open);
            MatchesData data = (MatchesData)bf.Deserialize(file);
            file.Close();

            allMatches = data.allMatches;
        }
        else
        {
            allMatches = new List<DataMatch>();
            SaveMatchesData();
        }
    }
}

[Serializable]
class PlayerData
{
    public List<Player> allPlayers;
}

[Serializable]
class MatchesData
{
    public List<DataMatch> allMatches;
}