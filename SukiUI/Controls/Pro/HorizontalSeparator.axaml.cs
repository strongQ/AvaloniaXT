using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SukiUI.Controls;

public partial class HorizontalSeparator : UserControl
{
    public HorizontalSeparator()
    {
        InitializeComponent();
    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}