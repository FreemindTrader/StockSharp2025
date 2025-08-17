using Ecng.Common;
using Ecng.Logging;
using StockSharp.Configuration;
using StockSharp.Messages;
using StockSharp.Studio.WebApi;
using System;
using System.Collections.Generic;
using System.Security;

#nullable disable
namespace StockSharp.Studio.Core;

public class StudioMessageAdapterProvider(
  IEnumerable<IMessageAdapter> currentAdapters,
  Type transportAdapter) : InMemoryMessageAdapterProvider(currentAdapters, transportAdapter)
{
    public override IEnumerable<IMessageAdapter> CreateStockSharpAdapters(
      IdGenerator transactionIdGenerator,
      string login,
      SecureString password)
    {
        try
        {
            return transactionIdGenerator.CreateStockSharpAdapters(login, password);
        }
        catch (Exception ex)
        {
            LoggingHelper.LogError(ex, (string)null);
            return (IEnumerable<IMessageAdapter>)Array.Empty<IMessageAdapter>();
        }
    }
}
