using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using AvaloniaXT.Selectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTExternalPage.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace XTExternalPage.Selectors
{
    public class EcsTagSelector : DataTemplateSelector
    {
        public override string GetKey(object? param)
        {
            var tag = param as UnitTagModel;
            return tag.Type.ToString();
        }

        
    }
}
