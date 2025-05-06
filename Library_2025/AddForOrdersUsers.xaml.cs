using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Library_2025
{
    public partial class AddForOrdersUsers : Page
    {
        private Orders _order = new Orders();

        public AddForOrdersUsers(Orders selectedOrder)
        {
            InitializeComponent();
            if (selectedOrder != null)
            {
                _order = selectedOrder;
            }
            DataContext = _order;

            // Загрузка списка книг
            var books = Library_2025Entities.GetContext().Books.ToList();
            CmbCategory.ItemsSource = books;
        }

        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (_order.Books == null)
            {
                errors.AppendLine("Выберите книгу!");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            try
            {
                if (_order.ID_orders == 0)
                {
                    Library_2025Entities.GetContext().Orders.Add(_order);
                }

                Library_2025Entities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}\n{ex.InnerException?.Message}");
            }
        }
    }
}
