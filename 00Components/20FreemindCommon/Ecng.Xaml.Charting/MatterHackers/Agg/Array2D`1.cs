// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.Array2D`1
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal class Array2D<dataType>
    {
        private dataType[][] internalArray;

        public Array2D( int width, int height )
        {
            this.internalArray = new dataType[ height ][ ];
            for ( int index = 0 ; index < height ; ++index )
                this.internalArray[ index ] = new dataType[ width ];
        }

        public int Width
        {
            get
            {
                return this.GetRow( 0 ).Length;
            }
        }

        public int Height
        {
            get
            {
                return this.internalArray.Length;
            }
        }

        public dataType[ ] GetRow( int y )
        {
            return this.internalArray[ y ];
        }

        public void Fill( dataType valueToFillWith )
        {
            for ( int y = 0 ; y < this.Height ; ++y )
            {
                dataType[] row = this.GetRow(y);
                for ( int index = 0 ; index < this.Width ; ++index )
                    row[ index ] = valueToFillWith;
            }
        }

        public dataType GetValue( int x, int y )
        {
            return this.GetRow( y )[ x ];
        }

        public void SetValue( int x, int y, dataType value )
        {
            this.GetRow( y )[ x ] = value;
        }
    }
}
