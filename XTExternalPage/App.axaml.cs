using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Templates;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaXT;
using AvaloniaXT.Interfaces;
using AvaloniaXT.Services;
using AvaloniaXT.ViewModels;
using AvaloniaXT.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SukiUI.Controls;
using SukiUI.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using XT.Common.Services;
using XTExternalPage.Apis;
using XTExternalPage.Dialogs;
using XTExternalPage.Services;
using XTExternalPage.ViewModels;

namespace XTExternalPage
{
    public partial class App : Application
    {
        private  IServiceProvider _provider;

     

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            var services = new ServiceCollection();
            services.InitialXTServices();

            services.AddSingleton<XTPageBase, OneFloorViewModel>();

            services.AddSingleton<XTPageBase, TwoFloorViewModel>();

            services.AddSingleton<XTPageBase, ThreeFloorViewModel>();

            services.AddSingleton<XTPageBase, PhoneTestViewModel>();

            services.AddKeyedSingleton<XTPageBase, SearchDialogViewModel>("Search");
            services.AddSingleton<EcsTagApi>();

            services.AddSingleton<IMenuToolService, MenuToolService>();

            // services.AddSingleton<DbService>();

            _provider = services.BuildServiceProvider();

            
        }

        public override void OnFrameworkInitializationCompleted()
        {
           // Lang.Resources.Culture = new CultureInfo("en-US");

           // Register.InitialCulture("en-US");

            ApplicationLifetime?.InitialCompleted(_provider);

            base.OnFrameworkInitializationCompleted();
        }

        private static bool IsProduction()
        {
#if DEBUG
            return false;
#else
        return true;
#endif
        }


        private void NativeMenuItem_Click(object? sender, EventArgs e)
        {
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime)
            {
                lifetime?.Shutdown();
                
            }
        }

        private void NativeMenuItem_Show(object? sender, EventArgs e)
        {
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime)
            {
                var window = lifetime?.MainWindow;
                window.WindowState = WindowState.Normal;
                window.Show();


            }
        }


    }
}