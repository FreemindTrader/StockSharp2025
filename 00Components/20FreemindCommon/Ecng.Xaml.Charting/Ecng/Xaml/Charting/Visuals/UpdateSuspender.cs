// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.UpdateSuspender
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;

namespace StockSharp.Xaml.Charting.Visuals
{
    internal class UpdateSuspender : IUpdateSuspender, IDisposable
    {
        private static readonly IDictionary<ISuspendable, UpdateSuspender.SuspendInfo> _suspendedInstances = (IDictionary<ISuspendable, UpdateSuspender.SuspendInfo>) new Dictionary<ISuspendable, UpdateSuspender.SuspendInfo>();
        private static readonly object _syncRoot = new object();
        private readonly object _tag;
        private readonly ISuspendable _target;
        private bool _resumeTargetOnDispose;

        public bool IsSuspended
        {
            get
            {
                return UpdateSuspender.GetIsSuspended( this._target );
            }
        }

        public bool ResumeTargetOnDispose
        {
            get
            {
                return this._resumeTargetOnDispose;
            }
            set
            {
                if ( this._resumeTargetOnDispose == value )
                    return;
                this._resumeTargetOnDispose = value;
                lock ( UpdateSuspender._syncRoot )
                {
                    UpdateSuspender.SuspendInfo suspendedInstance = UpdateSuspender._suspendedInstances[this._target];
                    UpdateSuspender._suspendedInstances[ this._target ] = new UpdateSuspender.SuspendInfo( suspendedInstance.Counter, suspendedInstance.NeedToResumeCounter + ( value ? 1 : -1 ) );
                }
            }
        }

        internal UpdateSuspender( ISuspendable target, object tag )
          : this( target )
        {
            this._tag = tag;
        }

        internal UpdateSuspender( ISuspendable target )
        {
            this._target = target;
            lock ( UpdateSuspender._syncRoot )
            {
                if ( !UpdateSuspender._suspendedInstances.ContainsKey( this._target ) )
                    UpdateSuspender._suspendedInstances.Add( this._target, new UpdateSuspender.SuspendInfo( 0, 0 ) );
                this.Inc( this._target );
                this.ResumeTargetOnDispose = true;
            }
        }

        public void Dispose()
        {
            lock ( UpdateSuspender._syncRoot )
            {
                this._target.DecrementSuspend();
                if ( this.Dec( this._target ) != 0 )
                    return;
                this._resumeTargetOnDispose = UpdateSuspender._suspendedInstances[ this._target ].NeedToResumeCounter > 0;
                UpdateSuspender._suspendedInstances.Remove( this._target );
                this._target.ResumeUpdates( ( IUpdateSuspender ) this );
            }
        }

        public object Tag
        {
            get
            {
                return this._tag;
            }
        }

        internal static bool GetIsSuspended( ISuspendable target )
        {
            lock ( UpdateSuspender._syncRoot )
                return UpdateSuspender._suspendedInstances.ContainsKey( target ) && ( uint ) UpdateSuspender._suspendedInstances[ target ].Counter > 0U;
        }

        private void Inc( ISuspendable target )
        {
            lock ( UpdateSuspender._syncRoot )
            {
                UpdateSuspender.SuspendInfo suspendedInstance = UpdateSuspender._suspendedInstances[target];
                UpdateSuspender._suspendedInstances[ target ] = new UpdateSuspender.SuspendInfo( suspendedInstance.Counter + 1, suspendedInstance.NeedToResumeCounter );
            }
        }

        private int Dec( ISuspendable target )
        {
            lock ( UpdateSuspender._syncRoot )
            {
                UpdateSuspender.SuspendInfo suspendedInstance = UpdateSuspender._suspendedInstances[target];
                UpdateSuspender._suspendedInstances[ target ] = new UpdateSuspender.SuspendInfo( suspendedInstance.Counter - 1, suspendedInstance.NeedToResumeCounter );
                return suspendedInstance.Counter - 1;
            }
        }

        private struct SuspendInfo
        {
            public SuspendInfo( int c, int ntrc )
            {
                this.Counter = c;
                this.NeedToResumeCounter = ntrc;
            }

            public int Counter
            {
                get;
            }

            public int NeedToResumeCounter
            {
                get;
            }
        }
    }
}
