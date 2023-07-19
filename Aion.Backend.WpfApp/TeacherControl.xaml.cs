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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
using Microsoft.Data.Sqlite;

namespace Aion.Backend.WpfApp
{
    /// <summary>
    /// TeacherControl.xaml 的互動邏輯
    /// </summary>
    public partial class TeacherControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Teacher SelectedTeacher { get; set; }
        public ObservableCollection<Teacher> Teachers { get; set; }

        public TeacherControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void Update()
        {
            Teachers = new ObservableCollection<Teacher>();
            var teachers = SQLiteMananergment.TeacherService.GetAll();
            foreach (var teacher in teachers)
            {
                Teachers.Add(new Teacher()
                {
                    Id = teacher.Id,
                    Name = teacher.Name,
                    Email = teacher.Email,
                    Style = teacher.Style,
                    Phone = teacher.Phone,
                    Level = teacher.Level,
                    Position = teacher.Position,
                });
            }
            SelectedTeacher = null;
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            var window = new CreateWindow();

            if (window.ShowDialog() == true)
            {
                SelectedTeacher = window.Teacher;
                SQLiteMananergment.TeacherService.Create(SelectedTeacher);
                Update();
            }
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            var window = new CreateWindow();
            window.Teacher = SelectedTeacher;

            if (window.ShowDialog() == true)
            {
                SelectedTeacher = window.Teacher;
                SQLiteMananergment.TeacherService.Edit(SelectedTeacher);
                Update();
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (SelectedTeacher != null)
            {
                SQLiteMananergment.TeacherService.Delete(SelectedTeacher);
                Update();
            }
        }

        private void ShowTeacherInfo(object sender, RoutedEventArgs e)
        {
            if(SelectedTeacher != null)
            {
                var id = SelectedTeacher.Id;
                var window = new Window()
                {
                    Content = new TeacherInfoControl(id),
                    Width = 1200,
                };
                window.Show();
            }
        }
    }
}
