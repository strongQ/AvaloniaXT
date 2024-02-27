using Avalonia.Data.Converters;
using Avalonia;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SukiUI.Controls
{
    public class TimelineFormatConverter : IMultiValueConverter
    {
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            if (values.Count > 1 && values[0] is DateTime date && values[1] is string s)
            {
                return date.ToString(s, culture);
            }
            return AvaloniaProperty.UnsetValue;
        }
    }
}
