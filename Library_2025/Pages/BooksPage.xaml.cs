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

namespace Library_2025
{
    public partial class BooksPage : Page
    {
        public BooksPage()
        {
            InitializeComponent();

            // Загрузка данных с использованием Include для связанных таблиц
            var context = Library_2025Entities.GetContext();
            var books = context.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .Include(b => b.Languages)
                .Include(b => b.Publishers)
                .ToList();

            DataGridProducts.ItemsSource = books;

           
        }

        private void ButtonEdit_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddForOrdersUsers(null));
        }


        private void ButtonReturnToMain_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
