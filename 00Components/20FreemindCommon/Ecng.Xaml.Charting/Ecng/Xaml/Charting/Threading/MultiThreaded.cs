// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Threading.MultiThreaded
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Threading;

namespace Ecng.Xaml.Charting
{
    internal class MultiThreaded
    {
        internal static void For( int fromInclusive, int toExclusive, Action<int> action )
        {
            int processorCount = Environment.ProcessorCount;
            int index = fromInclusive - 1;
            object obj = new object();
            Action action1 = (Action) (() =>
      {
          int num;
          while ((num = Interlocked.Increment(ref index)) < toExclusive)
              action(num);
      });
            IAsyncResult[] asyncResultArray = new IAsyncResult[processorCount];
            for ( int index1 = 0 ; index1 < processorCount ; ++index1 )
                asyncResultArray[ index1 ] = action1.BeginInvoke( ( AsyncCallback ) null, ( object ) null );
            for ( int index1 = 0 ; index1 < processorCount ; ++index1 )
                action1.EndInvoke( asyncResultArray[ index1 ] );
        }
    }
}
