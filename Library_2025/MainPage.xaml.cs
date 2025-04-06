using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Library_2025
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        //public static string GetHash(string password)
        //{
        //    using (var hash = SHA1.Create())
        //    return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
        //}

        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            // Проверка, что логин не пустой
            if (textBoxLogin.Text.Length == 0)
            {
                MessageBox.Show("Укажите логин!");
                return;
            }

            using (var db = new Entities())
            {
                // Проверка, существует ли уже пользователь с таким логином
                var user = db.Users.AsNoTracking().FirstOrDefault(u => u.Login == textBoxLogin.Text);
                if (user != null)
                {
                    MessageBox.Show("Пользователь с такими данными уже существует!");
                    return;
                }
            }

            bool en = true;
            bool number = false;

            // Проверка пароля на наличие английских букв и цифр
            for (int i = 0; i < passBox.Password.Length; i++)
            {
                if (passBox.Password[i] >= 'A' && passBox.Password[i] <= 'Z')
                {
                    en = true;
                }
                else if (passBox.Password[i] >= '0' && passBox.Password[i] <= '9')
                {
                    number = true;
                }
            }

            var regex = new Regex(@"^\+7\d{10}$");
            StringBuilder errors = new StringBuilder();

            // Проверка длины пароля
            if (passBox.Password.Length < 6)
            {
                errors.AppendLine("Пароль должен быть больше 6 символов");
            }


            // Проверка наличия английских букв в пароле
            if (!en)
            {
                errors.AppendLine("Пароль должен содержать хотя бы одну английскую букву");
            }

            // Проверка наличия цифр в пароле
            if (!number)
            {
                errors.AppendLine("Пароль должен содержать хотя бы одну цифру");
            }

            // Проверка корректности email
            if (!isValidMail(textBoxEmail.Text))
            {
                errors.AppendLine("Введите корректный email");
            }

            // Если есть ошибки, показать их
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            else
            {
                // Создание нового пользователя и сохранение в базе данных
                using (var db = new Entities())
                {
                    Users userObject = new Users
                    {
                        Login = textBoxLogin.Text,
                        Password = GetHash(passBox.Password),
                        Role = 1,
                        E_mail = textBoxEmail.Text,
                        
                    };

                    db.Users.Add(userObject);
                    db.SaveChanges();
                }

                MessageBox.Show("Вы успешно зарегистрировались!", "Успешно!", MessageBoxButton.OK);
                NavigationService.Navigate(new Uri("/Pages/AuthLog.xaml", UriKind.Relative));
            }
        }
        private bool isValidMail(string email)
        {
            // Регулярное выражение для проверки корректности email
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex regex = new Regex(emailPattern);
            return regex.IsMatch(email);
        }
        public static string GetHash(string password)
        {
            using (var hash = SHA1.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
        }

        //private bool isValidMail(object text)
        //{
        //    var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        //}

    }
}
