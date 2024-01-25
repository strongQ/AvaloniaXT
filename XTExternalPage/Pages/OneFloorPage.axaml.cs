using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Remote.Protocol.Input;
using XTExternalPage.ViewModels;

namespace XTExternalPage;

public partial class OneFloorPage : UserControl
{
    public OneFloorPage()
    {
        InitializeComponent();
        this.Loaded += OneFloorPage_Loaded;
    }

    private bool canDrag = false;
    private Point position;
    private ScrollViewer? scrollView;

    private async void OneFloorPage_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
       var control= this.FindControl<ScrollViewer>("view");
        scrollView = control;

        var context = this.DataContext as BaseFloorViewModel;

        await context.Initial();
    }

    private void PointerPressedHandler(object sender, PointerPressedEventArgs args)
    {
       
        var control = sender as Control;
        var point = args.GetCurrentPoint(control);
       

        if (point.Properties.IsLeftButtonPressed)
        {
          
            position = point.Position;


            canDrag = true;
        }

    }

    private void PointerMovedHandler(object sender, PointerEventArgs args)
    {

        var control = sender as Control;
        var point = args.GetCurrentPoint(control);


        if (point.Properties.IsLeftButtonPressed)
        {
            var x = point.Position.X - position.X;
            var y = point.Position.Y - position.Y;
            if (scrollView != null)
                scrollView.Offset = scrollView.Offset + new Vector(x, y);
          
           
        }

    }



    private void PointerReleasedHandler(object sender, PointerReleasedEventArgs args)
    {
        canDrag = false;
       
    }
}