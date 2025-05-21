using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Data.Entity;

namespace Library_2025
{
    /// <summary>
    /// Логика взаимодействия для PageForClient.xaml
    /// </summary>
    public partial class PageForClient : Page
    {
        private Library_2025Entities context = Library_2025Entities.GetContext();

        public PageForClient(Users user)
        {
            InitializeComponent();
            DataContext = user;
        }

        public static class AuthenticationService
        {
            public static int CurrentUserId { get; set; }
        }

        private void Books_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BooksPage());
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            // Получаем идентификатор текущего пользователя
            int currentUserId = GetCurrentUserId();

            // Передаем идентификатор текущего пользователя при создании экземпляра MyOrders
            NavigationService.Navigate(new MyOrders(currentUserId));
        }

        // Пример метода для получения идентификатора текущего пользователя
        private int GetCurrentUserId()
        {
            // Используем сервис аутентификации для получения идентификатора текущего пользователя
            return AuthenticationService.CurrentUserId;
        }

        private void Reviews_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Reviews());
        }

        private void Auth_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите сменить пользователя?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new AuthPage());
            }
        }

        // Метод для аутентификации пользователя
        public void AuthenticateUser(string login, string password)
        {
            // Здесь должна быть логика аутентификации пользователя
            // Например, проверка логина и пароля в базе данных

            // После успешной аутентификации установите CurrentUserId
            var user = context.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
            if (user != null)
            {
                AuthenticationService.CurrentUserId = user.ID_users;
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }
    }
}
