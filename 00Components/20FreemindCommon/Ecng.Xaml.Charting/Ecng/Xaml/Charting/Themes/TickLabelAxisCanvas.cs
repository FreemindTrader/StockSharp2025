// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Themes.TickLabelAxisCanvas
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Numerics.GenericMath;
using StockSharp.Xaml.Charting.Visuals.Axes;

namespace StockSharp.Xaml.Charting.Themes
{
    public class TickLabelAxisCanvas : AxisCanvas
    {
        public static readonly DependencyProperty IsLabelCullingEnabledProperty = DependencyProperty.Register(nameof (IsLabelCullingEnabled), typeof (bool), typeof (TickLabelAxisCanvas), new PropertyMetadata((object) true));
        private readonly DoubleMath _doubleMath = new DoubleMath();
        private readonly List<DefaultTickLabel> _placedLabels = new List<DefaultTickLabel>();

        public bool IsLabelCullingEnabled
        {
            get
            {
                return ( bool ) this.GetValue( TickLabelAxisCanvas.IsLabelCullingEnabledProperty );
            }
            set
            {
                this.SetValue( TickLabelAxisCanvas.IsLabelCullingEnabledProperty, ( object ) value );
            }
        }

        protected override Size MeasureOverride( Size constraint )
        {
            if ( !this.SizeWidthToContent )
                return this.MeasureHeight();
            return this.MeasureWidth();
        }

        private Size MeasureWidth()
        {
            Size availableSize = new Size(double.PositiveInfinity, double.PositiveInfinity);
            double num = 0.0;
            foreach ( UIElement child in this.Children )
            {
                child.Measure( availableSize );
                Size desiredSize = child.DesiredSize;
                double width1 = desiredSize.Width;
                double a = this._doubleMath.Max(AxisCanvas.GetLeft(child) + width1, AxisCanvas.GetCenterLeft(child) + width1 / 2.0);
                if ( this._doubleMath.IsNaN( a ) )
                {
                    double right = AxisCanvas.GetRight(child);
                    desiredSize = child.DesiredSize;
                    double width2 = desiredSize.Width;
                    a = right + width2;
                }
                num = this._doubleMath.Max( a, num );
            }
            return new Size( num, 0.0 );
        }

        private Size MeasureHeight()
        {
            Size availableSize = new Size(double.PositiveInfinity, double.PositiveInfinity);
            double num = 0.0;
            foreach ( UIElement child in this.Children )
            {
                child.Measure( availableSize );
                Size desiredSize = child.DesiredSize;
                double height1 = desiredSize.Height;
                double a = this._doubleMath.Max(AxisCanvas.GetTop(child) + height1, AxisCanvas.GetCenterTop(child) + height1 / 2.0);
                if ( this._doubleMath.IsNaN( a ) )
                {
                    double bottom = AxisCanvas.GetBottom(child);
                    desiredSize = child.DesiredSize;
                    double height2 = desiredSize.Height;
                    a = bottom + height2;
                }
                num = this._doubleMath.Max( a, num );
            }
            return new Size( 0.0, num );
        }

        protected override Size ArrangeOverride( Size arrangeSize )
        {
            this._placedLabels.Clear();
            bool flag1 = false;
            bool labelCullingEnabled = this.IsLabelCullingEnabled;
            foreach ( IGrouping<int, DefaultTickLabel> source in ( IEnumerable<IGrouping<int, DefaultTickLabel>> ) this.Children.OfType<DefaultTickLabel>().GroupBy<DefaultTickLabel, int>( ( Func<DefaultTickLabel, int> ) ( x => x.CullingPriority ) ).OrderByDescending<IGrouping<int, DefaultTickLabel>, int>( ( Func<IGrouping<int, DefaultTickLabel>, int> ) ( x => x.Key ) ) )
            {
                if ( !flag1 )
                {
                    foreach ( DefaultTickLabel label in ( IEnumerable<DefaultTickLabel> ) source )
                    {
                        Rect arrangedRect = this.GetArrangedRect(arrangeSize, (UIElement) label);
                        label.ArrangedRect = arrangedRect;
                        if ( labelCullingEnabled )
                        {
                            flag1 = this._placedLabels.Any<DefaultTickLabel>( ( Func<DefaultTickLabel, bool> ) ( lbl => lbl.ArrangedRect.IntersectsWith( arrangedRect ) ) );
                            if ( flag1 )
                                break;
                        }
                        TickLabelAxisCanvas.ShowLabel( label );
                        this._placedLabels.Add( label );
                        label.Arrange( arrangedRect );
                    }
                }
                if ( flag1 )
                {
                    bool flag2 = this._placedLabels.Count == 1 && this._placedLabels[0].CullingPriority == source.Key;
                    source.Skip<DefaultTickLabel>( flag2 ? 1 : 0 ).ForEachDo<DefaultTickLabel>( ( Action<DefaultTickLabel> ) ( lbl =>
                    {
                        TickLabelAxisCanvas.HideLabel( lbl );
                        this._placedLabels.Remove( lbl );
                    } ) );
                }
            }
            return arrangeSize;
        }

        private static void ShowLabel( DefaultTickLabel label )
        {
            label.Opacity = 1.0;
        }

        private static void HideLabel( DefaultTickLabel label )
        {
            label.Opacity = 0.0;
        }
    }
}
