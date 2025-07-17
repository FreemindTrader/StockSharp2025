// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.VertexSource.IGenerator
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace MatterHackers.Agg.VertexSource
{
    internal interface IGenerator
    {
        void RemoveAll();

        void AddVertex( double x, double y, Path.FlagsAndCommand unknown );

        void Rewind( int path_id );

        Path.FlagsAndCommand Vertex( ref double x, ref double y );

        LineCap line_cap();

        LineJoin line_join();

        InnerJoin inner_join();

        void line_cap( LineCap lc );

        void line_join( LineJoin lj );

        void inner_join( InnerJoin ij );

        void width( double w );

        void miter_limit( double ml );

        void miter_limit_theta( double t );

        void inner_miter_limit( double ml );

        void approximation_scale( double approxScale );

        double width();

        double miter_limit();

        double inner_miter_limit();

        double approximation_scale();

        void auto_detect_orientation( bool v );

        bool auto_detect_orientation();

        void shorten( double s );

        double shorten();
    }
}
