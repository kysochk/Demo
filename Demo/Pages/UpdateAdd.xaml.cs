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
using DemoDll;
using Demo.Pages;
using Microsoft.Win32;
using System.IO;

namespace Demo
{
    /// <summary>
    /// Логика взаимодействия для UpdateAdd.xaml
    /// </summary>
    public partial class UpdateAdd
    {
        int index = 0;
        string img = "\\null\\null";
        public UpdateAdd()
        {
            InitializeComponent();
            SaveBtn.Visibility = Visibility.Collapsed;
            AddImgBtn.Visibility = Visibility.Visible;
        }

        public UpdateAdd(int i)
        {
            InitializeComponent();
            services services = BaseConnect.BaseModel.services.FirstOrDefault(x => x.id_service == i);
            DataContext = services;
            index = i;


            AddBtn.Visibility = Visibility.Collapsed;

        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDouble(DurationTxt.Text) / 60 > 4)
            {
                DurationTxt.Text = "240";
                MessageBox.Show("Время урока не может привышать 4 часов (240 минут)!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
            {
                try
                {
                    DiscountTxt.Text = DiscountTxt.Text.Replace(".", ",");
                    CostTxt.Text = CostTxt.Text.Replace(".", ",");

                    double sale = Convert.ToDouble(DiscountTxt.Text);
                    services upService = BaseConnect.BaseModel.services.FirstOrDefault(x => x.id_service == index);
                    upService.cost = Convert.ToDecimal(CostTxt.Text);
                    if (sale > 1)
                        sale = sale / 100;
                    upService.discount = sale;
                    upService.duration = Convert.ToInt32(DurationTxt.Text);
                    BaseConnect.BaseModel.SaveChanges();
                    MessageBox.Show("Изменения успешно применены!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {

            BaseConnect.BaseModel = new Entities();
            LoadPages.MainFrame.Navigate(new ServicePage(1));

        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDouble(DurationTxt.Text) / 60 > 4)
            {
                DurationTxt.Text = "240";
                MessageBox.Show("Время урока не может привышать 4 часов (240 минут)!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
            {
                BaseConnect.BaseModel = new Entities();
                DiscountTxt.Text = DiscountTxt.Text.Replace(".", ",");
                DiscountTxt.Text = (Convert.ToDouble(DiscountTxt.Text) / 100).ToString();
                CostTxt.Text = CostTxt.Text.Replace(".", ",");
                int i = 0;
                foreach (services s in BaseConnect.BaseModel.services.ToList())
                {
                    if (s.name_service == NameServiceTxt.Text)
                    {
                        MessageBox.Show("Такая услуга уже сущетсвует в системе");
                        i = 0;
                        break;
                    }
                    else
                    {
                        i = 1;
                    }
                }

                if (i == 1)
                {
                    services newService = new services() { name_service = NameServiceTxt.Text, duration = Convert.ToInt32(DurationTxt.Text), discount = Convert.ToDouble(DiscountTxt.Text), cost = Convert.ToDecimal(CostTxt.Text), img = img };
                    BaseConnect.BaseModel.services.Add(newService);
                    BaseConnect.BaseModel.SaveChanges();
                }
            }
        }
        private void AddImgBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".jpg"; // задаем расширение по умолчанию
            openFileDialog.Filter = "Изображения |*.jpg;*.png"; // задаем фильтр на форматы файлов
            var result = openFileDialog.ShowDialog();

            if (result == true)//если файл выбран
            {

                img = openFileDialog.FileName;
                imgBox.Source = BitmapFrame.Create(new Uri(img));
            }
        }
        string goodText;
        private void DurationTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (double.TryParse(tb.Text, out double value))
                goodText = tb.Text;
            else
            {
                tb.Text = goodText;
                tb.CaretIndex = tb.Text.Length;
            }

        }
    }
}
