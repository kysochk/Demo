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
using System.Windows.Shapes;
using System.Windows.Threading;
using DemoDll;

namespace Demo
{
    /// <summary>
    /// Логика взаимодействия для NextZapis.xaml
    /// </summary>
    public partial class NextZapis : Window
    {
        ViewModel VM = new ViewModel();

        public NextZapis()
        {
            InitializeComponent();
            lbEntry.ItemsSource = VM.createSC();
            tbTime.Text = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(30);
            timer.Tick += timer_Tick;
            timer.Start();

        }

        void timer_Tick(object sender, EventArgs e)
        {

            lbEntry.ItemsSource = VM.createSC();
            tbTime.Text = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
        }


    }
}
