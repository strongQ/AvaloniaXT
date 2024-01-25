using Avalonia.Collections;
using Avalonia.Controls;
using AvaloniaXT;
using AvaloniaXT.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Material.Icons;
using Microsoft.Extensions.DependencyInjection;
using SukiUI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTExternalPage.Apis;
using XTExternalPage.Models;

namespace XTExternalPage.ViewModels
{
    public  partial class BaseFloorViewModel:XTPageBase
    {
        [ObservableProperty]
        private int _height;
        [ObservableProperty]
        private int _width;

        private string _floorName = string.Empty;

        public override Control GetView()
        {
            return new OneFloorPage();
        }



        public AvaloniaList<UnitTagModel> Datas { get; set; } = new();

        public BaseFloorViewModel(string displayName,MaterialIconKind kind,int index) : base(displayName, kind, index)
        {
            _floorName = displayName;
        }

       
       

        [RelayCommand]
        public async Task Initial()
        {
            IsLoading = true;

            var api = Register.App.GetService<EcsTagApi>();

            try
            {

                var view = await api.GetTags(_floorName);

                if (view.Tags == null)
                {
                    IsLoading = false;
                    return;
                }

                var big = api.GetView(view);
                Height = big.Height;
                Width = big.Width;

                await SukiHost.ShowToast("Successfully Get Tags", $"Number {view.Tags?.Count}.");


                var tags = api.ChangeToUnits(view);
                Datas.Clear();
                Datas.AddRange(tags);
            }
            catch(Exception ex)
            {
                await SukiHost.ShowToast("Error",ex.Message);
            }


            IsLoading = false;
        }


    }
}
