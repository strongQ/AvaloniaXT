using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System;

namespace SukiUI.Controls;

[TemplatePart(PART_Image, typeof(Image))]
[TemplatePart(PART_Layer, typeof(VisualLayerManager))]
[PseudoClasses(PC_Moving)]
public  class ImageViewer : TemplatedControl
{
    public const string PART_Image = "PART_Image";
    public const string PART_Layer = "PART_Layer";
    public const string PC_Moving = ":moving";

    private Image? _image = null!;
    private VisualLayerManager? _layer;
    private Point? _lastClickPoint;
    private Point? _lastLocation;
    private bool _moving;

    public static readonly StyledProperty<Control?> OverlayerProperty = AvaloniaProperty.Register<ImageViewer, Control?>(
        nameof(Overlayer));

    public Control? Overlayer
    {
        get => GetValue(OverlayerProperty);
        set => SetValue(OverlayerProperty, value);
    }

    public static readonly StyledProperty<IImage?> SourceProperty = Image.SourceProperty.AddOwner<ImageViewer>();
    public IImage? Source
    {
        get => GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    private double _scale = 1;

    public static readonly DirectProperty<ImageViewer, double> ScaleProperty = AvaloniaProperty.RegisterDirect<ImageViewer, double>(
        nameof(Scale), o => o.Scale, (o, v) => o.Scale = v, unsetValue: 1);

    public double Scale
    {
        get => _scale;
        set => SetAndRaise(ScaleProperty, ref _scale, value);
    }

    private double _translateX;

    public static readonly DirectProperty<ImageViewer, double> TranslateXProperty = AvaloniaProperty.RegisterDirect<ImageViewer, double>(
        nameof(TranslateX), o => o.TranslateX, (o, v) => o.TranslateX = v, unsetValue: 0);

    public double TranslateX
    {
        get => _translateX;
        set => SetAndRaise(TranslateXProperty, ref _translateX, value);
    }

    private double _translateY;

    public static readonly DirectProperty<ImageViewer, double> TranslateYProperty =
        AvaloniaProperty.RegisterDirect<ImageViewer, double>(
            nameof(TranslateY), o => o.TranslateY, (o, v) => o.TranslateY = v, unsetValue: 0);

    public double TranslateY
    {
        get => _translateY;
        set => SetAndRaise(TranslateYProperty, ref _translateY, value);
    }

    public static readonly StyledProperty<double> SmallChangeProperty = AvaloniaProperty.Register<ImageViewer, double>(
        nameof(SmallChange), defaultValue: 1);

    public double SmallChange
    {
        get => GetValue(SmallChangeProperty);
        set => SetValue(SmallChangeProperty, value);
    }

    public static readonly StyledProperty<double> LargeChangeProperty = AvaloniaProperty.Register<ImageViewer, double>(
        nameof(LargeChange), defaultValue: 10);

    public double LargeChange
    {
        get => GetValue(LargeChangeProperty);
        set => SetValue(LargeChangeProperty, value);
    }

    public static readonly StyledProperty<Stretch> StretchProperty =
        Image.StretchProperty.AddOwner<ImageViewer>(new StyledPropertyMetadata<Stretch>(Stretch.Uniform));

    public Stretch Stretch
    {
        get => GetValue(StretchProperty);
        set => SetValue(StretchProperty, value);
    }

    static ImageViewer()
    {
        FocusableProperty.OverrideDefaultValue<ImageViewer>(true);
        OverlayerProperty.Changed.AddClassHandler<ImageViewer>((o, e) => o.OnOverlayerChanged(e));
        SourceProperty.Changed.AddClassHandler<ImageViewer>((o, e) => o.OnSourceChanged(e));
        TranslateXProperty.Changed.AddClassHandler<ImageViewer>((o, e) => o.OnTranslateXChanged(e));
        TranslateYProperty.Changed.AddClassHandler<ImageViewer>((o, e) => o.OnTranslateYChanged(e));
        StretchProperty.Changed.AddClassHandler<ImageViewer>((o, e) => o.OnStretchChanged(e));
    }

    private void OnTranslateYChanged(AvaloniaPropertyChangedEventArgs args)
    {
        if (_moving) return;
        var newValue = args.GetNewValue<double>();
        if (_lastLocation is not null)
        {
            _lastLocation = _lastLocation.Value.WithY(newValue);
        }
        else
        {
            _lastLocation = new Point(0, newValue);
        }
    }

    private void OnTranslateXChanged(AvaloniaPropertyChangedEventArgs args)
    {
        if (_moving) return;
        var newValue = args.GetNewValue<double>();
        if (_lastLocation is not null)
        {
            _lastLocation = _lastLocation.Value.WithX(newValue);
        }
        else
        {
            _lastLocation = new Point(newValue, 0);
        }
    }

    private void OnOverlayerChanged(AvaloniaPropertyChangedEventArgs args)
    {
        var control = args.GetNewValue<Control?>();
        if (control is { } c)
        {
            AdornerLayer.SetAdorner(this, c);
        }
    }

    private void OnSourceChanged(AvaloniaPropertyChangedEventArgs args)
    {
        IImage image = args.GetNewValue<IImage>();
        Size size = image.Size;
        double width = this.Bounds.Width;
        double height = this.Bounds.Height;
        if (_image is not null)
        {
            _image.Width = size.Width;
            _image.Height = size.Height;
        }
        Scale = GetScaleRatio(width / size.Width, height / size.Height, this.Stretch);
    }

    private void OnStretchChanged(AvaloniaPropertyChangedEventArgs args)
    {
        var stretch = args.GetNewValue<Stretch>();
        Scale = GetScaleRatio(Width / _image!.Width, Height / _image!.Height, stretch);
    }

    private double GetScaleRatio(double widthRatio, double heightRatio, Stretch stretch)
    {
        return stretch switch
        {
            Stretch.Fill => 1d,
            Stretch.None => 1d,
            Stretch.Uniform => Math.Min(widthRatio, heightRatio),
            Stretch.UniformToFill => Math.Max(widthRatio, heightRatio),
            _ => 1d,
        };
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _image = e.NameScope.Get<Image>(PART_Image);
        _layer = e.NameScope.Get<VisualLayerManager>(PART_Layer);
        if (Source is { } i)
        {
            Size size = i.Size;
            double width = Bounds.Width;
            double height = Bounds.Height;
            _image.Width = size.Width;
            _image.Height = size.Height;
            Scale = GetScaleRatio(width / size.Width, height / size.Height, this.Stretch);
        }
        if (Overlayer is { } c)
        {
            AdornerLayer.SetAdorner(this, c);
        }
    }



    protected override void OnPointerWheelChanged(PointerWheelEventArgs e)
    {
        base.OnPointerWheelChanged(e);
        if (e.Delta.Y > 0)
        {
            Scale *= 1.1;
        }
        else
        {
            var scale = Scale;
            scale /= 1.1;
            if (scale < 0.1) scale = 0.1;
            Scale = scale;
        }
    }

    protected override void OnPointerMoved(PointerEventArgs e)
    {
        base.OnPointerMoved(e);
        if (e.Pointer.Captured == this && _lastClickPoint != null)
        {
            PseudoClasses.Set(PC_Moving, true);
            Point p = e.GetPosition(this);
            double deltaX = p.X - _lastClickPoint.Value.X;
            double deltaY = p.Y - _lastClickPoint.Value.Y;
            TranslateX = deltaX + (_lastLocation?.X ?? 0);
            TranslateY = deltaY + (_lastLocation?.Y ?? 0);
        }
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
        e.Pointer.Capture(this);
        _lastClickPoint = e.GetPosition(this);
        _moving = true;
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        base.OnPointerReleased(e);
        e.Pointer.Capture(null);
        _lastLocation = new Point(TranslateX, TranslateY);
        PseudoClasses.Set(PC_Moving, false);
        _moving = false;
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        double step = e.KeyModifiers.HasFlag(KeyModifiers.Control) ? LargeChange : SmallChange;
        switch (e.Key)
        {
            case Key.Left:
                TranslateX -= step;
                break;
            case Key.Right:
                TranslateX += step;
                break;
            case Key.Up:
                TranslateY -= step;
                break;
            case Key.Down:
                TranslateY += step;
                break;
        }
        base.OnKeyDown(e);
    }
}