using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Library_2025
{
    public partial class LanguagesPage : Page
    {
        public LanguagesPage()
        {
            InitializeComponent();
            LoadLanguages();
        }

        private void LoadLanguages()
        {
            LanguagesDataGrid.ItemsSource = Library_2025Entities.GetContext().Languages
                .Select(l => new
                {
                    l.ID_languages,
                    l.Title
                }).ToList();
        }

        private void ButtonReturn_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
