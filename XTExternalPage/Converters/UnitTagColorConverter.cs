using Avalonia.Data.Converters;
using Avalonia.Media;
using SukiUI.Theme;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTExternalPage.Converters
{
    public class UnitTagColorConverter : IValueConverter
    {
        
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
          
            if (parameter!=null && parameter.ToString() == "1")
            {
                if (value!=null && value.ToString() == "1")
                {
                    return Brushes.LightPink;
                }
                else return Brushes.Black;
            }
            int color = (int)value;
            if (color == 0)
            {
                return null;
            }
            else if (color == 1)
            {
                return Brushes.Green;
            }
            else if (color == 2) { return Brushes.Red; }
            else return Brushes.LightPink;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
