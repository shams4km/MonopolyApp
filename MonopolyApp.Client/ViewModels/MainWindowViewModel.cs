using System.Threading.Tasks;
using System.Windows.Input;
using MonopolyApp.Client.Views;
using CommunityToolkit.Mvvm.Input;

namespace MonopolyApp.Client.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _playerName;
        public string PlayerName
        {
            get => _playerName;
            set => SetProperty(ref _playerName, value);
        }

        public ICommand StartGameCommand { get; }

        public MainWindowViewModel()
        {
            StartGameCommand = new AsyncRelayCommand(StartGameAsync);
        }

        private async Task StartGameAsync()
        {
            if (string.IsNullOrWhiteSpace(PlayerName))
            {
                return;
            }

            // Логика для создания нового окна игры
            var gameWindow = new GameWindow(PlayerName);
            await gameWindow.LoadGameAsync();
            gameWindow.Show();
        }
    }
}