// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.VertexSource.Stroke
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace MatterHackers.Agg.VertexSource
{
    internal sealed class Stroke : VertexSourceAdapter, IVertexSource
    {
        public Stroke( IVertexSource vs, double inWidth = 1.0 )
          : base( vs, ( IGenerator ) new StrokeGenerator() )
        {
            this.width( inWidth );
        }

        public void line_cap( LineCap lc )
        {
            this.GetGenerator().line_cap( lc );
        }

        public void line_join( LineJoin lj )
        {
            this.GetGenerator().line_join( lj );
        }

        public void inner_join( InnerJoin ij )
        {
            this.GetGenerator().inner_join( ij );
        }

        public LineCap line_cap()
        {
            return this.GetGenerator().line_cap();
        }

        public LineJoin line_join()
        {
            return this.GetGenerator().line_join();
        }

        public InnerJoin inner_join()
        {
            return this.GetGenerator().inner_join();
        }

        public void width( double w )
        {
            this.GetGenerator().width( w );
        }

        public void miter_limit( double ml )
        {
            this.GetGenerator().miter_limit( ml );
        }

        public void miter_limit_theta( double t )
        {
            this.GetGenerator().miter_limit_theta( t );
        }

        public void inner_miter_limit( double ml )
        {
            this.GetGenerator().inner_miter_limit( ml );
        }

        public void approximation_scale( double approxScale )
        {
            this.GetGenerator().approximation_scale( approxScale );
        }

        public double width()
        {
            return this.GetGenerator().width();
        }

        public double miter_limit()
        {
            return this.GetGenerator().miter_limit();
        }

        public double inner_miter_limit()
        {
            return this.GetGenerator().inner_miter_limit();
        }

        public double approximation_scale()
        {
            return this.GetGenerator().approximation_scale();
        }

        public void shorten( double s )
        {
            this.GetGenerator().shorten( s );
        }

        public double shorten()
        {
            return this.GetGenerator().shorten();
        }
    }
}
