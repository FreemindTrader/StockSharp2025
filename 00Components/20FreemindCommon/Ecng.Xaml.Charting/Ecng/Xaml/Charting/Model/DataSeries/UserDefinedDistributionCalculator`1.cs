// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Model.DataSeries.UserDefinedDistributionCalculator`1
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;

namespace Ecng.Xaml.Charting.Model.DataSeries
{
    public class UserDefinedDistributionCalculator<TX> : BaseDataDistributionCalculator<TX> where TX : IComparable
    {
        public UserDefinedDistributionCalculator()
        {
            this.IsSortedAscending = true;
            this.IsEvenlySpaced = true;
        }

        public bool IsSortedAscending
        {
            get
            {
                return this.DataIsSortedAscending;
            }
            set
            {
                this.DataIsSortedAscending = value;
            }
        }

        public bool IsEvenlySpaced
        {
            get
            {
                return this.DataIsEvenlySpaced;
            }
            set
            {
                this.DataIsEvenlySpaced = value;
            }
        }

        public override void OnAppendXValue( ISeriesColumn<TX> xValues, TX newXValue, bool acceptsUnsortedData )
        {
        }

        public override void OnAppendXValues( ISeriesColumn<TX> xValues, int countBeforeAppending, IEnumerable<TX> newXValues, bool acceptsUnsortedData )
        {
        }

        public override void OnInsertXValue( ISeriesColumn<TX> xValues, int indexWhereInserted, TX newXValue, bool acceptsUnsortedData )
        {
        }

        public override void OnInsertXValues( ISeriesColumn<TX> xValues, int indexWhereInserted, int insertedCount, IEnumerable<TX> newXValues, bool acceptsUnsortedData )
        {
        }
    }
}
