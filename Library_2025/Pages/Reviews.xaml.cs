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
            DG.ItemsSource = Library_2025Entities.GetContext().Reviews
                .Select(o => new
                {
                    BookName = o.Books.Name, // Предполагается, что у вас есть навигационное свойство Book и у Book есть свойство Name
                    UserLogin = o.Users.Login, // Предполагается, что у вас есть навигационное свойство User и у User есть свойство Login
                    o.ReviewText,
                    o.Rating,
                    o.ReviewDate
                }).ToList();
        }

        private void ButtonEdit_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var review = button.DataContext as Library_2025.Reviews; // Используйте полное имя класса
                if (review == null)
                {
                    MessageBox.Show("Отзыв не выбран", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                NavigationService.Navigate(new AddReviewPage(review));
            }
        }

        private void ButtonDel_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedReviews = DG.SelectedItems.Cast<Library_2025.Reviews>().ToList(); // Используйте полное имя класса

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

