using Avalonia.Controls;

using Material.Icons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaXT.ViewModels
{
    public class AboutViewModel() : XTPageBase("About", MaterialIconKind.About, 1)
    {
       

        public override Control GetView()
        {
            return new About();
        }
    }
}
