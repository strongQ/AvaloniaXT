using Avalonia.Collections;
using Avalonia.Styling;
using AvaloniaXT.Services;
using AvaloniaXT.Utilities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using SukiUI;
using SukiUI.Controls;
using SukiUI.Models;
using System.Collections.Generic;
using System.Linq;
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
            _theme.OnBaseThemeChanged += async variant =>
            {
                BaseTheme = variant;
                await SukiHost.ShowToast("Successfully Changed Theme", $"Changed Theme To {variant}");
            };
            nav.NavigationRequested += t =>
            {
                var page = XTPages.FirstOrDefault(x => x.GetType() == t);
                if (page is null || ActivePage?.GetType() == t) return;
                ActivePage = page;
            };
            _theme.OnColorThemeChanged += async theme =>
          await SukiHost.ShowToast("Successfully Changed Color", $"Changed Color To {theme.DisplayName}.");
            _theme.OnBackgroundAnimationChanged +=
           value => AnimationsEnabled = value;
        }
#pragma warning disable CA1822 // Mark members as static
       
#pragma warning restore CA1822 // Mark members as static

        public IAvaloniaReadOnlyList<SukiColorTheme> Themes { get; }
        public IAvaloniaReadOnlyList<XTPageBase> XTPages { get; }

        [ObservableProperty] private ThemeVariant _baseTheme;
        [ObservableProperty] private bool _animationsEnabled;
        [ObservableProperty] private XTPageBase? _activePage;

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
        public void OpenSetting(string url) => SukiHost.ShowDialog(Register.App.GetKeyedService<XTPageBase>("Setting"));

        public void ChangeTheme(SukiColorTheme theme) =>
        _theme.ChangeColorTheme(theme);


    }
}
