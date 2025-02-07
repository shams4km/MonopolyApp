enum MessageType
{
    Register,
    RollDice,
    UpdateState
}

class MessageObject
{
    public MessageType Type { get; set; }
    public string Data { get; set; }
}