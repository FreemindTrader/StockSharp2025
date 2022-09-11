// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.PositionEditCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using StockSharp.BusinessEntities;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class PositionEditCommand : BaseStudioCommand
    {
        public override StudioCommandDirections PossibleDirection
        {
            get
            {
                return StudioCommandDirections.Top;
            }
        }

        public Position Position { get; }

        public PositionEditCommand( Position position )
        {
            Position position1 = position;
            if ( position1 == null )
                throw new ArgumentNullException( nameof( position ) );
            Position = position1;
        }
    }
}
