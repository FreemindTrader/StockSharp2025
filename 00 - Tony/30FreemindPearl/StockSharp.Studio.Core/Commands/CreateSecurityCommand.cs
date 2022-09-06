// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.CreateSecurityCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using StockSharp.BusinessEntities;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class CreateSecurityCommand : BaseStudioCommand
    {
        public override StudioCommandDirections PossibleDirection
        {
            get
            {
                return StudioCommandDirections.Top;
            }
        }

        public CreateSecurityCommand( Type securityType )
        {
            Type type = securityType;
            if ( ( object )type == null )
                throw new ArgumentNullException( nameof( securityType ) );
            this.SecurityType = type;
        }

        public Type SecurityType { get; }

        public Security Security { get; set; }
    }
}
