// Decompiled with JetBrains decompiler
// Type: StockSharp.Hydra.Tools.ImportTask
// Assembly: StockSharp.Hydra.Tools, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C423F466-676A-45BB-911E-C4AE5112AA81
// Assembly location: T:\00-StockSharp\Data\Plugins\StockSharp.Hydra.Tools.dll

using Ecng.Collections;
using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.Algo.Import;
using StockSharp.Hydra.Core;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Xaml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StockSharp.Hydra.Tools
{
    [DisplayNameLoc( "ImportAuto" )]
    [DescriptionLoc( "ImportAutoTask", false )]
    [Doc( "topics/HydraTasksImport.html" )]
    [XamlIcon( "Save.svg" )]
    [MessageAdapterCategory( MessageAdapterCategories.Tool )]
    internal class ImportTask : BaseHydraTask
    {
        private const string _sourceName = "ImportAuto";
        private ImportSettings _importSettings;

        public ImportTask()
        {
            this.ImportSettings = new ImportSettings();
            this.Interval = TimeSpan.FromDays( 1.0 );
        }

        [Display( Description = "Str2842", GroupName = "ImportAuto", Name = "Settings", Order = 0, ResourceType = typeof( LocalizedStrings ) )]
        [Editor( typeof( ImportSettingsEditor ), typeof( ImportSettingsEditor ) )]
        public ImportSettings ImportSettings
        {
            get
            {
                return this._importSettings;
            }
            set
            {
                ImportSettings importSettings = value;
                if ( importSettings == null )
                    throw new ArgumentNullException( nameof( value ) );
                this._importSettings = importSettings;
            }
        }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<SettingsStorage>( "ImportSettings", this.ImportSettings.Save() );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            SettingsStorage storage1 = storage.GetValue<object>( "ImportSettings", null ) as SettingsStorage;
            if ( storage1 == null )
                return;
            this.ImportSettings.Load( storage1 );
        }

        public override IEnumerable<StockSharp.Messages.DataType> SupportedDataTypes { get; } = ( new StockSharp.Messages.DataType[7] { Messages.DataType.Ticks, Messages.DataType.OrderLog, Messages.DataType.Transactions, Messages.DataType.PositionChanges, Messages.DataType.News, Messages.DataType.MarketDepth, Messages.DataType.Level1 } ).Concat<StockSharp.Messages.DataType>( Core.Extensions.GeneratedTimeFrames );

        protected override TimeSpan OnProcess()
        {
            ImportSettings settings = this.ImportSettings;
            string[ ] files = settings.GetFiles().ToArray<string>();
            if ( files.IsEmpty<string>() )
            {
                this.AddWarningLog( LocalizedStrings.Str3014 );
                return TimeSpan.MaxValue;
            }
            CsvImporter csvImporter = new CsvImporter( settings.DataType, settings.SelectedFields, ServicesRegistry.SecurityStorage, ServicesRegistry.ExchangeInfoProvider, this.Drive, this.StorageFormat );
            csvImporter.Parent = this;
            CsvImporter importer = csvImporter;
            settings.FillImporter( importer );
            HydraTaskSecurity allSec = this.GetAllSecurity();
            Do.Invariant( () =>
            {
                foreach ( string fileName in files )
                {
                    ValueTuple<int, DateTimeOffset?> valueTuple = importer.Import( fileName, null, () => !this.CanProcess() );
                    this.RaiseDataLoaded( allSec.Security, settings.DataType, valueTuple.Item2, valueTuple.Item1 );
                }
            } );
            return base.OnProcess();
        }
    }
}
