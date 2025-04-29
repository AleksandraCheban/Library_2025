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
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        public UsersPage()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            DataGridUsers.ItemsSource = Library_2025Entities.GetContext().Users
                .Select(u => new
                {
                    
                    
                    u.Login,
                    //Role = u.RoleNavigation.Name, // Предполагается, что есть навигационное свойство для роли
                    u.E_mail
                }).ToList();
        }

        private void ButtonEdit_OnClick(object sender, RoutedEventArgs e)
        {
            // Логика редактирования пользователя
        }

        //private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
        //{
        //    NavigationService.Navigate(new AddUser ,Page());
        //}

        private void ButtonDel_OnClick(object sender, RoutedEventArgs e)
        {
            // Логика удаления пользователя
        }
    }
}