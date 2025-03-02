// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.StudioMessageAdapterProvider
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using Ecng.Common;
using Ecng.Logging;
using StockSharp.Configuration;
using StockSharp.Messages;
using StockSharp.Studio.WebApi;
using System;
using System.Collections.Generic;
using System.Security;

namespace StockSharp.Studio.Core
{
    public class StudioMessageAdapterProvider : InMemoryMessageAdapterProvider
    {
        public StudioMessageAdapterProvider(
          IEnumerable<IMessageAdapter> currentAdapters,
          Type transportAdapter )
          : base( currentAdapters, transportAdapter )
        {
        }

        public override IEnumerable<IMessageAdapter> CreateStockSharpAdapters(
          IdGenerator transactionIdGenerator,
          string login,
          SecureString password )
        {
            try
            {
                return transactionIdGenerator.CreateStockSharpAdapters( login, password );
            }
            catch ( Exception ex )
            {
                LoggingHelper.LogError( ex, ( string ) null );
                return ( IEnumerable<IMessageAdapter> ) Array.Empty<IMessageAdapter>();
            }
        }
    }
}
