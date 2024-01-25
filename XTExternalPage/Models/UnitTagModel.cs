using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTExternalPage.Models
{
    public partial class UnitTagModel : ObservableObject
    {
        [ObservableProperty] private string _id;

        [ObservableProperty] private string _name;

        [ObservableProperty] private int _left;

        [ObservableProperty] private int _top;

        [ObservableProperty] private int _width;

        [ObservableProperty] private int _height;

        [ObservableProperty] private int _colorType;

       [ObservableProperty] private EquipTypeEnum _type;

       [ObservableProperty] private int _isCondition;


       [ObservableProperty] private string _condition;


    }
}
