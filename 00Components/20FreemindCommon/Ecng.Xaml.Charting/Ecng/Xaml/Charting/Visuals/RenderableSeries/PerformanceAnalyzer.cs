// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.RenderableSeries.PerformanceAnalyzer
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace StockSharp.Xaml.Charting.Visuals.RenderableSeries
{
    public class PerformanceAnalyzer
    {
        private readonly Stopwatch _watch = new Stopwatch();
        private readonly List<Tuple<int, TimeSpan, string>> _messages = new List<Tuple<int, TimeSpan, string>>(100);

        public void Restart( string msg = null )
        {
            _messages.Clear();
            _watch.Restart();
            if ( msg == null )
            {
                return;
            }

            Checkpoint( msg );
        }

        public void Checkpoint( string msg )
        {
            _messages.Add( Tuple.Create<int, TimeSpan, string>( Thread.CurrentThread.ManagedThreadId, _watch.Elapsed, msg ) );
        }

        public void Stop( string msg = null )
        {
            _watch.Stop();
            if ( msg == null )
            {
                return;
            }

            Checkpoint( msg );
        }

        public IEnumerable<string> Report()
        {
            return _messages.Select<Tuple<int, TimeSpan, string>, string>( ( Func<Tuple<int, TimeSpan, string>, string> ) ( t => string.Format( "{0} - {1:0.###}ms - {2}", ( object ) t.Item1, ( object ) t.Item2.TotalMilliseconds, ( object ) t.Item3 ) ) );
        }

        public string Report( string separator )
        {
            return string.Join( separator, Report() );
        }
    }
}
