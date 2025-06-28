using System;
using StockSharp.BusinessEntities;
using System.Collections.Generic;

#nullable disable
namespace StockSharp.Studio.Core.Commands;

public class RevertPositionCommand : BaseStudioCommand
{
    public Position Position { get; }

    public Security Security { get; }

    public RevertPositionCommand(Security security) => this.Security = security;

    public RevertPositionCommand(Position position)
    {
        this.Position = position ?? throw new ArgumentNullException(nameof(position));
    }
}
