// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.ExecutionTimer
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MatterHackers.Agg
{
    internal sealed class ExecutionTimer
    {
        private static readonly ExecutionTimer instanceExecutionTimer = new ExecutionTimer();
        private List<NamedExecutionTimer> CallStack;
        private List<NamedExecutionTimer> NamedTimerList;

        private ExecutionTimer()
        {
            this.NamedTimerList = new List<NamedExecutionTimer>();
            this.CallStack = new List<NamedExecutionTimer>();
        }

        public static ExecutionTimer Instance
        {
            get
            {
                return ExecutionTimer.instanceExecutionTimer;
            }
        }

        internal void Starting( NamedExecutionTimer namedTimer )
        {
            this.CallStack.Add( namedTimer );
        }

        internal void Stoping( NamedExecutionTimer namedTimer, double timeThisRun )
        {
            int count = this.CallStack.Count;
            if ( count > 1 )
                this.CallStack[ count - 2 ].TotalTimeExcludingSubroutines -= timeThisRun;
            this.CallStack.RemoveAt( count - 1 );
        }

        internal void AddTimer( NamedExecutionTimer namedTimer )
        {
            this.NamedTimerList.Add( namedTimer );
        }

        public void Reset()
        {
            foreach ( NamedExecutionTimer namedTimer in this.NamedTimerList )
                namedTimer.Reset();
        }

        public string GetResults( double totalTime )
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append( "***************************************\n" );
            stringBuilder.Append( "Total  | No Subs| %Total |%No Subs| Name\n" );
            foreach ( NamedExecutionTimer namedTimer in this.NamedTimerList )
            {
                if ( namedTimer.GetTotalSeconds() > 0.0 )
                {
                    stringBuilder.Append( string.Format( "{0:000.00}", ( object ) ( namedTimer.GetTotalSeconds() * 1000.0 ) ) + " | " + string.Format( "{0:000.00}", ( object ) ( namedTimer.GetTotalSecondsExcludingSubroutines() * 1000.0 ) ) + " | " + string.Format( "{0:00.00}", ( object ) Math.Min( 99.99, namedTimer.GetTotalSeconds() / totalTime * 100.0 ) ) + "% | " + string.Format( "{0:00.00}", ( object ) ( namedTimer.GetTotalSecondsExcludingSubroutines() / totalTime * 100.0 ) ) + "% | " + namedTimer.m_Name );
                    stringBuilder.Append( " (" + namedTimer.NumStarts.ToString() + ")\n" );
                }
            }
            stringBuilder.Append( "\n\n" );
            return stringBuilder.ToString();
        }

        public void AppendResultsToFile( string fileName, double totalTime )
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Append, FileAccess.Write);
            StreamWriter streamWriter = new StreamWriter((Stream) fileStream);
            streamWriter.Write( this.GetResults( totalTime ) );
            streamWriter.Close();
            fileStream.Close();
        }
    }
}
