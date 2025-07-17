// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Common.Extensions.DispatcherExtensions
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Windows.Threading;

namespace Ecng.Xaml.Charting
{
    internal static class DispatcherExtensions
    {
        internal static void BeginInvokeAlways( this Dispatcher dispatcher, Action operation )
        {
            dispatcher.BeginInvoke( ( Delegate ) operation );
        }

        internal static void BeginInvokeIfRequired( this Dispatcher dispatcher, Action operation )
        {
            if ( dispatcher == null || dispatcher.CheckAccess() )
                operation();
            else
                dispatcher.BeginInvoke( ( Delegate ) operation );
        }
    }
}
