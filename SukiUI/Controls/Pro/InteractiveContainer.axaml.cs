using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using System.Threading.Tasks;
using System.Threading;
using System;
using Avalonia.VisualTree;
using System.Linq;
using Avalonia.Controls.Primitives;
using SukiUI.Helpers;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Media;

namespace SukiUI.Controls;

public partial class InteractiveContainer : UserControl
{
    public InteractiveContainer()
    {
        InitializeComponent();
    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public static readonly StyledProperty<bool> ShowAtBottomProperty = AvaloniaProperty.Register<InteractiveContainer, bool>(nameof(InteractiveContainer), defaultValue: false);

    public bool ShowAtBottom
    {
        get { return GetValue(ShowAtBottomProperty); }
        set
        {

            SetValue(ShowAtBottomProperty, value);
        }
    }

    public static readonly StyledProperty<bool> IsDialogOpenProperty = AvaloniaProperty.Register<InteractiveContainer, bool>(nameof(InteractiveContainer), defaultValue: false);

    public bool IsDialogOpen
    {
        get { return GetValue(IsDialogOpenProperty); }
        set
        {

            SetValue(IsDialogOpenProperty, value);
        }
    }

    public static readonly StyledProperty<bool> IsToastOpenProperty = AvaloniaProperty.Register<InteractiveContainer, bool>(nameof(InteractiveContainer), defaultValue: false);

    public bool IsToastOpen
    {
        get { return GetValue(IsToastOpenProperty); }
        set
        {

            SetValue(IsToastOpenProperty, value);
        }
    }

    public static readonly StyledProperty<bool> AllowBackgroundCloseProperty =
       AvaloniaProperty.Register<InteractiveContainer, bool>(nameof(AllowBackgroundClose), defaultValue: true);

    public bool AllowBackgroundClose
    {
        get => GetValue(AllowBackgroundCloseProperty);
        set => SetValue(AllowBackgroundCloseProperty, value);
    }

    public static readonly StyledProperty<Control> DialogContentProperty = AvaloniaProperty.Register<InteractiveContainer, Control>(nameof(InteractiveContainer), defaultValue: new Grid());

    public Control DialogContent
    {
        get { return GetValue(DialogContentProperty); }
        set
        {

            SetValue(DialogContentProperty, value);
        }
    }

    public static readonly StyledProperty<Control> ToastContentProperty = AvaloniaProperty.Register<InteractiveContainer, Control>(nameof(InteractiveContainer), defaultValue: new Grid());

    public Control ToastContent
    {
        get { return GetValue(ToastContentProperty); }
        set
        {

            SetValue(ToastContentProperty, value);
        }
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        e.NameScope.Get<Grid>("gridDialog").PointerPressed += (_, _) => BackgroundRequestClose();
        e.NameScope.Get<Button>("PART_MaximizeButton").Click += (_, _) => MaxWindow();
        e.NameScope.Get<Button>("PART_CloseButton").Click += (_, _) =>
        {
            var container = GetInteractiveContainerInstance();
            container.IsDialogOpen = false;
        };
    }

    private void MaxWindow()
    {
        try
        {
            var container = GetInteractiveContainerInstance();
            container.IsDialogOpen = false;
            SuWindow win = new SuWindow();
            win.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            win.Content = ViewLocator.TryBuild(container.DialogContent.DataContext);
            win.Show();
        }
        catch(Exception ex)
        {
            ShowDialog(ex.Message);
        }
    }

    private void BackgroundRequestClose()
    {
        var container = GetInteractiveContainerInstance();
        if (container.AllowBackgroundClose)
        {
            container.IsDialogOpen = false;
        }
    }

    private static InteractiveContainer GetInteractiveContainerInstance()
    {
        InteractiveContainer container = null;
        try
        {
            container = ((ISingleViewApplicationLifetime)Application.Current.ApplicationLifetime).MainView.GetVisualDescendants().OfType<InteractiveContainer>().First();

        }
        catch (Exception exc)
        {

            try
            {
                container = ((IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime).MainWindow.GetVisualDescendants().OfType<InteractiveContainer>().First();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "You are trying to use a InteractiveContainer functionality without declaring one !");
            }

        }

        return container;
    }


    public static void ShowToast(Control Message, int seconds)
    {
        var container = GetInteractiveContainerInstance();

        container.ToastContent = Message;
        container.IsToastOpen = true;


        Task.Run((() =>
        {
            Thread.Sleep(seconds * 1000);
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                container.IsToastOpen = false;
            });
        }));
    }

    public static void ShowToast(string Message,int seconds=3)
    {
        var container = GetInteractiveContainerInstance();
        
        var txt = new TextBlock { Text = Message };

       
        txt[!ForegroundProperty] = new DynamicResourceExtension("SukiText");
        container.ToastContent = txt;
        container.IsToastOpen = true;

        Task.Run((() =>
        {
            Thread.Sleep(seconds * 1000);
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                container.IsToastOpen = false;
            });
        }));

    }

    public static void CloseDialog()
    {
        GetInteractiveContainerInstance().IsDialogOpen = false;
    }

    public static void WaitUntilDialogIsClosed()
    {
        InteractiveContainer container = null;

        Dispatcher.UIThread.InvokeAsync(() =>
        {
            container = GetInteractiveContainerInstance();
        });
        bool flag = true;

        do
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                flag = container.IsDialogOpen;
            });

            Thread.Sleep(200);
        } while (flag);

    }



    public static void ShowDialog(object content, bool allowBackgroundClose = true, bool showAtBottom = false)
    {
        var control = content as Control ?? ViewLocator.TryBuild(content);
        var container = GetInteractiveContainerInstance();

        container.IsDialogOpen = true;
        container.DialogContent = control;
        container.AllowBackgroundClose = allowBackgroundClose;
        container.ShowAtBottom = showAtBottom;
    }
}