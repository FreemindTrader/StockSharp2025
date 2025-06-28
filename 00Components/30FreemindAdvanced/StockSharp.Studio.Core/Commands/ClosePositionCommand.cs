using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using System;
using StockSharp.BusinessEntities;
#nullable disable
namespace StockSharp.Studio.Core.Commands;

public class ClosePositionCommand : BaseStudioCommand
{
    public Position Position { get; }

    public Security Security { get; }

    public ClosePositionCommand(Security sec) => this.Security = sec;

    public ClosePositionCommand(Position position)
    {
        this.Position = position ?? throw new ArgumentNullException(nameof(position));
    }
}
