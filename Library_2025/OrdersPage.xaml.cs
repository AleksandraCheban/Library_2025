﻿using System;
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
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        public OrdersPage()
        {
            InitializeComponent();
            LoadOrders();
        }

        private void LoadOrders()
        {
            DataGridOrders.ItemsSource = Entities.GetContext().Orders
                .Select(o => new
                {
                    o.ID_orders,
                    //Book = o.Book, // Предполагается, что есть навигационное свойство для книги
                    //User = o.User, // Предполагается, что есть навигационное свойство для пользователя
                    o.Cost,
                    o.Quantity,
                    o.Result
                }).ToList();
        }

        private void ButtonEdit_OnClick(object sender, RoutedEventArgs e)
        {
            // Логика редактирования заказа
        }

        //private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
        //{
        //    NavigationService.Navigate(new AddOrderPage());
        //}

        private void ButtonDel_OnClick(object sender, RoutedEventArgs e)
        {
            // Логика удаления заказа
        }
    }
}