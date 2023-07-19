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

namespace Aion.Backend.WpfApp
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<PageItem> PageItems { get; set; }
        public PageItem SelectedPageItem { get; set; }
        public IAuthentication Authentication { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;


        public MainWindow()
        {
            InitializeComponent();
            PageItems = new ObservableCollection<PageItem>()
            {
                new PageItem
                {
                    Name = "會員",
                    Content = new MyUserControl(),
                    RequireAuthentication = false
                },
                new PageItem
                {
                    Name = "老師",
                    Content = new TeacherControl(),
                    RequireAuthentication = false
                },
            };

            SelectedPageItem = PageItems[0];
        }

        private void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var listBoxItem = ItemsControl.ContainerFromElement(sender as ListBox, (DependencyObject)e.OriginalSource) as ListBoxItem;
            var content = listBoxItem?.Content;
            if (content is PageItem pageItem && content != SelectedPageItem)
            {
                if (SelectPageItem(pageItem) == false)
                {
                    e.Handled = true;
                }
            }

            MenuToggleButton.IsChecked = false;

        }

        private bool SelectPageItem(PageItem pageItem)
        {
            if (pageItem.RequireAuthentication)
            {
                if (Authentication.Authenticate())
                {
                    SelectedPageItem = pageItem;
                }
                else
                {
                    return false;
                }
            }
            else if (pageItem.Content is Window window)
            {
                ShowContentWindow(window);
                return false;
            }
            else
            {
                SelectedPageItem = pageItem;
            }

            return true;
        }

        private void ShowContentWindow(Window window)
        {
            window.Closing -= ContentWindow_Closing;
            window.Closing += ContentWindow_Closing;

            window.Owner = this;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.Show();
        }
        private void ContentWindow_Closing(object sender, CancelEventArgs e)
        {
            ((Window)sender).Visibility = Visibility.Hidden;
            e.Cancel = true;
        }

        public interface IAuthentication
        {
            bool Authenticate();
        }
    }
}
