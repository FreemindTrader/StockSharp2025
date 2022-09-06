
using System;

namespace StockSharp.Studio.Controls
{
    public class DockingWindowTypeAttribute : Attribute
    {
        public bool IsToolWindow { get; }

        public DockingWindowTypeAttribute( bool isToolWindow = false )
        {
            this.IsToolWindow = isToolWindow;
        }
    }
}
