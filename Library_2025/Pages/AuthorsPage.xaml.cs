using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Library_2025
{
    public partial class AuthorsPage : Page
    {
        public AuthorsPage()
        {
            InitializeComponent();
            LoadAuthors();
        }

        private void LoadAuthors()
        {
            AuthorsDataGrid.ItemsSource = Library_2025Entities.GetContext().Authors
                .Select(a => new
                {
                    a.ID_authors,
                    a.Surname,
                    a.Name,
                    a.Nickname,
                    a.Years_of_life
                }).ToList();
        }



        private void ButtonReturn_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ButtonAddAuthor_OnClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
