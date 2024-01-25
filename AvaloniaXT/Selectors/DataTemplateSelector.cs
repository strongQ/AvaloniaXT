using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using HarfBuzzSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaXT.Selectors
{
    public abstract class DataTemplateSelector:IDataTemplate
    {
        // This Dictionary should store our shapes. We mark this as [Content], so we can directly add elements to it later.
        [Content]
        public Dictionary<string, IDataTemplate> AvailableTemplates { get; } = new Dictionary<string, IDataTemplate>();

        // Build the DataTemplate here
        public Control Build(object? param)
        {
            var key = GetKey(param); // Our Keys in the dictionary are strings, so we call .ToString() to get the key to look up
            if (key is null) // If the key is null, we throw an ArgumentNullException
            {
                return new TextBlock { Text = $"No View ." };
            }
            return AvailableTemplates[key].Build(param); // finally we look up the provided key and let the System build the DataTemplate for us
        }
        public abstract string GetKey(object? param);
        // Check if we can accept the provided data
        public  bool Match(object? data)
        {
            if (data is null) return false;
            var key = GetKey(data);
            if (AvailableTemplates.ContainsKey(key))
            {
                return true;
            }
            else { return false; }
        }
      
    }
}
