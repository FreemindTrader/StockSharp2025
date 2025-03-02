// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.CreateSecurityCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using StockSharp.BusinessEntities;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class CreateSecurityCommand : BaseStudioCommand
    {
        public CreateSecurityCommand( Type securityType )
        {
            Type type = securityType;
            if ( ( object ) type == null )
                throw new ArgumentNullException( nameof( securityType ) );
            this.SecurityType = type;            
        }

        public override StudioCommandDirections PossibleDirection
        {
            get
            {
                return StudioCommandDirections.Top;
            }
        }

        public Type SecurityType { get; }

        public Security Security { get; set; }
    }
}
