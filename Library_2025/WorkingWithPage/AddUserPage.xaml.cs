using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Library_2025
{
    /// <summary>
    /// Логика взаимодействия для AddUserPage.xaml
    /// </summary>
    public partial class AddUserPage : Page
    {
        private Users _currentUser;

        public AddUserPage(Users user = null)
        {
            InitializeComponent();

            if (user != null)
                _currentUser = user;
            else
                _currentUser = new Users();

            DataContext = _currentUser;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(TxtBox_Login.Text.Trim()))
                errors.AppendLine("Введите логин");
            if (string.IsNullOrEmpty(PwdBox_Password.Password))
                errors.AppendLine("Введите пароль");
            if (string.IsNullOrEmpty(TxtBox_Email.Text.Trim()))
                errors.AppendLine("Введите email");
            if (CmbRole.SelectedItem == null)
                errors.AppendLine("Выберите роль");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var db = new Library_2025Entities())
                {
                    _currentUser.Login = TxtBox_Login.Text.Trim();
                    _currentUser.Password = PwdBox_Password.Password;
                    _currentUser.E_mail = TxtBox_Email.Text.Trim();

                    // Проверка и преобразование роли
                    if (CmbRole.SelectedItem is ComboBoxItem selectedItem && int.TryParse(selectedItem.Content.ToString(), out int role))
                    {
                        _currentUser.Role = role;
                    }
                    else
                    {
                        MessageBox.Show("Некорректное значение роли", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (_currentUser.ID_users == 0)
                        db.Users.Add(_currentUser);

                    db.SaveChanges();

                    MessageBox.Show("Данные успешно сохранены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
