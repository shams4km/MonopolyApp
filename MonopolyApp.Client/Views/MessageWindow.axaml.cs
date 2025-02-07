using Avalonia.Controls;
using Avalonia.Interactivity;

namespace MonopolyApp.Client.Views
{
    public partial class MessageWindow : Window
    {
        public MessageWindow(string message)
        {
            InitializeComponent();
            this.FindControl<TextBlock>("MessageText").Text = message;
        }

        public void OnOkClicked(object sender, RoutedEventArgs e)
        {
            this.Close(); // Закрытие окна
        }
    }
}