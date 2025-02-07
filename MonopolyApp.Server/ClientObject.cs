using System;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using MonopolyApp.Server.Models;

class ClientObject
{
    private TcpClient _client;
    private ServerObject _server;
    protected internal StreamWriter Writer { get; }
    protected internal StreamReader Reader { get; }
    protected internal string Username { get; set; }

    public ClientObject(TcpClient tcpClient, ServerObject serverObject)
    {
        _client = tcpClient;
        _server = serverObject;
        var stream = _client.GetStream();
        Reader = new StreamReader(stream);
        Writer = new StreamWriter(stream);
    }

    public async Task ProcessAsync()
    {
        try
        {
            while (true)
            {
                string? str = await Reader.ReadLineAsync();
                if (str == null) continue;

                var message = JsonSerializer.Deserialize<MessageObject>(str);
                if (message != null)
                {
                    await _server.HandleMessageAsync(this, message);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка у клиента {Username}: {ex.Message}");
        }
        finally
        {
            _server.RemoveConnection(this);
        }
    }

    public void Close()
    {
        Writer.Close();
        Reader.Close();
        _client.Close();
    }
}