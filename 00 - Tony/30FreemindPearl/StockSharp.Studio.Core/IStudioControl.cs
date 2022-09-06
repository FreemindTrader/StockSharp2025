// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.IStudioControl
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using Ecng.Serialization;
using StockSharp.Studio.Core.Commands;
using System;

namespace StockSharp.Studio.Core
{
    public interface IStudioControl : IPersistable, IDisposable
    {
        string Title { get; set; }

        Uri Icon { get; }

        string Key { get; set; }

        bool SaveWithLayout { get; }

        bool IsTitleEditable { get; }

        void FirstTimeInit();

        void SendCommand( IStudioCommand command );
    }
}
