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
            //var context = Library_2025Entities.GetContext();
            //var orders = context.Orders
            //    .Include(o => o.Books) // Убедитесь, что вы загружаете связанные данные
            //    .Select(o => new
            //    {
            //        BookName = o.Books.Name, // Используйте свойство Name из связанной сущности Books
            //        o.Cost,
            //        o.Quantity,
            //        o.Result
            //    })
            //    .ToList();

            //DataGridOrders.ItemsSource = orders;
        }

        private void LoadOrders()
        {

            DataGridOrders.ItemsSource = Library_2025Entities.GetContext().Orders
                .Select(o => new
                {
                    BookName = o.Books.Name, // Предполагается, что у вас есть навигационное свойство Book и у Book есть свойство Name
                    UserLogin = o.Users.Login, // Предполагается, что у вас есть навигационное свойство User и у User есть свойство Login
                    o.Cost,
                    o.Quantity,
                    o.Result
                }).ToList();
            //var context = Library_2025Entities.GetContext();
            //DataGridOrders.ItemsSource = context.Orders
            //    .Include(o => o.Books) // Загружаем связанные данные для книг
            //    .Include(o => o.Users) // Загружаем связанные данные для пользователей
            //    .Select(o => new
            //    {
            //        BookName = o.Books.Name, // Используем навигационное свойство Books и свойство Name
            //        UserLogin = o.Users.Login, // Используем навигационное свойство Users и свойство Login
            //        o.Cost,
            //        o.Quantity,
            //        o.Result
            //    })
            //    .ToList();
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