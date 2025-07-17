// Decompiled with JetBrains decompiler
// Type: MatterHackers.Agg.QuickSort_range_adaptor_uint
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

namespace MatterHackers.Agg
{
    internal class QuickSort_range_adaptor_uint
    {
        public void Sort( VectorPOD_RangeAdaptor dataToSort )
        {
            this.Sort( dataToSort, 0, dataToSort.size() - 1 );
        }

        public void Sort( VectorPOD_RangeAdaptor dataToSort, int beg, int end )
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

        private int getPivotPoint( VectorPOD_RangeAdaptor dataToSort, int begPoint, int endPoint )
        {
            int index1 = begPoint;
            int index2 = begPoint + 1;
            int index3 = endPoint;
            while ( index2 < endPoint && dataToSort[ index1 ] >= dataToSort[ index2 ] )
                ++index2;
            while ( index3 > begPoint && dataToSort[ index1 ] <= dataToSort[ index3 ] )
                --index3;
            label_10:
            while ( index2 < index3 )
            {
                int num = dataToSort[index2];
                dataToSort[ index2 ] = dataToSort[ index3 ];
                dataToSort[ index3 ] = num;
                while ( index2 < endPoint && dataToSort[ index1 ] >= dataToSort[ index2 ] )
                    ++index2;
                while ( true )
                {
                    if ( index3 > begPoint && dataToSort[ index1 ] <= dataToSort[ index3 ] )
                        --index3;
                    else
                        goto label_10;
                }
            }
            if ( index1 != index3 )
            {
                int num = dataToSort[index3];
                dataToSort[ index3 ] = dataToSort[ index1 ];
                dataToSort[ index1 ] = num;
            }
            return index3;
        }
    }
}
