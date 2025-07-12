using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;

namespace ToDoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<taskItem> taskActive { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer LiveTime = new DispatcherTimer();
            LiveTime.Interval = TimeSpan.FromSeconds(1);
            LiveTime.Tick += timer_Tick;
            LiveTime.Start();

            taskActive = new ObservableCollection<taskItem>();
            todolist.ItemsSource = taskActive;

        }
        void timer_Tick(object sender, EventArgs e)
        {
            currentTimeDate.Content = DateTime.Now.ToString("HH:mm:ss | dd/MM/yyyy");
        }

        private void buttonAddTask_Click(object sender, RoutedEventArgs e)
        {
            var newTask = new taskItem { isNew=true, isImportantChecked=false, isImportantColor=false, contentTask=inputTaskBox.Text };

            if (!string.IsNullOrWhiteSpace(inputTaskBox.Text))
            {
                taskActive.Add(newTask);
                inputTaskBox.Clear();
            }


        }

        private void buttonRemoveTask_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}