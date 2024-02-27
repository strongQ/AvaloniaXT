using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaXT.Interfaces;
using AvaloniaXT.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using Material.Icons.Avalonia;
using Microsoft.Extensions.DependencyInjection;
using SukiUI.Controls;
using SukiUI.Models;
using System;

namespace AvaloniaXT.Views
{
    public partial class MainWindow : SukiWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
          
        }

      

        private async void MainWindow_Loaded(object? sender, RoutedEventArgs e)
        {
          
            
            var menuService = Register.App.GetService<IMenuToolService>();
            if (menuService != null)
            {
              
                await menuService.Initial();
                this.Title = menuService.GetMainTitle();
                this.IsMenuVisible = true;
            }
            else
            {
                this.Title = "AvaloniaXT";
              
                this.IsMenuVisible = true;
            }
        }

        private void MenuItem_OnClick(object? sender, RoutedEventArgs e)
        {
            if (DataContext is not MainViewModel vm) return;
            if (e.Source is not MenuItem mItem) return;
            if (mItem.DataContext is not SukiColorTheme cTheme) return;
            vm.ChangeTheme(cTheme);
        }

        private void Alarm_OnClick(object? sender, RoutedEventArgs e)
        {
            if (DataContext is not MainViewModel vm) return;
            var icon= this.FindControl<MaterialIcon>("AlarmIcon");

            if (icon == null) return;
           if( icon.Kind == Material.Icons.MaterialIconKind.VolumeHigh)
            {
                icon.Kind = Material.Icons.MaterialIconKind.VolumeOff;
                vm.PlayAudio(false);
            }
            else
            {
                icon.Kind = Material.Icons.MaterialIconKind.VolumeHigh;
               
                vm.PlayAudio(true);
            }
        }
    }
}