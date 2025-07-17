// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Visuals.Annotations.AnnotationCollection
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Ecng.Xaml.Charting.Common.Extensions;
using Ecng.Xaml.Charting.Common.Helpers;
using Ecng.Xaml.Charting.Licensing;
using Ecng.Xaml.Charting.Numerics.CoordinateCalculators;
using Ecng.Xaml.Charting.Rendering.Common;
using StockSharp.Xaml.Licensing.Core;

namespace Ecng.Xaml.Charting.Visuals.Annotations
{

    public sealed class AnnotationCollection : ObservableCollection<IAnnotation>, IXmlSerializable
    {
        internal Action OnRootGridMouseDownHandled = (Action) (() => {});
        private IServiceContainer _serviceContainer;
        private ISciChartSurface _parentSurface;
        private Delegate _parentSurfaceMouseDownDelegate;

        public AnnotationCollection()
        {
            this.Initialize();
        }

        public AnnotationCollection( IEnumerable<IAnnotation> collection )
          : base( collection )
        {
            this.Initialize();
        }

        public ISciChartSurface ParentSurface
        {
            get
            {
                return this._parentSurface;
            }
            set
            {
                ISciChartSurface parentSurface = this._parentSurface;
                if ( parentSurface != null )
                {
                    this._serviceContainer = ( IServiceContainer ) null;
                    this.UnsubscribeSurfaceEvents( parentSurface );
                    this.ForEachDo<IAnnotation>( new Action<IAnnotation>( AnnotationCollection.DetachAnnotation ) );
                }
                this._parentSurface = value;
                if ( this._parentSurface == null )
                {
                    return;
                }

                this.SubscribeSurfaceEvents( this._parentSurface );
                this._serviceContainer = this._parentSurface.Services;
                this.ForEachDo<IAnnotation>( new Action<IAnnotation>( this.AttachAnnotation ) );
            }
        }

        public void SubscribeSurfaceEvents( ISciChartSurface parentSurface )
        {
            this.UnsubscribeSurfaceEvents( parentSurface );
            UltrachartSurface ultrachartSurface = parentSurface as UltrachartSurface;
            if ( ultrachartSurface == null || ( object ) this.ParentSurfaceMouseDownDelegate != null )
            {
                return;
            }

            this.ParentSurfaceMouseDownDelegate = ( Delegate ) new MouseButtonEventHandler( this.RootGridMouseDown );
            ultrachartSurface.AddHandler( UIElement.MouseLeftButtonDownEvent, this.ParentSurfaceMouseDownDelegate, true );
        }

        public void UnsubscribeSurfaceEvents( ISciChartSurface parentSurface )
        {
            UltrachartSurface ultrachartSurface = parentSurface as UltrachartSurface;
            if ( ultrachartSurface == null || ( object ) this.ParentSurfaceMouseDownDelegate == null )
            {
                return;
            }

            ultrachartSurface.RemoveHandler( UIElement.MouseLeftButtonDownEvent, this.ParentSurfaceMouseDownDelegate );
            this.ParentSurfaceMouseDownDelegate = ( Delegate ) null;
        }

        private void RootGridMouseDown( object sender, MouseButtonEventArgs e )
        {
            Rectangle originalSource = e.OriginalSource as Rectangle;
            if ( originalSource != null && RenderSurfaceBase.RectIdentifier.Equals( originalSource.Tag ) )
            {
                this.DeselectAll();
                e.Handled = true;
            }
            this.OnRootGridMouseDownHandled();
        }

        public void DeselectAll()
        {
            this.ForEachDo<IAnnotation>( ( Action<IAnnotation> ) ( annotation => annotation.IsSelected = false ) );
        }

        protected override void ClearItems()
        {
            foreach ( IAnnotation annotation in ( Collection<IAnnotation> ) this )
            {
                AnnotationCollection.DetachAnnotation( annotation );
            }

            base.ClearItems();
        }

        internal void AnnotationCollectionChanged( object sender, NotifyCollectionChangedEventArgs e )
        {
            if ( this.ParentSurface == null )
            {
                return;
            }

            if ( e.OldItems != null )
            {
                foreach ( IAnnotation oldItem in ( IEnumerable ) e.OldItems )
                {
                    AnnotationCollection.DetachAnnotation( oldItem );
                }
            }
            foreach ( IAnnotation annotation in ( Collection<IAnnotation> ) this )
            {
                if ( !annotation.IsAttached )
                {
                    this.AttachAnnotation( annotation );
                }

                this.ParentSurface.InvalidateElement();
            }
        }

        private static void DetachAnnotation( IAnnotation item )
        {
            item.OnDetached();
            item.Services = ( IServiceContainer ) null;
            item.ParentSurface = ( ISciChartSurface ) null;
            item.IsAttached = false;
        }

        private void AttachAnnotation( IAnnotation item )
        {
            item.Services = this._serviceContainer;
            item.ParentSurface = this._parentSurface;
            item.IsAttached = true;
            item.OnAttached();
        }

        public XmlSchema GetSchema()
        {
            return ( XmlSchema ) null;
        }

        public void ReadXml( XmlReader reader )
        {
            IUpdateSuspender updateSuspender = (IUpdateSuspender) null;
            if ( this.ParentSurface != null )
            {
                updateSuspender = this.ParentSurface.SuspendUpdates();
            }

            this.AddRange<IAnnotation>( AnnotationSerializationHelper.Instance.DeserializeCollection( reader ) );
            updateSuspender?.Dispose();
        }

        public void WriteXml( XmlWriter writer )
        {
            AnnotationSerializationHelper.Instance.SerializeCollection( ( IEnumerable<IAnnotation> ) this, writer );
        }

        public void RefreshPositions( RenderPassInfo rpi )
        {
            foreach ( IAnnotation annotation in ( Collection<IAnnotation> ) this )
            {
                ICoordinateCalculator<double> coordinateCalculator1 = this.GetCoordinateCalculator(rpi.XCoordinateCalculators, annotation, annotation.XAxisId, true);
                ICoordinateCalculator<double> coordinateCalculator2 = this.GetCoordinateCalculator(rpi.YCoordinateCalculators, annotation, annotation.YAxisId, false);
                annotation.Update( coordinateCalculator1, coordinateCalculator2 );
                if ( coordinateCalculator1 == null )
                {
                    rpi.Warnings.Add( string.Format( "Could not draw an annotation of type {0}. XAxis with Id == {1} doesn't exist. Please ensure that the XAxisId property is set to a valid value.", ( object ) annotation.GetType(), ( object ) ( annotation.XAxisId ?? "NULL" ) ) );
                }

                if ( coordinateCalculator2 == null )
                {
                    rpi.Warnings.Add( string.Format( "Could not draw an annotation of type {0}. YAxis with Id == {1} doesn't exist. Please ensure that the YAxisId property is set to a valid value.", ( object ) annotation.GetType(), ( object ) ( annotation.YAxisId ?? "NULL" ) ) );
                }
            }
        }

        private ICoordinateCalculator<double> GetCoordinateCalculator( IDictionary<string, ICoordinateCalculator<double>> coordinateCalculators, IAnnotation annotation, string axisId, bool isXAxis )
        {
            if ( axisId == null )
            {
                return ( ICoordinateCalculator<double> ) null;
            }

            ICoordinateCalculator<double> coordinateCalculator;
            if ( coordinateCalculators.TryGetValue( axisId, out coordinateCalculator ) )
            {
                return coordinateCalculator;
            }

            return ( ICoordinateCalculator<double> ) null;
        }

        [Obfuscation( Exclude = false, Feature = "encryptmethod" )]
        private void Initialize()
        {
            new LicenseManager().Validate<AnnotationCollection>( this, ( IProviderFactory ) new UltrachartLicenseProviderFactory() );
        }

        public bool TrySelectAnnotation( IAnnotation annotationBase )
        {
            if ( !annotationBase.IsEditable || annotationBase.IsSelected || !annotationBase.IsAttached )
            {
                return false;
            }

            this.ForEachDo<IAnnotation>( ( Action<IAnnotation> ) ( annotation => annotation.IsSelected = false ) );
            this.SelectAnnotation( annotationBase );
            return true;
        }

        private void SelectAnnotation( IAnnotation annotationBase )
        {
            annotationBase.IsSelected = true;
        }

        internal Delegate ParentSurfaceMouseDownDelegate
        {
            get
            {
                return this._parentSurfaceMouseDownDelegate;
            }
            set
            {
                this._parentSurfaceMouseDownDelegate = value;
            }
        }

        public void OnXAxesCollectionChanged( object sender, NotifyCollectionChangedEventArgs args )
        {
            foreach ( IAnnotation annotation in ( IEnumerable<IAnnotation> ) this.Items )
            {
                annotation.OnXAxesCollectionChanged( sender, args );
            }
        }

        public void OnYAxesCollectionChanged( object sender, NotifyCollectionChangedEventArgs args )
        {
            foreach ( IAnnotation annotation in ( IEnumerable<IAnnotation> ) this.Items )
            {
                annotation.OnYAxesCollectionChanged( sender, args );
            }
        }
    }
}
