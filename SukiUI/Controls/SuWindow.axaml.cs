using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using SukiUI.Controls;
using System.Reactive.Linq;
using System;
using System.Reactive;

namespace SukiUI.Controls;

public partial class SuWindow : Window
{
    protected override Type StyleKeyOverride => typeof(SuWindow);

    public static readonly StyledProperty<double> TitleFontSizeProperty =
        AvaloniaProperty.Register<SuWindow, double>(nameof(TitleFontSize), defaultValue: 13);

    public double TitleFontSize
    {
        get => GetValue(TitleFontSizeProperty);
        set => SetValue(TitleFontSizeProperty, value);
    }

    public static readonly StyledProperty<FontWeight> TitleFontWeightProperty =
        AvaloniaProperty.Register<SuWindow, FontWeight>(nameof(TitleFontWeight),
            defaultValue: FontWeight.Bold);

    public FontWeight TitleFontWeight
    {
        get => GetValue(TitleFontWeightProperty);
        set => SetValue(TitleFontWeightProperty, value);
    }

    public static readonly StyledProperty<Control> LogoContentProperty =
        AvaloniaProperty.Register<SuWindow, Control>(nameof(LogoContent));

    public Control LogoContent
    {
        get => GetValue(LogoContentProperty);
        set => SetValue(LogoContentProperty, value);
    }

    public static readonly StyledProperty<Control> TopContentProperty =
      AvaloniaProperty.Register<SuWindow, Control>(nameof(TopContent),
          defaultValue: new Border());

    public Control TopContent
    {
        get => GetValue(TopContentProperty);
        set => SetValue(TopContentProperty, value);
    }

    public static readonly StyledProperty<bool> ShowBottomBorderProperty =
        AvaloniaProperty.Register<SuWindow, bool>(nameof(ShowBottomBorder), defaultValue: true);

    public bool ShowBottomBorder
    {
        get => GetValue(ShowBottomBorderProperty);
        set => SetValue(ShowBottomBorderProperty, value);
    }

    public static readonly StyledProperty<bool> IsMenuVisibleProperty =
        AvaloniaProperty.Register<SuWindow, bool>(nameof(IsMenuVisible), defaultValue: false);

    public bool IsMenuVisible
    {
        get => GetValue(IsMenuVisibleProperty);
        set => SetValue(IsMenuVisibleProperty, value);
    }

   

    public static readonly StyledProperty<bool> BackgroundAnimationEnabledProperty =
        AvaloniaProperty.Register<SuWindow, bool>(nameof(BackgroundAnimationEnabled), defaultValue: false);

    public bool BackgroundAnimationEnabled
    {
        get => GetValue(BackgroundAnimationEnabledProperty);
        set => SetValue(BackgroundAnimationEnabledProperty, value);
    }

    public static readonly StyledProperty<bool> CanMinimizeProperty =
        AvaloniaProperty.Register<SuWindow, bool>(nameof(CanMinimize), defaultValue: true);

    public bool CanMinimize
    {
        get => GetValue(CanMinimizeProperty);
        set => SetValue(CanMinimizeProperty, value);
    }

    public static readonly StyledProperty<bool> CanMoveProperty =
        AvaloniaProperty.Register<SuWindow, bool>(nameof(CanMove), defaultValue: true);

    public bool CanMove
    {
        get => GetValue(CanMoveProperty);
        set => SetValue(CanMoveProperty, value);
    }

    public SuWindow()
    {
       
    }

    private IDisposable? _subscriptionDisposables;

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        // Create handlers for buttons
        if (e.NameScope.Find<Button>("PART_MaximizeButton") is { } maximize)
        {
            maximize.Click += (_, _) =>
            {
                if (!CanResize) return;
                WindowState = WindowState == WindowState.Maximized
                    ? WindowState.Normal
                    : WindowState.Maximized;
            };
        }

        if (e.NameScope.Find<Button>("PART_MinimizeButton") is { } minimize)
            minimize.Click += (_, _) => WindowState = WindowState.Minimized;

        if (e.NameScope.Find<Button>("PART_CloseButton") is { } close)
            close.Click += (_, _) => Close();

        if (e.NameScope.Find<GlassCard>("PART_TitleBarBackground") is { } titleBar)
            titleBar.PointerPressed += OnTitleBarPointerPressed;

        if (e.NameScope.Find<SukiBackground>("PART_Background") is { } background)
        {
            background.SetAnimationEnabled(BackgroundAnimationEnabled);
            var bgObs = this.GetObservable(BackgroundAnimationEnabledProperty)
                .Do(enabled => background.SetAnimationEnabled(enabled))
                .Select(_ => Unit.Default)
                .ObserveOn(new AvaloniaSynchronizationContext());

            _subscriptionDisposables = bgObs.Subscribe();
        }
    }

    private void OnTitleBarPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
        if (e.ClickCount >= 2 && CanResize)
        {
            WindowState = WindowState == WindowState.Maximized
                ? WindowState.Normal
                : WindowState.Maximized;
        }
        else if (CanMove)
            BeginMoveDrag(e);
    }

    protected override void OnUnloaded(RoutedEventArgs e)
    {
        base.OnUnloaded(e);


        _subscriptionDisposables?.Dispose();
    }
}