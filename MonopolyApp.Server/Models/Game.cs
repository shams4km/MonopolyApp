using System;
using System.Collections.Generic;

namespace MonopolyApp.Server.Models;

public class Game
{
    public List<Player> Players { get; set; }
    public List<Field> Board { get; set; }

    public Game()
    {
        Players = new List<Player>();
        Board = new List<Field>
        {
            new Field("Start", "start"),
            new Field("Property 1", "property", 200),
            new Field("Jail", "jail"),
            new Field("Chance", "chance")
        };
    }

    public void AddPlayer(string name)
    {
        Players.Add(new Player(name));
    }

    public int RollDice()
    {
        return new Random().Next(1, 7);
    }
}