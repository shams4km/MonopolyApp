using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using MonopolyApp.Server.Models;

class ServerObject
{
    private TcpListener _tcpListener = new TcpListener(IPAddress.Any, 8888);
    private List<ClientObject> _clients = new List<ClientObject>(); // Подключенные клиенты
    private Game _game = new Game(); // Логика игры

    public async Task ListenAsync()
    {
        try
        {
            _tcpListener.Start();
            Console.WriteLine("Сервер запущен. Ожидание подключений...");

            while (true)
            {
                TcpClient tcpClient = await _tcpListener.AcceptTcpClientAsync();
                ClientObject clientObject = new ClientObject(tcpClient, this);
                _clients.Add(clientObject);
                Task.Run(clientObject.ProcessAsync);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка сервера: {ex.Message}");
        }
        finally
        {
            Disconnect();
        }
    }

    protected internal async Task BroadcastMessageAsync(MessageObject message)
    {
        string package = JsonSerializer.Serialize(message);

        foreach (var client in _clients)
        {
            await client.Writer.WriteLineAsync(package);
            await client.Writer.FlushAsync();
        }
    }

    protected internal void RemoveConnection(ClientObject client)
    {
        _clients.Remove(client);
        client.Close();
    }

    protected internal void Disconnect()
    {
        foreach (var client in _clients)
        {
            client.Close();
        }
        _tcpListener.Stop();
    }

    protected internal async Task HandleMessageAsync(ClientObject client, MessageObject message)
    {
        switch (message.Type)
        {
            case MessageType.Register:
                client.Username = message.Data;
                _game.AddPlayer(client.Username);
                
                // Если 4 игрока, начинаем игру
                if (_game.GetPlayersCount() == 4)
                {
                    _game.StartGame();
                }

                await BroadcastMessageAsync(new MessageObject
                {
                    Type = MessageType.UpdateState,
                    Data = JsonSerializer.Serialize(_game.GetGameState())
                });
                break;

            case MessageType.RollDice:
                var currentPlayer = _game.GetCurrentPlayer();
                if (currentPlayer != null && currentPlayer.Name == client.Username)
                {
                    int diceResult = _game.RollDice();
                    _game.NextTurn();
                    await BroadcastMessageAsync(new MessageObject
                    {
                        Type = MessageType.UpdateState,
                        Data = JsonSerializer.Serialize(new { Player = client.Username, Dice = diceResult })
                    });
                }
                break;
        }
    }
}
