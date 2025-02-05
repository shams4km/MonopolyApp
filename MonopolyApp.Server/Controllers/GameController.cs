using Microsoft.AspNetCore.Mvc;
using MonopolyApp.Server.Models;

namespace MonopolyApp.Server;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private static readonly Game _game = new Game();

    [HttpPost("add-player")]
    public IActionResult AddPlayer([FromBody] string playerName)
    {
        _game.AddPlayer(playerName);
        return Ok($"Игрок {playerName} добавлен.");
    }

    [HttpGet("roll-dice")]
    public IActionResult RollDice()
    {
        int dice = _game.RollDice();
        return Ok($"Выпало: {dice}");
    }
}