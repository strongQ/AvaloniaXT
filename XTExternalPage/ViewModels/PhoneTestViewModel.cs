using Avalonia.Collections;
using Avalonia.Controls;
using AvaloniaXT.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Material.Icons;
using Microsoft.Maui.Devices;
using SukiUI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTExternalPage.ViewModels
{
    public partial class PhoneTestViewModel : XTPageBase
    {
        [ObservableProperty]
        private string _deviceInfoString;
        public PhoneTestViewModel(): base("Test", MaterialIconKind.Phone, 2)
        {

            if (OperatingSystem.IsAndroid())
            {
                var stringBuilder = new StringBuilder();

                stringBuilder.AppendLine($"Model: {DeviceInfo.Current.Model}");
                stringBuilder.AppendLine($"Manufacturer: {DeviceInfo.Current.Manufacturer}");
                stringBuilder.AppendLine($"Name: {DeviceInfo.Current.Name}");
                stringBuilder.AppendLine($"OS Version: {DeviceInfo.Current.VersionString}");
                stringBuilder.AppendLine($"Idiom: {DeviceInfo.Current.Idiom}");
                stringBuilder.AppendLine($"Platform: {DeviceInfo.Current.Platform}");

                DeviceInfoString = stringBuilder.ToString();
            }

            Items = new AvaloniaList<TimelineItemViewModel>
            {
                new()
        {
            Time = DateTime.Now,
            Description = "Item 1",
            Header = "审核中",         
            ItemType = TimelineItemType.Success,
        },
        new()
        {
            Time = DateTime.Now,
            Description = "Item 2",
            Header = "发布成功",
            ItemType = TimelineItemType.Ongoing,
        },
        new()
        {
            Time = DateTime.Now,
            Description = "Item 3",
            Header = "审核失败",
            ItemType = TimelineItemType.Error,
        }
            };

            Lists = new AvaloniaList<string>
            {
                "dd",
                "bb",
                "cc",
                "dd"
            };
        }
        public override Control GetView()
        {
            return new PhoneTest();
        }
        [RelayCommand]
        public void LaunchToast()
        {
            InteractiveContainer.ShowToast(new TextBlock() { Text = "Hit !" }, 3);
        }
        [ObservableProperty]
        private AvaloniaList<TimelineItemViewModel> _items;
        [ObservableProperty]
        private AvaloniaList<String> _lists;

    }
}
