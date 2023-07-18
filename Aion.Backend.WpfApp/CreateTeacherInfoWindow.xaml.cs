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
    /// CreateTeacherInfoWindow.xaml 的互動邏輯
    /// </summary>
    public partial class CreateTeacherInfoWindow : Window, INotifyPropertyChanged
    {
        public DateTime SelectedDate { get; set; } = DateTime.Now;
        public Teacher Teacher { get; set; }
        public ObservableCollection<User> Users { get; set; }
        public User SelectedUser { get; set; }
        public string Level { get; set; }
        public string SelectedLesson { get; set; }
        public List<string> Lessons { get; set; }
        private string myDateTimeString { get; set; }
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
            for (int i = 14; i < 22; i++)
            {
                Lessons.Add($"{i}:00");
            }
            var users = SQLiteMananergment.GetAllData(new User());
            Users = new ObservableCollection<User>(users);
        }
        private void OnSelectedDateChanged()
        {
            myDateTimeString = SelectedDate.ToString("yyyy/MM/dd");
        }

        private void Check(object sender, RoutedEventArgs e)
        {
            var report = CreateTeacherReport();
            SQLiteMananergment.ReportService.Create(report);
            DialogResult = true;
            Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private TeacherReport CreateTeacherReport()
        {
            var userId = SQLiteMananergment.GetAllData(new User()).FirstOrDefault(x => x.LastName == SelectedUser.LastName).Id;
            var report = new TeacherReport()
            {
                UserId = userId,
                TeacherId = Teacher.Id,
                Date = myDateTimeString,
                LessonName = Teacher.Style + Level
            };
            return report;
        }
    }
}
