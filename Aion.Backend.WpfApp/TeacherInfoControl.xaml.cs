using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Aion.Backend.WpfApp
{
    /// <summary>
    /// TeacherInfoControl.xaml 的互動邏輯
    /// </summary>
    public partial class TeacherInfoControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<TeacherViewModel> TeacherViewModel { get; set; }
        public ObservableCollection<string> LessonDate { get; set; }
        public ObservableCollection<TeacherViewModel> TeacherInfo { get; set; }
        public TeacherViewModel SelectedInfo { get; set; }
        public DateTime SelectedDate { get; set; } = DateTime.Now;
        public DateTime SelectedLessonDate { get; set; }
        public string SelectedKey { get; set; }
        public List<string> LessonKey { get; set; }
        public int TeacherId { get; set; }
        public int Count { get; set; }
        public int Total { get; set; }

        public int Level { get; set; }
        public string Position { get; set; }

        public TeacherInfoControl(int id)
        {
            InitializeComponent();
            TeacherId = id;
            Initialize();
        }
        private int GetTotal()
        {
            if (Position.Contains("助教"))
            {

                return Level;
            }
            else
            {
                if (Count > 15)
                {
                    return Level + 500;
                }
                else if (Count > 10)
                {
                    return Level + 300;
                }
                else
                {
                    return Level;
                }
            }
        }
        private void OnSelectedDateChanged()
        {
            try
            {
                LessonDate = new ObservableCollection<string>();
                var lessonDates = SQLiteMananergment.TeacherService.GetTeacherInfo(TeacherId)
                    .Where(x => x.Date == SelectedDate.ToString("yyyy/MM/dd"))
                    .Select(x => x.Date);
                foreach (var date in lessonDates)
                {
                    LessonDate.Add(date);
                }
            }
            catch (Exception ex)
            {

                return;
            }
        }

        private void Initialize()
        {
            LessonDate = new ObservableCollection<string>();
            LessonKey = new List<string>();
            for (int i = 14; i < 22; i++)
            {
                LessonKey.Add($"{i}:00");
            }

            var info = SQLiteMananergment.TeacherService.GetAll().First(x => x.Id == TeacherId);
            Level = info.Level;
            Position = info.Position;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void Update()
        {
            UpdateTeacherViewModel();
            UpdateLessonDate();
        }
        private void UpdateTeacherViewModel()
        {
            var infos = SQLiteMananergment.TeacherService.GetTeacherInfo(TeacherId);
            TeacherViewModel = new ObservableCollection<TeacherViewModel>(infos);
        }

        private void UpdateLessonDate()
        {
            var dates = TeacherViewModel.Select(x => x.Date).Distinct();
            LessonDate = new ObservableCollection<string>(dates);
        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TeacherInfo = new ObservableCollection<TeacherViewModel>();
            var datas = GetTeacherViewModel();
            GetTotoalByTeacher(datas);
        }

        private IEnumerable<TeacherViewModel> GetTeacherViewModel()
        {
            var datas = TeacherViewModel.Where(x => x.Lesson == SelectedKey && x.Date == SelectedLessonDate.ToString("yyyy/MM/dd"));
            TeacherInfo = new ObservableCollection<TeacherViewModel>(datas);
            return datas;
        }

        private void GetTotoalByTeacher(IEnumerable<TeacherViewModel> datas)
        {
            if (datas.Count() > 0)
            {
                Total = GetTotal();
                Count = datas.Count();
            }
            else
            {
                Total = 0;
                Count = 0;
            }
        }

        private void ResetLessonDate(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            var window = new CreateTeacherInfoWindow(TeacherId);
            if(window.ShowDialog() == true)
            {
                var report = CreateTeacherReport(window);
                SQLiteMananergment.ReportService.Create(report);
            }

            Update();
        }

        private void Edit(object sender, RoutedEventArgs e)
        {

        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (SelectedInfo != null && SelectedLessonDate != null && SelectedKey != null)
            {
                var report = SQLiteMananergment.GetAllData(new TeacherReport()).FirstOrDefault(x => x.UserId == SelectedInfo.UserId && x.Date == SelectedLessonDate.ToString("yyyy/MM/dd") && x.TeacherId == TeacherId);
                SQLiteMananergment.ReportService.Delete(report);
                Update();
            }
        }

        private TeacherReport CreateTeacherReport(CreateTeacherInfoWindow window)
        {
            var userId = SQLiteMananergment.GetAllData(new User()).FirstOrDefault(x => x.Id == window.SelectedUser.Id).Id;
            var report = new TeacherReport()
            {
                UserId = userId,
                TeacherId = window.Teacher.Id,
                Date = window.DateTimeString,
                LessonName = window.Teacher.Style + window.SelectedLevel
            };
            return report;
        }

    }
}
