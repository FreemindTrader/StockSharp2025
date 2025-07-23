using Ecng.Drawing;
using Ecng.Serialization;
using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting.IndicatorPainters;

/// <summary>
/// The chart element for <see cref="T:StockSharp.Algo.Indicators.VolumeIndicator" />.
/// </summary>

[Indicator(typeof(VolumeIndicator))]
public class VolumePainter : BaseChartIndicatorPainter<VolumeIndicator>
{

    private readonly IChartLineElement _upVolume;

    private readonly IChartLineElement _downVolume;

    /// <summary>Create instance.</summary>
    public VolumePainter()
    {
        _upVolume = (IChartLineElement)new ChartLineElement()
        {
            Color = Colors.Green
        };

        _downVolume = (IChartLineElement)new ChartLineElement()
        {
            Color = Colors.Red
        };

        UpVolume.Style = DownVolume.Style = DrawStyles.Histogram;
        AddChildElement((IChartElement)UpVolume);
        AddChildElement((IChartElement)DownVolume);
    }

    /// <summary>Up line color.</summary>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "UpColor", Description = "UpCandleColor")]
    public IChartLineElement UpVolume => _upVolume;

    /// <summary>Down line color.</summary>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "DownColor", Description = "DownCandleColor")]
    public IChartLineElement DownVolume => _downVolume;

    protected override bool OnDraw(VolumeIndicator indicator, IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
    {
        return (     ( DrawValues(data[(IIndicator)indicator], (IChartElement)UpVolume,   p=> !VolumePainter.IsUpOrDown(p.Value) ? double.NaN : (double)p.Value.ToDecimal())  ) 
                   | ( DrawValues(data[(IIndicator)indicator], (IChartElement)DownVolume, p=> !VolumePainter.IsUpOrDown(p.Value) ? (double)p.Value.ToDecimal() : double.NaN)  ) ) ;
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        PersistableHelper.Load((IPersistable)UpVolume, storage, "UpVolume");
        PersistableHelper.Load((IPersistable)DownVolume, storage, "DownVolume");
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.SetValue<SettingsStorage>("UpVolume", PersistableHelper.Save((IPersistable)UpVolume));
        storage.SetValue<SettingsStorage>("DownVolume", PersistableHelper.Save((IPersistable)DownVolume));
    }

    public static bool IsUpOrDown(IIndicatorValue indicatorValue)
    {
        if ( indicatorValue == null )
            return false;
        ICandleMessage icandleMessage = indicatorValue.GetValue<ICandleMessage>();
        return icandleMessage == null || icandleMessage.OpenPrice<icandleMessage.ClosePrice;
    }    
}
