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


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    public class MainPageViewModel : INotifyPropertyChanged
    {
        public ICommand AddTask { get; protected set; }
        public ICommand RemoveTask { get; protected set; }


        private string _enteredText;
        public string EnteredText
        {
            get { return _enteredText; }
            set
            {
                if (_enteredText == value)
                    return;

                _enteredText = value;
                OnPropertyChanged("EnteredText");
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
                    EnteredText = "";
                }
            });

            RemoveTask = new Command<TaskItem>((task) => { Tasks.Remove(task); });
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
