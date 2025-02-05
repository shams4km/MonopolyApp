namespace MonopolyApp.Server.Models;

public class Field
{
    public string Name { get; set; }
    public string Type { get; set; } // "property", "start", "jail", "chance"
    public int Cost { get; set; }
    public Player Owner { get; set; }

    public Field(string name, string type, int cost = 0)
    {
        Name = name;
        Type = type;
        Cost = cost;
    }
}