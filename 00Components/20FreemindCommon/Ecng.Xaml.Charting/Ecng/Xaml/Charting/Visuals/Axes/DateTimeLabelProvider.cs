using System;
using StockSharp.Xaml.Charting.Common.Extensions;

namespace StockSharp.Xaml.Charting.Visuals.Axes
{
    public class DateTimeLabelProvider : LabelProviderBase
    {
        public DateTimeLabelProvider()
        {
        }

        public override string FormatCursorLabel( IComparable dataValue )
        {
            DateTime dateTime = dataValue.ToDateTime();
            if ( base.ParentAxis.CursorTextFormatting.IsNullOrEmpty<char>() )
            {
                return this.FormatLabel( dataValue );
            }
            return dateTime.ToString( base.ParentAxis.CursorTextFormatting );
        }

        public override string FormatLabel( IComparable dataValue )
        {
            DateTimeAxis parentAxis = base.ParentAxis as DateTimeAxis;
            if ( parentAxis == null )
            {
                throw new InvalidOperationException( "The DateTimeLabelFormatter is only valid on instances of DateTimeAxis" );
            }
            DateTime dateTime = dataValue.ToDateTime();
            if ( ( base.ParentAxis.VisibleRange as DateRange ).Diff.Ticks > TimeSpan.FromDays( 1 ).Ticks )
            {
                return dateTime.ToString( parentAxis.TextFormatting );
            }
            return dateTime.ToString( parentAxis.SubDayTextFormatting );
        }
    }
}
