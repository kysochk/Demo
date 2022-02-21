using System;
using System.Collections.Generic;
using System.ComponentModel;
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


namespace Demo.Pages
{
    /// <summary>
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage
    {

        ViewModel VM;
        List<services> ls;
        bool flag = true;
        int FlagCost = 0;
        int zap;
        public ServicePage()
        {
            VM = new ViewModel(0);  //если смертный
            InitializeComponent();
            ServiceList.ItemsSource = VM.service;
            ls = VM.service;
            DiscountCB.SelectedIndex = 0;
            autorizTxt.Text = "Режим: пользователь";
        }

        public ServicePage(int i) //если админ
        {

            InitializeComponent();
            VM = new ViewModel(1);
            ServiceList.ItemsSource = VM.service;
            ls = VM.service;
            DiscountCB.SelectedIndex = 0;
            autorizTxt.Text = "Режим: администратор";
            AddUsBtn.Visibility = Visibility.Visible;
            ZapBtn.Visibility = Visibility.Visible;

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            flag = !flag;
            FlagCost = 1;
            if (flag == false)
            {
                CostSortBtn.Content = "Цене ↑";
            }
            else
            {
                CostSortBtn.Content = "Цене ↓";
            }
            Filter();
        }

        void Filter()
        {

            ls = VM.service;
            zap = ls.Count;
            try
            {
                if (FlagCost == 1)
                {

                    ls = ls.OrderBy(x => x.newcost).ToList();
                    if (flag == false)
                    {
                    }
                    else
                    {
                        ls.Reverse();

                    }
                }
            }
            catch { }

            try
            {
                switch (DiscountCB.SelectedIndex)
                {
                    case 0:
                        break;
                    case 1:  //0-5
                        ls = ls.Where(x => ((0.00 <= x.discount) && (x.discount < 0.04))).ToList();
                        break;
                    case 2:  //5-15
                        ls = ls.Where(x => ((0.05 <= x.discount) && (x.discount < 0.14))).ToList();
                        break;
                    case 3:  //15-30
                        ls = ls.Where(x => ((0.15 <= x.discount) && (x.discount < 0.29))).ToList();
                        break;
                    case 4:     //30-70
                        ls = ls.Where(x => ((0.30 <= x.discount) && (x.discount < 0.69))).ToList();
                        break;
                    case 5:             //70-100
                        ls = ls.Where(x => ((0.70 <= x.discount) && (x.discount < 0.100))).ToList();
                        break;
                }
            }
            catch { }
            try
            {
                ls = ls.Where(x => x.name_service.Contains(SearchBarTxt.Text)).ToList();
            }
            catch
            { }
            ServiceList.ItemsSource = ls;
            info();
        }
        void info()
        {
            kol_voZap.Text = "Количество записей на странице: " + ls.Count + " из " + zap;
        }
        private void DiscountCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void SearchBarTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void DeleteService_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button IdBtn = (Button)sender;
                int id = Convert.ToInt32(IdBtn.Uid);
                services ServicesRemove = BaseConnect.BaseModel.services.FirstOrDefault(x => x.id_service == id);
                BaseConnect.BaseModel.services.Remove(ServicesRemove);
                BaseConnect.BaseModel.SaveChanges();
                VM = new ViewModel(1);
                MessageBox.Show("Запись успешно удалена.");
                Filter();
            }
            catch
            {
                MessageBox.Show("Присутствует запись на данную услугу.\nДанную запись нельзя удалить.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void autorizBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.Navigate(new Autorizacia());
        }

        private void AddService_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.Navigate(new UpdateAdd());
        }

        private void NewZapis_Click(object sender, RoutedEventArgs e)
        {

            Button btn = (Button)sender;
            int id = Convert.ToInt32(btn.Uid);
            LoadPages.MainFrame.Navigate(new NewZap(id));

        }

        private void EditService_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int id = Convert.ToInt32(btn.Uid);
            LoadPages.MainFrame.Navigate(new UpdateAdd(id));
        }

        private void NextZap_Click(object sender, RoutedEventArgs e)
        {
            new NextZapis().ShowDialog();
        }
    }


}
