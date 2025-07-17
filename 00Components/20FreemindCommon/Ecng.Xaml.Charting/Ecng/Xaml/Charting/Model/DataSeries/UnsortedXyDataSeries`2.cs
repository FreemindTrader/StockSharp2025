// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.Model.DataSeries.UnsortedXyDataSeries`2
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;

namespace fx.Xaml.Charting
{
    [Obsolete( "UnsortedXyDataSeries is obsolete. Please use the XyDataseries which now correctly detects if your data is sorted or unsorted", true )]
    public class UnsortedXyDataSeries<TX, TY> : XyDataSeries<TX, TY> where TX : IComparable where TY : IComparable
    {
    }
}
