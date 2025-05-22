using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data.Entity;

namespace Library_2025
{
    public partial class AddForOrdersUsers : Page, INotifyPropertyChanged
    {
        private Orders _order;
        private readonly Library_2025Entities _context;
        private decimal _selectedBookCost;
        private int _quantity;

        public event PropertyChangedEventHandler PropertyChanged;

        public decimal SelectedBookCost
        {
            get { return _selectedBookCost; }
            set
            {
                _selectedBookCost = value;
                OnPropertyChanged();
            }
        }

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged();
            }
        }

        public AddForOrdersUsers(Orders selectedOrder)
        {
            InitializeComponent();

            _context = Library_2025Entities.GetContext();
            CmbBooks.ItemsSource = _context.Books.ToList();

            // Загружаем только текущего пользователя
            int currentUserId = PageForClient.AuthenticationService.CurrentUserId;
            var currentUser = _context.Users.FirstOrDefault(u => u.ID_users == currentUserId);
            if (currentUser != null)
            {
                CmbUsers.ItemsSource = new[] { currentUser };
                CmbUsers.SelectedItem = currentUser;
            }

            if (selectedOrder != null)
            {
                _order = selectedOrder;
                // Загружаем данные заказа
                CmbBooks.SelectedItem = _order.Books;
                TbQuantity.Text = _order.Quantity.ToString();
                TbCost.Text = _order.Cost.ToString();
                TbResult.Text = _order.Result.ToString();
            }
            else
            {
                _order = new Orders();
            }

            DataContext = this;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CmbBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbBooks.SelectedItem is Books selectedBook && selectedBook.Costs.HasValue)
            {
                // Устанавливаем стоимость книги в зависимости от выбранной книги
                SelectedBookCost = selectedBook.Costs.Value;
                _order.Cost = selectedBook.Costs.Value;

                // Устанавливаем количество по умолчанию равным 1
                Quantity = 1;
                TbQuantity.Text = Quantity.ToString();
                _order.Quantity = Quantity;

                // Вычисляем результат
                TbResult.Text = (selectedBook.Costs.Value * Quantity).ToString("F2");
                _order.Result = selectedBook.Costs.Value * Quantity;
            }
        }

        private void TbQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Попытка вычислить результат при изменении количества
            if (int.TryParse(TbQuantity.Text, out int quantity) && quantity > 0)
            {
                Quantity = quantity;
                decimal result = quantity * SelectedBookCost;
                TbResult.Text = result.ToString("F2"); // Формат с 2 знаками после запятой
                _order.Result = result;
            }
            else
            {
                TbResult.Text = string.Empty;
            }
        }

        private void ButtonBack_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (CmbBooks.SelectedItem == null)
                errors.AppendLine("Выберите книгу!");

            if (CmbUsers.SelectedItem == null)
                errors.AppendLine("Выберите пользователя!");

            if (Quantity <= 0)
                errors.AppendLine("Введите корректное количество!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            _order.Quantity = Quantity;

            try
            {
                // Убедитесь, что все необходимые данные заполнены
                if (CmbBooks.SelectedItem is Books selectedBook)
                {
                    _order.ID_books = selectedBook.ID_books;
                }

                if (CmbUsers.SelectedItem is Users selectedUser)
                {
                    _order.ID_users = selectedUser.ID_users;
                }

                if (_order.ID_orders == 0)
                {
                    _context.Orders.Add(_order);
                }
                else
                {
                    _context.Entry(_order).State = EntityState.Modified;
                }

                _context.SaveChanges();
                MessageBox.Show("Данные успешно сохранены");

                // Сброс формы после сохранения
                _order = new Orders();
                DataContext = this;
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
