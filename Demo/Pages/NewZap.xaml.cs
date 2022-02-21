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
using System.Text.RegularExpressions;

namespace Demo
{
    /// <summary>
    /// Логика взаимодействия для NewZap.xaml
    /// </summary>
    public partial class NewZap
    {
        int identif;
        public NewZap(int i)
        {
            InitializeComponent();
            services services = BaseConnect.BaseModel.services.FirstOrDefault(x => x.id_service == i);
            DataContext = services;
            ClientCm.ItemsSource = BaseConnect.BaseModel.clients.ToList();
            ClientCm.SelectedValuePath = "id_clients";
            ClientCm.DisplayMemberPath = "fullname";
            identif = i;
            DatePick.SelectedDate = DateTime.Now;

        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            int checker = 0;
            string date = DatePick.Text + " " + TimeTxt.Text;
            DateTime dated = Convert.ToDateTime(date);

            Regex r = new Regex("([0][8,9]|[1][0-9]|[2][0]):[0-5][0-9]");
            try
            {
                if (r.IsMatch(TimeTxt.Text))
                {
                    foreach (service_client s in BaseConnect.BaseModel.service_client.ToList())
                    {
                        if ((s.date == dated && s.id_service == identif) || (dated <= DateTime.Now) || (s.date == dated && s.id_clients == (int)ClientCm.SelectedValue))
                        {

                            checker = 0;
                            break;
                        }
                        else
                        {
                            checker++;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Введено не верное время.\n Часы работы школы с 8:00 до 20:00\nФормат записи должен быть следующим: 08:00", "Ошибка записи!", MessageBoxButton.OK, MessageBoxImage.Error);

                }

                if (checker > 0)
                {
                    service_client newEntry = new service_client() { id_service = identif, id_clients = (int)ClientCm.SelectedValue, date = Convert.ToDateTime(date) };
                    BaseConnect.BaseModel.service_client.Add(newEntry);
                    BaseConnect.BaseModel.SaveChanges();
                    MessageBox.Show("Запись добавлена");
                }
                else
                {
                    MessageBox.Show("Данное время не подходит для записи.");

                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка. Повторите попытку позже");
            }

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.Navigate(new ServicePage(1));
        }

        private void TimeTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            TimeEnd();
        }

        void TimeEnd()
        {
            try
            {
                TimeEndTxt.Text = (Convert.ToDateTime(TimeTxt.Text).AddMinutes(Convert.ToDouble(DurationTxt.Text))).ToString("HH:mm");
            }
            catch
            {

            }
        }

        private void DatePick_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            TimeEnd();
        }
    }
}
