//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Windows;
//using System.Windows.Controls;

//namespace Library_2025
//{
//    public partial class AddForOrdersUsers : Page
//    {
//        private Orders _order = new Orders();

//        public AddForOrdersUsers(Orders selectedOrder)
//        {
//            InitializeComponent();
//            if (selectedOrder != null)
//            {
//                _order = selectedOrder;
//            }
//            DataContext = _order;

//            // Загрузка списка книг
//            var books = Library_2025Entities.GetContext().Books.ToList();
//            CmbCategory.ItemsSource = books;
//        }

//        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
//        {
//            StringBuilder errors = new StringBuilder();

//            if (_order.Books == null)
//            {
//                errors.AppendLine("Выберите книгу!");
//            }

//            if (errors.Length > 0)
//            {
//                MessageBox.Show(errors.ToString());
//                return;
//            }

//            try
//            {
//                if (_order.ID_orders == 0)
//                {
//                    Library_2025Entities.GetContext().Orders.Add(_order);
//                }

//                Library_2025Entities.GetContext().SaveChanges();
//                MessageBox.Show("Данные успешно сохранены");
//                NavigationService.GoBack();
//            }
//            catch (Exception ex)
//            {
//                // Логирование ошибки
//                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}\n{ex.InnerException?.Message}");
//            }
//        }
//    }
//}

using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Library_2025
{
    public partial class AddForOrdersUsers : Page
    {
        private Orders _order;

        public AddForOrdersUsers(Orders selectedOrder)
        {
            InitializeComponent();

            var context = Library_2025Entities.GetContext();
            CmbBooks.ItemsSource = context.Books.ToList();
            CmbUsers.ItemsSource = context.Users.ToList();

            if (selectedOrder != null)
            {
                _order = selectedOrder;
                CmbBooks.SelectedItem = _order.Books;
                CmbUsers.SelectedItem = _order.Users;
                TbQuantity.Text = _order.Quantity?.ToString();
                TbCost.Text = _order.Cost?.ToString();
                TbResult.Text = _order.Result?.ToString();
            }
            else
            {
                _order = new Orders();
            }
        }

        private void TbQuantityOrCost_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Попытка вычислить результат при изменении количества или стоимости
            if (int.TryParse(TbQuantity.Text, out int quantity) && quantity > 0 &&
                decimal.TryParse(TbCost.Text, out decimal cost) && cost >= 0)
            {
                decimal result = quantity * cost;
                TbResult.Text = result.ToString("F2"); // Формат с 2 знаками после запятой
            }
            else
            {
                TbResult.Text = string.Empty;
            }
        }

        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            var selectedBook = CmbBooks.SelectedItem as Books;
            if (selectedBook == null)
                errors.AppendLine("Выберите книгу!");

            var selectedUser = CmbUsers.SelectedItem as Users;
            if (selectedUser == null)
                errors.AppendLine("Выберите пользователя!");

            if (!int.TryParse(TbQuantity.Text, out int quantity) || quantity <= 0)
                errors.AppendLine("Введите корректное количество!");

            if (!decimal.TryParse(TbCost.Text, out decimal cost) || cost < 0)
                errors.AppendLine("Введите корректную стоимость!");

            if (!decimal.TryParse(TbResult.Text, out decimal result) || result < 0)
                errors.AppendLine("Результат вычислен некорректно!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            _order.ID_books = selectedBook.ID_books;
            _order.ID_users = selectedUser.ID_users;
            _order.Quantity = quantity;
            _order.Cost = cost;
            _order.Result = result;

            try
            {
                var context = Library_2025Entities.GetContext();
                if (_order.ID_orders == 0)
                {
                    context.Orders.Add(_order);
                }
                context.SaveChanges();
                MessageBox.Show("Данные успешно сохранены");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}\n{ex.InnerException?.Message}");
            }
        }
    }
}
