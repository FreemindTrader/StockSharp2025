// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Annotations.AnchorPointAnnotation
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml.Serialization;
using StockSharp.Xaml.Charting.Utility.Mouse;
using StockSharp.Xaml.Charting.Visuals.Events;

namespace StockSharp.Xaml.Charting.Visuals.Annotations
{
    public abstract class AnchorPointAnnotation : AnnotationBase, IAnchorPointAnnotation, IAnnotation, IHitTestable, IPublishMouseEvents, IXmlSerializable
    {
        public static readonly DependencyProperty HorizontalAnchorPointProperty = DependencyProperty.Register(nameof (HorizontalAnchorPoint), typeof (HorizontalAnchorPoint), typeof (AnchorPointAnnotation), new PropertyMetadata((object) HorizontalAnchorPoint.Left, new PropertyChangedCallback(AnchorPointAnnotation.OnAnchorPointChanged)));
        public static readonly DependencyProperty VerticalAnchorPointProperty = DependencyProperty.Register(nameof (VerticalAnchorPoint), typeof (VerticalAnchorPoint), typeof (AnchorPointAnnotation), new PropertyMetadata((object) VerticalAnchorPoint.Top, new PropertyChangedCallback(AnchorPointAnnotation.OnAnchorPointChanged)));

        event EventHandler<TouchManipulationEventArgs> IPublishMouseEvents.TouchDown
        {
            add
            {
                throw new NotImplementedException();
            }
            remove
            {
                throw new NotImplementedException();
            }
        }

        event EventHandler<TouchManipulationEventArgs> IPublishMouseEvents.TouchMove
        {
            add
            {
                throw new NotImplementedException();
            }
            remove
            {
                throw new NotImplementedException();
            }
        }

        event EventHandler<TouchManipulationEventArgs> IPublishMouseEvents.TouchUp
        {
            add
            {
                throw new NotImplementedException();
            }
            remove
            {
                throw new NotImplementedException();
            }
        }

        protected AnchorPointAnnotation()
        {
            this.IsResizable = false;
        }

        public HorizontalAnchorPoint HorizontalAnchorPoint
        {
            get
            {
                return ( HorizontalAnchorPoint ) this.GetValue( AnchorPointAnnotation.HorizontalAnchorPointProperty );
            }
            set
            {
                this.SetValue( AnchorPointAnnotation.HorizontalAnchorPointProperty, ( object ) value );
            }
        }

        public VerticalAnchorPoint VerticalAnchorPoint
        {
            get
            {
                return ( VerticalAnchorPoint ) this.GetValue( AnchorPointAnnotation.VerticalAnchorPointProperty );
            }
            set
            {
                this.SetValue( AnchorPointAnnotation.VerticalAnchorPointProperty, ( object ) value );
            }
        }

        public double VerticalOffset
        {
            get
            {
                if ( this.AnnotationRoot != null && this.VerticalAnchorPoint != VerticalAnchorPoint.Top )
                {
                    if ( this.VerticalAnchorPoint == VerticalAnchorPoint.Center )
                    {
                        return this.AnnotationRoot.ActualHeight * 0.5;
                    }

                    if ( this.VerticalAnchorPoint == VerticalAnchorPoint.Bottom )
                    {
                        return this.AnnotationRoot.ActualHeight;
                    }
                }
                return 0.0;
            }
        }

        public double HorizontalOffset
        {
            get
            {
                if ( this.AnnotationRoot != null && this.HorizontalAnchorPoint != HorizontalAnchorPoint.Left )
                {
                    if ( this.HorizontalAnchorPoint == HorizontalAnchorPoint.Center )
                    {
                        return this.AnnotationRoot.ActualWidth * 0.5;
                    }

                    if ( this.HorizontalAnchorPoint == HorizontalAnchorPoint.Right )
                    {
                        return this.AnnotationRoot.ActualWidth;
                    }
                }
                return 0.0;
            }
        }

        private static void OnAnchorPointChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            Action toBeDone = delegate()
            {
                AnnotationBase.OnRenderablePropertyChanged( d, e );
            };

            d.Dispatcher.BeginInvoke( toBeDone, DispatcherPriority.DataBind, new object[ 0 ] );
        }

        protected AnnotationCoordinates GetAnchorAnnotationCoordinates( AnnotationCoordinates annotationCoordinates )
        {
            annotationCoordinates.X1Coord -= this.HorizontalOffset;
            annotationCoordinates.Y1Coord -= this.VerticalOffset;
            annotationCoordinates.X2Coord -= this.HorizontalOffset;
            annotationCoordinates.Y2Coord -= this.VerticalOffset;
            return annotationCoordinates;
        }

        protected override Cursor GetSelectedCursor()
        {
            return Cursors.SizeAll;
        }

        [SpecialName]
        object IAnnotation.DataContext
        {
            get
            {
                return this.DataContext;
            }

            set
            {
                this.DataContext = value;
            }
        }

        [SpecialName]
        double IHitTestable.ActualWidth
        {
            get
            {
                return this.ActualWidth;
            }
        }

        [SpecialName]
        double IHitTestable.ActualHeight
        {
            get
            {
                return this.ActualHeight;
            }
        }
    }
}
