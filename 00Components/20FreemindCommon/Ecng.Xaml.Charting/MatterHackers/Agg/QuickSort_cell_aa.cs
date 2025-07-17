// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.QuickSort_cell_aa
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal class QuickSort_cell_aa
    {
        public void Sort( cell_aa[ ] dataToSort )
        {
            this.Sort( dataToSort, 0, dataToSort.Length - 1 );
        }

        public void Sort( cell_aa[ ] dataToSort, int beg, int end )
        {
            if ( end == beg )
                return;
            int pivotPoint = this.getPivotPoint(dataToSort, beg, end);
            if ( pivotPoint > beg )
                this.Sort( dataToSort, beg, pivotPoint - 1 );
            if ( pivotPoint >= end )
                return;
            this.Sort( dataToSort, pivotPoint + 1, end );
        }

        private int getPivotPoint( cell_aa[ ] dataToSort, int begPoint, int endPoint )
        {
            int index1 = begPoint;
            int index2 = begPoint + 1;
            int index3 = endPoint;
            while ( index2 < endPoint && dataToSort[ index1 ].x >= dataToSort[ index2 ].x )
                ++index2;
            while ( index3 > begPoint && dataToSort[ index1 ].x <= dataToSort[ index3 ].x )
                --index3;
            label_10:
            while ( index2 < index3 )
            {
                cell_aa cellAa = dataToSort[index2];
                dataToSort[ index2 ] = dataToSort[ index3 ];
                dataToSort[ index3 ] = cellAa;
                while ( index2 < endPoint && dataToSort[ index1 ].x >= dataToSort[ index2 ].x )
                    ++index2;
                while ( true )
                {
                    if ( index3 > begPoint && dataToSort[ index1 ].x <= dataToSort[ index3 ].x )
                        --index3;
                    else
                        goto label_10;
                }
            }
            if ( index1 != index3 )
            {
                cell_aa cellAa = dataToSort[index3];
                dataToSort[ index3 ] = dataToSort[ index1 ];
                dataToSort[ index1 ] = cellAa;
            }
            return index3;
        }
    }
}
