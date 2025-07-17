// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.line_aa_vertex_sequence
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal class line_aa_vertex_sequence : VectorPOD<line_aa_vertex>
    {
        public override void add( line_aa_vertex val )
        {
            if ( this.size() > 1 && !this.Array[ this.size() - 2 ].Compare( this.Array[ this.size() - 1 ] ) )
                this.RemoveLast();
            base.add( val );
        }

        public void modify_last( line_aa_vertex val )
        {
            this.RemoveLast();
            this.add( val );
        }

        public void close( bool closed )
        {
            while ( this.size() > 1 && !this.Array[ this.size() - 2 ].Compare( this.Array[ this.size() - 1 ] ) )
            {
                line_aa_vertex val = this[this.size() - 1];
                this.RemoveLast();
                this.modify_last( val );
            }
            if ( !closed )
                return;
            while ( this.size() > 1 && !this.Array[ this.size() - 1 ].Compare( this.Array[ 0 ] ) )
                this.RemoveLast();
        }

        internal line_aa_vertex prev( int idx )
        {
            return this[ ( idx + this.currentSize - 1 ) % this.currentSize ];
        }

        internal line_aa_vertex curr( int idx )
        {
            return this[ idx ];
        }

        internal line_aa_vertex next( int idx )
        {
            return this[ ( idx + 1 ) % this.currentSize ];
        }
    }
}
