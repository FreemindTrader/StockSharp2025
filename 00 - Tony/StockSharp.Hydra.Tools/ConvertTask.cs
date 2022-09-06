// Decompiled with JetBrains decompiler
// Type: StockSharp.Hydra.Tools.ConvertTask
// Assembly: StockSharp.Hydra.Tools, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C423F466-676A-45BB-911E-C4AE5112AA81
// Assembly location: T:\00-StockSharp\Data\Plugins\StockSharp.Hydra.Tools.dll

using Ecng.Common;
using Ecng.ComponentModel;
using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.Algo.Candles;
using StockSharp.Algo.Candles.Compression;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Hydra.Core;
using StockSharp.Localization;
using StockSharp.Logging;
using StockSharp.Messages;
using StockSharp.Studio.Controls.Editors;
using StockSharp.Xaml;
using StockSharp.Xaml.PropertyGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StockSharp.Hydra.Tools
{
    [DisplayNameLoc( "Str3131" )]
    [DescriptionLoc( "Str3785", false )]
    [Doc( "topics/HydraTasksConverter.html" )]
    [XamlIcon( "Sort.svg" )]
    [MessageAdapterCategory( MessageAdapterCategories.Tool )]
    internal class ConvertTask : BaseHydraTask
    {
        private const string _sourceName = "Str3131";

        public ConvertTask()
        {
            Offset                   = 1;
            StartFrom                = DateTime.Today.Subtract( TimeSpan.FromDays( 30.0 ) );
            ConvertFrom              = Messages.DataType.Ticks;
            Interval                 = TimeSpan.FromDays( 1.0 );
            MarketDepthInterval      = TimeSpan.FromMilliseconds( 10.0 );
            MarketDepthMaxDepth      = 50;
            MarketDepthBuilder       = typeof( OrderLogMarketDepthBuilder );
            DestinationDrive         = null;
            DestinationStorageFormat = StorageFormats.Binary;
        }

        [Display( Description = "Str213", GroupName = "Str3131", Name = "XamlStr193", Order = 0, ResourceType = typeof( LocalizedStrings ) )]
        [ItemsSource( typeof( ConvertTask.ConvertFromItemsSource ) )]
        public StockSharp.Messages.DataType ConvertFrom { get; set; }

        [Display( Description = "Str2240", GroupName = "Str3131", Name = "Str2239", Order = 1, ResourceType = typeof( LocalizedStrings ) )]
        public StorageFormats DestinationStorageFormat { get; set; }

        [Display( Description = "Str3779", GroupName = "Str3131", Name = "Str2282", Order = 2, ResourceType = typeof( LocalizedStrings ) )]
        public DateTime StartFrom { get; set; }

        [Display( Description = "Str3778", GroupName = "Str3131", Name = "Str2284", Order = 3, ResourceType = typeof( LocalizedStrings ) )]
        public int Offset { get; set; }

        [Display( Description = "Str3784", GroupName = "Str3131", Name = "Str3783", Order = 4, ResourceType = typeof( LocalizedStrings ) )]
        [Editor( typeof( DriveComboBoxEditor ), typeof( DriveComboBoxEditor ) )]
        public IMarketDataDrive DestinationDrive { get; set; }

        [Display( Description = "Str3781", GroupName = "MarketDepths", Name = "Str175", Order = 0, ResourceType = typeof( LocalizedStrings ) )]
        [TimeSpanEditor( Mask = TimeSpanEditorMask.Seconds | TimeSpanEditorMask.Milliseconds | TimeSpanEditorMask.Microseconds )]
        public TimeSpan MarketDepthInterval { get; set; }

        [Display( Description = "Str3782", GroupName = "MarketDepths", Name = "Str1660", Order = 1, ResourceType = typeof( LocalizedStrings ) )]
        public int MarketDepthMaxDepth { get; set; }

        [Display( Description = "OrderLogBuilderDot", GroupName = "MarketDepths", Name = "OrderLog", Order = 2, ResourceType = typeof( LocalizedStrings ) )]
        [Editor( typeof( OrderLogBuilderComboEditor ), typeof( OrderLogBuilderComboEditor ) )]
        public Type MarketDepthBuilder { get; set; }

        public override void Save( SettingsStorage storage )
        {
            base.Save( storage );
            storage.SetValue<SettingsStorage>( "ConvertFrom", ConvertFrom.Save() );
            storage.SetValue<StorageFormats>( "DestinationStorageFormat", DestinationStorageFormat );
            storage.SetValue<DateTime>( "StartFrom", StartFrom );
            storage.SetValue<int>( "Offset", Offset );
            if ( DestinationDrive != null )
                storage.SetValue<string>( "DestinationDrive", DestinationDrive.Path );
            storage.SetValue<TimeSpan>( "MarketDepthInterval", MarketDepthInterval );
            storage.SetValue<int>( "MarketDepthMaxDepth", MarketDepthMaxDepth );
            SettingsStorage settingsStorage = storage;
            Type marketDepthBuilder = MarketDepthBuilder;
            string str = ( object )marketDepthBuilder != null ? marketDepthBuilder.GetTypeName( false ) : null;
            settingsStorage.SetValue<string>( "MarketDepthBuilder", str );
        }

        public override void Load( SettingsStorage storage )
        {
            base.Load( storage );
            ConvertFrom = !storage.ContainsKey( "ConvertFrom" ) ? Messages.DataType.Ticks : storage.GetValue<SettingsStorage>( "ConvertFrom", null ).Load<StockSharp.Messages.DataType>();
            DestinationStorageFormat = storage.GetValue<StorageFormats>( "DestinationStorageFormat", DestinationStorageFormat );
            StartFrom = storage.GetValue<DateTime>( "StartFrom", StartFrom );
            Offset = storage.GetValue<int>( "Offset", Offset );
            if ( storage.Contains( "DestinationDrive" ) )
                DestinationDrive = ServicesRegistry.DriveCache.GetDrive( storage.GetValue<string>( "DestinationDrive", null ) );
            MarketDepthInterval = storage.GetValue<TimeSpan>( "MarketDepthInterval", MarketDepthInterval );
            MarketDepthMaxDepth = storage.GetValue<int>( "MarketDepthMaxDepth", MarketDepthMaxDepth );
            Type marketDepthBuilder = storage.GetValue<Type>( "MarketDepthBuilder", null );
            if ( ( object )marketDepthBuilder == null )
                marketDepthBuilder = MarketDepthBuilder;
            MarketDepthBuilder = marketDepthBuilder;
        }

        public override IEnumerable<StockSharp.Messages.DataType> SupportedDataTypes { get; } = ( new StockSharp.Messages.DataType[4] { Messages.DataType.Ticks, Messages.DataType.OrderLog, Messages.DataType.MarketDepth, Messages.DataType.Level1 } ).Concat<StockSharp.Messages.DataType>( Core.Extensions.GeneratedTimeFrames );

        protected override TimeSpan OnProcess()
        {
            bool flag = false;
            this.AddInfoLog( LocalizedStrings.Str2306Params.Put( StartFrom ) );
            DateTime dateTime1 = StartFrom.UtcKind();
            DateTime dateTime2 = DateTime.Today.UtcKind();
            StockSharp.Messages.DataType convertFrom = ConvertFrom;
            foreach ( HydraTaskSecurity workingSecurity in GetWorkingSecurities() )
            {
                Security security = workingSecurity.Security;
                flag = true;
                if ( CanProcess() )
                {
                    SecurityId securityId = security.ToSecurityId( null, true, false );
                    foreach ( StockSharp.Messages.DataType dataType in workingSecurity.GetDataTypes() )
                    {
                        if ( convertFrom == dataType )
                        {
                            this.AddWarningLog( "{0}->{1}", convertFrom, dataType );
                        }
                        else
                        {
                            Type messageType = dataType.MessageType;
                            object obj = dataType.Arg;
                            IMarketDataStorage storage = null;
                            IMarketDataStorage marketDataStorage = null;
                            if ( convertFrom == Messages.DataType.Ticks )
                            {
                                storage = StorageRegistry.GetTickMessageStorage( securityId, Drive, StorageFormat );
                                if ( dataType.IsCandles )
                                    marketDataStorage = StorageRegistry.GetCandleMessageStorage( messageType, securityId, obj, DestinationDrive, DestinationStorageFormat );
                                else if ( dataType == Messages.DataType.Level1 )
                                    marketDataStorage = StorageRegistry.GetLevel1MessageStorage( securityId, DestinationDrive, DestinationStorageFormat );
                            }
                            else if ( convertFrom == Messages.DataType.OrderLog )
                            {
                                storage = StorageRegistry.GetOrderLogMessageStorage( securityId, Drive, StorageFormat );
                                if ( dataType.IsCandles )
                                    marketDataStorage = StorageRegistry.GetCandleMessageStorage( messageType, securityId, obj, DestinationDrive, DestinationStorageFormat );
                                else if ( dataType == Messages.DataType.MarketDepth )
                                {
                                    if ( MarketDepthBuilder == null )
                                    {
                                        this.AddErrorLog( LocalizedStrings.OrderLogBuilder );
                                        continue;
                                    }
                                    marketDataStorage = StorageRegistry.GetQuoteMessageStorage( securityId, DestinationDrive, DestinationStorageFormat );
                                }
                                else if ( dataType == Messages.DataType.Ticks )
                                    marketDataStorage = StorageRegistry.GetTickMessageStorage( securityId, DestinationDrive, DestinationStorageFormat );
                                else if ( dataType == Messages.DataType.Level1 )
                                {
                                    if ( MarketDepthBuilder == null )
                                    {
                                        this.AddErrorLog( LocalizedStrings.OrderLogBuilder );
                                        continue;
                                    }
                                    marketDataStorage = StorageRegistry.GetLevel1MessageStorage( securityId, DestinationDrive, DestinationStorageFormat );
                                }
                            }
                            else if ( convertFrom == Messages.DataType.MarketDepth )
                            {
                                storage = StorageRegistry.GetQuoteMessageStorage( securityId, Drive, StorageFormat );
                                if ( dataType.IsCandles )
                                    marketDataStorage = StorageRegistry.GetCandleMessageStorage( messageType, securityId, obj, DestinationDrive, DestinationStorageFormat );
                                else if ( dataType == Messages.DataType.Level1 )
                                    marketDataStorage = StorageRegistry.GetLevel1MessageStorage( securityId, DestinationDrive, DestinationStorageFormat );
                            }
                            else if ( convertFrom == Messages.DataType.Level1 )
                            {
                                storage = StorageRegistry.GetLevel1MessageStorage( securityId, Drive, StorageFormat );
                                if ( dataType.IsCandles )
                                    marketDataStorage = StorageRegistry.GetCandleMessageStorage( messageType, securityId, obj, DestinationDrive, DestinationStorageFormat );
                                else if ( dataType == Messages.DataType.MarketDepth )
                                    marketDataStorage = StorageRegistry.GetQuoteMessageStorage( securityId, DestinationDrive, DestinationStorageFormat );
                                else if ( dataType == Messages.DataType.Ticks )
                                    marketDataStorage = StorageRegistry.GetTickMessageStorage( securityId, DestinationDrive, DestinationStorageFormat );
                            }
                            if ( storage == null || marketDataStorage == null )
                            {
                                this.AddErrorLog( LocalizedStrings.CannotConvert, convertFrom, dataType );
                            }
                            else
                            {
                                DateTime? fromDate = storage.GetFromDate();
                                DateTime? toDate = storage.GetToDate();
                                if ( fromDate.HasValue && toDate.HasValue )
                                {
                                    ref DateTime? local1 = ref fromDate;
                                    DateTime? nullable = workingSecurity.GetBeginDate( storage.DataType );
                                    DateTime dateTime3 = ( nullable ?? dateTime1 ).Max( dateTime1 ).Max( fromDate.Value );
                                    local1 = new DateTime?( dateTime3 );
                                    ref DateTime? local2 = ref toDate;
                                    nullable = workingSecurity.GetEndDate( storage.DataType );
                                    DateTime dateTime4 = ( nullable ?? dateTime2 - TimeSpan.FromDays( Offset ) ).Min( toDate.Value );
                                    local2 = new DateTime?( dateTime4 );
                                    foreach ( DateTime date in fromDate.Value.Range( toDate.Value, TimeSpan.FromDays( 1.0 ) ).Except<DateTime>( marketDataStorage.Dates ) )
                                    {
                                        if ( CanProcess() )
                                        {
                                            this.AddInfoLog( LocalizedStrings.Str3786Params.Put( security.Id, convertFrom, dataType, date ) );
                                            try
                                            {
                                                if ( convertFrom == Messages.DataType.Ticks )
                                                {
                                                    if ( dataType.IsCandles )
                                                    {
                                                        IEnumerable<CandleMessage> candles = ( ( IMarketDataStorage<ExecutionMessage> )storage ).Load( date ).ToCandles( dataType.ToCandleSeries( security ), null );
                                                        RaiseDataLoaded( security, dataType, new DateTimeOffset?( ( DateTimeOffset )date ), marketDataStorage.Save( candles ) );
                                                    }
                                                    else if ( dataType == Messages.DataType.Level1 )
                                                    {
                                                        IEnumerable<Level1ChangeMessage> level1 = ( ( IMarketDataStorage<ExecutionMessage> )storage ).Load( date ).ToLevel1( null, new TimeSpan() );
                                                        RaiseDataLoaded( security, dataType, new DateTimeOffset?( ( DateTimeOffset )date ), marketDataStorage.Save( level1 ) );
                                                    }
                                                }
                                                else if ( convertFrom == Messages.DataType.OrderLog )
                                                {
                                                    if ( dataType.IsCandles )
                                                    {
                                                        IEnumerable<CandleMessage> candles = ( ( IMarketDataStorage<ExecutionMessage> )storage ).Load( date ).ToTicks().ToCandles( dataType.ToCandleSeries( security ), null );
                                                        RaiseDataLoaded( security, dataType, new DateTimeOffset?( ( DateTimeOffset )date ), marketDataStorage.Save( candles ) );
                                                    }
                                                    else if ( dataType == Messages.DataType.MarketDepth )
                                                    {
                                                        IOrderLogMarketDepthBuilder marketDepthBuilder = MarketDepthBuilder.CreateOrderLogMarketDepthBuilder( security.ToSecurityId( null, true, false ) );
                                                        IEnumerable<QuoteChangeMessage> quoteChangeMessages = ( ( IMarketDataStorage<ExecutionMessage> )storage ).Load( date ).ToOrderBooks( marketDepthBuilder, MarketDepthInterval, MarketDepthMaxDepth ).BuildIfNeed( null );
                                                        RaiseDataLoaded( security, dataType, new DateTimeOffset?( ( DateTimeOffset )date ), marketDataStorage.Save( quoteChangeMessages ) );
                                                    }
                                                    else if ( dataType == Messages.DataType.Ticks )
                                                    {
                                                        IEnumerable<ExecutionMessage> ticks = ( ( IMarketDataStorage<ExecutionMessage> )storage ).Load( date ).ToTicks();
                                                        RaiseDataLoaded( security, dataType, new DateTimeOffset?( ( DateTimeOffset )date ), marketDataStorage.Save( ticks ) );
                                                    }
                                                    else if ( dataType == Messages.DataType.Level1 )
                                                    {
                                                        IOrderLogMarketDepthBuilder marketDepthBuilder = MarketDepthBuilder.CreateOrderLogMarketDepthBuilder( security.ToSecurityId( null, true, false ) );
                                                        IEnumerable<Level1ChangeMessage> level1 = ( ( IMarketDataStorage<ExecutionMessage> )storage ).Load( date ).ToLevel1( marketDepthBuilder, new TimeSpan() );
                                                        RaiseDataLoaded( security, dataType, new DateTimeOffset?( ( DateTimeOffset )date ), marketDataStorage.Save( level1 ) );
                                                    }
                                                }
                                                else if ( convertFrom == Messages.DataType.MarketDepth )
                                                {
                                                    if ( dataType.IsCandles )
                                                    {
                                                        IEnumerable<CandleMessage> candles = ( ( IMarketDataStorage<QuoteChangeMessage> )storage ).Load( date ).BuildIfNeed( null ).ToCandles( dataType.ToCandleSeries( security ), Level1Fields.SpreadMiddle, null );
                                                        RaiseDataLoaded( security, dataType, new DateTimeOffset?( ( DateTimeOffset )date ), marketDataStorage.Save( candles ) );
                                                    }
                                                    else if ( dataType == Messages.DataType.Level1 )
                                                    {
                                                        IEnumerable<Level1ChangeMessage> level1 = ( ( IMarketDataStorage<QuoteChangeMessage> )storage ).Load( date ).ToLevel1();
                                                        RaiseDataLoaded( security, dataType, new DateTimeOffset?( ( DateTimeOffset )date ), marketDataStorage.Save( level1 ) );
                                                    }
                                                }
                                                else if ( convertFrom == Messages.DataType.Level1 )
                                                {
                                                    if ( dataType.IsCandles )
                                                    {
                                                        IEnumerable<CandleMessage> candles = ( ( IMarketDataStorage<Level1ChangeMessage> )storage ).Load( date ).ToTicks().ToCandles( dataType.ToCandleSeries( security ), null );
                                                        RaiseDataLoaded( security, dataType, new DateTimeOffset?( ( DateTimeOffset )date ), marketDataStorage.Save( candles ) );
                                                    }
                                                    else if ( dataType == Messages.DataType.MarketDepth )
                                                    {
                                                        IEnumerable<QuoteChangeMessage> orderBooks = ( ( IMarketDataStorage<Level1ChangeMessage> )storage ).Load( date ).ToOrderBooks();
                                                        RaiseDataLoaded( security, dataType, new DateTimeOffset?( ( DateTimeOffset )date ), marketDataStorage.Save( orderBooks ) );
                                                    }
                                                    else if ( dataType == Messages.DataType.Ticks )
                                                    {
                                                        IEnumerable<ExecutionMessage> ticks = ( ( IMarketDataStorage<Level1ChangeMessage> )storage ).Load( date ).ToTicks();
                                                        RaiseDataLoaded( security, dataType, new DateTimeOffset?( ( DateTimeOffset )date ), marketDataStorage.Save( ticks ) );
                                                    }
                                                }
                                            }
                                            catch ( Exception ex )
                                            {
                                                HandleError( ex );
                                            }
                                        }
                                        else
                                            break;
                                    }
                                }
                            }
                        }
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
                this.AddInfoLog( LocalizedStrings.Str2300 );
            return base.OnProcess();
        }

        private class ConvertFromItemsSource : ItemsSourceBase<StockSharp.Messages.DataType>
        {
            private static readonly StockSharp.Messages.DataType[ ] _values = new StockSharp.Messages.DataType[4] { Messages.DataType.Ticks, Messages.DataType.OrderLog, Messages.DataType.MarketDepth, Messages.DataType.Level1 };

            protected override IEnumerable<StockSharp.Messages.DataType> GetValues()
            {
                return _values;
            }
        }
    }
}
