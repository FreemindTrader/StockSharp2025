using System;
using StockSharp.BusinessEntities;
using System.Collections.Generic;
#nullable disable
namespace StockSharp.Studio.Core.Commands;

public class LoggedInCommand(Web.DomainModel.Client profile) : BaseStudioCommand
{
    public Web.DomainModel.Client Profile { get; } = profile ?? throw new ArgumentNullException(nameof(profile));
}
