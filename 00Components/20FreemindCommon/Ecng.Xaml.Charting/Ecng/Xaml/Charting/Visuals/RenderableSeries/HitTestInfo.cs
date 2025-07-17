// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.HitTestInfo
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Windows;
namespace fx.Xaml.Charting
{
    public struct HitTestInfo
    {
        public static readonly HitTestInfo Empty = new HitTestInfo(true);
        private bool _isEmpty;

        public string DataSeriesName
        {
            get; set;
        }

        public DataSeriesType DataSeriesType
        {
            get; set;
        }

        public Point HitTestPoint
        {
            get; set;
        }

        public Point Y1HitTestPoint
        {
            get; set;
        }

        public IComparable XValue
        {
            get; set;
        }

        public IComparable YValue
        {
            get; set;
        }

        public IComparable Y1Value
        {
            get; set;
        }

        public IComparable ZValue
        {
            get; set;
        }

        public int DataSeriesIndex
        {
            get; set;
        }

        public bool IsHit
        {
            get; set;
        }

        public bool IsVerticalHit
        {
            get; set;
        }

        public bool IsWithinDataBounds
        {
            get; set;
        }

        public IComparable ErrorHigh
        {
            get; set;
        }

        public IComparable ErrorLow
        {
            get; set;
        }

        public IComparable OpenValue
        {
            get; set;
        }

        public IComparable HighValue
        {
            get; set;
        }

        public IComparable LowValue
        {
            get; set;
        }

        public IComparable CloseValue
        {
            get; set;
        }

        public IComparable Minimum
        {
            get; set;
        }

        public IComparable Maximum
        {
            get; set;
        }

        public IComparable Median
        {
            get; set;
        }

        public IComparable LowerQuartile
        {
            get; set;
        }

        public IComparable UpperQuartile
        {
            get; set;
        }

        public double Persentage
        {
            get; set;
        }

        public long Volume
        {
            get; set;
        }

        private HitTestInfo( bool isEmpty )
        {
            this = new HitTestInfo();
            this._isEmpty = isEmpty;
        }

        public bool IsEmpty()
        {
            return this._isEmpty;
        }
    }
}
