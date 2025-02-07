namespace MonopolyApp.Client.Models
{
    public class PlayerState
    {
        public string Name { get; set; }
        public int Money { get; set; }

        // Конструктор с параметрами
        public PlayerState(string name, int money)
        {
            Name = name;
            Money = money;
        }

        // Конструктор по умолчанию
        public PlayerState()
        {
            // Инициализация свойств значениями по умолчанию, если нужно
            Name = string.Empty;
            Money = 0;
        }
    }

}