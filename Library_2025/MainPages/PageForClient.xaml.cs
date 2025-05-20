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

namespace Library_2025
{
    /// <summary>
    /// Логика взаимодействия для PageForClient.xaml
    /// </summary>
    public partial class PageForClient : Page
    {
        public PageForClient(Users user)
        {
            InitializeComponent();
            DataContext = user;
        }

        private void Books_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BooksPage());

        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            // Предположим, что у вас есть способ получить идентификатор текущего пользователя
            int currentUserId = GetCurrentUserId(); // Замените это на реальный способ получения идентификатора пользователя

            // Передаем идентификатор текущего пользователя при создании экземпляра MyOrders
            NavigationService.Navigate(new MyOrders(currentUserId));
        }

        // Пример метода для получения идентификатора текущего пользователя
        private int GetCurrentUserId()
        {
            // Здесь должен быть ваш код для получения идентификатора текущего пользователя
            // Например, из статического свойства или сервиса аутентификации
            return 1; // Замените это на реальный идентификатор пользователя
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
    }
}
