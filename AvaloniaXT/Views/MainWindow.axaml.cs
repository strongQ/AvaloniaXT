using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaXT.ViewModels;
using SukiUI.Controls;
using SukiUI.Models;

namespace AvaloniaXT.Views
{
    public partial class MainWindow : SukiWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void MenuItem_OnClick(object? sender, RoutedEventArgs e)
        {
            if (DataContext is not MainViewModel vm) return;
            if (e.Source is not MenuItem mItem) return;
            if (mItem.DataContext is not SukiColorTheme cTheme) return;
            vm.ChangeTheme(cTheme);
        }
    }
}