using System;
using System.Collections.Generic;
using System.Linq;
using MonopolyApp.Server.Models;

class Game
{
    private List<Player> _players = new List<Player>();
    private int _currentPlayerIndex = 0;
    private bool _isGameStarted = false;

    public void AddPlayer(string name)
    {
        if (!_isGameStarted && _players.Count < 4)
        {
            _players.Add(new Player(name));
        }
    }

    public void StartGame()
    {
        if (_players.Count > 1)
        {
            _isGameStarted = true;
        }
    }

    public int RollDice()
    {
        Random random = new Random();
        return random.Next(1, 7) + random.Next(1, 7);
    }

    public string GetGameState()
    {
        if (!_isGameStarted) return "Ожидание игроков...";
        return $"Ходит игрок: {_players[_currentPlayerIndex].Name}";
    }

    public Player GetCurrentPlayer()
    {
        if (_players.Count == 0) return null;
        return _players[_currentPlayerIndex];
    }

    public void NextTurn()
    {
        if (_players.Count > 0)
        {
            _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count;
        }
    }

    public int GetPlayersCount()
    {
        return _players.Count;
    }
}