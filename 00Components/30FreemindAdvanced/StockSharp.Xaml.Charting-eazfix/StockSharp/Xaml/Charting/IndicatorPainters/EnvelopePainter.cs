// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.EnvelopePainter
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using StockSharp.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting.IndicatorPainters;

[Indicator( typeof( Envelope ) )]
public class EnvelopePainter : BaseChartIndicatorPainter<Envelope>
{

    private readonly IChartBandElement _bollingBand;

    private readonly IChartLineElement _bollingMean;

    public EnvelopePainter()
    {
        IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<Envelope>.GetColorProvider();
        Color nextColor1 = indicatorColorProvider.GetNextColor();
        Color nextColor2 = indicatorColorProvider.GetNextColor();
        ChartBandElement chartBandElement = new ChartBandElement();
        chartBandElement.Line1.AdditionalColor = chartBandElement.Line2.AdditionalColor = nextColor1.ToTransparent( ( byte ) 30 );
        chartBandElement.Line1.Color = chartBandElement.Line2.Color = nextColor1;
        ChartLineElement chartLineElement = new ChartLineElement()
        {
            Color = nextColor2
        };
        this.AddChildElement( ( IChartElement ) ( this._bollingBand = ( IChartBandElement ) chartBandElement ) );
        this.AddChildElement( ( IChartElement ) ( this._bollingMean = ( IChartLineElement ) chartLineElement ) );
        chartBandElement.SetName( ( IChartElement ) chartBandElement.Line1, LocalizedStrings.UpperLine );
        chartBandElement.SetName( ( IChartElement ) chartBandElement.Line2, LocalizedStrings.LowerLine );
        chartBandElement.Line2.AddExtraName( "AdditionalColor" );
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "Band", Description = "Band" )]
    public IChartBandElement Band => this._bollingBand;

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "MovingAverage", Description = "MovingAverage" )]
    public IChartLineElement MovingAverage => this._bollingMean;

    protected override bool OnDraw(
      Envelope indicator,
      IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data )
    {
        return ( 0 | ( this.DrawValues( data[ ( IIndicator ) indicator.Upper ], data[ ( IIndicator ) indicator.Lower ], ( IChartElement ) this.Band ) ? 1 : 0 ) | ( this.DrawValues( data[ ( IIndicator ) indicator.Middle ], ( IChartElement ) this.MovingAverage ) ? 1 : 0 ) ) != 0;
    }

    public override void Load( SettingsStorage storage )
    {
        base.Load( storage );
        PersistableHelper.Load( ( IPersistable ) this.Band, storage, "Band" );
        PersistableHelper.Load( ( IPersistable ) this.MovingAverage, storage, "MovingAverage" );
    }

    public override void Save( SettingsStorage storage )
    {
        base.Save( storage );
        storage.SetValue<SettingsStorage>( "Band", PersistableHelper.Save( ( IPersistable ) this.Band ) );
        storage.SetValue<SettingsStorage>( "MovingAverage", PersistableHelper.Save( ( IPersistable ) this.MovingAverage ) );
    }
}
