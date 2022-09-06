// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.RemoveLicenseCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using StockSharp.Licensing;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class RemoveLicenseCommand : BaseStudioCommand
    {
        public License License { get; }

        public RemoveLicenseCommand( License license )
        {
            License license1 = license;
            if ( license1 == null )
                throw new ArgumentNullException( nameof( license ) );
            this.License = license1;
        }
    }
}
