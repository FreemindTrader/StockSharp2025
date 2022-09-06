// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.CloseWindowCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using System;

namespace StockSharp.Studio.Core.Commands
{
    public class CloseWindowCommand : BaseStudioCommand
    {
        public string Id { get; }

        public Type CtrlType { get; }

        public CloseWindowCommand( string id, Type ctrlType )
        {
            if ( ctrlType == ( Type )null )
                throw new ArgumentNullException( nameof( ctrlType ) );
            this.Id = id;
            this.CtrlType = ctrlType;
        }
    }
}
