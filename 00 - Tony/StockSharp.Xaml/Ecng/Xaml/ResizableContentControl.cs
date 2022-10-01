using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

namespace Ecng.Xaml
{
    [TemplatePart( Name = "PART_Presenter", Type = typeof( ContentPresenter ) )]
    [TemplatePart( Name = "PART_Gripper", Type = typeof( Thumb ) )]
    public class ResizableContentControl : ContentControl
    {
        public static readonly DependencyProperty CanAutoSizeProperty = DependencyProperty.Register( nameof( CanAutoSize ), typeof( bool ), typeof( ResizableContentControl ), ( PropertyMetadata )new FrameworkPropertyMetadata( ( object )true ) );
        public static readonly DependencyProperty GripperBackgroundProperty = DependencyProperty.Register( nameof( GripperBackground ), typeof( Brush ), typeof( ResizableContentControl ), ( PropertyMetadata )new FrameworkPropertyMetadata( ( object )Brushes.Transparent ) );
        public static readonly DependencyProperty GripperForegroundProperty = DependencyProperty.Register( nameof( GripperForeground ), typeof( Brush ), typeof( ResizableContentControl ), ( PropertyMetadata )new FrameworkPropertyMetadata( ( object )new SolidColorBrush( Color.FromRgb( ( byte )184, ( byte )180, ( byte )162 ) ) ) );
        public static readonly DependencyProperty ResizeModeProperty = DependencyProperty.Register( nameof( ResizeMode ), typeof( ControlResizeMode ), typeof( ResizableContentControl ), ( PropertyMetadata )new FrameworkPropertyMetadata( ( object )ControlResizeMode.Both ) );
        private Thumb _gripper;
        private Size _controlSize;
        private Point _currentPosition;

        public bool CanAutoSize
        {
            get
            {
                return ( bool )this.GetValue( ResizableContentControl.CanAutoSizeProperty );
            }
            set
            {
                this.SetValue( ResizableContentControl.CanAutoSizeProperty, ( object )value );
            }
        }

        public Brush GripperBackground
        {
            get
            {
                return ( Brush )this.GetValue( ResizableContentControl.GripperBackgroundProperty );
            }
            set
            {
                this.SetValue( ResizableContentControl.GripperBackgroundProperty, ( object )value );
            }
        }

        public Brush GripperForeground
        {
            get
            {
                return ( Brush )this.GetValue( ResizableContentControl.GripperForegroundProperty );
            }
            set
            {
                this.SetValue( ResizableContentControl.GripperForegroundProperty, ( object )value );
            }
        }

        public ControlResizeMode ResizeMode
        {
            get
            {
                return ( ControlResizeMode )this.GetValue( ResizableContentControl.ResizeModeProperty );
            }
            set
            {
                this.SetValue( ResizableContentControl.ResizeModeProperty, ( object )value );
            }
        }

        private Thumb Gripper
        {
            get
            {
                return this.GetTemplateChild( "PART_Gripper" ) as Thumb;
            }
        }

        static ResizableContentControl()
        {
            Control.IsTabStopProperty.OverrideMetadata( typeof( ResizableContentControl ), ( PropertyMetadata )new FrameworkPropertyMetadata( ( object )false ) );
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata( typeof( ResizableContentControl ), ( PropertyMetadata )new FrameworkPropertyMetadata( ( object )typeof( ResizableContentControl ) ) );
            FrameworkElement.MinHeightProperty.OverrideMetadata( typeof( ResizableContentControl ), ( PropertyMetadata )new FrameworkPropertyMetadata( ( object )4.0 ) );
            FrameworkElement.MinWidthProperty.OverrideMetadata( typeof( ResizableContentControl ), ( PropertyMetadata )new FrameworkPropertyMetadata( ( object )4.0 ) );
            UIElement.FocusableProperty.OverrideMetadata( typeof( ResizableContentControl ), ( PropertyMetadata )new FrameworkPropertyMetadata( ( object )false ) );
        }

        public ResizableContentControl()
        {
        }

        public ResizableContentControl( object content )
          : this()
        {
            this.Content = content;
        }

        private void GripperDragDelta( object sender, DragDeltaEventArgs e )
        {
            Size size1 = new Size( double.PositiveInfinity, double.PositiveInfinity );
            Size size2 = new Size( this.MinWidth, this.MinHeight );
            FrameworkElement content = this.Content as FrameworkElement;
            while ( content is ContentPresenter )
                content = ( ( ContentPresenter )content ).Content as FrameworkElement;
            if ( content != null )
            {
                Rect rect = content.TransformToAncestor( ( Visual )this ).TransformBounds( new Rect( new Point( 0.0, 0.0 ), content.RenderSize ) );
                double num1 = Math.Max( 0.0, this.RenderSize.Width - rect.Width );
                double num2 = Math.Max( 0.0, this.RenderSize.Height - rect.Height );
                size2.Width = Math.Max( size2.Width, content.MinWidth + num1 );
                size2.Height = Math.Max( size2.Height, content.MinHeight + num2 );
                if ( !double.IsNaN( content.MaxWidth ) && content.MaxWidth < 100000.0 )
                    size1.Width = Math.Min( size1.Width, content.MaxWidth + num1 );
                if ( !double.IsNaN( content.MaxHeight ) && content.MaxHeight < 100000.0 )
                    size1.Height = Math.Min( size1.Height, content.MaxHeight + num2 );
            }
            if ( BrowserInteropHelper.IsBrowserHosted )
            {
                if ( this.ResizeMode == ControlResizeMode.Both || this.ResizeMode == ControlResizeMode.Horizontal )
                    this.Width = Math.Min( size1.Width, Math.Max( size2.Width, this.RenderSize.Width + e.HorizontalChange ) );
                if ( this.ResizeMode != ControlResizeMode.Both && this.ResizeMode != ControlResizeMode.Vertical )
                    return;
                this.Height = Math.Min( size1.Height, Math.Max( size2.Height, this.RenderSize.Height + e.VerticalChange ) );
            }
            else
            {
                Point screen = this.PointToScreen( Mouse.GetPosition( ( IInputElement )this ) );
                if ( this.ResizeMode == ControlResizeMode.Both || this.ResizeMode == ControlResizeMode.Horizontal )
                    this.Width = Math.Min( size1.Width, Math.Max( size2.Width, this._controlSize.Width + ( this.FlowDirection == FlowDirection.LeftToRight ? 1.0 : -1.0 ) * ( screen.X - this._currentPosition.X ) ) );
                if ( this.ResizeMode != ControlResizeMode.Both && this.ResizeMode != ControlResizeMode.Vertical )
                    return;
                this.Height = Math.Min( size1.Height, Math.Max( size2.Height, this._controlSize.Height + ( screen.Y - this._currentPosition.Y ) ) );
            }
        }

        private void GripperDragStarted( object sender, DragStartedEventArgs e )
        {
            if ( BrowserInteropHelper.IsBrowserHosted )
                return;
            this._currentPosition = this.PointToScreen( Mouse.GetPosition( ( IInputElement )this ) );
            this._controlSize = this.RenderSize;
        }

        private void GripperMouseDoubleClick( object sender, MouseButtonEventArgs e )
        {
            if ( !this.CanAutoSize )
                return;
            this.AutoSize();
        }

        public void AutoSize()
        {
            this.Width = double.NaN;
            this.Height = double.NaN;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if ( this._gripper != null )
            {
                this._gripper.DragDelta -= new DragDeltaEventHandler( this.GripperDragDelta );
                this._gripper.DragStarted -= new DragStartedEventHandler( this.GripperDragStarted );
                this._gripper.MouseDoubleClick -= new MouseButtonEventHandler( this.GripperMouseDoubleClick );
            }
            this._gripper = this.Gripper;
            if ( this._gripper == null )
                return;
            this._gripper.DragDelta += new DragDeltaEventHandler( this.GripperDragDelta );
            this._gripper.DragStarted += new DragStartedEventHandler( this.GripperDragStarted );
            this._gripper.MouseDoubleClick += new MouseButtonEventHandler( this.GripperMouseDoubleClick );
        }
    }
}
