﻿
using StockSharp.Messages;
using System.Windows;
using System.Windows.Controls;

namespace StockSharp.Studio.Controls
{
    internal sealed class CandleSeriesCheckBox : CheckBox
    {
        public static readonly DependencyProperty SeriesProperty = DependencyProperty.Register( nameof( Series ), typeof( DataType ), typeof( CandleSeriesCheckBox ) );

        public DataType Series
        {
            get
            {
                return ( DataType )GetValue( SeriesProperty );
            }
            set
            {
                SetValue( SeriesProperty, value );
            }
        }
    }
}
