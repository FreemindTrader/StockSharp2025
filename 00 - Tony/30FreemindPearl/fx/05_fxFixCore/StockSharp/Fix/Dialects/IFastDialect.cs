// Decompiled with JetBrains decompiler
// Type: StockSharp.Fix.Dialects.IFastDialect
// Assembly: StockSharp.Fix.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B9148E39-A5BB-4657-14B1-EA8DED27B1C2
// Assembly location: A:\StockSharpBin\Terminal\StockSharp.Fix.Core.dll

using Ecng.Common;
using Ecng.Net;
using Ecng.Serialization;
using StockSharp.Logging;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security;

namespace StockSharp.Fix.Dialects
{
    /// <summary>
    /// The interface describing the dialect of the FAST protocol.
    /// </summary>
    public interface IFastDialect :
      ICloneable<IMessageChannel>,
      IMessageAdapter,
      IMessageChannel,
      IDisposable,
      ICloneable,
      IPersistable,
      ILogReceiver,
      ILogSource
    {
        /// <summary>Login.</summary>
        string Login { get; set; }

        /// <summary>Password.</summary>
        SecureString Password { get; set; }

        /// <summary>Test dump files.</summary>
        /// <param name="dumpFiles">Dump files.</param>
        void Dump(
          IDictionary<MulticastSourceAddress, IEnumerable<Stream>> dumpFiles);

        /// <summary>Load settings from specified file.</summary>
        /// <param name="settingsFile">Settings file.</param>
        void LoadSettingsFromFile(string settingsFile);

        /// <summary>Feeds.</summary>
        IList<IFastNetworkFeed> Feeds { get; }
    }
}
