// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.CandlePatternElement
// Assembly: StockSharp.Xaml.Charting, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B81ABC38-30E9-4E5C-D0FB-A30B79FCF2D6
// Assembly location: C:\00-Reverse\StockSharp.Xaml.Charting-eazfix.dll

using Ecng.Collections;
using Ecng.Serialization;
using Ecng.Xaml;
using StockSharp.Charting;
using StockSharp.Localization;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Windows.Media;

#nullable disable
namespace StockSharp.Xaml.Charting;

public class CandlePatternElement :
  ChartComponentViewModel<CandlePatternElement>,
  IChartElement,
  IChartPart<IChartElement>,
  INotifyPropertyChanged,
  INotifyPropertyChanging,
  IPersistable,
  IChartComponent,
  IChartElementUiDomain
{

    private Color _downColor;

    private Color _upColor;

    private ChartElementUiDomain _baseViewModel;

    [Display(ResourceType = typeof(LocalizedStrings), Name = "Decrease", Description = "ColorOfDecreaseCandle", GroupName = "Style", Order = 30)]
    public Color DownColor
    {
        get => _downColor;
        set
        {
            _downColor = value;
            RaisePropertyChanged(nameof(DownColor));
        }
    }

    [Display(ResourceType = typeof(LocalizedStrings), Name = "Increase", Description = "ColorOfIncreaseCandle", GroupName = "Style", Order = 31 /*0x1F*/)]
    public Color UpColor
    {
        get => _upColor;
        set
        {
            _upColor = value;
            RaisePropertyChanged(nameof(UpColor));
        }
    }

    ChartElementUiDomain IChartElementUiDomain.CreateViewModel(IDrawingSurfaceVM viewModel)
    {
        return _baseViewModel = new CandlePatternElementViewModel(this);
    }
    

    bool IChartElementUiDomain.StartDrawing(IEnumerableEx<ChartDrawData.IDrawValue> _param1)
    {
        return _baseViewModel.Draw(_param1);
    }

    void IChartElementUiDomain.StartDrawing()
    {
        _baseViewModel.Draw(CollectionHelper.ToEx<ChartDrawData.IDrawValue>(Enumerable.Empty<ChartDrawData.IDrawValue>(), 0));
    }

    protected override bool OnDraw(ChartDrawData data) => throw new NotSupportedException();

    Color IChartElementUiDomain.Color
    {
        get
        {
            return Colors.Transparent;
        }    
    }

    public override void Load(SettingsStorage storage)
    {
        base.Load(storage);
        DownColor = storage.GetValue<int>("DownColor", 0).ToColor();
        UpColor = storage.GetValue<int>("UpColor", 0).ToColor();
    }

    public override void Save(SettingsStorage storage)
    {
        base.Save(storage);
        storage.SetValue<int>("DownColor", DownColor.ToInt());
        storage.SetValue<int>("UpColor", UpColor.ToInt());
    }
}
