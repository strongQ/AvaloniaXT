using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Styling;
using AvaloniaXT.Interfaces;
using AvaloniaXT.Services;
using AvaloniaXT.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.Extensions.DependencyInjection;
using NetCoreAudio;
using SukiUI;
using SukiUI.Controls;
using SukiUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XT.Common.Interfaces;

namespace AvaloniaXT.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        private readonly SukiTheme _theme;
      

        public MainViewModel(IEnumerable<XTPageBase> xtPages, PageNavigationService nav,IApiConfig config)
        {
            config.RemoteApiUrl = "http://localhost";
            XTPages = new AvaloniaList<XTPageBase>(xtPages.Where(x=>!x.IsDialog()).OrderBy(x => x.Index).ThenBy(x => x.DisplayName));
            _theme = SukiTheme.GetInstance();
            Themes = _theme.ColorThemes;
            BaseTheme = _theme.ActiveBaseTheme;
            _theme.OnBaseThemeChanged +=  variant =>
            {
                BaseTheme = variant;
                 InteractiveContainer.ShowToast($"Successfully Changed Theme,Changed Theme To {variant}");
            };
            nav.NavigationRequested += t =>
            {
                var page = XTPages.FirstOrDefault(x => x.GetType() == t);
                if (page is null || ActivePage?.GetType() == t) return;
                ActivePage = page;
            };
            _theme.OnColorThemeChanged += async theme =>
           InteractiveContainer.ShowToast($"Successfully Changed Color,Changed Color To {theme.DisplayName}.");
            _theme.OnBackgroundAnimationChanged +=
           value => AnimationsEnabled = value;


            var menuService = Register.App.GetService<IMenuToolService>();
            if(menuService != null)
            {
                ShowSearch=menuService.ShowSearch;
                ShowAudio = menuService.ShowAudio;
              
            }
        }
#pragma warning disable CA1822 // Mark members as static
       
#pragma warning restore CA1822 // Mark members as static

        public IAvaloniaReadOnlyList<SukiColorTheme> Themes { get; }
        public IAvaloniaReadOnlyList<XTPageBase> XTPages { get; }

        [ObservableProperty] private ThemeVariant _baseTheme;
        [ObservableProperty] private bool _animationsEnabled;
        [ObservableProperty] private XTPageBase? _activePage;

        [ObservableProperty] private bool _showSearch;

        [ObservableProperty] private bool _showAudio;

        /// <summary>
        /// 点击搜索
        /// </summary>
        [RelayCommand]
        public async void SearchClick()
        {
            var menuService = Register.App.GetService<IMenuToolService>();
            if (menuService != null)
            {
                await menuService.SearchClick();

            }
        }
        

        [RelayCommand]
        public void ToggleBaseTheme() =>
        _theme.SwitchBaseTheme();

        [RelayCommand]
        public Task ToggleAnimations()
        {
            AnimationsEnabled = !AnimationsEnabled;
            var title = AnimationsEnabled ? "Animation Enabled" : "Animation Disabled";
            var content = AnimationsEnabled
                ? "Background animations are now enabled."
                : "Background animations are now disabled.";
            return SukiHost.ShowToast(title, content);
        }
        [RelayCommand]
        public void OpenSetting(string url) => InteractiveContainer.ShowDialog(Register.App.GetKeyedService<XTPageBase>("Setting"));

        public void ChangeTheme(SukiColorTheme theme) =>
        _theme.ChangeColorTheme(theme);


        public void PlayAudio(bool play)
        {
            var menuService = Register.App.GetService<IMenuToolService>();

            if (menuService != null)
            {
                menuService.ShowAudio = play;
            }
           
            
            if (!play)
            {
                var audio = Register.App.GetService<AudioPlayService>();
                audio.PlayAudio(play);
            }
           
        }

       
    }
}
