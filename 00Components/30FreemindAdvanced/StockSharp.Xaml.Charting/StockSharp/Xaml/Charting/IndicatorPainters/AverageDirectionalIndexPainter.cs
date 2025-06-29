// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.AverageDirectionalIndexPainter
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.dll
// XML documentation location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Xaml.Charting.xml

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
/// Chart painter for <see cref="T:StockSharp.Algo.Indicators.AverageDirectionalIndex" /> indicator.
/// </summary>
[Indicator(typeof(AverageDirectionalIndex))]
public class AverageDirectionalIndexPainter : BaseChartIndicatorPainter<AverageDirectionalIndex>
{
    
    private readonly IChartLineElement _diPlusLine;
    
    private readonly IChartLineElement _diMinusLine;
    
    private readonly IChartLineElement _adxLine;

    /// <summary>Create instance.</summary>
    public AverageDirectionalIndexPainter()
    {
        IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<AverageDirectionalIndex>.GetIndicatorColorProvider();
        this._diPlusLine = (IChartLineElement)new ChartLineElement()
        {
            Color = indicatorColorProvider.GetNextColor()
        };
        this._diMinusLine = (IChartLineElement)new ChartLineElement()
        {
            Color = indicatorColorProvider.GetNextColor()
        };
        this._adxLine = (IChartLineElement)new ChartLineElement()
        {
            Color = indicatorColorProvider.GetNextColor()
        };
        this.AddChildElement((IChartElement)this.DiPlus);
        this.AddChildElement((IChartElement)this.DiMinus);
        this.AddChildElement((IChartElement)this.Adx);
    }

    /// <summary>
    /// <see cref="P:StockSharp.Xaml.Charting.IndicatorPainters.AverageDirectionalIndexPainter.DiPlus" />.
    ///     </summary>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "DiPlus", Description = "DiPlusLine")]
    public IChartLineElement DiPlus => this._diPlusLine;

    /// <summary>
    /// <see cref="P:StockSharp.Xaml.Charting.IndicatorPainters.AverageDirectionalIndexPainter.DiMinus" />.
    ///     </summary>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "DiMinus", Description = "DiMinusLine")]
    public IChartLineElement DiMinus => this._diMinusLine;

    /// <summary>
    /// <see cref="T:StockSharp.Algo.Indicators.AverageDirectionalIndex" />.
    ///     </summary>
    [Display(ResourceType = typeof(LocalizedStrings), Name = "Adx", Description = "AdxLine")]
    public IChartLineElement Adx => this._adxLine;

    /// <inheritdoc />
    protected override bool OnDraw(
      AverageDirectionalIndex indicator,
      IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data)
    {
        bool flag = false | this.DrawValues(data[(IIndicator)indicator.Dx.Plus], (IChartElement)this.DiPlus) | this.DrawValues(data[(IIndicator)indicator.Dx.Minus], (IChartElement)this.DiMinus);
        IList<ChartDrawData.IndicatorData> vals;
        if (data.TryGetValue((IIndicator)indicator.MovingAverage, out vals))
            flag |= this.DrawValues(vals, (IChartElement)this.Adx);
        return flag;
    }

    /// <inheritdoc />
    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        PersistableHelper.Load((IPersistable)this.DiPlus, storage, XXX.SSS(-539442751));
        PersistableHelper.Load((IPersistable)this.DiMinus, storage, XXX.SSS(-539442732));
        PersistableHelper.Load((IPersistable)this.Adx, storage, XXX.SSS(-539442774));
    }

    /// <inheritdoc />
    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.SetValue<SettingsStorage>(XXX.SSS(-539442751), PersistableHelper.Save((IPersistable)this.DiPlus));
        storage.SetValue<SettingsStorage>(XXX.SSS(-539442732), PersistableHelper.Save((IPersistable)this.DiMinus));
        storage.SetValue<SettingsStorage>(XXX.SSS(-539442774), PersistableHelper.Save((IPersistable)this.Adx));
    }
}
