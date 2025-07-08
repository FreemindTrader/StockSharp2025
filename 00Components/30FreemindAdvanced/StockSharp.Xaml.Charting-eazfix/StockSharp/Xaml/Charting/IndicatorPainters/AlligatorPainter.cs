// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.AlligatorPainter
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Serialization;
using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using StockSharp.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

#nullable disable
namespace StockSharp.Xaml.Charting.IndicatorPainters;

[Display( ResourceType = typeof( LocalizedStrings ), Name = "Alligator" )]
[Indicator( typeof( Alligator ) )]
public class AlligatorPainter : BaseChartIndicatorPainter<Alligator>
{
    
    private readonly IChartLineElement _lips;
    
    private readonly IChartLineElement _teeth;
  
    private readonly IChartLineElement _jaw;

  public AlligatorPainter()
    {
        IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<Alligator>.GetColorProvider();
        this._lips = ( IChartLineElement ) new ChartLineElement()
        {
            Color = indicatorColorProvider.GetNextColor()
        };
        this._teeth = ( IChartLineElement ) new ChartLineElement()
        {
            Color = indicatorColorProvider.GetNextColor()
        };
        this._jaw = ( IChartLineElement ) new ChartLineElement()
        {
            Color = indicatorColorProvider.GetNextColor()
        };
        this.AddChildElement( ( IChartElement ) this.Lips );
        this.AddChildElement( ( IChartElement ) this.Teeth );
        this.AddChildElement( ( IChartElement ) this.Jaw );
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "Lips", Description = "Lips" )]
    public IChartLineElement Lips => this._lips;

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "Teeth", Description = "Teeth" )]
    public IChartLineElement Teeth => this._teeth;

  [Display( ResourceType = typeof( LocalizedStrings ), Name = "Jaw", Description = "Jaw" )]
    public IChartLineElement Jaw => this._jaw;

  protected override bool OnDraw(
    Alligator indicator,
    IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data )
    {
        return ( 0 | ( this.DrawValues( data[ ( IIndicator ) indicator.Lips ], ( IChartElement ) this.Lips ) ? 1 : 0 ) | ( this.DrawValues( data[ ( IIndicator ) indicator.Teeth ], ( IChartElement ) this.Teeth ) ? 1 : 0 ) | ( this.DrawValues( data[ ( IIndicator ) indicator.Jaw ], ( IChartElement ) this.Jaw ) ? 1 : 0 ) ) != 0;
    }

    public override void Load( SettingsStorage storage )
    {
        base.Load( storage );
        PersistableHelper.Load( ( IPersistable ) this.Lips, storage, "Lips" );
        PersistableHelper.Load( ( IPersistable ) this.Teeth, storage, "Teeth" );
        PersistableHelper.Load( ( IPersistable ) this.Jaw, storage, "Jaw" );
    }

    public override void Save( SettingsStorage storage )
    {
        base.Save( storage );
        storage.SetValue<SettingsStorage>( "Lips", PersistableHelper.Save( ( IPersistable ) this.Lips ) );
        storage.SetValue<SettingsStorage>( "Teeth", PersistableHelper.Save( ( IPersistable ) this.Teeth ) );
        storage.SetValue<SettingsStorage>( "Jaw", PersistableHelper.Save( ( IPersistable ) this.Jaw ) );
    }
}
