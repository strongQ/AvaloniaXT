using Avalonia.Controls;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using Material.Icons;
using ReactiveUI;
using SukiUI;
using SukiUI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaXT.ViewModels
{
    public partial class AboutViewModel:XTPageBase
    {
       
        private readonly SukiTheme _theme;
        public AboutViewModel() : base(Lang.Resources.About, MaterialIconKind.About, 1)
        {
            _theme = SukiTheme.GetInstance();
          
        }

        public override Control GetView()
        {
            return new About();
        }
        private bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set 
            { 
                _isChecked = value;
                OnPropertyChanged();
                _theme.SwitchBaseTheme();
            }
        }

    }
}
