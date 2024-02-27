using Avalonia.Controls;
using AvaloniaXT.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SukiUI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTExternalPage.Dialogs
{
    public partial class SearchDialogViewModel : XTPageBase
    {
        public override Control GetView()
        {
            return new SearchDialogView();
        }
        public override bool IsDialog()
        {
            return true;
        }
        [ObservableProperty]
        private string _search;

        [RelayCommand]
        public void CloseDialog()
        {
            SukiHost.CloseDialog();
        }
    }
}
