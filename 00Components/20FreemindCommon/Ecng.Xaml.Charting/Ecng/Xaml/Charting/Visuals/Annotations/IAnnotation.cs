// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Annotations.IAnnotation
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using System.Xml.Serialization;
namespace fx.Xaml.Charting
{
    public interface IAnnotation : IHitTestable, IPublishMouseEvents, IXmlSerializable
    {
        event EventHandler<EventArgs> DragStarted;

        event EventHandler<EventArgs> DragEnded;

        event EventHandler<AnnotationDragDeltaEventArgs> DragDelta;

        event EventHandler Selected;

        event EventHandler Unselected;

        string XAxisId
        {
            get; set;
        }

        string YAxisId
        {
            get; set;
        }

        bool IsAttached
        {
            get; set;
        }

        bool IsSelected
        {
            get; set;
        }

        bool IsEditable
        {
            get; set;
        }

        bool IsHidden
        {
            get; set;
        }

        IAxis YAxis
        {
            get;
        }

        IEnumerable<IAxis> YAxes
        {
            get;
        }

        IAxis XAxis
        {
            get;
        }

        IEnumerable<IAxis> XAxes
        {
            get;
        }

        IServiceContainer Services
        {
            get; set;
        }

        IComparable X1
        {
            get; set;
        }

        IComparable Y1
        {
            get; set;
        }

        IComparable X2
        {
            get; set;
        }

        IComparable Y2
        {
            get; set;
        }

        ISciChartSurface ParentSurface
        {
            get; set;
        }

        XyDirection DragDirections
        {
            get; set;
        }

        XyDirection ResizeDirections
        {
            get; set;
        }

        bool IsResizable
        {
            get;
        }

        object DataContext
        {
            get; set;
        }

        bool CaptureMouse();

        void ReleaseMouseCapture();

        void Update( ICoordinateCalculator<double> xCoordinateCalculator, ICoordinateCalculator<double> yCoordinateCalculator );

        void OnDetached();

        void OnAttached();

        void Hide();

        void Show();

        void MoveAnnotation( double offsetX, double offsetY );

        void SetBasePoint( Point newPoint, int index );

        Point[ ] GetBasePoints();

        bool Refresh();

        void OnDragStarted();

        void OnDragEnded();

        void OnDragDelta();

        void OnXAxesCollectionChanged( object sender, NotifyCollectionChangedEventArgs args );

        void OnYAxesCollectionChanged( object sender, NotifyCollectionChangedEventArgs args );
    }
}
