// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.VertexSequence
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal class VertexSequence : VectorPOD<VertexDistance>
    {
        public override void add( VertexDistance val )
        {
            if ( this.size() > 1 && !this.Array[ this.size() - 2 ].IsEqual( this.Array[ this.size() - 1 ] ) )
                this.RemoveLast();
            base.add( val );
        }

        public void modify_last( VertexDistance val )
        {
            this.RemoveLast();
            this.add( val );
        }

        public void close( bool closed )
        {
            while ( this.size() > 1 && !this.Array[ this.size() - 2 ].IsEqual( this.Array[ this.size() - 1 ] ) )
            {
                VertexDistance val = this[this.size() - 1];
                this.RemoveLast();
                this.modify_last( val );
            }
            if ( !closed )
                return;
            while ( this.size() > 1 && !this.Array[ this.size() - 1 ].IsEqual( this.Array[ 0 ] ) )
                this.RemoveLast();
        }

        internal VertexDistance prev( int idx )
        {
            return this[ ( idx + this.currentSize - 1 ) % this.currentSize ];
        }

        internal VertexDistance curr( int idx )
        {
            return this[ idx ];
        }

        internal VertexDistance next( int idx )
        {
            return this[ ( idx + 1 ) % this.currentSize ];
        }
    }
}
