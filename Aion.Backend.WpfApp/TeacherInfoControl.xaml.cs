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
        public DateTime SelectedDate { get; set; } = DateTime.Now;
        public DateTime SelectedLessonDate { get; set; } 
        public string SelectedKey { get; set; }
        public string[] LessonKey { get; set; }
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
                    .Where(x => x.Date.ToString("yyyy/MM/dd") == SelectedDate.ToString("yyyy/MM/dd"))
                    .Select(x => x.Date.ToString("yyyy/MM/dd"));
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
            TeacherViewModel = new ObservableCollection<TeacherViewModel>();
            LessonDate = new ObservableCollection<string>();
            LessonKey = new string[] { "L1", "L2", "L3", "L4" };
            Level = SQLiteMananergment.TeacherService.GetAll().First(x => x.Id == TeacherId).Level;
            Position = SQLiteMananergment.TeacherService.GetAll().First(x => x.Id == TeacherId).Position;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TeacherViewModel = new ObservableCollection<TeacherViewModel>();
            var viewmodel = InitializeTeacherViewModel();
            InitializeLessonDate(viewmodel);
        }

        private List<TeacherViewModel> InitializeTeacherViewModel()
        {
            var infos = SQLiteMananergment.TeacherService.GetTeacherInfo(TeacherId);
            foreach (var info in infos)
            {
                TeacherViewModel.Add(info);
            }
            return infos;
        }

        private void InitializeLessonDate(List<TeacherViewModel> infos)
        {
            var dates = infos.Select(x => x.Date.ToString("yyyy/MM/dd")).Distinct();
            foreach (var date in dates)
            {
                LessonDate.Add(date);
            }
        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TeacherInfo = new ObservableCollection<TeacherViewModel>();
            var datas = GetTeacherViewModel();
            GetTotoalByTeacher(datas);
        }

        private IEnumerable<TeacherViewModel> GetTeacherViewModel()
        {
            var datas = TeacherViewModel.Where(x => x.Lesson == SelectedKey && x.Date.ToString("yyyy/MM/dd") == SelectedLessonDate.ToString("yyyy/MM/dd"));
            foreach (var data in datas)
            {
                TeacherInfo.Add(data);
            }
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
            LessonDate = new ObservableCollection<string>();
            var infos = InitializeTeacherViewModel();
            InitializeLessonDate(infos);
        }
    }
}
