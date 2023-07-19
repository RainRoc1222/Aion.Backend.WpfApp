using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace Aion.Backend.WpfApp
{
    /// <summary>
    /// CreateUserInfoWindow.xaml 的互動邏輯
    /// </summary>
    public partial class CreateUserInfoWindow : Window, INotifyPropertyChanged
    {
        public DateTime SelectedDate { get; set; } = DateTime.Now;
        public TeacherReport Report { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string[] Levels { get; set; }
        public List<string> Lessons { get; set; }
        public string SelectedLesson { get; set; }
        public string SelectedLevel { get; set; }
        public ObservableCollection<Teacher> Teachers { get; set; }
        public Teacher SelectedTeacher { get; set;}
        public event PropertyChangedEventHandler PropertyChanged;
        public CreateUserInfoWindow(int id)
        {
            InitializeComponent();
            UserId = id;
            Initialize();
        }
        private void Initialize()
        {
            Lessons = new List<string>();
            Levels = new string[] { "", "4-6歲", "初級", "中級" };
            for (int i = 14; i < 22; i++)
            {
                Lessons.Add($"{i}:00");
            }
            var teachers = SQLiteMananergment.GetAllData(new Teacher()).Distinct();
            Teachers = new ObservableCollection<Teacher>(teachers);
            User = SQLiteMananergment.GetAllData(new User()).FirstOrDefault(x => x.Id == UserId);
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
