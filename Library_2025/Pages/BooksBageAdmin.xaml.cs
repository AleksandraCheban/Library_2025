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
    /// Логика взаимодействия для BooksBageAdmin.xaml
    /// </summary>
    public partial class BooksBageAdmin : Page
    {
        public BooksBageAdmin()
        {
            InitializeComponent();
            DataGridProducts.ItemsSource = Library_2025Entities.GetContext().Books.ToList();
        }
        private void ButtonEdit_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedProduct = DataGridProducts.SelectedItem as Books;
            if (selectedProduct != null)
            {
                try
                {
                    // Переход на страницу редактирования с выбранной книгой
                    NavigationService.Navigate(new AddBooksPage(selectedProduct));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при переходе на страницу редактирования: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Выберите книгу для редактирования.");
            }
        }


        private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // Переход на страницу добавления с пустыми полями
                NavigationService.Navigate(new AddBooksPage(null));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при переходе на страницу добавления: {ex.Message}");
            }
        }


        private void ButtonDel_OnClick(object sender, RoutedEventArgs e)
        {
            // Логика для удаления продукта
            var selectedProduct = DataGridProducts.SelectedItem as Books;
            if (selectedProduct != null)
            {
                var context = Library_2025Entities.GetContext();
                context.Books.Remove(selectedProduct);
                context.SaveChanges();
                DataGridProducts.ItemsSource = context.Books.ToList();
            }
        }

        private void ButtonReturnToMain_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}