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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Registration()
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

        private void TextBoxPasswordPlaceholder_2_GotFocus(object sender, RoutedEventArgs e)
        {
            textBoxPasswordPlaceholder_2.Visibility = Visibility.Collapsed;
            passBox_2.Visibility = Visibility.Visible;
            passBox_2.Focus();
        }

        private void PassBox_2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(passBox_2.Password))
            {
                passBox_2.Visibility = Visibility.Collapsed;
                textBoxPasswordPlaceholder_2.Visibility = Visibility.Visible;
                textBoxPasswordPlaceholder_2.Foreground = Brushes.Gray; // Установите цвет текста-подсказки по вашему усмотрению
            }
        }

        private void TextBoxEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxEmail.Text == "Email")
            {
                textBoxEmail.Text = string.Empty;
                textBoxEmail.Foreground = Brushes.Black; // Установите цвет текста по вашему усмотрению
            }
        }

        private void TextBoxEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEmail.Text))
            {
                textBoxEmail.Text = "Email";
                textBoxEmail.Foreground = Brushes.Gray; // Установите цвет текста-подсказки по вашему усмотрению
            }
        }

        //public static string GetHash(string password)
        //{
        //    using (var hash = SHA1.Create())
        //    return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
        //}

        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            


            if (textBoxLogin.Text.Length > 0)
            {
                using (var db = new Library_2025Entities())
                {
                    var user = db.Users.AsNoTracking().FirstOrDefault(u => u.Login == textBoxLogin.Text);
                    if (user != null) { MessageBox.Show("Пользователь с такими данными уже существует!"); return; }
                }

                bool en = true;
                bool number = false;
                for (int i = 0; i < passBox.Password.Length; i++)
                {
                    if ((passBox.Password[i] >= 'A' && passBox.Password[i] <= 'Z') || (passBox.Password[i] >= 'a' && passBox.Password[i] <= 'z'))
                    {
                        en = true;
                    }
                    if (passBox.Password[i] >= '0' && passBox.Password[i] <= '9')
                    {
                        number = true;
                    }
                }

                StringBuilder errors = new StringBuilder();

                if (passBox.Password.Length < 6) errors.AppendLine("Пароль должен быть больше 6 сиmвоnов");
                if (!en) errors.AppendLine("Пароль должен быть на английском языкe");
                if (!number) errors.AppendLine("Пароль должен содержать хотя бы одну цифру");
                if (!isValidMail(textBoxEmail.Text)) errors.AppendLine("Введите корректный email");

                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }
                else
                {
                    Library_2025Entities db = new Library_2025Entities();
                    Users userObject = new Users
                    {
                        Login = textBoxLogin.Text,
                        Password = GetHash(passBox.Password),
                        Role = 1,
                        E_mail = textBoxEmail.Text,
                    };
                    db.Users.Add(userObject);
                    db.SaveChanges();
                    MessageBox.Show("Вы успешно зарегистрировались!", "Успешно!", MessageBoxButton.OK);
                    //NavigationService.Navigate((new Uri("/Pages/AuthLog.xaml", UriKind.Relative)));
                }
            }
            else MessageBox.Show("Укажите лоигн!");
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

        private bool isValidMail(string email)
        {
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }


    }
}
