using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MonopolyApp.Client.ViewModels;

namespace MonopolyApp.Client.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly HttpClient _httpClient;

    public MainWindowViewModel()
    {
        _httpClient = new HttpClient();
        StartGameCommand = new RelayCommand(AddPlayerAsync);
    }

    public ICommand StartGameCommand { get; }

    private async void AddPlayerAsync()
    {
        var playerName = "Player1"; // Замените на имя игрока
        var response = await _httpClient.PostAsJsonAsync("http://localhost:5000/api/game/add-player", playerName);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine(result); // Вывод результата в консоль
        }
    }
}