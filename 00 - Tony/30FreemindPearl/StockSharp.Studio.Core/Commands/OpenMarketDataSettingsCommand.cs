// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Commands.OpenMarketDataSettingsCommand
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using StockSharp.Algo.Storages;
using System;

namespace StockSharp.Studio.Core.Commands
{
    public class OpenMarketDataSettingsCommand : BaseStudioCommand
    {
        public IMarketDataDrive Drive { get; }

        public StorageFormats? Format { get; }

        public OpenMarketDataSettingsCommand( IMarketDataDrive drive, StorageFormats? format = null )
        {
            IMarketDataDrive marketDataDrive = drive;
            if ( marketDataDrive == null )
                throw new ArgumentNullException( nameof( drive ) );
            this.Drive = marketDataDrive;
            this.Format = format;
        }
    }
}
