using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Library_2025
{
    public partial class PublishersPage : Page
    {
        public PublishersPage()
        {
            InitializeComponent();
            LoadPublishers();
        }

        private void LoadPublishers()
        {
            PublishersDataGrid.ItemsSource = Library_2025Entities.GetContext().Publishers
                .Select(p => new
                {
                    p.ID_publishers,
                    p.Title
                }).ToList();
        }

        private void ButtonReturn_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
