using StockSharp.Algo.Indicators;
using StockSharp.Charting;
using System;
using StockSharp.BusinessEntities;
#nullable disable
namespace StockSharp.Studio.Core.Commands;

public class CreateSecurityCommand(Type securityType) : BaseStudioCommand
{
    public override StudioCommandDirections PossibleDirection => StudioCommandDirections.Top;

    public Type SecurityType { get; } = securityType ?? throw new ArgumentNullException(nameof(securityType));

    public Security Security { get; set; }
}
