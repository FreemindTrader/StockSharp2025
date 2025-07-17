// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartModifiers.MasterSlaveChartModifier
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.ObjectModel;
using StockSharp.Xaml.Charting.Common.Extensions;

namespace StockSharp.Xaml.Charting.ChartModifiers
{
    public abstract class MasterSlaveChartModifier : ChartModifierBase
    {
        private ObservableCollection<MasterSlaveChartModifier> _slaves = new ObservableCollection<MasterSlaveChartModifier>();
        private bool _processingEvent;

        private ObservableCollection<MasterSlaveChartModifier> Slaves
        {
            get
            {
                return this._slaves;
            }
        }

        public override void OnModifierMouseMove( ModifierMouseArgs e )
        {
            try
            {
                if ( this._processingEvent )
                    return;
                this._processingEvent = true;
                base.OnModifierMouseMove( e );
                this.Slaves.ForEachDo<MasterSlaveChartModifier>( ( Action<MasterSlaveChartModifier> ) ( x => x.OnModifierMouseMove( e ) ) );
            }
            finally
            {
                this._processingEvent = false;
            }
        }

        public override void OnModifierDoubleClick( ModifierMouseArgs e )
        {
            try
            {
                if ( this._processingEvent )
                    return;
                this._processingEvent = true;
                base.OnModifierDoubleClick( e );
                this.Slaves.ForEachDo<MasterSlaveChartModifier>( ( Action<MasterSlaveChartModifier> ) ( x => x.OnModifierDoubleClick( e ) ) );
            }
            finally
            {
                this._processingEvent = false;
            }
        }

        public override void OnModifierMouseDown( ModifierMouseArgs e )
        {
            try
            {
                if ( this._processingEvent )
                    return;
                this._processingEvent = true;
                base.OnModifierMouseDown( e );
                this.Slaves.ForEachDo<MasterSlaveChartModifier>( ( Action<MasterSlaveChartModifier> ) ( x => x.OnModifierMouseDown( e ) ) );
            }
            finally
            {
                this._processingEvent = false;
            }
        }

        public override void OnModifierMouseUp( ModifierMouseArgs e )
        {
            try
            {
                if ( this._processingEvent )
                    return;
                this._processingEvent = true;
                base.OnModifierMouseUp( e );
                this.Slaves.ForEachDo<MasterSlaveChartModifier>( ( Action<MasterSlaveChartModifier> ) ( x => x.OnModifierMouseUp( e ) ) );
            }
            finally
            {
                this._processingEvent = false;
            }
        }

        public override void OnModifierMouseWheel( ModifierMouseArgs e )
        {
            try
            {
                if ( this._processingEvent )
                    return;
                this._processingEvent = true;
                base.OnModifierMouseWheel( e );
                this.Slaves.ForEachDo<MasterSlaveChartModifier>( ( Action<MasterSlaveChartModifier> ) ( x => x.OnModifierMouseWheel( e ) ) );
            }
            finally
            {
                this._processingEvent = false;
            }
        }

        public override void OnModifierTouchDown( ModifierTouchManipulationArgs e )
        {
            try
            {
                if ( this._processingEvent )
                    return;
                this._processingEvent = true;
                base.OnModifierTouchDown( e );
                this.Slaves.ForEachDo<MasterSlaveChartModifier>( ( Action<MasterSlaveChartModifier> ) ( x => x.OnModifierTouchDown( e ) ) );
            }
            finally
            {
                this._processingEvent = false;
            }
        }

        public override void OnModifierTouchMove( ModifierTouchManipulationArgs e )
        {
            try
            {
                if ( this._processingEvent )
                    return;
                this._processingEvent = true;
                base.OnModifierTouchDown( e );
                this.Slaves.ForEachDo<MasterSlaveChartModifier>( ( Action<MasterSlaveChartModifier> ) ( x => x.OnModifierTouchMove( e ) ) );
            }
            finally
            {
                this._processingEvent = false;
            }
        }

        public override void OnModifierTouchUp( ModifierTouchManipulationArgs e )
        {
            try
            {
                if ( this._processingEvent )
                    return;
                this._processingEvent = true;
                base.OnModifierTouchDown( e );
                this.Slaves.ForEachDo<MasterSlaveChartModifier>( ( Action<MasterSlaveChartModifier> ) ( x => x.OnModifierTouchUp( e ) ) );
            }
            finally
            {
                this._processingEvent = false;
            }
        }
    }
}
