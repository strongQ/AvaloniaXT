
using AvaloniaXT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaXT.Services
{
    public class PageNavigationService
    {
        public Action<Type>? NavigationRequested { get; set; }

        public void RequestNavigation<T>() where T : XTPageBase
        {
            NavigationRequested?.Invoke(typeof(T));
        }
    }
}
