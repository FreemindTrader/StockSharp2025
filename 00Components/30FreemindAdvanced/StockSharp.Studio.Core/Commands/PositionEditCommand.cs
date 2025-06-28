using System;
using StockSharp.BusinessEntities;
using System.Collections.Generic;
#nullable disable
namespace StockSharp.Studio.Core.Commands;

public class PositionEditCommand(Position position) : BaseStudioCommand
{
    public override StudioCommandDirections PossibleDirection => StudioCommandDirections.Top;

    public Position Position { get; } = position ?? throw new ArgumentNullException(nameof(position));
}
