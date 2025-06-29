using Ecng.Serialization;
using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using StockSharp.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

#nullable disable
namespace StockSharp.Xaml.Charting.IndicatorPainters;

/// <summary>
/// Chart painter for <see cref="T:StockSharp.Algo.Indicators.Alligator" /> indicator.
/// </summary>
[Display(ResourceType = typeof(LocalizedStrings), Name = "Alligator")]
[Indicator(typeof(Alligator))]
public class AlligatorPainter : BaseChartIndicatorPainter<Alligator>
{
    
    private readonly IChartLineElement _lips;
    
    private readonly IChartLineElement _teeth = null;
    
    private readonly IChartLineElement _jaw;

    /// <summary>Create instance.</summary>
    public AlligatorPainter()
    {
        IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<Alligator>.GetIndicatorColorProvider();

        this._lips = (IChartLineElement)new ChartLineElement()
        {
            Color = indicatorColorProvider.GetNextColor()
        };

        this._teeth = (IChartLineElement)new ChartLineElement()
        {
            Color = indicatorColorProvider.GetNextColor()
        };

        this._jaw = (IChartLineElement)new ChartLineElement()
        {
            Color = indicatorColorProvider.GetNextColor()
        };

        this.AddChildElement( Lips );
        this.AddChildElement( Teeth );
        this.AddChildElement( Jaw );
    }

    /// <summary>
    /// <see cref="P:StockSharp.Algo.Indicators.Alligator.Lips" />.
    ///     </summary>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Lips", Description = "Lips")]
    public IChartLineElement Lips => this._lips;

    /// <summary>
    /// <see cref="P:StockSharp.Algo.Indicators.Alligator.Teeth" />.
    ///     </summary>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Teeth", Description = "Teeth")]
    public IChartLineElement Teeth => this._teeth;

    /// <summary>
    /// <see cref="P:StockSharp.Algo.Indicators.Alligator.Jaw" />.
    ///     </summary>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Jaw", Description = "Jaw")]
    public IChartLineElement Jaw => this._jaw;

    /// <inheritdoc />
    protected override bool OnDraw( Alligator indicator, IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
    {
        return (0 | (this.DrawValues(data[(IIndicator)indicator.Lips], (IChartElement)this.Lips) ? 1 : 0) | (this.DrawValues(data[(IIndicator)indicator.Teeth], (IChartElement)this.Teeth) ? 1 : 0) | (this.DrawValues(data[(IIndicator)indicator.Jaw], (IChartElement)this.Jaw) ? 1 : 0)) != 0;
    }

    /// <inheritdoc />
    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        Lips.Load( storage, "Lips");
        Teeth.Load( storage, "Teeth" );
        Jaw.Load( storage, "Jaw" );
    }

    /// <inheritdoc />
    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.SetValue<SettingsStorage>("Lips", Lips.Save());
        storage.SetValue<SettingsStorage>("Teeth", Teeth.Save());
        storage.SetValue<SettingsStorage>("Jaw", Jaw.Save());
    }
}
