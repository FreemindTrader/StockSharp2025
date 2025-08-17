using System;
using StockSharp.BusinessEntities;
using System.Collections.Generic;
#nullable disable
namespace StockSharp.Studio.Core.Commands;

public class LookupSecuritiesCommand(Messages.SecurityLookupMessage criteria) : BaseStudioCommand
{
    public Messages.SecurityLookupMessage Criteria { get; } = criteria ?? throw new ArgumentNullException(nameof(criteria));
}
