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
        public ObservableCollection<taskItem> finishedList { get; set; }

        public MainWindow()
        {
            InitializeComponent();


            DispatcherTimer LiveTime = new DispatcherTimer();               // Create new DispatcherTimer instance for UI updates
            LiveTime.Interval = TimeSpan.FromSeconds(1);                    // Set timer interval to 1 sec
            LiveTime.Tick += timer_Tick;                                    // Attach event handler to be called each tick
            LiveTime.Start();                                               // Start

            taskActive = new ObservableCollection<taskItem>();
            finishedList = new ObservableCollection<taskItem>();
            todolist.ItemsSource = taskActive;
            finList.ItemsSource = finishedList;

            // Init buttons disabled
            buttonFinishTask.IsEnabled = false;
            taskDescriptionBox.IsEnabled = false;
            buttonRemoveTask.IsEnabled = false;
            buttonSaveDesc.IsEnabled = false;

        }
        void timer_Tick(object sender, EventArgs e)
        {
            currentTimeDate.Content = DateTime.Now.ToString("HH:mm:ss | dd/MM/yyyy");   // Timer output
        }

        private void buttonAddTask_Click(object sender, RoutedEventArgs e)
        {
            var newTask = new taskItem { isNew=true, isImportantColor=false, contentTask=inputTaskBox.Text, deadlineTimeAndDate=deadLineBox.Text};  // Init taskitem

            if (!string.IsNullOrWhiteSpace(inputTaskBox.Text))
            {
                // Add normal task
                taskActive.Add(newTask);
                inputTaskBox.Clear();
                deadLineBox.Clear();
                newTask.importantColor = Brushes.Black;
                newTask.timeOfCreation = DateTime.Now.ToString("ddd, /dd/MMM/yyyy | HH/mm");

                // Make task important
                if (checkImportant.IsChecked == true && newTask.isNew)
                {
                    newTask.isImportantColor = true;
                    newTask.importantColor = Brushes.White;
                    newTask.importantBackgroundColor = Brushes.Red;
                    checkImportant.IsChecked = false;
                }
                newTask.isNew = false;
            }
        }

        private void buttonRemoveTask_Click(object sender, RoutedEventArgs e)
        {
            var removeItemSelected = todolist.SelectedItems.Cast<taskItem>().ToList();

            foreach (var item in removeItemSelected)
            {
                taskActive.Remove(item);
            }
        }

        private void buttonFinishTask_Click(object sender, RoutedEventArgs e)
        {
            var finishItem = todolist.SelectedItems.Cast<taskItem>().FirstOrDefault();

            if (finishItem != null) 
            {
                    finishedList.Add(finishItem);
                    taskActive.Remove(finishItem);
            }
        }
        private void buttonSaveDesc_Click(object sender, RoutedEventArgs e)
        {
            var selectedTask = todolist.SelectedItems.Cast<taskItem>().FirstOrDefault();

            if (selectedTask != null)
            {

                selectedTask.taskDescription = taskDescriptionBox.Text;

            }

        }

        private void todolist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Enable the buttons and description box if any items are selected, disable otherwise
            buttonRemoveTask.IsEnabled = todolist.SelectedItems.Count > 0;
            buttonFinishTask.IsEnabled = todolist.SelectedItems.Count > 0;
            buttonSaveDesc.IsEnabled = todolist.SelectedItems.Count > 0;
            taskDescriptionBox.IsEnabled = todolist.SelectedItems.Count > 0;

            selectionLogic(sender, e);
        }

        private void selectionLogic(object sender, EventArgs e)
        {
            var selectedTask = todolist.SelectedItems.Cast<taskItem>().FirstOrDefault();

            if (selectedTask != null)
            {
                selectedTaskLabel.Content = selectedTask.contentTask;
                deadlineTaskLabel.Content = selectedTask.deadlineTimeAndDate;
                createdTaskLabel.Content = selectedTask.timeOfCreation;
                taskDescriptionBox.Text = selectedTask.taskDescription;
            }
            else
            {
                // Clear when nothing is selected
                selectedTaskLabel.Content = "";
                deadlineTaskLabel.Content = "";
                createdTaskLabel.Content = "";
                taskDescriptionBox.Text =  "";
            }

        }
    }
}