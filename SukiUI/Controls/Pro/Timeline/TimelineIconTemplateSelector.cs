using Avalonia.Controls.Shapes;
using Avalonia.Controls.Templates;
using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace SukiUI.Controls
{
    public class TimelineIconTemplateSelector : ResourceDictionary, IDataTemplate
    {

        public Control? Build(object? param)
        {
          
                string s = param.ToString();
                if (ContainsKey(s))
                {
                    object? o = this[s];
                    if (o is SolidColorBrush c)
                    {
                        var ellipse = new Ellipse() { Width = 12, Height = 12, Fill = c };
                        return ellipse;
                    }
                }
            
            return null;
        }

        public bool Match(object? data)
        {
            return data is TimelineItemType;
        }
    }
}
