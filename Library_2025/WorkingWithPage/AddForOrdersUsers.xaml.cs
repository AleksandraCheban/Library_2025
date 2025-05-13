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
            }
            else
            {
                _order = new Orders();
            }

            DataContext = _order;
        }

        //private void BooksPage_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    if (Visibility == Visibility.Visible)
        //    {
        //        Library_2025Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        //        DataGridProducts.ItemsSourde = Library_2025Entities.GetContext().Products.ToList();
        //    }
        //}


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

            if (_order.Books == null)
                errors.AppendLine("Выберите книгу!");

            if (_order.Users == null)
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

                // Сброс формы после сохранения
                _order = new Orders();
                DataContext = _order;
                CmbBooks.SelectedItem = null;
                CmbUsers.SelectedItem = null;
                TbQuantity.Text = string.Empty;
                TbCost.Text = string.Empty;
                TbResult.Text = string.Empty;

                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}\n{ex.InnerException?.Message}");
            }
        }
    }
}
