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
    /// Логика взаимодействия для PublishersPage.xaml
    /// </summary>
    public partial class PublishersPage : Page
    {
        public PublishersPage()
        {
            InitializeComponent();
            LoadPublishers();
        }

        private void LoadPublishers()
        {
            DataGridPublishers.ItemsSource = Entities.GetContext().Publishers
                .Select(p => new
                {
                    p.ID_publishers,
                    p.Title
                }).ToList();
        }

        private void ButtonEdit_OnClick(object sender, RoutedEventArgs e)
        {
            // Логика редактирования издателя
        }

        //private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
        //{
         //   NavigationService.Navigate(new AddPublisherPage());
        //}

        private void ButtonDel_OnClick(object sender, RoutedEventArgs e)
        {
            // Логика удаления издателя
        }
    }
}
