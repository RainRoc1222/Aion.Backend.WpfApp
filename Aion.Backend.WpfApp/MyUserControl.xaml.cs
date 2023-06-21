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
using AutoMapper;
using MaterialDesignThemes.Wpf;

namespace Aion.Backend.WpfApp
{
    /// <summary>
    /// MyUserControl.xaml 的互動邏輯
    /// </summary>
    public partial class MyUserControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public User SelectedUser { get; set; }
        public ParentInfoViewModel ParentInfo { get; set; }
        public ObservableCollection<User> Users { get; set; }
        public MyUserControl()
        {
            InitializeComponent();
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
            Users = new ObservableCollection<User>();
            var users = SQLiteMananergment.GetAllData(new User());
            foreach (var user in users)
            {
                Users.Add(user);
            }
        }
    }
}
