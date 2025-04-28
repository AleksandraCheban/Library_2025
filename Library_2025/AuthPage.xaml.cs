using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Library_2025
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void TextBoxLogin_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxLogin.Text == "Введите логин")
            {
                textBoxLogin.Text = string.Empty;
                textBoxLogin.Foreground = Brushes.Black; // Установите цвет текста по вашему усмотрению
            }
        }

        private void TextBoxLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLogin.Text))
            {
                textBoxLogin.Text = "Введите логин";
                textBoxLogin.Foreground = Brushes.Gray; // Установите цвет текста-подсказки по вашему усмотрению
            }
        }

        private void TextBoxPasswordPlaceholder_GotFocus(object sender, RoutedEventArgs e)
        {
            textBoxPasswordPlaceholder.Visibility = Visibility.Collapsed;
            passBox.Visibility = Visibility.Visible;
            passBox.Focus();
        }

        private void PassBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(passBox.Password))
            {
                passBox.Visibility = Visibility.Collapsed;
                textBoxPasswordPlaceholder.Visibility = Visibility.Visible;
                textBoxPasswordPlaceholder.Foreground = Brushes.Gray; // Установите цвет текста-подсказки по вашему усмотрению
            }
        }

                private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Проверка, что логин и пароль не пустые
            if (string.IsNullOrEmpty(textBoxLogin.Text) || string.IsNullOrEmpty(passBox.Password))
            {
                MessageBox.Show("Введите логин и пароль!");
                return;
            }

            string _password = GetHash(passBox.Password);

            using (var db = new Library_2025Entities())
            {
                // Поиск пользователя в базе данных
                var user = db.Users.AsNoTracking().FirstOrDefault(u => u.Login == textBoxLogin.Text && u.Password == _password);

                if (user == null)
                {
                    MessageBox.Show("Пользователь с такими данными не найден!");
                    return;
                }

                // Открытие соответствующего окна в зависимости от роли пользователя
                if (user.Role == 1)
                {
                    NavigationService.Navigate(new PageForClient(user));
                    
                }
                else
                {
                    NavigationService.Navigate(new PageForAdmin(user)); 
                }

          
            }
        }

        public static string GetHash (string password)
        {
            using (var hash = SHA1.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Registration());
        }


    }
}
