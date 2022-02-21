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
using Demo.Pages;
using DemoDll;

namespace Demo
{
    /// <summary>
    /// Логика взаимодействия для Autorizacia.xaml
    /// </summary>
    public partial class Autorizacia
    {
        public Autorizacia()
        {
            InitializeComponent();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.GoBack();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (loginTxt.Text == "0000")
            {
                MessageBox.Show("Вы вошли в режим администратора.", "Успешная авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadPages.MainFrame.Navigate(new ServicePage(1));
            }
            else
            {
                MessageBox.Show("Ошибка ввода пароля!\nПовторите попытку.", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
