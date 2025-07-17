// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.ValueToTopMarginConverter
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
namespace fx.Xaml.Charting
{
    public class ValueToTopMarginConverter : IValueConverter
    {
        private TextBlock _measurement = new TextBlock();

        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            TextBlock textBlock = value as TextBlock;
            NumericTickLabelViewModel tickLabelViewModel = textBlock == null || textBlock.DataContext == null ? (NumericTickLabelViewModel) null : textBlock.DataContext as NumericTickLabelViewModel;
            Thickness thickness = new Thickness(0.0, 0.0, 0.0, 0.0);
            if ( tickLabelViewModel != null && tickLabelViewModel.HasExponent )
            {
                double num = double.Parse((string) parameter, (IFormatProvider) CultureInfo.InvariantCulture);
                this._measurement.FontSize = textBlock.FontSize;
                this._measurement.MeasureArrange();
                thickness.Top = this._measurement.ActualHeight * num;
            }
            return ( object ) thickness;
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
