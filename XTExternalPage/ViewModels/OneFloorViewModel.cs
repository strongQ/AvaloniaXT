using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Styling;
using AvaloniaXT;
using AvaloniaXT.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Material.Icons;
using Microsoft.Extensions.DependencyInjection;
using SukiUI.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XT.Common.Extensions;
using XT.Common.Services;
using XTExternalPage.Apis;
using XTExternalPage.Models;
using XTExternalPage.Services;

namespace XTExternalPage.ViewModels
{
    public partial class OneFloorViewModel : BaseFloorViewModel
    {
        public OneFloorViewModel() : base("1楼", MaterialIconKind.One, 2)
        {
            
          
        }
       
    }
}
