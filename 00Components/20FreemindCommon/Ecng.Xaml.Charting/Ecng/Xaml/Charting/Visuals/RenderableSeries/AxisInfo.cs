// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.AxisInfo
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
namespace Ecng.Xaml.Charting
{
    public class AxisInfo : BindableObject
    {
        private bool _isMasterChartAxis = true;
        private string _axisId;
        private string _axisTitle;
        private AxisAlignment _axisAlignment;
        private IComparable _dataValue;
        private string _axisFormattedDataValue;
        private bool _isHorizontal;
        private bool _isXAxis;
        private string _cursorFormattedDataValue;

        public string AxisId
        {
            get
            {
                return this._axisId;
            }
            set
            {
                this._axisId = value;
                this.OnPropertyChanged( nameof( AxisId ) );
            }
        }

        public string AxisTitle
        {
            get
            {
                return this._axisTitle;
            }
            set
            {
                this._axisTitle = value;
                this.OnPropertyChanged( nameof( AxisTitle ) );
            }
        }

        public AxisAlignment AxisAlignment
        {
            get
            {
                return this._axisAlignment;
            }
            set
            {
                this._axisAlignment = value;
                this.OnPropertyChanged( nameof( AxisAlignment ) );
            }
        }

        public IComparable DataValue
        {
            get
            {
                return this._dataValue;
            }
            set
            {
                this._dataValue = value;
                this.OnPropertyChanged( nameof( DataValue ) );
            }
        }

        public string AxisFormattedDataValue
        {
            get
            {
                return this._axisFormattedDataValue;
            }
            set
            {
                this._axisFormattedDataValue = value;
                this.OnPropertyChanged( nameof( AxisFormattedDataValue ) );
            }
        }

        public bool IsHorizontal
        {
            get
            {
                return this._isHorizontal;
            }
            set
            {
                this._isHorizontal = value;
                this.OnPropertyChanged( nameof( IsHorizontal ) );
            }
        }

        public bool IsXAxis
        {
            get
            {
                return this._isXAxis;
            }
            set
            {
                this._isXAxis = value;
                this.OnPropertyChanged( nameof( IsXAxis ) );
            }
        }

        public string CursorFormattedDataValue
        {
            get
            {
                return this._cursorFormattedDataValue;
            }
            set
            {
                this._cursorFormattedDataValue = value;
                this.OnPropertyChanged( nameof( CursorFormattedDataValue ) );
            }
        }

        public bool IsMasterChartAxis
        {
            get
            {
                return this._isMasterChartAxis;
            }
            set
            {
                this._isMasterChartAxis = value;
                this.OnPropertyChanged( nameof( IsMasterChartAxis ) );
            }
        }
    }
}
