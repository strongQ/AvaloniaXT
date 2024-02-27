using CommunityToolkit.Mvvm.ComponentModel;
using SukiUI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTExternalPage.ViewModels
{
    public partial class TimelineItemViewModel:ObservableObject
    {
        [ObservableProperty]
        private DateTime _time;
        [ObservableProperty]
        private string _timeFormat;
        [ObservableProperty]
        private string description;
        [ObservableProperty]
        private string header;
        [ObservableProperty]
        private TimelineItemType _itemType;
    }
}
