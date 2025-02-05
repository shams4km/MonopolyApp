using MonopolyApp.Server.Models;

namespace MonopolyApp.Server.Services;

public class GameService
{
    private readonly Game _game;

    public GameService()
    {
        _game = new Game();
    }

    public void AddPlayer(string name)
    {
        _game.AddPlayer(name);
    }

    public int RollDice()
    {
        return _game.RollDice();
    }
}