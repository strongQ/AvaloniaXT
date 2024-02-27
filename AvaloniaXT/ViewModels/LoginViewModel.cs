using Avalonia.Controls;
using AvaloniaXT.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using SukiUI.Controls;
using SukiUI.MessageBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XT.Common.Extensions;

namespace AvaloniaXT.ViewModels
{
    public partial class LoginViewModel : ObservableValidator
    {
        [ObservableProperty]
        private string _username="user";
        [ObservableProperty]
        private string _password="pwd";
        [ObservableProperty]
        private string _error;

        public LoginViewModel()
        {

        }
        [RelayCommand]
        public async Task Login()
        {
            var menuService = Register.App.GetService<IMenuToolService>();
            if (menuService != null)
            {
              var error= await menuService.Login(Username, Password);

                if (error.IsNotNullOrEmpty())
                {
                    Error = error;
                    return;
                }
                else
                {
                    Error = "";
                }
            }

            WeakReferenceMessenger.Default.Send<string>("Login");
        }
      


    }
}
