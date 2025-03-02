// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.RemoveLogListenerCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using StockSharp.Logging;

namespace StockSharp.Studio.Core.Commands
{
    public class RemoveLogListenerCommand : BaseStudioCommand
    {
        public ILogListener Listener { get; }

        public RemoveLogListenerCommand( ILogListener info )
        {
            Listener = info;
        }
    }
}
