using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Data;
using Ecng.Serialization;
using Ecng.Xaml;
using Ecng.Xaml.Database;
using StockSharp.Algo;
using StockSharp.Algo.Export;
using StockSharp.Algo.Storages;
using StockSharp.Hydra.Core;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Xaml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace StockSharp.Hydra.Tools
{
    [DisplayNameLoc( "Str3754" )]
    [DescriptionLoc( "Str3767", false )]
    [Doc( "topics/HydraTasksExport.html" )]
    [XamlIcon( "Load.svg" )]
    [MessageAdapterCategory( MessageAdapterCategories.Tool )]
    internal class ExportTask : BaseHydraTask
    {
        private const string _sourceName = "Str3754";
        private DateTime _startFrom;

        public ExportTask()
        {
            ExportType          = ExportTypes.Txt;
            Offset              = 1;
            ExportFolder        = string.Empty;
            Interval            = TimeSpan.FromDays( 1.0 );
            StartFrom           = DateTime.Today;
            Connection          = null;
            BatchSize           = 50;
            CheckUnique         = true;
            TemplateTxtRegistry = new TemplateTxtRegistry();
            Header              = string.Empty;
        }

        [Display( Description = "Str3756", GroupName = "Str3754", Name = "Type", Order = 0, ResourceType = typeof( LocalizedStrings ) )]
        public ExportTypes ExportType { get; set; }

        [Display( Description = "Str3757", GroupName = "Str3754", Name = "Str2282", Order = 1, ResourceType = typeof( LocalizedStrings ) )]
        public DateTime StartFrom
        {
            get
            {
                return _startFrom;
            }
            set
            {
                _startFrom = value;
                NotifyPropertyChanged( nameof( StartFrom ) );
            }
        }

        [Display( Description = "Str3760", GroupName = "Str3754", Name = "Str2284", Order = 2, ResourceType = typeof( LocalizedStrings ) )]
        public int Offset { get; set; }

        [Display( Description = "Str3759", GroupName = "Str3754", Name = "Str3758", Order = 3, ResourceType = typeof( LocalizedStrings ) )]
        [Editor( typeof( FolderBrowserEditor ), typeof( FolderBrowserEditor ) )]
        public string ExportFolder { get; set; }

        [Display( Description = "Str2240", GroupName = "Str3754", Name = "Str2239", Order = 4, ResourceType = typeof( LocalizedStrings ) )]
        public StorageFormats ExportFormat { get; set; }

        [Display( Description = "SplitType", GroupName = "Str3754", Name = "Split", Order = 5, ResourceType = typeof( LocalizedStrings ) )]
        public ExportTask.SplitTypes? SplitType { get; set; }

        [Display( Description = "Str3762", GroupName = "Database", Name = "Str174", Order = 20, ResourceType = typeof( LocalizedStrings ) )]
        [Editor( typeof( DatabaseConnectionEditor ), typeof( DatabaseConnectionEditor ) )]
        public DatabaseConnectionPair Connection { get; set; }

        [Display( Description = "Str3764", GroupName = "Database", Name = "Str3763", Order = 21, ResourceType = typeof( LocalizedStrings ) )]
        public int BatchSize { get; set; }

        [Display( Description = "Str3766", GroupName = "Database", Name = "Str3765", Order = 22, ResourceType = typeof( LocalizedStrings ) )]
        public bool CheckUnique { get; set; }

        [Display( Description = "TemplateDot", GroupName = "CSV", Name = "Template", Order = 10, ResourceType = typeof( LocalizedStrings ) )]
        public TemplateTxtRegistry TemplateTxtRegistry { get; }

        [Display( Description = "CsvHeaderDot", GroupName = "CSV", Name = "Str215", Order = 11, ResourceType = typeof( LocalizedStrings ) )]
        public string Header { get; set; }

        [Display( Description = "FileNameFormatDesc", GroupName = "CSV", Name = "FileNameFormat", Order = 12, ResourceType = typeof( LocalizedStrings ) )]
        public string FileFormat { get; set; }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<ExportTypes>( "ExportType", ExportType );
            storage.SetValue<DateTime>( "StartFrom", StartFrom );
            storage.SetValue<string>( "ExportFolder", ExportFolder );
            storage.SetValue<StorageFormats>( "ExportFormat", ExportFormat );
            storage.SetValue<int>( "Offset", Offset );
            storage.SetValue<ExportTask.SplitTypes?>( "SplitType", SplitType );
            if ( Connection != null )
            {
                SettingsStorage settingsStorage1 = storage;
                SettingsStorage settingsStorage2 = new SettingsStorage();
                settingsStorage2.Add( "Provider", Connection.Provider.GetType().AssemblyQualifiedName );
                settingsStorage2.Add( "ConnectionString", Connection.ConnectionString );
                settingsStorage1.SetValue<SettingsStorage>( "Connection", settingsStorage2 );
            }
            storage.SetValue<int>( "BatchSize", BatchSize );
            storage.SetValue<bool>( "CheckUnique", CheckUnique );
            storage.SetValue<SettingsStorage>( "TemplateTxtRegistry", TemplateTxtRegistry.Save() );
            storage.SetValue<string>( "Header", Header );
            storage.SetValue<string>( "FileFormat", FileFormat );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );

            throw new NotImplementedException( );

            //this.ExportType = storage.GetValue<ExportTypes>( "ExportType", this.ExportType );
            //this.StartFrom = storage.GetValue<DateTime>( "StartFrom", this.StartFrom );
            //this.ExportFolder = storage.GetValue<string>( "ExportFolder", this.ExportFolder );
            //this.ExportFormat = storage.GetValue<StorageFormats>( "ExportFormat", this.ExportFormat );
            //this.Offset = storage.GetValue<int>( "Offset", this.Offset );
            //this.SplitType = storage.GetValue<ExportTask.SplitTypes?>( "SplitType", this.SplitType );
            //if ( storage.Contains( "Connection" ) )
            //{
            //    SettingsStorage settingsStorage = storage.GetValue<SettingsStorage>( "Connection", null );
            //    this.Connection = DatabaseHelper.Cache.GetConnection( settingsStorage.GetValue<Type>( "Provider", null ), settingsStorage.GetValue<string>( "ConnectionString", null ) );
            //}
            //this.BatchSize = storage.GetValue<int>( "BatchSize", this.BatchSize );
            //this.CheckUnique = storage.GetValue<bool>( "CheckUnique", this.CheckUnique );
            //if ( storage.Contains( "TemplateTxtRegistry" ) )
            //    this.TemplateTxtRegistry.ForceLoad<TemplateTxtRegistry>( storage.GetValue<SettingsStorage>( "TemplateTxtRegistry", null ) );
            //this.Header = storage.GetValue<string>( "Header", this.Header );
            //this.FileFormat = storage.GetValue<string>( "FileFormat", this.FileFormat );
        }

        public override IEnumerable<StockSharp.Messages.DataType> SupportedDataTypes { get; } = ( new StockSharp.Messages.DataType[7] { Messages.DataType.Ticks, Messages.DataType.OrderLog, Messages.DataType.Transactions, Messages.DataType.PositionChanges, Messages.DataType.News, Messages.DataType.MarketDepth, Messages.DataType.Level1 } ).Concat<StockSharp.Messages.DataType>( Core.Extensions.GeneratedTimeFrames );

        protected override TimeSpan OnProcess()
        {
            if ( ExportType == ExportTypes.Sql && Connection == null )
            {
                this.AddErrorLog( LocalizedStrings.Str3768 );
                return TimeSpan.MaxValue;
            }

            Func<int, bool> isCancelled = count => !CanProcess();

            HydraTaskSecurity allSecurity = this.GetAllSecurity();
            StockSharp.Messages.DataType[ ] array = ( allSecurity == null ? Enumerable.Empty<StockSharp.Messages.DataType>() : SupportedDataTypes.Intersect<StockSharp.Messages.DataType>( allSecurity.GetDataTypes() ) ).ToArray<StockSharp.Messages.DataType>();
            this.AddInfoLog( LocalizedStrings.Str2306Params.Put( StartFrom ) );
            IStorageRegistry storageRegistry = ServicesRegistry.StorageRegistry;
            bool flag = false;
            DateTime dateTime1 = StartFrom.UtcKind();
            DateTime dateTime2 = DateTime.Today.UtcKind();
            foreach ( HydraTaskSecurity workingSecurity in GetWorkingSecurities() )
            {
                HydraTaskSecurity security = workingSecurity;
                flag = true;
                if ( CanProcess() )
                {
                    foreach ( StockSharp.Messages.DataType dataType1 in allSecurity == null ? security.GetDataTypes() : array )
                    {
                        StockSharp.Messages.DataType dataType = dataType1;
                        if ( CanProcess() )
                        {
                            Type messageType = dataType.MessageType;
                            object obj = dataType.Arg;
                            this.AddInfoLog( LocalizedStrings.Str3769Params.Put( security.Security.Id, messageType.Name, ExportType ) );
                            IMarketDataStorage fromStorage = StorageRegistry.GetStorage( security.Security, dataType, Drive, StorageFormat );
                            DateTime? fromDate = fromStorage.GetFromDate();
                            DateTime? toDate = fromStorage.GetToDate();
                            if ( !fromDate.HasValue || !toDate.HasValue )
                            {
                                this.AddInfoLog( LocalizedStrings.Str3770 );
                            }
                            else
                            {
                                ref DateTime? local1 = ref fromDate;
                                DateTime? nullable1 = security.GetBeginDate( dataType );
                                DateTime dateTime3 = ( nullable1 ?? dateTime1 ).Max( dateTime1 ).Max( fromDate.Value );
                                local1 = new DateTime?( dateTime3 );
                                ref DateTime? local2 = ref toDate;
                                nullable1 = security.GetEndDate( dataType );
                                DateTime dateTime4 = ( nullable1 ?? dateTime2 - TimeSpan.FromDays( Offset ) ).Min( toDate.Value ).EndOfDay();
                                local2 = new DateTime?( dateTime4 );
                                nullable1 = fromDate;
                                DateTime? nullable2 = toDate;
                                if ( ( nullable1.HasValue & nullable2.HasValue ? ( nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() ? 1 : 0 ) : 0 ) == 0 )
                                {
                                    if ( ExportType == ExportTypes.Sql )
                                    {
                                        // ISSUE: method pointer
                                        Export( ( BaseExporter )new DatabaseExporter( security.Security.PriceStep, security.Security.VolumeStep, dataType, isCancelled, Connection )
                                        {
                                            BatchSize = BatchSize,
                                            CheckUnique = CheckUnique
                                        }, fromDate.Value, toDate.Value );
                                    }
                                    else
                                    {
                                        string str = ExportFolder;
                                        if ( str.IsEmpty() )
                                            str = ServicesRegistry.DriveCache.DefaultDrive.Path;
                                        StorageFormats exportFormat = ExportFormat;
                                        List<Range<DateTime>> rangeList = new List<Range<DateTime>>();
                                        DateTime dateTime5;
                                        for ( DateTime min = fromDate.Value; min < toDate.Value; min = dateTime5.AddTicks( 1L ) )
                                        {
                                            ExportTask.SplitTypes? splitType = SplitType;
                                            if ( splitType.HasValue )
                                            {
                                                switch ( splitType.GetValueOrDefault() )
                                                {
                                                    case SplitTypes.ByDays:
                                                        dateTime5 = min;
                                                        break;
                                                    case SplitTypes.ByMonths:
                                                        dateTime5 = new DateTime( min.Year, min.Month, DateTime.DaysInMonth( min.Year, min.Month ) );
                                                        break;
                                                    case SplitTypes.ByYears:
                                                        dateTime5 = new DateTime( min.Year, 12, DateTime.DaysInMonth( min.Year, 12 ) );
                                                        break;
                                                    default:
                                                        throw new ArgumentOutOfRangeException( SplitType.To<string>() );
                                                }
                                            }
                                            else
                                                dateTime5 = toDate.Value;
                                            dateTime5 = dateTime5.EndOfDay().UtcKind();
                                            rangeList.Add( new Range<DateTime>( min, dateTime5 ) );
                                        }

                                        foreach ( Range<DateTime> range in rangeList )
                                        {
                                            string fileName = Path.Combine( str, security.Security.GetFileName( null, FileFormat, dataType, new DateTime?( range.Min ), new DateTime?( range.Max ), ExportType, exportFormat ) );
                                            switch ( ExportType )
                                            {
                                                case ExportTypes.Excel:
                                                    // ISSUE: method pointer
                                                    Export( ( BaseExporter )new ExcelExporter( ServicesRegistry.ExcelProvider, dataType, isCancelled, fileName, () => this.AddErrorLog( LocalizedStrings.Str3771 ) ), range.Min, range.Max );
                                                    continue;
                                                case ExportTypes.Xml:
                                                    // ISSUE: method pointer
                                                    Export( ( BaseExporter )new XmlExporter( dataType, isCancelled, fileName ), range.Min, range.Max );
                                                    continue;
                                                case ExportTypes.Txt:
                                                    // ISSUE: method pointer
                                                    Export( ( BaseExporter )new TextExporter( dataType, isCancelled, fileName, GetTxtTemplate( dataType ), Header ), range.Min, range.Max );
                                                    continue;
                                                case ExportTypes.Json:
                                                    // ISSUE: method pointer
                                                    Export( ( BaseExporter )new JsonExporter( dataType, isCancelled, fileName ), range.Min, range.Max );
                                                    continue;
                                                case ExportTypes.StockSharp:
                                                    // ISSUE: method pointer
                                                    Export( ( BaseExporter )new StockSharpExporter( dataType, isCancelled, storageRegistry, ServicesRegistry.DriveCache.GetDrive( str ), exportFormat ), range.Min, range.Max );
                                                    continue;
                                                default:
                                                    throw new ArgumentOutOfRangeException( ExportType.To<string>() );
                                            }
                                        }
                                    }
                                }
                            }

                            void Export( BaseExporter exporter, DateTime f, DateTime t )
                            {
                                try
                                {
                                    this.AddInfoLog( LocalizedStrings.Str3772Params.Put( security.Security.Id, messageType.Name, ExportType, f, t ) );
                                    IEnumerable values = ( IEnumerable )typeof( StorageHelper ).GetMethod( "Load" ).MakeGenericMethod( fromStorage.DataType.MessageType ).Invoke( null, new object[3] { fromStorage, ( DateTimeOffset )f, ( DateTimeOffset )t } );
                                    ValueTuple<int, DateTimeOffset?> valueTuple = exporter.Export( values );
                                    int count = valueTuple.Item1;
                                    DateTimeOffset? time = valueTuple.Item2;
                                    RaiseDataLoaded( security.Security, dataType, time, count );
                                }
                                catch ( Exception ex )
                                {
                                    HandleError( ex );
                                }
                            }
                        }
                        else
                            break;
                    }
                }
                else
                    break;
            }
            if ( !flag )
            {
                this.AddWarningLog( LocalizedStrings.Str2292 );
                return TimeSpan.MaxValue;
            }
            if ( CanProcess() )
            {
                this.AddInfoLog( LocalizedStrings.Str2300 );
                StartFrom = DateTime.Today - TimeSpan.FromDays( Offset );
                this.SaveSettings();
            }
            return base.OnProcess();
        }

        private string GetTxtTemplate( StockSharp.Messages.DataType dataType )
        {
            if ( dataType == null )
                throw new ArgumentNullException( nameof( dataType ) );
            TemplateTxtRegistry templateTxtRegistry = TemplateTxtRegistry;
            if ( dataType == Messages.DataType.Securities )
                return templateTxtRegistry.TemplateTxtSecurity;
            if ( dataType == Messages.DataType.News )
                return templateTxtRegistry.TemplateTxtNews;
            if ( dataType == Messages.DataType.Board )
                return templateTxtRegistry.TemplateTxtBoard;
            if ( dataType == Messages.DataType.BoardState )
                return templateTxtRegistry.TemplateTxtBoardState;
            if ( dataType.IsCandles )
                return templateTxtRegistry.TemplateTxtCandle;
            if ( dataType == Messages.DataType.Level1 )
                return templateTxtRegistry.TemplateTxtLevel1;
            if ( dataType == Messages.DataType.MarketDepth )
                return templateTxtRegistry.TemplateTxtDepth;
            if ( dataType == Messages.DataType.Ticks )
                return templateTxtRegistry.TemplateTxtTick;
            if ( dataType == Messages.DataType.Transactions )
                return templateTxtRegistry.TemplateTxtTransaction;
            if ( dataType == Messages.DataType.OrderLog )
                return templateTxtRegistry.TemplateTxtOrderLog;
            throw new ArgumentOutOfRangeException( nameof( dataType ), dataType, LocalizedStrings.Str721 );
        }

        public enum SplitTypes
        {
            [Display( Name = "ByDays", ResourceType = typeof( LocalizedStrings ) )] ByDays,
            [Display( Name = "ByMonths", ResourceType = typeof( LocalizedStrings ) )] ByMonths,
            [Display( Name = "ByYears", ResourceType = typeof( LocalizedStrings ) )] ByYears,
        }
    }
}
