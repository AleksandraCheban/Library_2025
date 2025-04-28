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

    public partial class BooksPage : Page
    {
        // <summary>
        /// Логика взаимодействия для ProductsPage.xaml
        /// </summary>

            public BooksPage()
            {
                InitializeComponent();
                DataGridProducts.ItemsSource = Library_2025Entities.GetContext().Books.ToList();
            }

            private void ButtonEdit_OnClick(object sender, RoutedEventArgs e)
            {
                // Логика для редактирования продукта
                // Например, можно открыть страницу редактирования с выбранным продуктом
                var selectedProduct = DataGridProducts.SelectedItem as Books;
                if (selectedProduct != null)
                {
                    //NavigationService.Navigate(new EditProductPage(selectedProduct)); 
                }
            }

            private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
            {
                //NavigationService.Navigate(new AddProductPage());
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