using Avalonia.Controls;
using Avalonia.Interactivity;
using MonopolyApp.Client.ViewModels;
using System.Threading.Tasks;

namespace MonopolyApp.Client.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Этот метод вызывается при нажатии на кнопку
        private async void OnStartGameClicked(object sender, RoutedEventArgs e)
        {
            // Получаем PlayerName из DataContext, который должен быть MainWindowViewModel
            string playerName = (DataContext as MainWindowViewModel)?.PlayerName;

            if (string.IsNullOrEmpty(playerName))
            {
                await ShowMessage("Введите имя перед началом игры!");
                return;
            }

            // Открытие окна игры с передачей имени игрока
            var gameWindow = new GameWindow(playerName);
            gameWindow.Show();

            // Закрытие главного окна
            this.Close();
        }

        private async Task ShowMessage(string message)
        {
            var messageWindow = new MessageWindow(message);
            await messageWindow.ShowDialog(this);
        }
    }
}