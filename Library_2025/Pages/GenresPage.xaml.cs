using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Library_2025
{
    public partial class GenresPage : Page
    {
        public GenresPage()
        {
            InitializeComponent();
            LoadGenres();
        }

        private void LoadGenres()
        {
            GenresDataGrid.ItemsSource = Library_2025Entities.GetContext().Genres
                .Select(g => new
                {
                    g.ID_genre,
                    g.Genre
                }).ToList();
        }

        private void ButtonReturn_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
