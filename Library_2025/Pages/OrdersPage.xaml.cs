using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Library_2025
{
    public partial class OrdersPage : Page
    {
        private List<Users> _users;
        private Users _selectedUser;
        private Library_2025Entities _context;

        public Users SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                UpdateOrders();
            }
        }

        public OrdersPage()
        {
            InitializeComponent();
            _context = Library_2025Entities.GetContext();
            LoadUsers();
        }

        private void LoadUsers()
        {
            _users = _context.Users.ToList();
            CmbUsers.ItemsSource = _users;
        }

        private void Order1Change_IsVisibliChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                _context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                UpdateOrders();
            }
        }

        private void CmbUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbUsers.SelectedItem is Users selectedUser)
            {
                SelectedUser = selectedUser;
            }
        }

        private void UpdateOrders()
        {
            IQueryable<Orders> orders = _context.Orders.Include("Books").Include("Users");

            if (_selectedUser != null)
            {
                orders = orders.Where(o => o.ID_users == _selectedUser.ID_users);
            }

            DataGridOrders.ItemsSource = orders.ToList();
        }

        private void ButtonEdit_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedOrder = DataGridOrders.SelectedItem as Orders;
            if (selectedOrder != null)
            {
                NavigationService.Navigate(new AddOrdersForAdmin(selectedOrder));
            }
        }

        private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddOrdersForAdmin(null));
        }

        private void ButtonDel_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedOrder = DataGridOrders.SelectedItem as Orders;
            if (selectedOrder != null)
            {
                try
                {
                    _context.Orders.Remove(selectedOrder);
                    _context.SaveChanges();
                    UpdateOrders();
                }
                catch (DbUpdateException ex)
                {
                    // Получение внутреннего исключения
                    var innerException = ex.InnerException;
                    while (innerException != null)
                    {
                        MessageBox.Show($"Ошибка при удалении данных: {innerException.Message}");
                        innerException = innerException.InnerException;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении данных: {ex.Message}");
                }
            }
        }

        private void ButtonReturnToMain_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
