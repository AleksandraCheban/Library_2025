using Library_2025.Pages;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Проверка, что логин и пароль не пустые
            if (string.IsNullOrEmpty(textBoxLogin.Text) || string.IsNullOrEmpty(passBox.Password))
            {
                MessageBox.Show("Введите логин и пароль!");
                return;
            }

            string _password = GetHash(passBox.Password);

            using (var db = new Entities())
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
                    var newWindow = new PageForClient(user);
                    newWindow.Show();
                }
                else
                {
                    var newWindow = new var newWindow = new PageForAdmin(user);
                    (user);
                    newWindow.Show();
                }

                // Закрытие текущего окна
                AuthPage.Close();
            }
        }

        public static string GetHash (string password)
        {
            using (var hash = SHA1.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
            }
        }
    }
}
