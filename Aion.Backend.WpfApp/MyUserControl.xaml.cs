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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;

namespace Aion.Backend.WpfApp
{
    /// <summary>
    /// MyUserControl.xaml 的互動邏輯
    /// </summary>
    public partial class MyUserControl : UserControl, INotifyPropertyChanged
    {
        public User SelectedUser { get; set; }
        public MyCsvHelper CsvHelper { get; set; }
        public ParentInfoViewModel ParentInfo { get; set; }
        public ObservableCollection<User> Users { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public MyUserControl()
        {
            InitializeComponent();
            CsvHelper = new MyCsvHelper();
        }


        private void OnSelectedUserChanged()
        {
            if (SelectedUser != null)
            {
                ParentInfo = SQLiteMananergment.ParentService.GetParentInfoByUserId(SelectedUser.Id);
            }
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void CsvToDb_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                CsvHelper.GetCsv(dialog.FileName);
                Update();
            }
        }
        private void Update()
        {
            var users = SQLiteMananergment.GetAllData(new User());
            Users = new ObservableCollection<User>(users);
        }

        private void ShowUserInfo_Click(object sender, RoutedEventArgs e)
        {
            var window = new UserInfomartionWindow(SelectedUser.Id);
            window.Show();
        }
    }
}
