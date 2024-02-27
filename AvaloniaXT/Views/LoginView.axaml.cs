using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaXT.ViewModels;
using AvaloniaXT.Views;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using SukiUI.Controls;

namespace AvaloniaXT;

public partial class LoginView : UserControl
{
    public LoginView()
    {
        InitializeComponent();
        this.DataContext = new LoginViewModel();

       
    }

   
}