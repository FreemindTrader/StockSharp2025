using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Ecng.Xaml.Charting
{
    [TemplatePart( Name = "PART_ContentHost", Type = typeof( ContentPresenter ) )]
    [TemplatePart( Name = "PART_Header", Type = typeof( Grid ) )]
    [TemplatePart( Name = "PART_TopSplitter", Type = typeof( Thumb ) )]
    public class UltrachartGroupPane : ContentControl
    {
        public static DependencyProperty HeaderTemplateProperty;

        private ContentPresenter _mainPane;

        private Thumb _topSplitter;

        private Grid _headerPanel;

        private const double DefaultPaneHeight = 50;

        private double _topSplitterVerticalChange;

        private double _topSplitterHorizontalChange;

        public DataTemplate HeaderTemplate
        {
            get
            {
                return ( DataTemplate ) base.GetValue( UltrachartGroupPane.HeaderTemplateProperty );
            }
            set
            {
                base.SetValue( UltrachartGroupPane.HeaderTemplateProperty, value );
            }
        }

        static UltrachartGroupPane()
        {
            UltrachartGroupPane.HeaderTemplateProperty = DependencyProperty.Register( "HeaderTemplate", typeof( DataTemplate ), typeof( UltrachartGroupPane ), new PropertyMetadata( null, new PropertyChangedCallback( UltrachartGroupPane.OnHeaderTemplateChanged ) ) );
        }

        public UltrachartGroupPane()
        {
            base.DefaultStyleKey = typeof( UltrachartGroupPane );
            base.SetCurrentValue( FrameworkElement.MinHeightProperty, 50 );
            base.SetCurrentValue( FrameworkElement.HeightProperty, 50 );
        }

        internal double MeasureMinHeight()
        {
            Size size = new Size(double.PositiveInfinity, double.PositiveInfinity);
            _headerPanel.Measure( size );
            _topSplitter.Measure( size );

            double minHeight = base.MinHeight;
            double height    = _headerPanel.DesiredSize.Height;
            Size desiredSize = _topSplitter.DesiredSize;

            return Math.Max( minHeight, height + desiredSize.Height );
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _mainPane = ( ContentPresenter ) base.GetTemplateChild( "PART_ContentHost" );
            _headerPanel = ( Grid ) base.GetTemplateChild( "PART_Header" );
            _topSplitter = ( Thumb ) base.GetTemplateChild( "PART_TopSplitter" );

            if ( _topSplitter != null )
            {
                _topSplitter.DragDelta += new DragDeltaEventHandler( OnSplitterDragDelta );
                _topSplitter.DragCompleted += new DragCompletedEventHandler( OnSplitterDragCompleted );
            }

            TryApplyHeaderTemplate();
        }

        private static void OnHeaderTemplateChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            ( d as UltrachartGroupPane ).TryApplyHeaderTemplate();
        }

        private void OnResized( DragCompletedEventArgs args )
        {
            EventHandler<DragCompletedEventArgs> eventHandler = Resized;
            if ( eventHandler != null )
            {
                eventHandler( this, args );
            }
        }

        private void OnResizing( DragDeltaEventArgs args )
        {
            EventHandler<DragDeltaEventArgs> eventHandler = Resizing;
            if ( eventHandler != null )
            {
                eventHandler( this, args );
            }
        }

        private void OnSplitterDragCompleted( object sender, DragCompletedEventArgs e )
        {
            OnResized( e );
        }

        private void OnSplitterDragDelta( object sender, DragDeltaEventArgs e )
        {
            _topSplitterHorizontalChange = e.HorizontalChange;
            _topSplitterVerticalChange = e.VerticalChange;
            OnResizing( new DragDeltaEventArgs( _topSplitterHorizontalChange, _topSplitterVerticalChange ) );
        }

        private void TryApplyHeaderTemplate()
        {
            if ( HeaderTemplate != null && _headerPanel != null )
            {
                FrameworkElement frameworkElement = HeaderTemplate.LoadContent() as FrameworkElement;
                if ( frameworkElement != null )
                {
                    _headerPanel.Children.Add( frameworkElement );
                }
            }
        }

        public event EventHandler<DragCompletedEventArgs> Resized;

        public event EventHandler<DragDeltaEventArgs> Resizing;
    }
}
