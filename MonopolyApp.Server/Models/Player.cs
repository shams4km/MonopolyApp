namespace MonopolyApp.Server.Models;

public class Player
{
    public string Name { get; set; }
    public int Balance { get; set; }
    public int Position { get; set; }

    public Player(string name)
    {
        Name = name;
        Balance = 1500;
        Position = 0;
    }
}