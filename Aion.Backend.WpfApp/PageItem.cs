using System.ComponentModel;
using System.Windows.Controls;
using System.Windows;

namespace Aion.Backend.WpfApp
{
    public class PageItem
    {
        public string Name { get; set; }

        public object Content { get; set; }

        public object Icon { get; set; }

        public ScrollBarVisibility HorizontalScrollBarVisibilityRequirement { get; set; }

        public ScrollBarVisibility VerticalScrollBarVisibilityRequirement { get; set; }

        public Thickness MarginRequirement { get; set; } = new Thickness(16);

        public bool RequireAuthentication { get; set; }
        public int Order { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}