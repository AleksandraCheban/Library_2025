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
        public MyOrders()
        {
            InitializeComponent();
            LoadOrders();

        }

        private void LoadOrders()
        {
            ////var context = Library_2025Entities.GetContext();

            ////DataGridOrders.ItemsSource = context.Orders
            ////    .Join(context.Books,
            ////        o => o.ID_books,
            ////        b => b.ID_books,
            ////        (o, b) => new { Order = o, Book = b })
            ////    .Join(context.Users,
            ////        x => x.Order.ID_users,
            ////        u => u.ID_users,
            ////        (x, u) => new
            ////        {
            ////            BookName = x.Book.Name,
            ////            UserLogin = u.Login,
            ////            x.Order.Cost,
            ////            x.Order.Quantity,
            ////            x.Order.Result
            ////        })
            ////    .ToList();
        }


        private void OrderChange_IsVisibliChange(object sender, DependencyPropertyChangedEventArgs e)
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