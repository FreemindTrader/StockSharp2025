// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.RevertPositionCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using StockSharp.BusinessEntities;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class RevertPositionCommand : BaseStudioCommand
    {
        public Position Position { get; }

        public Security Security { get; }

        public RevertPositionCommand( Security security )
        {
            this.Security = security;
        }

        public RevertPositionCommand( Position position )
        {
            Position position1 = position;
            if ( position1 == null )
                throw new ArgumentNullException( nameof( position ) );
            this.Position = position1;
        }
    }
}
