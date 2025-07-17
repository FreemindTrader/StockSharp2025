// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Utility.DispatcherUtil
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Windows.Threading;

namespace StockSharp.Xaml.Charting.Utility
{
    internal class DispatcherUtil : IDispatcherFacade
    {
        private Dispatcher _dispatcherInstance;
        private static bool _testMode;

        public DispatcherUtil( Dispatcher dispatcher )
        {
            if ( DispatcherUtil._testMode )
                return;
            this._dispatcherInstance = dispatcher;
        }

        public void BeginInvoke( Action action, DispatcherPriority dispatcherPriority )
        {
            if ( this._dispatcherInstance == null )
                action();
            else
                this._dispatcherInstance.BeginInvoke( ( Delegate ) action, dispatcherPriority, new object[ 0 ] );
        }

        public void BeginInvokeIfRequired( Action action, DispatcherPriority priority )
        {
            if ( this._dispatcherInstance == null || this._dispatcherInstance.CheckAccess() )
                action();
            else
                this.BeginInvoke( action, priority );
        }

        public static void SetTestMode()
        {
            DispatcherUtil._testMode = true;
        }

        public static bool GetTestMode()
        {
            return DispatcherUtil._testMode;
        }
    }
}
