using Avalonia.Controls;
using Avalonia.Controls.Templates;
using AvaloniaXT.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AvaloniaXT
{
    public class ViewLocator : IDataTemplate
    {
        private readonly Dictionary<object, Control> _controlCache;

        public ViewLocator()
        {
            _controlCache = new Dictionary<object, Control>();
        }

        public Control Build(object? data)
        {
            var result = data as XTPageBase;
            if (result == null) 
                return new TextBlock { Text = $"No View ." };

            if (!_controlCache.TryGetValue(data!, out var res))
            {
                res ??= result.GetView();
                _controlCache[data!] = res;
            }

            res.DataContext = data;
            return res;
        }

        public bool Match(object? data) => data is INotifyPropertyChanged;
    }
}