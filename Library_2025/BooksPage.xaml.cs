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
    /// Логика взаимодействия для BooksPage.xaml
    /// </summary>
    public partial class BooksPage : Page
    {
        public BooksPage()
        {
            InitializeComponent();
            LoadBooks();
        }

        private void LoadBooks()
        {
            DataGridBooks.ItemsSource = Entities.GetContext().Books
                .Select(b => new
                {
                    b.Name,
                    //b.Author, // Предполагается, что есть навигационное свойство для автора
                    //Genre = b.Genre, // Предполагается, что есть навигационное свойство для жанра
                    //Language = b.Language, // Предполагается, что есть навигационное свойство для языка
                    //Publisher = b.Publisher, // Предполагается, что есть навигационное свойство для издателя
                    b.Costs,
                    b.Rating
                }).ToList();
        }

        private void ButtonEdit_OnClick(object sender, RoutedEventArgs e)
        {
            // Логика редактирования книги
        }

        //private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
        //{
        //    NavigationService.Navigate(new AddBookPage());
        //}

        private void ButtonDel_OnClick(object sender, RoutedEventArgs e)
        {
            // Логика удаления книги
        }
    }
}