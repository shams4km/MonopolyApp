using System.Threading.Tasks;
using Avalonia.Controls;
using MonopolyApp.Client.ViewModels;

namespace MonopolyApp.Client.Views
{
    public partial class GameWindow : Window
    {
        private readonly GameViewModel _viewModel;

        public GameWindow(string playerName)
        {
            InitializeComponent();
            _viewModel = new GameViewModel(playerName);  // Передаем имя игрока в конструктор
            DataContext = _viewModel;
        }

        public async Task LoadGameAsync()
        {
            await _viewModel.ListenForUpdates();  // Слушаем обновления игры
        }
    }
}