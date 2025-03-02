// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.OpenWindowCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using System;

namespace StockSharp.Studio.Core.Commands
{
    public class OpenWindowCommand : BaseStudioCommand
    {
        public OpenWindowCommand( string id, Type ctrlType, bool isToolWindow )
        {
            if ( ctrlType == null )
                throw new ArgumentNullException( nameof( ctrlType ) );
            Id = id;
            CtrlType = ctrlType;
            IsToolWindow = isToolWindow;
        }

        public string Id { get; }

        public Type CtrlType { get; }

        public bool IsToolWindow { get; }

        public IStudioControl Result { get; set; }
    }
}
