// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.ChartModifiers.XAxisDragModifier
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using StockSharp.Xaml.Charting.Visuals.Axes;

namespace StockSharp.Xaml.Charting.ChartModifiers
{
    public class XAxisDragModifier : AxisDragModifierBase
    {
        public static readonly DependencyProperty ClipModeXProperty = DependencyProperty.Register(nameof (ClipModeX), typeof (ClipMode), typeof (XAxisDragModifier), new PropertyMetadata((object) ClipMode.ClipAtExtents));
        private Dictionary<string, IRange> _startCategoryXRanges;
        private Point _startPoint;

        public XAxisDragModifier()
        {
            IsPolarChartSupported = false;
        }

        public ClipMode ClipModeX
        {
            get
            {
                return ( ClipMode ) GetValue( XAxisDragModifier.ClipModeXProperty );
            }
            set
            {
                SetValue( XAxisDragModifier.ClipModeXProperty, ( object ) value );
            }
        }

        protected override IAxis GetCurrentAxis()
        {
            return GetXAxis( AxisId );
        }

        public override void OnModifierMouseDown( ModifierMouseArgs e )
        {
            base.OnModifierMouseDown( e );
            _startPoint = e.MousePoint;
            _startCategoryXRanges = XAxes.Where<IAxis>( ( Func<IAxis, bool> ) ( x => x.IsCategoryAxis ) ).ToDictionary<IAxis, string, IRange>( ( Func<IAxis, string> ) ( x => x.Id ), ( Func<IAxis, IRange> ) ( x => x.VisibleRange ) );
        }

        public override void OnModifierMouseUp( ModifierMouseArgs e )
        {
            base.OnModifierMouseUp( e );
            _startPoint = new Point();
            _startCategoryXRanges = new Dictionary<string, IRange>();
        }

        protected override void PerformPan( Point currentPoint, Point lastPoint )
        {
            IAxis currentAxis = GetCurrentAxis();
            double pixelsToScroll = PrepareForScrolling(currentAxis, currentPoint, lastPoint);
            currentAxis.Scroll( pixelsToScroll, ClipModeX );
        }

        private double PrepareForScrolling( IAxis axis, Point currentPoint, Point lastPoint )
        {
            double num1 = currentPoint.X - lastPoint.X;
            double num2 = lastPoint.Y - currentPoint.Y;
            if ( axis.IsCategoryAxis )
            {
                axis.VisibleRange = _startCategoryXRanges[ axis.Id ];
                num1 = currentPoint.X - _startPoint.X;
                num2 = _startPoint.Y - currentPoint.Y;
            }
            if ( !axis.IsHorizontalAxis )
            {
                return -num2;
            }

            return num1;
        }

        protected override IRange CalculateScaledRange( Point currentPoint, Point lastPoint, bool isSecondHalf, IAxis axis )
        {
            IAxisInteractivityHelper interactivityHelper = axis.GetCurrentInteractivityHelper();
            double num = PrepareForScrolling(axis, currentPoint, lastPoint);
            IRange scaledRange = isSecondHalf ? interactivityHelper.ScrollInMaxDirection(axis.VisibleRange, num) : interactivityHelper.ScrollInMinDirection(axis.VisibleRange, num);
            if ( axis.AutoRange != AutoRange.Always )
            {
                scaledRange = ClipRange( scaledRange, num, isSecondHalf, axis );
            }

            return scaledRange;
        }

        private IRange ClipRange( IRange scaledRange, double pixelsToScroll, bool isSecondHalf, IAxis axis )
        {
            if ( ClipModeX != ClipMode.None )
            {
                IAxisInteractivityHelper interactivityHelper = axis.GetCurrentInteractivityHelper();
                IRange maximumRange = axis.GetMaximumRange();
                IRange range = ((IRange) scaledRange.Clone()).ClipTo(maximumRange);
                bool flag1 = (uint) range.Min.CompareTo((object) scaledRange.Min) > 0U;
                bool flag2 = (uint) range.Max.CompareTo((object) scaledRange.Max) > 0U;
                if ( isSecondHalf )
                {
                    if ( flag2 )
                    {
                        if ( ClipModeX != ClipMode.ClipAtMin )
                        {
                            scaledRange = RangeFactory.NewWithMinMax( axis.VisibleRange, scaledRange.Min, range.Max );
                        }

                        if ( ClipModeX == ClipMode.StretchAtExtents )
                        {
                            scaledRange = interactivityHelper.ScrollInMinDirection( axis.VisibleRange, pixelsToScroll );
                        }
                    }
                }
                else if ( flag1 )
                {
                    if ( ClipModeX != ClipMode.ClipAtMax )
                    {
                        scaledRange = RangeFactory.NewWithMinMax( axis.VisibleRange, range.Min, scaledRange.Max );
                    }

                    if ( ClipModeX == ClipMode.StretchAtExtents )
                    {
                        scaledRange = interactivityHelper.ScrollInMaxDirection( axis.VisibleRange, pixelsToScroll );
                    }
                }
            }
            return scaledRange;
        }
    }
}
