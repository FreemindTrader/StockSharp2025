// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Visuals.Axes.NumericLabelProvider
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: B:\00 - Programming\StockSharp\References\StockSharp.Xaml.Charting.dll

using System;
using System.Globalization;

namespace StockSharp.Xaml.Charting.Visuals.Axes
{
    public class NumericLabelProvider : LabelProviderBase
    {
        public override ITickLabelViewModel CreateDataContext( IComparable dataValue )
        {
            return this.UpdateDataContext( ( ITickLabelViewModel ) new NumericTickLabelViewModel(), dataValue );
        }

        public override ITickLabelViewModel UpdateDataContext( ITickLabelViewModel labelDataContext, IComparable dataValue )
        {
            base.UpdateDataContext( labelDataContext, dataValue );

            var tickLabelViewModel  = (NumericTickLabelViewModel) labelDataContext;
            var parentAxis          = (NumericAxis) this.ParentAxis;
            string text             = labelDataContext.Text;
            tickLabelViewModel.Text = text;
            int length              = text.IndexOfAny(new char[2]{ 'e', 'E' });

            tickLabelViewModel.HasExponent = parentAxis.ScientificNotation != ScientificNotation.None && length >= 0;

            if ( tickLabelViewModel.HasExponent )
            {
                tickLabelViewModel.Text = text.Substring( 0, length );
                tickLabelViewModel.Exponent = text.Substring( length + 1 );
                tickLabelViewModel.Separator = parentAxis.ScientificNotation == ScientificNotation.Normalized ? "x10" : text[ length ].ToString( ( IFormatProvider ) CultureInfo.InvariantCulture );
            }
            return ( ITickLabelViewModel ) tickLabelViewModel;
        }

        public override string FormatCursorLabel( IComparable dataValue )
        {
            if ( !string.IsNullOrEmpty( this.ParentAxis.CursorTextFormatting ) )
                return this.FormatText( dataValue, this.ParentAxis.CursorTextFormatting );
            return this.FormatLabel( dataValue );
        }

        private string FormatText( IComparable dataValue, string format )
        {
            return string.Format( "{0:" + format + "}", ( object ) dataValue );
        }

        public override string FormatLabel( IComparable dataValue )
        {
            return string.Format( "{0:" + this.ParentAxis.TextFormatting + "}", ( object ) dataValue );
        }
    }
}
