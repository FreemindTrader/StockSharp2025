// Decompiled with JetBrains decompiler
// Type: #=z9jHRW$4hcTcRirEhLafLfKwkzeHFx2BtVDw8LCsrGTu1
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using \u002D;
using System;

#nullable disable
public sealed class fxTradeChartAxisLabelProvider : LabelProviderBase
{
    public override string FormatCursorLabel( IComparable _param1, bool _param2 )
    {
        DateTime dateTime = _param1.\u0023\u003Dzxuo5aY4wjkaI();
        return !this.\u0023\u003DzHZDgUSdfqmkx().get_CursorTextFormatting().\u0023\u003DzCCMM80zDpO6N<char>() ? dateTime.ToString( this.\u0023\u003DzHZDgUSdfqmkx().get_CursorTextFormatting() ) : this.FormatLabel( _param1 );
    }

    public override string FormatLabel( IComparable _param1 )
    {
        if ( !( this.\u0023\u003DzHZDgUSdfqmkx() is DateTimeAxis y8VrcqY29Qf62Ejd))
      throw new InvalidOperationException( "The DateTimeLabelFormatter is only valid on instances of DateTimeAxis" );
        DateTime dateTime = _param1.\u0023\u003Dzxuo5aY4wjkaI();
        return ( this.\u0023\u003DzHZDgUSdfqmkx().VisibleRange as DateRange).Diff.Ticks > TimeSpan.FromDays( 1.0 ).Ticks ? dateTime.ToString( y8VrcqY29Qf62Ejd.TextFormatting ) : dateTime.ToString( y8VrcqY29Qf62Ejd.SubDayTextFormatting );
    }
}
