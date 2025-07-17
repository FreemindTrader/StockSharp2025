// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.DefaultTickLabel
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
namespace Ecng.Xaml.Charting
{
    public class DefaultTickLabel : TemplatableControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty HorizontalAnchorPointProperty = DependencyProperty.Register(nameof (HorizontalAnchorPoint), typeof (HorizontalAnchorPoint), typeof (DefaultTickLabel), new PropertyMetadata((object) HorizontalAnchorPoint.Center, new PropertyChangedCallback(DefaultTickLabel.OnHorizontalAnchorPointChanged)));
        public static readonly DependencyProperty VerticalAnchorPointProperty = DependencyProperty.Register(nameof (VerticalAnchorPoint), typeof (VerticalAnchorPoint), typeof (DefaultTickLabel), new PropertyMetadata((object) VerticalAnchorPoint.Top, new PropertyChangedCallback(DefaultTickLabel.OnVerticalAnchorPointChanged)));
        public static readonly DependencyProperty PositionProperty = DependencyProperty.Register(nameof (Position), typeof (Point), typeof (DefaultTickLabel), new PropertyMetadata((object) new Point(double.NaN, double.NaN), new PropertyChangedCallback(DefaultTickLabel.OnPositionChanged)));
        public static readonly DependencyProperty DefaultForegroundProperty = DependencyProperty.Register(nameof (DefaultForeground), typeof (Brush), typeof (DefaultTickLabel), new PropertyMetadata((object) null));
        public static readonly DependencyProperty DefaultVerticalAnchorPointProperty = DependencyProperty.Register(nameof (DefaultVerticalAnchorPoint), typeof (VerticalAnchorPoint), typeof (DefaultTickLabel), new PropertyMetadata((object) VerticalAnchorPoint.Top));
        public static readonly DependencyProperty DefaultHorizontalAnchorPointProperty = DependencyProperty.Register(nameof (DefaultHorizontalAnchorPoint), typeof (HorizontalAnchorPoint), typeof (DefaultTickLabel), new PropertyMetadata((object) HorizontalAnchorPoint.Left));
        public new static readonly DependencyProperty LayoutTransformProperty = DependencyProperty.Register("LayoutTransform", typeof (Transform), typeof (DefaultTickLabel), new PropertyMetadata((PropertyChangedCallback) null));

        public event PropertyChangedEventHandler PropertyChanged;

        public DefaultTickLabel()
        {
            this.DefaultStyleKey = ( object ) typeof( DefaultTickLabel );
        }

        public HorizontalAnchorPoint HorizontalAnchorPoint
        {
            get
            {
                return ( HorizontalAnchorPoint ) this.GetValue( DefaultTickLabel.HorizontalAnchorPointProperty );
            }
            set
            {
                this.SetValue( DefaultTickLabel.HorizontalAnchorPointProperty, ( object ) value );
            }
        }

        public VerticalAnchorPoint VerticalAnchorPoint
        {
            get
            {
                return ( VerticalAnchorPoint ) this.GetValue( DefaultTickLabel.VerticalAnchorPointProperty );
            }
            set
            {
                this.SetValue( DefaultTickLabel.VerticalAnchorPointProperty, ( object ) value );
            }
        }

        public Point Position
        {
            get
            {
                return ( Point ) this.GetValue( DefaultTickLabel.PositionProperty );
            }
            set
            {
                this.SetValue( DefaultTickLabel.PositionProperty, ( object ) value );
            }
        }

        public Brush DefaultForeground
        {
            get
            {
                return ( Brush ) this.GetValue( DefaultTickLabel.DefaultForegroundProperty );
            }
        }

        public VerticalAnchorPoint DefaultVerticalAnchorPoint
        {
            get
            {
                return ( VerticalAnchorPoint ) this.GetValue( DefaultTickLabel.DefaultVerticalAnchorPointProperty );
            }
        }

        public HorizontalAnchorPoint DefaultHorizontalAnchorPoint
        {
            get
            {
                return ( HorizontalAnchorPoint ) this.GetValue( DefaultTickLabel.DefaultHorizontalAnchorPointProperty );
            }
        }

        internal int CullingPriority
        {
            get; set;
        }

        internal Rect ArrangedRect
        {
            get; set;
        }

        protected virtual void OnPropertyChanged( string propertyName )
        {
            // ISSUE: reference to a compiler-generated field
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ( propertyChanged == null )
                return;
            propertyChanged( ( object ) this, new PropertyChangedEventArgs( propertyName ) );
        }

        private static void OnPositionChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            DefaultTickLabel.OnHorizontalAnchorPointChanged( d, new DependencyPropertyChangedEventArgs() );
            DefaultTickLabel.OnVerticalAnchorPointChanged( d, new DependencyPropertyChangedEventArgs() );
        }

        private static void OnHorizontalAnchorPointChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            DefaultTickLabel defaultTickLabel = d as DefaultTickLabel;
            if ( defaultTickLabel == null )
                return;
            defaultTickLabel.SetValue( AxisCanvas.LeftProperty, ( object ) double.NaN );
            defaultTickLabel.SetValue( AxisCanvas.RightProperty, ( object ) double.NaN );
            defaultTickLabel.SetValue( AxisCanvas.CenterLeftProperty, ( object ) double.NaN );
            defaultTickLabel.SetValue( AxisCanvas.CenterRightProperty, ( object ) double.NaN );
            switch ( defaultTickLabel.HorizontalAnchorPoint )
            {
                case HorizontalAnchorPoint.Left:
                    defaultTickLabel.SetValue( AxisCanvas.LeftProperty, ( object ) defaultTickLabel.Position.X );
                    break;
                case HorizontalAnchorPoint.Center:
                    defaultTickLabel.SetValue( AxisCanvas.CenterLeftProperty, ( object ) defaultTickLabel.Position.X );
                    break;
                case HorizontalAnchorPoint.Right:
                    defaultTickLabel.SetValue( AxisCanvas.RightProperty, ( object ) defaultTickLabel.Position.X );
                    break;
            }
        }

        private static void OnVerticalAnchorPointChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            DefaultTickLabel defaultTickLabel = d as DefaultTickLabel;
            if ( defaultTickLabel == null )
                return;
            defaultTickLabel.SetValue( AxisCanvas.TopProperty, ( object ) double.NaN );
            defaultTickLabel.SetValue( AxisCanvas.BottomProperty, ( object ) double.NaN );
            defaultTickLabel.SetValue( AxisCanvas.CenterTopProperty, ( object ) double.NaN );
            defaultTickLabel.SetValue( AxisCanvas.CenterBottomProperty, ( object ) double.NaN );
            switch ( defaultTickLabel.VerticalAnchorPoint )
            {
                case VerticalAnchorPoint.Top:
                    defaultTickLabel.SetValue( AxisCanvas.TopProperty, ( object ) defaultTickLabel.Position.Y );
                    break;
                case VerticalAnchorPoint.Center:
                    defaultTickLabel.SetValue( AxisCanvas.CenterTopProperty, ( object ) defaultTickLabel.Position.Y );
                    break;
                case VerticalAnchorPoint.Bottom:
                    defaultTickLabel.SetValue( AxisCanvas.BottomProperty, ( object ) defaultTickLabel.Position.Y );
                    break;
            }
        }
    }
}
