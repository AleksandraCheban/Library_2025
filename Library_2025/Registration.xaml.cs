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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Registration()
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
            //string login = textBoxLogin.Text.Trim();
            //string pass = passBox.Password.Trim();
            //string pass_2 = passBox_2.Password.Trim();
            //string email = textBoxEmail.Text.Trim().ToLower();

            //if (login.Length < 5)
            //{
            //    textBoxLogin.ToolTip = "Это поле введено не корректно!";
            //    textBoxLogin.Background = Brushes.DarkRed;
            //} else if (pass.Length < 5)
            //{
            //    passBox.ToolTip = "Это поле введено не корректно!";
            //    passBox.Background = Brushes.DarkRed;

            //}
            //else if (pass != pass_2)
            //{
            //    passBox_2.ToolTip = "Это поле введено не корректно!";
            //    passBox_2.Background = Brushes.DarkRed;

            //}
            //else if (email.Length<5 || !email.Contains("@") || !email.Contains("."))
            //{
            //    textBoxEmail.ToolTip = "Это поле введено не корректно!";
            //    textBoxEmail.Background = Brushes.DarkRed;

            //}
            //else
            //{
            //    textBoxLogin.ToolTip = "";
            //    textBoxLogin.Background = Brushes.Transparent;
            //    passBox.ToolTip = "";
            //    passBox.Background = Brushes.Transparent;
            //    passBox_2.ToolTip = "";
            //    passBox_2.Background = Brushes.Transparent;
            //    textBoxEmail.ToolTip = "";
            //    textBoxEmail.Background = Brushes.Transparent;

            //    MessageBox.Show("Все хорошо!");


            if (textBoxLogin.Text.Length > 0)
            {
                using (var db = new Library_2025Entities())
                {
                    var user = db.Users.AsNoTracking().FirstOrDefault(u => u.Login == textBoxLogin.Text);
                    if (user != null) { MessageBox.Show("Пользователь с такими данными уже существует!"); return; }
                }

                bool en = true;
                bool number = false;
                for (int i = 0; 1 < passBox.Password.Length; i++)
                {
                    if (passBox.Password[i] >= 'A' && passBox.Password[i] <= 'Я') en = false;
                    if (passBox.Password[i] >= '0' && passBox.Password[i] <= '9') number = true;
                }
                //var regex = new Regex(@"*((\+7))\d{10}$");

                StringBuilder errors = new StringBuilder();

                if (passBox.Password.Length < 6) errors.AppendLine("Пароль должен быть больше 6 сиmвоnов");
                if (!en) errors.AppendLine("Пароль должен быть на английском языкe");
                if (!number) errors.AppendLine("Пароль должен содержать хотя бы одну цифру");
                //if (!isValidMail(textBoxEmail.Text)) errors.AppendLine("Введите кoрректный email");

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

        //private bool isValidMail(object text)
        //{
        //    var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        //}

    }
}
