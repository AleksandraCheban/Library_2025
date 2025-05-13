using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Library_2025
{
    /// <summary>
    /// Логика взаимодействия для Reviews.xaml
    /// </summary>
    public partial class Reviews : Page
    {
        public Reviews()
        {
            InitializeComponent();
            LoadReviews();
        }

        private void LoadReviews()
        {
            DG.ItemsSource = Library_2025Entities.GetContext().Reviews.ToList();
        }

        private void ButtonEdit_OnClick(object sender, RoutedEventArgs e)
        {
            var review = (sender as Button)?.DataContext as Reviews;
            if (review == null)
            {
                MessageBox.Show("Отзыв не выбран", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NavigationService.Navigate(new AddReviewPage(review));
        }

        private void ButtonDel_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedReviews = DG.SelectedItems.Cast<Reviews>().ToList();

            if (selectedReviews.Count == 0)
            {
                MessageBox.Show("Выберите один или несколько отзывов для удаления.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show($"Вы действительно хотите удалить {selectedReviews.Count} выбранных отзывов?",
                                         "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            try
            {
                var context = Library_2025Entities.GetContext();

                context.Reviews.RemoveRange(selectedReviews);
                context.SaveChanges();

                MessageBox.Show("Данные успешно удалены.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                LoadReviews();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddReviewPage(null));
        }

        private void ButtonReturn_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
