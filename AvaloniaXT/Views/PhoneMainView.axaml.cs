using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaXT.ViewModels;
using AvaloniaXT.Views;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using SukiUI.Controls;

namespace AvaloniaXT.Views;

public partial class PhoneMainView : UserControl
{
    public PhoneMainView()
    {
        InitializeComponent();
        WeakReferenceMessenger.Default.Register<PhoneMainView, string>(this, OnNavigation);
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        this.Content = new LoginView();
        base.OnLoaded(e);
    }

    private void OnNavigation(PhoneMainView recipient, string message)
    {
        if (message == "Login")
        {

            var mainVm = Register.App.GetRequiredService<MainViewModel>();
           this.Content=new MainView
            {
                DataContext = mainVm
            };
        }
        else
        {
            this.Content = new LoginView();
        }

    }
}