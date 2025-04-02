using Module4.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Module4.View
{
    /// <summary>
    /// Логика взаимодействия для ValidationWindow.xaml
    /// </summary>
    public partial class ValidationWindow : Window
    {
        private ServerRequest request = new ServerRequest();

        int rowIndex = 2;

        public ValidationWindow()
        {
            InitializeComponent();
        }

        private async void GetRequestButtonClick(object sender, RoutedEventArgs e)
        {
            string url = "http://localhost:4444/TransferSimulator/fullName";

            string result = await request.GetRequestAsync(url);
            FullNameTextBlock.Text = GetFullNameFromString(result);
        }

        private string GetFullNameFromString(string result)
        {
            return result
                .Substring(result.IndexOf(":") + 2)
                .Replace("\"", "")
                .Replace("}", "");
        }

        private void SendResultButtonClick(object sender, RoutedEventArgs e)
        {
            if (FullNameTextBlock.Text.Equals(""))
            {
                WarningFullNameTextBlock.Text = "Данные с сервера ещё не получены. " +
                    "Отправьте запрос.";
            }
            else
            {
                if (!Regex.IsMatch(FullNameTextBlock.Text, @"^[а-яА-ЯёЁ\s]*$"))
                {
                    WarningFullNameTextBlock.Text = "Данные ФИО не валидны. Попробуйте ещё раз.";
                }
                else
                {
                    WarningFullNameTextBlock.Text = "Данные ФИО успешно прошли валидацию.";
                }

                string filePath = "C:\\Users\\admin\\source\\repos\\Module4\\ТестКейс.docx";
                int tableIndex = 1;
                int columnIndex = 3;
                
                string data = WarningFullNameTextBlock.Text;

                WordWriter wordWriter = new WordWriter();

                if (!data.Equals(""))
                {
                    rowIndex++;
                }

                wordWriter.SaveToWord(filePath, tableIndex, columnIndex, rowIndex, data);
            }
        }
    }
}
