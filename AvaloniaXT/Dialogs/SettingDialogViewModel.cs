using Avalonia.Controls;
using AvaloniaXT.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Material.Icons;
using SukiUI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XT.Common.Interfaces;

namespace AvaloniaXT.Dialogs
{
    public partial class SettingDialogViewModel : XTPageBase
    {
        private IApiConfig _config;
        public SettingDialogViewModel(IApiConfig apiConfig)
        {
            _config = apiConfig;
            Url = _config.RemoteApiUrl;
        }
        [ObservableProperty]
        private string _url;
        public override bool IsDialog()
        {
            return true;
        }
        public override Control GetView()
        {
            return new SettingDialog();
        }
        [RelayCommand]
        public void CloseDialog()
        {
            _config.RemoteApiUrl = Url;
            InteractiveContainer.CloseDialog();
        }
       
    }
}
