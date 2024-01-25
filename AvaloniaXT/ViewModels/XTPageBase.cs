using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Material.Icons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaXT.ViewModels
{
    public abstract partial class XTPageBase(string displayName="Page", MaterialIconKind icon=MaterialIconKind.CursorDefault, int index = 0) : ObservableValidator
    {
        [ObservableProperty] private string _displayName = displayName;
        [ObservableProperty] private MaterialIconKind _icon = icon;
        [ObservableProperty] private int _index = index;
        [ObservableProperty]
        private bool _isLoading = false;
        /// <summary>
        /// 获取视图
        /// </summary>
        /// <returns></returns>
        public abstract Control GetView();

        public virtual bool IsDialog()
        {
            return false;
        }
    }
}
