using System.Net.Http;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;

namespace MonopolyApp.Client.ViewModels;

public class GameViewModel : ReactiveObject
{
    private readonly HttpClient _httpClient;

    private string _status;
    public string Status
    {
        get => _status;
        set => this.RaiseAndSetIfChanged(ref _status, value);
    }

    public ReactiveCommand<Unit, Unit> RollDiceCommand { get; }

    public GameViewModel()
    {
        _httpClient = new HttpClient();
        Status = "Нажмите 'Бросить кубик' для начала.";
        RollDiceCommand = ReactiveCommand.CreateFromTask(RollDiceAsync);
    }

    private async Task RollDiceAsync()
    {
        var response = await _httpClient.GetAsync("http://localhost:5000/api/game/roll-dice");

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            Status = result;
        }
    }
}