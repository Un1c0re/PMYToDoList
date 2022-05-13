using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyToDoList
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }
    }

    public class TaskItem : INotifyPropertyChanged
    {
        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                if (_text == value)
                    return;
                _text = value;
                OnPropertyChanged("Text");
            }
        }


        private bool _isDone = false;
        public bool IsDone
        {
            get { return _isDone; }
            set
            {
                if (_isDone == value)
                    return;
                _isDone = value;
                OnPropertyChanged("IsDone");
            }
        }

        public override string ToString() { return _text; }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }


    public class MainPageViewModel : INotifyPropertyChanged
    {
        private string _entryText;
        public string EntryText
        {
            get { return _entryText; }
            set
            {
                if (_entryText == value)
                    return;
                _entryText = value;
                OnPropertyChanged("EntryText");
            }
        }

        private ObservableCollection<TaskItem> _tasks = new ObservableCollection<TaskItem>();
        public ObservableCollection<TaskItem> Tasks
        {
            get { return _tasks; }
            set
            {
                if (_tasks == value)
                    return;

                _tasks = value;
                OnPropertyChanged("Tasks");
            }
        }

        public MainPageViewModel()
        {
            AddTask = new Command<string>((text) =>
            {
                if (!String.IsNullOrWhiteSpace(text))
                {
                    Tasks.Add(new TaskItem { Text = text.TrimStart() });
                    EntryText = "";
                }
            });

            RemoveTask = new Command<TaskItem>((task) =>
            {
                Tasks.Remove(task);
            });
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand AddTask { protected set; get; }
        public ICommand RemoveTask { protected set; get; }
    }

}
