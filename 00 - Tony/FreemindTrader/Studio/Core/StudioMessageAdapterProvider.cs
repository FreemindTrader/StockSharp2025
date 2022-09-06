// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.StudioMessageAdapterProvider
// Assembly: Terminal, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C7FF2C7B-469F-4E71-BC76-9E79C0E574D9
// Assembly location: T:\00-StockSharp\Terminal\Terminal.dll

using Ecng.Common;
using StockSharp.Configuration;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Studio.Community;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace StockSharp.Studio.Core
{
    public class StudioMessageAdapterProvider : FullInMemoryMessageAdapterProvider
    {
        public StudioMessageAdapterProvider( IEnumerable<IMessageAdapter> currentAdapters )
          : base( currentAdapters )
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
                ex.LogError( null );
                return Enumerable.Empty<IMessageAdapter>();
            }
        }
    }
}
