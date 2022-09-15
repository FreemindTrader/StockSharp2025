// Decompiled with JetBrains decompiler
// Type: Ecng.ComponentModel.DispatcherNotifiableObject`1
// Assembly: Ecng.ComponentModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 261D7AEC-F6F7-407C-AFF2-E6AD402BE28A
// Assembly location: T:\00-FreemindTrader\packages\ecng.componentmodel\1.0.143\lib\netstandard2.0\Ecng.ComponentModel.dll

using Ecng.Collections;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Ecng.ComponentModel
{
    public class DispatcherNotifiableObject<T> : CustomObjectWrapper<T> where T : class, INotifyPropertyChanged
    {
        private readonly SynchronizedSet<string> _names = new SynchronizedSet<string>();
        private readonly IDispatcher _dispatcher;
        private DateTime _nextTime;
        private TimeSpan _notifyInterval;

        private static DispatcherNotifiableObjectTimer Timer
        {
            get
            {
                return DispatcherNotifiableObjectTimer.Instance;
            }
        }

        protected TimeSpan NotifyInterval
        {
            get
            {
                return this._notifyInterval;
            }
            set
            {
                this._notifyInterval = value;
                DispatcherNotifiableObject<T>.Timer.Interval = value;
            }
        }

        public DispatcherNotifiableObject( IDispatcher dispatcher, T obj )
          : base( obj )
        {
            IDispatcher dispatcher1 = dispatcher;
            if ( dispatcher1 == null )
                throw new ArgumentNullException( nameof( dispatcher ) );
            this._dispatcher = dispatcher1;
            this.NotifyInterval = TimeSpan.FromMilliseconds( 333.0 );
            DispatcherNotifiableObject<T>.Timer.Tick += new Action( this.NotifiableObjectGuiWrapperTimerOnTick );
            this.Obj.PropertyChanged += ( PropertyChangedEventHandler )( ( _, args ) => this._names.Add( args.PropertyName ) );
        }

        private void NotifiableObjectGuiWrapperTimerOnTick()
        {
            if ( this.IsDisposed )
                return;
            DateTime utcNow = DateTime.UtcNow;
            if ( utcNow < this._nextTime )
                return;
            TimeSpan timeSpan = this.NotifyInterval;
            if ( timeSpan < DispatcherNotifiableObject<T>.Timer.Interval )
                timeSpan = DispatcherNotifiableObject<T>.Timer.Interval;
            this._nextTime = utcNow + timeSpan;
            string[ ] names;
            lock ( this._names.SyncRoot )
            {
                names = this._names.Where<string>( new Func<string, bool>( this.NeedToNotify ) ).ToArray<string>();
                this._names.Clear();
            }
            if ( names.Length == 0 )
                return;

            this._dispatcher.InvokeAsync( ( Action )( () => ( ( IEnumerable<string> )names ).ForEach<string>( new Action<string>( OnPropertyChanged ) ) ) );
        }

        protected override void DisposeManaged()
        {
            DispatcherNotifiableObject<T>.Timer.Tick -= new Action( this.NotifiableObjectGuiWrapperTimerOnTick );
            base.DisposeManaged();
        }

        protected virtual bool NeedToNotify( string propName )
        {
            return true;
        }

        protected override IEnumerable<EventDescriptor> OnGetEvents()
        {
            return base.OnGetEvents().Where<EventDescriptor>( ( Func<EventDescriptor, bool> )( ed => ed.Name != "PropertyChanged" ) ).Concat<EventDescriptor>( ( IEnumerable<EventDescriptor> )new EventDescriptor[1]
            {
        TypeDescriptor.GetEvents((object) this, true).OfType<EventDescriptor>().First<EventDescriptor>((Func<EventDescriptor, bool>) (ed => ed.Name == "PropertyChanged"))
            } );
        }
    }
}
