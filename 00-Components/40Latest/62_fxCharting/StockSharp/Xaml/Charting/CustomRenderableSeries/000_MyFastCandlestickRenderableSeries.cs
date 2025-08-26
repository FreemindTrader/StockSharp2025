using DevExpress.Xpf.Charts;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Visuals.PaletteProviders;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Data.Model;
using SciChart.Drawing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StockSharp.Xaml.Charting
{
    public interface ISeriesPoint<T> : IComparable where T : IComparable
    {
        T Max
        {
            get;
        }

        T Min
        {
            get;
        }

        T Y
        {
            get;
        }
    }

    public class MyPaletteProvider : MyPaletteProviderBase
    {
        
    }

    public class MyFastCandlestickRenderableSeries : FastCandlestickRenderableSeries
    {
        protected IPen2D _upWickPen;
        protected IPen2D _downWickPen;
        protected IBrush2D _upBodyBrush;
        protected IBrush2D _downBodyBrush;

        /// <summary>
        /// Initializes a new instance of the <see cref="MyFastCandlestickRenderableSeries" /> class.
        /// </summary>
        public MyFastCandlestickRenderableSeries()
        {
            PaletteProvider = new MyPaletteProvider();
        }

        public ISSPaletteProvider XxxPaletteProvider
        {
            get
            {
                return (ISSPaletteProvider)GetValue(PaletteProviderProperty);
            }
            set
            {
                SetValue(PaletteProviderProperty, value);
            }
        }

        protected virtual void DrawReduced(IRenderContext2D renderContext, IRenderPassData renderPassData, IPenManager penManager)
        {
        }

        protected virtual void DrawVanilla(IRenderContext2D renderContext, IRenderPassData renderPassData, IPenManager penManager)
        {
        }

        public static Point TransformPoint(Point point, bool isVerticalChart)
        {
            if ( isVerticalChart )
            {
                double x = point.X;
                point.X = point.Y;
                point.Y = x;
            }
            return point;
        }



        protected override void InternalDraw(IRenderContext2D rc, IRenderPassData renderPassData)
        {        
            IndexRange pointRange = this.CurrentRenderPassData.PointRange;

            using ( PenManager pm = new PenManager(rc, this.AntiAliasing, (float)this.StrokeThickness, this.Opacity, (double[])null) )
            {
                using ( this._upWickPen = pm.GetPen(this.StrokeUp) )
                {
                    using ( this._downWickPen = pm.GetPen(this.StrokeDown) )
                    {
                        _upBodyBrush = pm.GetBrush(this.FillUp);
                        _downBodyBrush = pm.GetBrush(this.FillDown);

                        rc.DisposeResourceAfterDraw( _upBodyBrush );
                        rc.DisposeResourceAfterDraw( _downBodyBrush );

                        rc.SetPrimitivesCachingEnabled(true);

                        if ( RequiresReduction(this.ResamplingMode, pointRange, (int)rc.ViewportSize.Width) )
                        {
                            this.DrawReduced(rc, renderPassData, (IPenManager)pm);
                        }                            
                        else
                        {
                            this.DrawVanilla(rc, renderPassData, (IPenManager)pm);
                        }
                            
                        rc.SetPrimitivesCachingEnabled(false);
                    }
                }
            }
        }

        internal static bool RequiresReduction(SciChart.Data.Numerics.ResamplingMode resamplingMode, IndexRange pointIndices, int viewportWidth)
        {
            int width = pointIndices.Max - pointIndices.Min + 1;
            int vp4X = 4 * viewportWidth;

            return resamplingMode != SciChart.Data.Numerics.ResamplingMode.None & width > vp4X;
        }
    }
}
