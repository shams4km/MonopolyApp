using Avalonia.Controls;
using Avalonia.Controls.Templates;
using MonopolyApp.Client.ViewModels;
using System;

namespace MonopolyApp.Client
{
    public class ViewLocator : IDataTemplate
    {
        // Этот метод создаёт Control (View) для заданной ViewModel
        public Control Build(object? param)
        {
            if (param == null)
                return new TextBlock { Text = "Не найдено: " };

            // Имя класса View должно быть связано с ViewModel
            var name = param.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
            var type = Type.GetType(name);

            // Если тип найден, создаём экземпляр View
            if (type != null)
            {
                return (Control)Activator.CreateInstance(type)!;
            }

            // Если View не найден, возвращаем текстовое сообщение
            return new TextBlock { Text = "Не найдено представление для: " + name };
        }

        // Проверка, является ли переданный объект ViewModel
        public bool Match(object? data)
        {
            return data is ViewModelBase;
        }
    }
}