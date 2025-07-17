// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.NamedExecutionTimer
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Diagnostics;

namespace MatterHackers.Agg
{
    internal class NamedExecutionTimer
    {
        internal long LastStartTicks;
        internal uint RecurseLevel;
        internal uint NumStarts;
        internal string m_Name;
        internal double TotalRunningTime;
        internal double TotalTimeExcludingSubroutines;
        private static Stopwatch singleStopWatch;

        private long GetCurrentTicks()
        {
            if ( NamedExecutionTimer.singleStopWatch == null )
            {
                NamedExecutionTimer.singleStopWatch = new Stopwatch();
                NamedExecutionTimer.singleStopWatch.Start();
            }
            return NamedExecutionTimer.singleStopWatch.ElapsedTicks;
        }

        public NamedExecutionTimer( string Name )
        {
            this.Reset();
            this.m_Name = Name;
            ExecutionTimer.Instance.AddTimer( this );
        }

        public void Start()
        {
            if ( this.RecurseLevel != 0U )
                throw new NotImplementedException();
            this.LastStartTicks = this.GetCurrentTicks();
            ExecutionTimer.Instance.Starting( this );
            ++this.NumStarts;
            ++this.RecurseLevel;
        }

        public bool IsRunning()
        {
            return this.LastStartTicks > 0L;
        }

        public void Stop()
        {
            if ( this.RecurseLevel == 0U )
                throw new InvalidOperationException();
            --this.RecurseLevel;
            if ( this.RecurseLevel != 0U )
                return;
            long num = this.GetCurrentTicks() - this.LastStartTicks;
            this.LastStartTicks = 0L;
            double timeThisRun = (double) num / (double) Stopwatch.Frequency;
            this.TotalRunningTime += timeThisRun;
            this.TotalTimeExcludingSubroutines += timeThisRun;
            ExecutionTimer.Instance.Stoping( this, timeThisRun );
        }

        internal void Reset()
        {
            this.LastStartTicks = 0L;
            this.RecurseLevel = 0U;
            this.NumStarts = 0U;
            this.TotalRunningTime = 0.0;
            this.TotalTimeExcludingSubroutines = 0.0;
        }

        public double GetTotalSeconds()
        {
            return this.TotalRunningTime;
        }

        public double GetTotalSecondsExcludingSubroutines()
        {
            return this.TotalTimeExcludingSubroutines;
        }
    }
}
