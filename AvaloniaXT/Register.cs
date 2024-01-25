using Avalonia.Controls.ApplicationLifetimes;
using AvaloniaXT.Dialogs;
using AvaloniaXT.Services;
using AvaloniaXT.ViewModels;
using AvaloniaXT.Views;
using Microsoft.Extensions.DependencyInjection;
using Projektanker.Icons.Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XT.Common;
using XT.Common.Interfaces;
using XT.Common.Services;
using Projektanker.Icons.Avalonia.FontAwesome;

namespace AvaloniaXT
{
    public static class Register
    {
        public static IServiceProvider App;
       
        /// <summary>
        /// 初始化服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static ServiceCollection InitialXTServices(this ServiceCollection services)
        {
            services.AddSingleton<PageNavigationService>();

            // Viewmodels
            services.AddSingleton<MainViewModel>();

            services.AddKeyedSingleton<XTPageBase, SettingDialogViewModel>("Setting");

            services.AddSingleton<XTPageBase, AboutViewModel>();

            services.AddSingleton<IApiConfig, ApiConfigService>();

            services.AddOriginHttpClient();
            return services;
        }

        public static void AddIcons()
        {
            IconProvider.Current
           .Register<FontAwesomeIconProvider>();
        }
        /// <summary>
        /// 完成初始化，配置主窗体和界面
        /// </summary>
        /// <param name="lifetime"></param>
        /// <param name="provider"></param>
        public static void InitialCompleted(this IApplicationLifetime lifetime,IServiceProvider provider)
        {
            App = provider;
            var mainVm = provider?.GetRequiredService<MainViewModel>();
            if (lifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {

                desktop.MainWindow = new MainWindow { DataContext = mainVm };
            }
            else if (lifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                singleViewPlatform.MainView = new MainView
                {
                    DataContext = mainVm
                };
            }
        }



    }
}
