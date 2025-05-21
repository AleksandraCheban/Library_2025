using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Library_2025
{
    /// <summary>
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        private List<Users> _allUsers;

        public UsersPage()
        {
            InitializeComponent();
            LoadUsers();
        }
        private void UsersChange_IsVisibliChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                LoadUsers();
            }
        }
        private void LoadUsers()
        {
            _allUsers = Library_2025Entities.GetContext().Users.ToList();
            DataGridUsers.ItemsSource = _allUsers;
        }

        private void SearchUserLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateUsers();
        }

        private void SortUserRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateUsers();
        }

        private void UpdateUsers()
        {
            var filteredUsers = _allUsers.AsQueryable();

            if (!string.IsNullOrEmpty(SearchUserLogin.Text))
            {
                filteredUsers = filteredUsers.Where(u => u.Login.ToLower().Contains(SearchUserLogin.Text.ToLower()));
            }

            if (SortUserRole.SelectedIndex > 0)
            {
                string selectedRole = (SortUserRole.SelectedItem as ComboBoxItem)?.Content.ToString();
                int roleId = selectedRole == "Администратор" ? 1 : 2;
                filteredUsers = filteredUsers.Where(u => u.Role == roleId);
            }

            DataGridUsers.ItemsSource = filteredUsers.ToList();
        }

        private void CleanFilter_Click(object sender, RoutedEventArgs e)
        {
            SearchUserLogin.Text = "";
            SortUserRole.SelectedIndex = 0;
            DataGridUsers.ItemsSource = _allUsers;
        }

        private void ButtonEdit_OnClick(object sender, RoutedEventArgs e)
        {
            var user = (sender as Button)?.DataContext as Users;
            if (user == null)
            {
                MessageBox.Show("Пользователь не выбран", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NavigationService.Navigate(new AddUserPage(user));
        }

        private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddUserPage(null));
        }

        private void ButtonDel_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedUsers = DataGridUsers.SelectedItems.Cast<Users>().ToList();

            if (selectedUsers.Count == 0)
            {
                MessageBox.Show("Выберите одного или нескольких пользователей для удаления.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Вы действительно хотите удалить {selectedUsers.Count} выбранных пользователей?",
                                         "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            try
            {
                var context = Library_2025Entities.GetContext();

                context.Users.RemoveRange(selectedUsers);
                context.SaveChanges();

                MessageBox.Show("Данные успешно удалены.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonReturn_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
