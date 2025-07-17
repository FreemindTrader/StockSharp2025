// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.LoggedMessageBase
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using StockSharp.Xaml.Charting.Utility;
using TinyMessenger;

namespace StockSharp.Xaml.Charting
{
    public abstract class LoggedMessageBase : TinyMessageBase
    {
        public LoggedMessageBase( object sender )
          : base( sender )
        {
            UltrachartDebugLogger.Instance.WriteLine( "Publishing {0}, Sender={1}", ( object ) this.GetType().Name, ( object ) sender.GetType().Name );
        }
    }
}
