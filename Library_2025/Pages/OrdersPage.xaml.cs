using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        public OrdersPage()
        {
            InitializeComponent();
           
        }

        private void Order1Change_IsVisibliChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Library_2025Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                DataGridOrders.ItemsSource = Library_2025Entities.GetContext().Orders.ToList();
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
                    DataGridOrders.ItemsSource = context.Orders.ToList();
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