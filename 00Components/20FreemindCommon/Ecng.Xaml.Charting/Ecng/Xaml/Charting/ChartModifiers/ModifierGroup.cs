// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartModifiers.ModifierGroup
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Common.Helpers.XmlSerialization;
using StockSharp.Xaml.Charting.Utility;
using StockSharp.Xaml.Charting.Visuals;

namespace StockSharp.Xaml.Charting.ChartModifiers
{
    [ContentProperty( "ChildModifiers" )]
    public class ModifierGroup : MasterSlaveChartModifier
    {
        public static readonly DependencyProperty ChildModifiersProperty = DependencyProperty.Register(nameof (ChildModifiers), typeof (ObservableCollection<IChartModifier>), typeof (ModifierGroup), new PropertyMetadata((object) null, new PropertyChangedCallback(ModifierGroup.OnChildModifiersChanged)));
        private readonly Grid _grid = new Grid();

        public ModifierGroup()
          : this( new IChartModifier[ 0 ] )
        {
        }

        public ModifierGroup( params IChartModifier[ ] childModifiers )
        {
            Guard.NotNull( ( object ) childModifiers, nameof( childModifiers ) );
            for ( int index = 0 ; index < childModifiers.Length ; ++index )
                Guard.NotNull( ( object ) childModifiers[ index ], string.Format( "childModifiers[{0}]", ( object ) index ) );
            this.Content = ( object ) this._grid;
            this.SetCurrentValue( ModifierGroup.ChildModifiersProperty, ( object ) new ObservableCollection<IChartModifier>( ( IEnumerable<IChartModifier> ) childModifiers ) );
        }

        public ObservableCollection<IChartModifier> ChildModifiers
        {
            get
            {
                return ( ObservableCollection<IChartModifier> ) this.GetValue( ModifierGroup.ChildModifiersProperty );
            }
            set
            {
                this.SetValue( ModifierGroup.ChildModifiersProperty, ( object ) value );
            }
        }

        public IChartModifier this[ string name ]
        {
            get
            {
                return this.FindModifierByName( name );
            }
        }

        public IChartModifier this[ int index ]
        {
            get
            {
                return this.ChildModifiers[ index ];
            }
        }

        public override void OnAttached()
        {
            base.OnAttached();
            this.AttachAll( ( IEnumerable<IChartModifier> ) this.ChildModifiers );
        }

        public override void OnDetached()
        {
            base.OnDetached();
            this.DetachAll( ( IEnumerable<IChartModifier> ) this.ChildModifiers );
        }

        private void AttachAll( IEnumerable<IChartModifier> childModifiers )
        {
            if ( !this.IsAttached )
                return;
            childModifiers.ForEachDo<IChartModifier>( new Action<IChartModifier>( this.AttachChild ) );
        }

        private void AttachChild( IChartModifier obj )
        {
            this.AttachAsElement( obj );
            obj.ParentSurface = this.ParentSurface;
            obj.Services = this.Services;
            obj.DataContext = this.DataContext;
            obj.IsAttached = true;
            obj.OnAttached();
        }

        private void AttachAsElement( IChartModifier chartModifier )
        {
            this._grid.SafeAddChild( ( object ) chartModifier, -1 );
        }

        private void DetachAll( IEnumerable<IChartModifier> childModifiers )
        {
            childModifiers.ForEachDo<IChartModifier>( new Action<IChartModifier>( this.DetachChild ) );
        }

        private void DetachChild( IChartModifier obj )
        {
            this.DetachAsElement( obj );
            obj.OnDetached();
            obj.IsAttached = false;
            obj.ParentSurface = ( ISciChartSurface ) null;
            obj.Services = ( IServiceContainer ) null;
        }

        private void DetachAsElement( IChartModifier chartModifier )
        {
            this._grid.SafeRemoveChild( ( object ) chartModifier );
        }

        protected override void OnXAxesCollectionChanged( object sender, NotifyCollectionChangedEventArgs args )
        {
            if ( this.ChildModifiers == null )
                return;
            this.ChildModifiers.ForEachDo<IChartModifier>( ( Action<IChartModifier> ) ( x => x.OnXAxesCollectionChanged( sender, args ) ) );
        }

        protected override void OnYAxesCollectionChanged( object sender, NotifyCollectionChangedEventArgs args )
        {
            if ( this.ChildModifiers == null )
                return;
            this.ChildModifiers.ForEachDo<IChartModifier>( ( Action<IChartModifier> ) ( x => x.OnYAxesCollectionChanged( sender, args ) ) );
        }

        protected override void OnAnnotationCollectionChanged( object sender, NotifyCollectionChangedEventArgs args )
        {
            if ( this.ChildModifiers == null )
                return;
            this.ChildModifiers.ForEachDo<IChartModifier>( ( Action<IChartModifier> ) ( x => x.OnAnnotationCollectionChanged( sender, args ) ) );
        }

        protected override void OnIsEnabledChanged()
        {
            this.ChildModifiers.ForEachDo<IChartModifier>( ( Action<IChartModifier> ) ( x => x.IsEnabled = this.IsEnabled ) );
        }

        public override void OnModifierDoubleClick( ModifierMouseArgs e )
        {
            this.HandleEvent( ( Action<IChartModifier, ModifierEventArgsBase> ) ( ( modifier, args ) => modifier.OnModifierDoubleClick( ( ModifierMouseArgs ) args ) ), ( ModifierEventArgsBase ) e );
        }

        private void HandleEvent( Action<IChartModifier, ModifierEventArgsBase> handler, ModifierEventArgsBase e )
        {
            if ( this.ChildModifiers == null )
                return;
            foreach ( IChartModifier chartModifier in this.ChildModifiers.Where<IChartModifier>( ( Func<IChartModifier, bool> ) ( x => x.IsEnabled ) ) )
            {
                if ( chartModifier.ReceiveHandledEvents || !e.Handled )
                    handler( chartModifier, e );
            }
        }

        public override void OnModifierMouseDown( ModifierMouseArgs e )
        {
            this.HandleEvent( ( Action<IChartModifier, ModifierEventArgsBase> ) ( ( modifier, args ) => modifier.OnModifierMouseDown( ( ModifierMouseArgs ) args ) ), ( ModifierEventArgsBase ) e );
        }

        public override void OnModifierMouseMove( ModifierMouseArgs e )
        {
            this.HandleEvent( ( Action<IChartModifier, ModifierEventArgsBase> ) ( ( modifier, args ) => modifier.OnModifierMouseMove( ( ModifierMouseArgs ) args ) ), ( ModifierEventArgsBase ) e );
        }

        public override void OnModifierMouseUp( ModifierMouseArgs e )
        {
            this.HandleEvent( ( Action<IChartModifier, ModifierEventArgsBase> ) ( ( modifier, args ) => modifier.OnModifierMouseUp( ( ModifierMouseArgs ) args ) ), ( ModifierEventArgsBase ) e );
        }

        public override void OnModifierMouseWheel( ModifierMouseArgs e )
        {
            this.HandleEvent( ( Action<IChartModifier, ModifierEventArgsBase> ) ( ( modifier, args ) => modifier.OnModifierMouseWheel( ( ModifierMouseArgs ) args ) ), ( ModifierEventArgsBase ) e );
        }

        public override void OnMasterMouseLeave( ModifierMouseArgs e )
        {
            this.HandleEvent( ( Action<IChartModifier, ModifierEventArgsBase> ) ( ( modifier, args ) => modifier.OnMasterMouseLeave( ( ModifierMouseArgs ) args ) ), ( ModifierEventArgsBase ) e );
        }

        public override void OnModifierTouchDown( ModifierTouchManipulationArgs e )
        {
            this.HandleEvent( ( Action<IChartModifier, ModifierEventArgsBase> ) ( ( modifier, args ) => modifier.OnModifierTouchDown( ( ModifierTouchManipulationArgs ) args ) ), ( ModifierEventArgsBase ) e );
        }

        public override void OnModifierTouchMove( ModifierTouchManipulationArgs e )
        {
            this.HandleEvent( ( Action<IChartModifier, ModifierEventArgsBase> ) ( ( modifier, args ) => modifier.OnModifierTouchMove( ( ModifierTouchManipulationArgs ) args ) ), ( ModifierEventArgsBase ) e );
        }

        public override void OnModifierTouchUp( ModifierTouchManipulationArgs e )
        {
            this.HandleEvent( ( Action<IChartModifier, ModifierEventArgsBase> ) ( ( modifier, args ) => modifier.OnModifierTouchUp( ( ModifierTouchManipulationArgs ) args ) ), ( ModifierEventArgsBase ) e );
        }

        public bool HasModifier( Type desiredType )
        {
            return this.ChildModifiers.Any<IChartModifier>( ( Func<IChartModifier, bool> ) ( x => x.GetType() == desiredType ) );
        }

        protected override void OnDataContextChanged( object sender, DependencyPropertyChangedEventArgs e )
        {
            this.ChildModifiers.ForEachDo<IChartModifier>( ( Action<IChartModifier> ) ( x => x.DataContext = e.NewValue ) );
        }

        private IChartModifier FindModifierByName( string name )
        {
            return this.ChildModifiers.FirstOrDefault<IChartModifier>( ( Func<IChartModifier, bool> ) ( modifier => name == modifier.ModifierName ) );
        }

        public override void ReadXml( XmlReader reader )
        {
            ObservableCollection<ChartModifierBase> collection = new ObservableCollection<ChartModifierBase>();
            IEnumerable<ChartModifierBase> values = ChartModifierSerializationHelper.Instance.DeserializeCollection(reader);
            collection.AddRange<ChartModifierBase>( values );
            foreach ( IChartModifier chartModifier in ( Collection<ChartModifierBase> ) collection )
                this.ChildModifiers.Add( chartModifier );
        }

        public override void WriteXml( XmlWriter writer )
        {
            ChartModifierSerializationHelper.Instance.SerializeCollection( this.ChildModifiers.Cast<ChartModifierBase>(), writer );
        }

        public override void ResetInertia()
        {
            foreach ( IChartModifier childModifier in ( Collection<IChartModifier> ) this.ChildModifiers )
                childModifier.ResetInertia();
        }

        private static void OnChildModifiersChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ModifierGroup modifierGroup = d as ModifierGroup;
            if ( modifierGroup == null )
                return;
            ObservableCollection<IChartModifier> oldValue = e.OldValue as ObservableCollection<IChartModifier>;
            ObservableCollection<IChartModifier> newValue = e.NewValue as ObservableCollection<IChartModifier>;
            if ( oldValue != null )
            {
                modifierGroup.DetachAll( ( IEnumerable<IChartModifier> ) oldValue );
                oldValue.CollectionChanged -= new NotifyCollectionChangedEventHandler( modifierGroup.ModifierCollectionChanged );
            }
            if ( newValue == null )
                return;
            modifierGroup.AttachAll( ( IEnumerable<IChartModifier> ) newValue );
            newValue.CollectionChanged += new NotifyCollectionChangedEventHandler( modifierGroup.ModifierCollectionChanged );
        }

        private void ModifierCollectionChanged( object sender, NotifyCollectionChangedEventArgs e )
        {
            if ( e.NewItems != null )
                this.AttachAll( e.NewItems.Cast<IChartModifier>() );
            if ( e.OldItems == null )
                return;
            this.DetachAll( e.OldItems.Cast<IChartModifier>() );
        }
    }
}
