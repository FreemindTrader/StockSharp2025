// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.IndicatorPainters.DefaultPainter
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
using System.Drawing;

#nullable disable
namespace StockSharp.Xaml.Charting.IndicatorPainters;

public class DefaultPainter : BaseChartIndicatorPainter<IIndicator>
{

    private readonly IChartLineElement _line;

    public DefaultPainter()
    {
        IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<IIndicator>.GetColorProvider();
        
        _line = ( IChartLineElement ) new ChartLineElement()
        {
            Color = indicatorColorProvider.GetNextColor()
        };
        
        AddChildElement( ( IChartElement ) Line );
    }

    [Display( ResourceType = typeof( LocalizedStrings ), Name = "Line2", Description = "Line2" )]
    public IChartLineElement Line => _line;

    protected override bool OnDraw( IIndicator indicator, IDictionary<IIndicator, IList<ChartDrawData.IndicatorData>> data )
    {
        if ( !( indicator is IComplexIndicator complexIndicator ) )
            return DrawValues( data[ indicator ], ( IChartElement ) Line );
        IReadOnlyList<IIndicator> innerIndicators = complexIndicator.InnerIndicators;
        int count1 = innerIndicators.Count;
        int count2 = InnerElements.Count;
        int num1 = count2;
        int num2 = count1 - num1;
        if ( num2 > 0 )
        {
            IIndicatorColorProvider indicatorColorProvider = BaseChartIndicatorPainter<IIndicator>.GetColorProvider();
            for ( int index = 0 ; index < num2 ; ++index )
            {
                IIndicator indicator1 = innerIndicators[count2 + index];
                ChartLineElement element = new ChartLineElement();
                Color? color = indicator1.Color;
                ref Color? local = ref color;
                element.Color = local.HasValue ? local.GetValueOrDefault().ToWpf() : indicatorColorProvider.GetNextColor();
                element.Style = indicator1.Style;
                AddChildElement( ( IChartElement ) element );
            }
        }
        else if ( num2 < 0 )
        {
            int num3 = -num2;
            for ( int index = 0 ; index < num3 ; ++index )
                RemoveChildElement( InnerElements[ InnerElements.Count - 1 ] );
        }
        bool flag = false;
        int num4 = 0;
        foreach ( IIndicator key in ( IEnumerable<IIndicator> ) innerIndicators )
            flag |= DrawValues( data[ key ], InnerElements[ num4++ ] );
        return flag;
    }

    public override void Load( SettingsStorage storage )
    {
        base.Load( storage );
        PersistableHelper.Load( ( IPersistable ) Line, storage, "Line" );
    }

    public override void Save( SettingsStorage storage )
    {
        base.Save( storage );
        storage.SetValue<SettingsStorage>( "Line", PersistableHelper.Save( ( IPersistable ) Line ) );
    }
}
