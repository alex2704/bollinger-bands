using ClassLibrary;
using DataController;
using DataRepresentor;
using Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task7
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DataRepresentorController RepresentController { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            RepresentController = new DataRocController(20); 
            DataContext = this;
        }

        public static readonly DependencyProperty ChartColorProperty =
            DependencyProperty.Register("ChartColor", typeof(Brush), typeof(MainWindow));

        public static readonly DependencyProperty LastTimeProperty =
            DependencyProperty.Register("LastTime", typeof(string), typeof(MainWindow));

        public static readonly DependencyProperty OpenProperty =
            DependencyProperty.Register("OpenValue", typeof(decimal), typeof(MainWindow));

        public static readonly DependencyProperty CloseProperty =
            DependencyProperty.Register("CloseValue", typeof(decimal), typeof(MainWindow));

        public static readonly DependencyProperty LowProperty =
            DependencyProperty.Register("LowValue", typeof(decimal), typeof(MainWindow));

        public static readonly DependencyProperty HighProperty =
            DependencyProperty.Register("HighValue", typeof(decimal), typeof(MainWindow));

        public Brush ChartColor
        {
            get => Dispatcher.Invoke(() => (Brush)GetValue(OpenProperty));
            set => Dispatcher.Invoke(() => SetValue(ChartColorProperty, value));
        }
        public string LastTime
        {
            get => Dispatcher.Invoke(() => (string)GetValue(LastTimeProperty));
            set => Dispatcher.Invoke(() => SetValue(LastTimeProperty, value));
        }
        public decimal OpenValue
        {
            get => Dispatcher.Invoke(() => (decimal)GetValue(OpenProperty));
            set => Dispatcher.Invoke(() => SetValue(OpenProperty, value));
        }
        public decimal CloseValue
        {
            get => Dispatcher.Invoke(() => (decimal)GetValue(CloseProperty));
            set => Dispatcher.Invoke(() => SetValue(CloseProperty,value));
        }

        public decimal LowValue
        {
            get => Dispatcher.Invoke(() => (decimal)GetValue(LowProperty));
            set => Dispatcher.Invoke(() => SetValue(LowProperty, value));
        }

        public decimal HighValue
        {
            get => Dispatcher.Invoke(() => (decimal)GetValue(HighProperty));
            set => Dispatcher.Invoke(() => SetValue(HighProperty, value));
        }

        private void Open_btn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                GettingDataController controller = new GettingDataController(openFileDialog);
                controller.StartReceiveData(1000);
                controller.DataReceived += Controller_DataReceived;
            }
        }
        private void Controller_DataReceived(ClassLibrary.Candle data)
        {
            if (data != null)
            {
                OpenValue = data.Open;
                CloseValue = data.Close;
                HighValue = data.High;
                LowValue = data.Low;
                LastTime = data.Time.ToString();
                RepresentController.AddValueToLine(data, (x) => x.Open, (x) => x.Close);
            }
        }
    }
}
