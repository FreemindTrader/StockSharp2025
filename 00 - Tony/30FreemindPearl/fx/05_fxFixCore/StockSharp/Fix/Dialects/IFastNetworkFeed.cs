// Decompiled with JetBrains decompiler
// Type: StockSharp.Fix.Dialects.IFastNetworkFeed
// Assembly: StockSharp.Fix.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B9148E39-A5BB-4657-14B1-EA8DED27B1C2
// Assembly location: A:\StockSharpBin\Terminal\StockSharp.Fix.Core.dll

using Ecng.Net;
using System.Collections.Generic;
using System.IO;

namespace StockSharp.Fix.Dialects
{
    /// <summary>FAST feed.</summary>
    public interface IFastNetworkFeed
    {
        /// <summary>Name.</summary>
        string Name { get; }

        /// <summary>Network configuration group.</summary>
        FastFeedGroup FeedGroup { get; }

        /// <summary>Close.</summary>
        void Close();

        /// <summary>Test dump files.</summary>
        /// <param name="dumpFiles">Dump files.</param>
        void Dump(
          IDictionary<MulticastSourceAddress, IEnumerable<Stream>> dumpFiles);

        /// <summary>Make gap in incremental messages for test purpose.</summary>
        /// <param name="gapSize">Gap size (in messages).</param>
        void MakeGap(int gapSize);
    }
}
