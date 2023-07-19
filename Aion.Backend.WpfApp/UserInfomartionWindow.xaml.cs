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
    /// UserInfomartionWindow.xaml 的互動邏輯
    /// </summary>
    public partial class UserInfomartionWindow : Window, INotifyPropertyChanged
    {
        public int UserId { get; set; }
        public ObservableCollection<TeacherReport> Report { get; set; }
        public DateTime SelectedDate { get; set; } = DateTime.Now;
        public int Count { get; set; }
        public int Total { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public UserInfomartionWindow(int id)
        {
            InitializeComponent();
            UserId = id;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void OnSelectedDateChanged()
        {
            Update();
        }
        private void Add(object sender, RoutedEventArgs e)
        {
            var window = new CreateUserInfoWindow(UserId);
            if (window.ShowDialog() == true)
            {
                var report = new TeacherReport()
                {
                    UserId = UserId,
                    TeacherId = window.SelectedTeacher.Id,
                    Date = window.SelectedDate.ToString("yyyy/MM/dd"),
                    Lesson = window.SelectedLesson,
                    LessonName = window.SelectedTeacher.Style + window.SelectedLevel
                };

                SQLiteMananergment.ReportService.Create(report);
                Update();
            }
        }

        private void Edit(object sender, RoutedEventArgs e)
        {

        }

        private void Delete(object sender, RoutedEventArgs e)
        {

        }

        private void GetTotal()
        {
            if (Report.Count > 4)
            {
                Total = (Report.Count - 4) * 250;
            }
            else
            {
                Total = 1600;
            }
        }
        private void Update()
        {
            var report = SQLiteMananergment.GetAllData(new TeacherReport()).Where(x => x.UserId == UserId && x.Date.Contains(SelectedDate.ToString("yyyy/MM")));
            Report = new ObservableCollection<TeacherReport>(report);
            Count = report.Count();
            GetTotal();
        }
    }
}
