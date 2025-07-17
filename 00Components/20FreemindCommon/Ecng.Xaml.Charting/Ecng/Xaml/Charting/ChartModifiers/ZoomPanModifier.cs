// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.ZoomPanModifier
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\fx.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
namespace fx.Xaml.Charting
{
    public class ZoomPanModifier : ZoomPanModifierBase
    {
        private Dictionary<string, IRange> _startCategoryXRanges;

        public override void OnModifierMouseDown( ModifierMouseArgs e )
        {
            base.OnModifierMouseDown( e );
            _startCategoryXRanges = XAxes.Where<IAxis>( ( Func<IAxis, bool> ) ( x => x.IsCategoryAxis ) ).ToDictionary<IAxis, string, IRange>( ( Func<IAxis, string> ) ( x => x.Id ), ( Func<IAxis, IRange> ) ( x => x.VisibleRange ) );
        }

        public override void Pan( Point currentPoint, Point lastPoint, Point startPoint )
        {
            PerformPan( currentPoint, lastPoint, startPoint );
        }

        public override void OnModifierMouseUp( ModifierMouseArgs e )
        {
            if ( !IsDragging )
                return;
            base.OnModifierMouseUp( e );
            _startCategoryXRanges = new Dictionary<string, IRange>();
        }

        private void PerformPan( Point currentPoint, Point lastPoint, Point startPoint )
        {
            double num1 = currentPoint.X - lastPoint.X;
            double num2 = lastPoint.Y - currentPoint.Y;
            using ( ParentSurface.SuspendUpdates() )
            {
                if ( XyDirection != XyDirection.YDirection )
                {
                    foreach ( IAxis xax in XAxes )
                    {
                        int num3 = xax.IsHorizontalAxis ? 1 : 0;
                        bool? isHorizontalAxis = XAxis?.IsHorizontalAxis;
                        int num4 = isHorizontalAxis.GetValueOrDefault() ? 1 : 0;
                        if ( num3 == num4 & isHorizontalAxis.HasValue )
                        {
                            using ( IUpdateSuspender updateSuspender = xax.SuspendUpdates() )
                            {
                                updateSuspender.ResumeTargetOnDispose = false;
                                double num5 = num1;
                                double num6 = num2;
                                if ( xax.IsCategoryAxis )
                                {
                                    xax.VisibleRange = _startCategoryXRanges[ xax.Id ];
                                    num5 = currentPoint.X - startPoint.X;
                                    num6 = startPoint.Y - currentPoint.Y;
                                }
                                xax.Scroll( xax.IsHorizontalAxis ? num5 : -num6, ClipModeX );
                            }
                        }
                        else
                            break;
                    }
                }
                if ( XyDirection == XyDirection.XDirection )
                {
                    if ( !ZoomExtentsY )
                        return;
                    ParentSurface.ZoomExtentsY();
                }
                else
                {
                    foreach ( IAxis yax in YAxes )
                        yax.Scroll( yax.IsHorizontalAxis ? -num1 : num2, ClipMode.None );
                }
            }
        }
    }
}
