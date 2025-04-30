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
    /// Логика взаимодействия для AvailabilityPage.xaml
    /// </summary>
    public partial class AvailabilityPage : Page
    {
        private Products _product = new Products();

        public AddProductPage(Products selectedProduct)
        {
            InitializeComponent();
            if (selectedProduct != null)
            {
                _product = selectedProduct;
            }
            DataContext = _product;
            CmbCategory.ItemsSource = Library_2025Entities.GetContext().ProductCategories.ToList();
        }

        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (_product.ProductCategories == null)
            {
                errors.AppendLine("Выберите категорию!");
            }
            if (string.IsNullOrWhiteSpace(_product.NameOfProduct))
            {
                errors.AppendLine("Введите название товара!");
            }
            if (string.IsNullOrWhiteSpace(_product.Brand))
            {
                errors.AppendLine("Введите марку товара!");
            }
            if (string.IsNullOrWhiteSpace(_product.Model))
            {
                errors.AppendLine("Введите модель товара!");
            }
            if (string.IsNullOrWhiteSpace(_product.ManufacturerCountry))
            {
                errors.AppendLine("Введите страну-изготовителя!");
            }
            if (string.IsNullOrWhiteSpace(Convert.ToString(_product.Price)))
            {
                errors.AppendLine("Введите стоимость!");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_product.IDProduct == 0)
            {
                Library_2025Entities.GetContext().Products.Add(_product);
            }

            try
            {
                Library_2025Entities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}