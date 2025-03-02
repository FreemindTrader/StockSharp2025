// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.PositionEditCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using StockSharp.BusinessEntities;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class PositionEditCommand : BaseStudioCommand
    {
        public PositionEditCommand( Position position )
        {
            Position position1 = position;
            if ( position1 == null )
                throw new ArgumentNullException( nameof( position ) );
            this.Position = position1;
            
        }

        public override StudioCommandDirections PossibleDirection
        {
            get
            {
                return StudioCommandDirections.Top;
            }
        }

        public Position Position { get; }
    }
}
