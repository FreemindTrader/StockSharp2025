// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Configuration.ControlElementCollection
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using System.Configuration;

namespace StockSharp.Studio.Core.Configuration
{
    public class ControlElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ControlElement();
        }

        protected override object GetElementKey( ConfigurationElement element )
        {
            return ( ( ControlElement )element ).Type;
        }
    }
}
