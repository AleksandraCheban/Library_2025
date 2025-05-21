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
using System.Data.Entity.Infrastructure;

namespace Library_2025
{
    public partial class MyOrders : Page
    {
        private int _currentUserId; // Предположим, что это идентификатор текущего пользователя

        public MyOrders(int currentUserId)
        {
            InitializeComponent();
            _currentUserId = currentUserId;
           

        }

        private void OrderChange_IsVisibliChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                var context = Library_2025Entities.GetContext();
                Library_2025Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(x => x.Reload());

                // Фильтруем заказы по идентификатору текущего пользователя
                DataGridOrders.ItemsSource = context.Orders
                    .Where(o => o.ID_users == _currentUserId)
                    .Include(o => o.Books)
                    .Include(o => o.Users)
                    .ToList();
            }
        }

        private void ButtonEdit_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedOrder = DataGridOrders.SelectedItem as Orders;
            if (selectedOrder != null)
            {
                NavigationService.Navigate(new AddForOrdersUsers(selectedOrder));
            }
        }

        private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddForOrdersUsers(null));
        }

        private void ButtonDel_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedOrder = DataGridOrders.SelectedItem as Orders;
            if (selectedOrder != null)
            {
                try
                {
                    var context = Library_2025Entities.GetContext();
                    context.Orders.Remove(selectedOrder);
                    context.SaveChanges();

                    // Обновляем список заказов после удаления
                    DataGridOrders.ItemsSource = context.Orders
                        .Where(o => o.ID_users == _currentUserId)
                        .Include(o => o.Books)
                        .Include(o => o.Users)
                        .ToList();
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
