using log4net.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Aion.Backend.WpfApp
{
    /// <summary>
    /// CreateTeacherInfoWindow.xaml 的互動邏輯
    /// </summary>
    public partial class CreateTeacherInfoWindow : Window, INotifyPropertyChanged
    {
        public DateTime SelectedDate { get; set; } = DateTime.Now;
        public Teacher Teacher { get; set; }
        public ObservableCollection<User> Users { get; set; }
        public User SelectedUser { get; set; }
        public string SelectedLesson { get; set; }
        public string SelectedLevel { get; set; }
        public string[] Levels { get; set; }
        public List<string> Lessons { get; set; }
        public string DateTimeString { get; set; }
        public ObservableCollection<Teacher> Teachers { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public CreateTeacherInfoWindow(int id)
        {
            InitializeComponent();
            Initialize();
            Teacher = SQLiteMananergment.GetAllData(new Teacher()).FirstOrDefault(x => x.Id == id);
        }

        private void Initialize()
        {
            Lessons = new List<string>();
            Levels = new string[] {"","4-6歲","初級","中級"};
            for (int i = 14; i < 22; i++)
            {
                Lessons.Add($"{i}:00");
            }
            var users = SQLiteMananergment.GetAllData(new User());
            Users = new ObservableCollection<User>(users);
        }
        private void OnSelectedDateChanged()
        {
            DateTimeString = SelectedDate.ToString("yyyy/MM/dd");
        }

        private void Check(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
 
    }
}
