// Decompiled with JetBrains decompiler
// Type: -.ValueToTopMarginConverter
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using SciChart.Charting.Visuals.Axes.LabelProviders;
using SciChart.Core.Extensions;

#nullable disable
namespace SciChart.Charting;

internal sealed class ValueToTopMarginConverter : IValueConverter
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

    public object ConvertBack( object _param1, Type _param2, object _param3, CultureInfo _param4 )
    {
        throw new NotImplementedException();
    }
}
