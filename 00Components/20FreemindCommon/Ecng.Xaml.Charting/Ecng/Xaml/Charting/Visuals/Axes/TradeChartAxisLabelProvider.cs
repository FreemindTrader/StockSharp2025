// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.TradeChartAxisLabelProvider
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
namespace Ecng.Xaml.Charting
{
    public class TradeChartAxisLabelProvider : LabelProviderBase
    {
        private readonly string[] _cursorFormatStrings = new string[4]{ "MMM {0}", "yyyy {0}", "{0} yyyy", "dd MMM {0}" };
        private readonly string[] _majorFormatStrings = new string[4]{ "yyyy", "MMM", "dd MMM", "HH:mm" };
        private int _formatIndex;

        public override void Init( IAxis parentAxis )
        {
            if ( !( parentAxis is CategoryDateTimeAxis ) )
                throw new InvalidOperationException( "The TradeChartAxisLabelFormatter is only valid on instances of CategoryDateTimeAxis" );
            base.Init( parentAxis );
        }

        public override void OnBeginAxisDraw()
        {
            CategoryDateTimeAxis parentAxis = (CategoryDateTimeAxis) this.ParentAxis;
            double barTimeFrame = parentAxis.GetBarTimeFrame();
            DateRange dateRange = parentAxis.ToDateRange((IndexRange) parentAxis.VisibleRange);
            this._formatIndex = 3;
            if ( dateRange != null && dateRange.IsDefined )
            {
                long ticks = dateRange.Diff.Ticks;
                if ( ticks > TimeSpanExtensions.FromYears( 2 ).Ticks )
                    this._formatIndex = 0;
                else if ( ticks > TimeSpan.FromDays( 14.0 ).Ticks || barTimeFrame >= ( double ) TimeSpan.FromDays( 1.0 ).Ticks )
                    this._formatIndex = -1;
            }
            base.OnBeginAxisDraw();
        }

        public override string FormatCursorLabel( IComparable dataValue )
        {
            DateTime dateTime = dataValue.ToDateTime();
            string str;
            if ( this.ParentAxis.CursorTextFormatting.IsNullOrEmpty<char>() )
            {
                int formattingIndex = this.GetFormattingIndex(dateTime, true);
                str = string.Format( dateTime.ToString( this._cursorFormatStrings[ formattingIndex ] ), ( object ) dateTime.ToString( this._majorFormatStrings[ formattingIndex ] ) );
            }
            else
                str = dateTime.ToString( this.ParentAxis.CursorTextFormatting );
            return str;
        }

        public override string FormatLabel( IComparable dataValue )
        {
            DateTime dateTime = dataValue.ToDateTime();
            string majorFormatString = this._majorFormatStrings[this.GetFormattingIndex(dateTime, false)];
            return dateTime.ToString( majorFormatString );
        }

        private int GetFormattingIndex( DateTime dataValue, bool forCursor = false )
        {
            int num = this._formatIndex;
            if ( num < 0 )
            {
                num = 2;
                if ( dataValue.Day == 1 && !forCursor )
                    num = 1;
            }
            return num;
        }
    }
}
