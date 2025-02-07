using System.Collections.Generic;

public class GameState
{
    public List<PlayerData> Players { get; set; }
}

public class PlayerData
{
    public string Name { get; set; }
    public int Money { get; set; }
}