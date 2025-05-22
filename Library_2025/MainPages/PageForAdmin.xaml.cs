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
    public partial class PageForAdmin : Page
    {
        public PageForAdmin(Users user)
        {
            InitializeComponent();
            DataContext = user;
        }

        private void Books_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BooksBageAdmin());
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrdersPage());
        }

        private void Users_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UsersPage());
        }

        private void Authors_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorsPage());
        }

        private void Genres_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GenresPage());
        }

        private void Languages_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LanguagesPage());
        }

        private void Publishers_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PublishersPage());
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
