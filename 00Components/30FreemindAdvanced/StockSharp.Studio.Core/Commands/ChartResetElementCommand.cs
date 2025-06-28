using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using System;
using StockSharp.BusinessEntities;
#nullable disable
namespace StockSharp.Studio.Core.Commands;

public class ChartResetElementCommand(IChartElement element, object tag = null) : BaseStudioCommand
{
    public IChartElement Element { get; } = element ?? throw new ArgumentNullException(nameof(element));

    public object Tag { get; } = tag;
}
