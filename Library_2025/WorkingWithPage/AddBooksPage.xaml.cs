using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Library_2025
{
    public partial class AddBooksPage : Page
    {
        private Books _book;
        private readonly Library_2025Entities _context;

        public AddBooksPage(Books selectedBook)
        {
            InitializeComponent();

            _context = Library_2025Entities.GetContext();
            CmbAuthors.ItemsSource = _context.Authors.ToList();
            CmbGenres.ItemsSource = _context.Genres.ToList();
            CmbLanguages.ItemsSource = _context.Languages.ToList();
            CmbPublishers.ItemsSource = _context.Publishers.ToList();

            if (selectedBook != null)
            {
                _book = selectedBook;
            }
            else
            {
                _book = new Books();
            }

            DataContext = _book;
        }

        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(TbBookName.Text))
                errors.AppendLine("Введите название книги!");

            if (CmbAuthors.SelectedItem == null)
                errors.AppendLine("Выберите автора!");

            if (CmbGenres.SelectedItem == null)
                errors.AppendLine("Выберите жанр!");

            if (CmbLanguages.SelectedItem == null)
                errors.AppendLine("Выберите язык!");

            if (CmbPublishers.SelectedItem == null)
                errors.AppendLine("Выберите издателя!");

            if (!decimal.TryParse(TbCost.Text, out decimal cost) || cost < 0)
                errors.AppendLine("Введите корректную стоимость!");

            if (!decimal.TryParse(TbRating.Text, out decimal rating) || rating < 0 || rating > 10)
                errors.AppendLine("Введите корректный рейтинг (от 0 до 10)!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            _book.Name = TbBookName.Text;
            _book.ID_authors = (CmbAuthors.SelectedItem as Authors).ID_authors;
            _book.ID_genre = (CmbGenres.SelectedItem as Genres).ID_genre;
            _book.ID_languages = (CmbLanguages.SelectedItem as Languages).ID_languages;
            _book.ID_publishers = (CmbPublishers.SelectedItem as Publishers).ID_publishers;
            _book.Costs = cost;
            _book.Rating = rating;
            _book.Photo = TbPhoto.Text; // Сохранение URL изображения

            try
            {
                if (_book.ID_books == 0)
                {
                    _context.Books.Add(_book);
                }
                _context.SaveChanges();
                MessageBox.Show("Данные успешно сохранены");

                // Сброс формы после сохранения
                _book = new Books();
                DataContext = _book;
                TbBookName.Text = string.Empty;
                CmbAuthors.SelectedItem = null;
                CmbGenres.SelectedItem = null;
                CmbLanguages.SelectedItem = null;
                CmbPublishers.SelectedItem = null;
                TbCost.Text = string.Empty;
                TbRating.Text = string.Empty;
                TbPhoto.Text = string.Empty;

                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}\n{ex.InnerException?.Message}");
            }
        }

        private void ButtonBack_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
