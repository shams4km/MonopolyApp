using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MonopolyApp.Client.Models;

class Program
{
    private static List<TcpClient> clients = new List<TcpClient>();
    private static List<PlayerState> players = new List<PlayerState>();

    static async Task Main()
    {
        var listener = new TcpListener(IPAddress.Any, 8888);
        listener.Start();
        Console.WriteLine("Сервер запущен.");

        while (true)
        {
            var client = await listener.AcceptTcpClientAsync();
            clients.Add(client);
            Console.WriteLine("Новый клиент подключился.");

            // Запускаем обработку клиента в отдельном потоке
            _ = Task.Run(() => HandleClient(client));
        }
    }

    private static async Task HandleClient(TcpClient client)
    {
        var stream = client.GetStream();
        var reader = new StreamReader(stream, Encoding.UTF8);
        var writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

        while (true)
        {
            var data = await reader.ReadLineAsync();
            if (string.IsNullOrEmpty(data)) continue;

            var message = JsonSerializer.Deserialize<MessageObject>(data);

            if (message.Type == MessageType.Register)
            {
                var newPlayer = new PlayerState { Name = message.Data };
                players.Add(newPlayer);
                Console.WriteLine($"Игрок {message.Data} подключился.");

                await BroadcastGameState();
            }
        }
    }

    private static async Task BroadcastGameState()
    {
        var gameState = new GameState
        {
            Players = players.Select(p => new PlayerData { Name = p.Name, Money = p.Money }).ToList()
        };

        var message = new MessageObject
        {
            Type = MessageType.UpdateState,
            Data = JsonSerializer.Serialize(gameState)
        };

        var json = JsonSerializer.Serialize(message) + "\n";

        foreach (var client in clients)
        {
            var stream = client.GetStream();
            var writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
            await writer.WriteLineAsync(json);
        }
    }
}
