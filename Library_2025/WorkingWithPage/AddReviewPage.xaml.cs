using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Library_2025
{
    /// <summary>
    /// Логика взаимодействия для AddReviewPage.xaml
    /// </summary>
    public partial class AddReviewPage : Page
    {
        private Reviews _currentReview;

        public AddReviewPage(Reviews review = null)
        {
            InitializeComponent();

            // Загрузка списка книг
            var books = Library_2025Entities.GetContext().Books.ToList();
            CmbBooks.ItemsSource = books;

            if (review != null)
                _currentReview = review;
            else
                _currentReview = new Reviews();

            DataContext = _currentReview;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            var selectedBook = CmbBooks.SelectedItem as Books;
            if (selectedBook == null)
                errors.AppendLine("Выберите книгу!");

            if (string.IsNullOrEmpty(TxtBox_ReviewText.Text.Trim()))
                errors.AppendLine("Введите текст отзыва");

            if (CmbRating.SelectedItem == null)
                errors.AppendLine("Выберите рейтинг");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var db = new Library_2025Entities())
                {
                    _currentReview.BookID = selectedBook.ID_books;
                    _currentReview.ReviewText = TxtBox_ReviewText.Text.Trim();
                    _currentReview.Rating = int.Parse(CmbRating.SelectedItem.ToString());
                    _currentReview.ReviewDate = DateTime.Now;

                    if (_currentReview.ReviewID == 0)
                        db.Reviews.Add(_currentReview);

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
