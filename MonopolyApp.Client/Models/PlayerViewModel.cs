namespace MonopolyApp.Client.ViewModels
{
    public class PlayerViewModel
    {
        public string Name { get; }
        public int Money { get; }

        public PlayerViewModel(string name, int money)
        {
            Name = name;
            Money = money;
        }
    }
}