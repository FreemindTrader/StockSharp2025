// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Themes.PolarModifierAxisCanvas
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System.Windows;
using System.Windows.Media;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Common.Helpers;
using StockSharp.Xaml.Charting.Utility;

namespace StockSharp.Xaml.Charting.Themes
{
    public class PolarModifierAxisCanvas : ModifierAxisCanvas
    {
        private PolarCartesianTransformationHelper _transformationHelper;
        private double _outerRadius;
        private double _innerRadius;
        private const double MaxDegree = 360.0;

        protected override Size ArrangeOverride( Size arrangeSize )
        {
            this._transformationHelper = new PolarCartesianTransformationHelper( arrangeSize.Width, arrangeSize.Height );
            double thickness = PolarPanel.GetThickness((UIElement) this.ParentAxis);
            this._outerRadius = PolarUtil.CalculateViewportRadius( arrangeSize );
            this._innerRadius = this._outerRadius - thickness;
            return base.ArrangeOverride( arrangeSize );
        }

        protected override Rect GetArrangedRect( Size arrangeSize, UIElement element )
        {
            double xOrigin;
            double xcoord = PolarModifierAxisCanvas.GetXCoord(element, out xOrigin);
            double yOrigin;
            double ycoord = this.GetYCoord(element, out yOrigin);
            Point cartesian = this._transformationHelper.ToCartesian(xcoord, ycoord);
            RotateTransform rotateTransform = new RotateTransform() { Angle = xcoord + 90.0, CenterX = xOrigin, CenterY = yOrigin };
            element.RenderTransform = ( Transform ) rotateTransform;
            element.RenderTransformOrigin = new Point( xOrigin, yOrigin );
            cartesian.X -= element.DesiredSize.Width * xOrigin;
            cartesian.Y -= element.DesiredSize.Height * yOrigin;
            return new Rect( cartesian, element.DesiredSize );
        }

        private double GetYCoord( UIElement element, out double yOrigin )
        {
            double num = 0.0;
            yOrigin = 0.0;
            double top = AxisCanvas.GetTop(element);
            double centerTop = AxisCanvas.GetCenterTop(element);
            if ( !top.IsNaN() )
            {
                num = this._outerRadius - top;
                yOrigin = 0.0;
            }
            else if ( !centerTop.IsNaN() )
            {
                num = this._outerRadius - centerTop;
                yOrigin = 0.5;
            }
            else
            {
                double bottom = AxisCanvas.GetBottom(element);
                if ( !bottom.IsNaN() )
                {
                    num = this._innerRadius + bottom;
                    yOrigin = 1.0;
                }
                else
                {
                    double centerBottom = AxisCanvas.GetCenterBottom(element);
                    if ( !centerBottom.IsNaN() )
                    {
                        num = this._innerRadius + centerBottom;
                        yOrigin = 0.5;
                    }
                }
            }
            return num;
        }

        private static double GetXCoord( UIElement element, out double xOrigin )
        {
            double num = 0.0;
            xOrigin = 0.0;
            double left = AxisCanvas.GetLeft(element);
            double centerLeft = AxisCanvas.GetCenterLeft(element);
            if ( !left.IsNaN() )
            {
                num = left;
                xOrigin = 0.0;
            }
            else if ( !centerLeft.IsNaN() )
            {
                num = centerLeft;
                xOrigin = 0.5;
            }
            else
            {
                double right = AxisCanvas.GetRight(element);
                if ( !right.IsNaN() )
                {
                    num = 360.0 - right;
                    xOrigin = 1.0;
                }
                else
                {
                    double centerRight = AxisCanvas.GetCenterRight(element);
                    if ( !centerRight.IsNaN() )
                    {
                        num = 360.0 - centerRight;
                        xOrigin = 0.5;
                    }
                }
            }
            return num;
        }
    }
}
