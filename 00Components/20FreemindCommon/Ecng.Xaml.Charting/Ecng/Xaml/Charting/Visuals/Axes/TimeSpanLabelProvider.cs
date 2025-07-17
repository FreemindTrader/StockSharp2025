// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.TimeSpanLabelProvider
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
namespace Ecng.Xaml.Charting
{
    public class TimeSpanLabelProvider : LabelProviderBase
    {
        public override string FormatCursorLabel( IComparable dataValue )
        {
            TimeSpanAxis parentAxis = this.ParentAxis as TimeSpanAxis;
            if ( parentAxis == null )
                throw new InvalidOperationException( "The TimeSpanLabelFormatter is only valid on instances of TimeSpanAxis" );
            return this.FormatString( ( TimeSpan ) dataValue, parentAxis.CursorTextFormatting );
        }

        public override string FormatLabel( IComparable dataValue )
        {
            TimeSpanAxis parentAxis = this.ParentAxis as TimeSpanAxis;
            if ( parentAxis == null )
                throw new InvalidOperationException( "The TimeSpanLabelFormatter is only valid on instances of TimeSpanAxis" );
            return this.FormatString( ( TimeSpan ) dataValue, parentAxis.TextFormatting );
        }

        private string FormatString( TimeSpan dataValue, string textFormatting )
        {
            bool flag1 = textFormatting.Contains("-");
            bool flag2 = dataValue < TimeSpan.Zero & flag1;
            string str1;
            if ( !flag1 )
                str1 = textFormatting;
            else
                str1 = textFormatting.TrimStart( '-' );
            string format = str1;
            string str2 = dataValue.ToTimeSpan().ToString(format);
            if ( !flag2 )
                return str2;
            return "-" + str2;
        }
    }
}
