using AvaloniaXT.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using NetCoreAudio;
using SukiUI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AvaloniaXT.Services
{
    public class AudioPlayService
    {
        private Player _player;
        private CancellationTokenSource _source;
        public void PlayAudio(bool play)
        {

            var menuService = Register.App.GetService<IMenuToolService>();
            if (menuService != null)
            {
                if (_source != null)
                {
                    _source.Cancel();

                }

                if (!menuService.ShowAudio || !play)
                {
                    return;
                }
                _source = new CancellationTokenSource();

                Task.Factory.StartNew(async () =>
                {
                    try
                    {

                        _player = new Player();
                        await _player.Play("Audios/alarm.wav");
                        _player.PlaybackFinished += Player_PlaybackFinished;



                    }
                    catch (Exception ex)
                    {
                        await SukiHost.ShowToast("Error", ex.Message);
                    }
                }, _source.Token);


            }



        }

        private async void Player_PlaybackFinished(object? sender, EventArgs e)
        {
            if (_source.IsCancellationRequested) return;
            _player = new Player();
            await _player.Play("Audios/alarm.wav");
            _player.PlaybackFinished += Player_PlaybackFinished;
        }
    }
}
