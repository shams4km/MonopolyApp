using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace MonopolyApp.Client.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        public ObservableCollection<PlayerViewModel> Players { get; } = new ObservableCollection<PlayerViewModel>();
        public string Status { get; set; } = "Игра в процессе...";
        public ICommand RollDiceCommand { get; }

        // Конструктор, принимающий имя игрока
        public GameViewModel(string playerName)
        {
            // Логика добавления игрока
            Players.Add(new PlayerViewModel(playerName, 1500));  // Добавление нового игрока
            RollDiceCommand = new RelayCommand(RollDice);
        }

        private void RollDice()
        {
            // Логика для броска кубиков
        }

        // Метод для слушания обновлений
        public async Task ListenForUpdates()
        {
            // Имитация обновлений
            await Task.Delay(1000); // Замените на реальную логику для прослушивания обновлений от сервера
            Status = "Ожидание других игроков...";
        }
    }
}