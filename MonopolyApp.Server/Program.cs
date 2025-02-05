using Microsoft.AspNet;
using Microsoft.AspNet.Hosting;

namespace MonopolyApp.Server;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Добавляем контроллеры
        builder.Services.AddControllers();

        var app = builder.Build();

        // Настройка маршрутов
        app.MapControllers();

        app.Run();
    }
}