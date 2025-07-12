using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ToDoApp
{
    public class taskItem : INotifyPropertyChanged
    {

        public bool isNew { get; set; }
        public bool isImportantChecked { get; set; }
        public bool isImportantColor { get; set; }
        public int timeRemain { get; set; }
        public string? contentTask {  get; set; }
        public Brush? importantColor { get; set; }

        public string? timeOfCreation { get; set; }
        public string? timeLeft { get; set; }
        public bool? finishedTask { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
