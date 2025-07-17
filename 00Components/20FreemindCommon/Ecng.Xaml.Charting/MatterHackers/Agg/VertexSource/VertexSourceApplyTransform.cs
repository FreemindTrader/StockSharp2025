// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.VertexSource.VertexSourceApplyTransform
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using MatterHackers.Agg.Transform;

namespace MatterHackers.Agg.VertexSource
{
    internal class VertexSourceApplyTransform : IVertexSource
    {
        private IVertexSource vertexSource;
        private ITransform transformToApply;

        public VertexSourceApplyTransform( IVertexSource VertexSource, ITransform newTransformeToApply )
        {
            this.vertexSource = VertexSource;
            this.transformToApply = newTransformeToApply;
        }

        public void attach( IVertexSource VertexSource )
        {
            this.vertexSource = VertexSource;
        }

        public void rewind( int path_id )
        {
            this.vertexSource.rewind( path_id );
        }

        public Path.FlagsAndCommand vertex( out double x, out double y )
        {
            Path.FlagsAndCommand c = this.vertexSource.vertex(out x, out y);
            if ( Path.is_vertex( c ) )
                this.transformToApply.transform( ref x, ref y );
            return c;
        }

        public void SetTransformToApply( ITransform newTransformeToApply )
        {
            this.transformToApply = newTransformeToApply;
        }
    }
}
