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
        public ObservableCollection<TeacherViewModel> TeacherViewModel { get; set; }
        public DateTime SelectedDate { get; set; } = DateTime.Now;
        public TeacherViewModel SelectedItem { get; set; }
        public int Count { get; set; }
        public int Total { get; set; } = 0;

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
                    LessonName = window.SelectedTeacher.Style + window.SelectedLevel,
                    IsMissClass = window.IsMissClass
                };

                SQLiteMananergment.ReportService.Create(report);
                Update();
            }
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            if (SelectedItem != null)
            {
                var window = new CreateUserInfoWindow(UserId);

                if (window.ShowDialog() == true)
                {
                    var report = new TeacherReport()
                    {
                        Id = SelectedItem.ReportId,
                        UserId = UserId,
                        TeacherId = window.SelectedTeacher.Id,
                        Date = window.SelectedDate.ToString("yyyy/MM/dd"),
                        Lesson = window.SelectedLesson,
                        LessonName = window.SelectedTeacher.Style + window.SelectedLevel,
                        IsMissClass = window.IsMissClass
                    };

                    SQLiteMananergment.ReportService.Edit(report);
                    Update();
                }
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (SelectedItem != null)
            {
                SQLiteMananergment.ReportService.Delete(SelectedItem.ReportId);
                Update();
            }
        }

        private void GetTotal()
        {

            if (TeacherViewModel.Count == 0)
            {
                Total = 0;
            }
            else if (TeacherViewModel.Count > 0 && TeacherViewModel.Count <= 4)
            {
                Total = 1600;
            }
            else
            {
                var tempCount = 0;
                foreach (var data in TeacherViewModel)
                {
                    if (!data.IsMissClass)
                    {
                        tempCount++;
                    }
                }
                var extra = tempCount - 4 >= 0 ? tempCount - 4 : 0;
                Total = 1600 + extra * 250;
            }

        }
        private void Update()
        {
            if (UserId != 0)
            {
                var viewmodel = SQLiteMananergment.UserService.GetUserInfo(UserId).Where(x => x.Date.Contains(SelectedDate.ToString("yyyy/MM")));
                TeacherViewModel = new ObservableCollection<TeacherViewModel>(viewmodel);
                Count = TeacherViewModel.Count();
                GetTotal();
            }
        }
    }
}
