using AvaloniaXT;
using AvaloniaXT.Interfaces;
using AvaloniaXT.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using SukiUI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using XT.Common.Interfaces;

namespace XTExternalPage.Services
{
    public class MenuToolService : IMenuToolService
    {
        private readonly IApiConfig _config;

        private CancellationTokenSource _source=new CancellationTokenSource();

        private ClientWebSocket _ws;
        public bool ShowSearch { get; set; } = true;
        public bool ShowAudio { get; set;} = false;
        

        public MenuToolService(IApiConfig apiConfig) { _config = apiConfig; }
        public async Task Initial()
        {
            return;
            
            _source.Cancel();

          

            _ws?.Dispose();



            _source = new CancellationTokenSource();


             SocketsHttpHandler handler = new();
              _ws = new();

          
            await _ws.ConnectAsync(new Uri($"{_config.RemoteApiUrl.Replace("http","ws")}/ws"), new HttpMessageInvoker(handler), _source.Token);


            while (true)
            {
                try
                {
                    if (_source.IsCancellationRequested) return;
                    if (_ws.State == WebSocketState.Open)
                    {
                        var buffer = new byte[1024 * 4];
                        var result = await _ws.ReceiveAsync(new ArraySegment<byte>(buffer), _source.Token);
                        var msg = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        if (result.MessageType == WebSocketMessageType.Binary)
                        {
                            await SukiHost.ShowToast("Warn", msg,TimeSpan.FromSeconds(30));
                        }
                        else
                        {
                            await SukiHost.ShowToast("Log", msg,TimeSpan.FromSeconds(30));
                        }
                      

                       
                    }
                    else
                    {
                        _ws?.Dispose();
                        _ws = new();
                        await _ws.ConnectAsync(new Uri($"{_config.RemoteApiUrl.Replace("http", "ws")}/ws"), new HttpMessageInvoker(handler), _source.Token);
                    }




                    // handle

                    await Task.Delay(100);
                }
                catch (Exception ex)
                {
                    await SukiHost.ShowToast("Error", ex.Message);
                    await Task.Delay(5000);
                }
            }
        }

        public async Task SearchClick()
        {
            SukiHost.ShowDialog(Register.App.GetRequiredKeyedService<XTPageBase>("Search"),true,true);
           
        }

        public async Task<string> Login(string username, string password)
        {
            if (username == "user" && password == "pwd") return "";
            return "账号密码有误";
        }

        public string GetMainTitle()
        {
            return Lang.Resources.MainTitle;
        }
    }
}
