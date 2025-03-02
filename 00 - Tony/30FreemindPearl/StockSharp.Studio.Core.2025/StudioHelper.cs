// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.StudioHelper
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 287A45E4-79AD-43B4-B98B-3CD330C45DE5
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Studio.Core.dll

using Ecng.Common;
using Ecng.Compilation;
using Ecng.ComponentModel;
using Ecng.Configuration;
using Ecng.Data;
using Ecng.Logging;
using Ecng.Serialization;
using StockSharp.Alerts;
using StockSharp.Algo;
using StockSharp.Algo.Candles.Patterns;
using StockSharp.Algo.Compilation;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Configuration;
using StockSharp.Messages;
using StockSharp.Studio.Core.Commands;
using StockSharp.Studio.Core.Configuration;
using StockSharp.Studio.Core.Services;
using StockSharp.Studio.WebApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;

namespace StockSharp.Studio.Core
{
    public static class StudioHelper
    {
        public static string CreateKey( this Guid id )
        {
            return "_" + Ecng.ComponentModel.Extensions.ToN( id );
        }

        public static string CreateKey( this Type controlType )
        {
            return ( Ecng.ComponentModel.Extensions.TryGetGuid( controlType ) ?? Guid.NewGuid() ).CreateKey();
        }

        public static void InitializeDatabaseCache()
        {
            DatabaseConnectionCache cache = new DatabaseConnectionCache();
            ConfigManager.RegisterService<DatabaseConnectionCache>( cache );
            SettingsStorage dbSettings = StudioUserConfig.Instance.GetValue<SettingsStorage>("DatabaseConnectionCache", (SettingsStorage) null);
            LoggingHelper.DoWithLog<bool>(  ( () => PersistableHelper.LoadIfNotNull( ( IPersistable ) cache, dbSettings ) ) );
            ObservableCollection<DatabaseConnectionPair> connections = new ObservableCollection<DatabaseConnectionPair>(cache.Connections);
            ConfigManager.RegisterService<ObservableCollection<DatabaseConnectionPair>>( connections );
            cache.ConnectionCreated += new Action<DatabaseConnectionPair>( ( ( Collection<DatabaseConnectionPair> ) connections ).Add );
            cache.ConnectionDeleted += ( Action<DatabaseConnectionPair> ) ( c => connections.Remove( c ) );
            cache.Updated += () => StudioUserConfig.Instance.SetDelayValue( "DatabaseConnectionCache", ( cache ).Save);
        }

        public static void InitializeStorage()
        {
            IEntityRegistry entityRegistry = ServicesRegistry.EntityRegistry;
            entityRegistry.DelayAction.RegisterServices();
            StorageExchangeInfoProvider exchangeInfoProvider = new StorageExchangeInfoProvider(entityRegistry, false);
            ConfigManager.RegisterService<IExchangeInfoProvider>( ( IExchangeInfoProvider ) exchangeInfoProvider );
            ConfigManager.RegisterService<IBoardMessageProvider>( ( IBoardMessageProvider ) exchangeInfoProvider );
            StudioHelper.InitializeDriveCache();
            IStorageRegistry service = (IStorageRegistry) new StorageRegistry((IExchangeInfoProvider) exchangeInfoProvider);
            IMarketDataDrive defaultDrive = ServicesRegistry.DriveCache.DefaultDrive;
            if ( defaultDrive != null )
            {
                ConfigManager.RegisterService<IMarketDataDrive>( defaultDrive );
                service.DefaultDrive = defaultDrive;
            }
            ConfigManager.RegisterService<IStorageRegistry>( service );
            StudioHelper.InitializeDatabaseCache();
        }

        public static void InitializeDriveCache()
        {
            DriveCache cache = new DriveCache();
            StudioUserConfig config = StudioUserConfig.Instance;
            if ( config.GetIsFirstRun() )
            {
                Directory.CreateDirectory( Paths.StorageDir );
                if ( ServicesRegistry.TryStorageRegistry == null )
                    cache.GetDrive( Paths.StorageDir );
                cache.GetDrive( ( string ) Converter.To<string>( ( object ) RemoteMarketDataDrive.DefaultAddress ) );
            }
            else
                config.TryLoadSettings( "DriveCache", new Action<SettingsStorage>( cache.Load ) );
            cache.Changed += ( Action ) ( () => config.SetDriveCache( PersistableHelper.Save( ( IPersistable ) cache ) ) );
            ConfigManager.RegisterService<DriveCache>( cache );
        }

        public static bool EnsureFirstTime( this IPersistableService service )
        {
            if ( !service.GetIsFirstRun() )
                return false;
            service.SetIsFirstRun( false );
            service.SetStorageFormat();
            service.SetDriveCache( PersistableHelper.Save( ( IPersistable ) ServicesRegistry.DriveCache ) );
            return true;
        }

        public static EntityCommand<TEntity> ToCommand<TEntity>(
          this Subscription subscription,
          TEntity entity )
          where TEntity : class
        {
            return new EntityCommand<TEntity>( subscription, entity );
        }

        public static string GetFileName( this CodeInfo info )
        {
            if ( info == null )
                throw new ArgumentNullException( nameof( info ) );
            return info.Id.GetFileName();
        }

        public static void RegisterCompilerCache()
        {
            ConfigManager.RegisterService<ICompilerCache>( ( ICompilerCache ) new FileCompilerCache( Paths.CompilerCacheDir, TimeSpan.FromDays( 30.0 ) ) );
            ServicesRegistry.CompilerCache.Init();
        }

        public static void RegisterServices( this DelayAction delayAction )
        {
            ConfigManager.RegisterService<INativeIdStorage>( ( INativeIdStorage ) new CsvNativeIdStorage( Paths.SecurityNativeIdDir )
            {
                DelayAction = delayAction
            } );
            ConfigManager.RegisterService<ICandlePatternProvider>( ( ICandlePatternProvider ) new CandlePatternFileStorage( Paths.CandlePatternsFile, CandlePatternRegistry.All )
            {
                DelayAction = delayAction
            } );
            ConfigManager.RegisterService<ISecurityMappingStorage>( ( ISecurityMappingStorage ) new CsvSecurityMappingStorage( Paths.SecurityMappingDir )
            {
                DelayAction = delayAction
            } );
            ConfigManager.RegisterService<IExtendedInfoStorage>( ( IExtendedInfoStorage ) new CsvExtendedInfoStorage( Paths.SecurityExtendedInfo )
            {
                DelayAction = delayAction
            } );
        }

        public static void RegisterProviders( this IEntityRegistry entityRegistry )
        {
            FilterableSecurityProvider securityProvider = new FilterableSecurityProvider((ISecurityProvider) entityRegistry.Securities);
            ConfigManager.RegisterService<ISecurityProvider>( ( ISecurityProvider ) securityProvider );
            ConfigManager.RegisterService<ISecurityMessageProvider>( ( ISecurityMessageProvider ) securityProvider );
            ConfigManager.RegisterService<ISecurityMessageAdapterProvider>( ( ISecurityMessageAdapterProvider ) new CsvSecurityMessageAdapterProvider( Path.Combine( Paths.AppDataPath, "security_adapter.csv" ) ) );
            ConfigManager.RegisterService<IPortfolioMessageAdapterProvider>( ( IPortfolioMessageAdapterProvider ) new CsvPortfolioMessageAdapterProvider( Path.Combine( Paths.AppDataPath, "portfolio_adapter.csv" ) ) );
        }

        public static void InitServices()
        {
            LoggingHelper.DoWithLog<string>( ServicesRegistry.NativeIdStorage.Init );
            LoggingHelper.DoWithLog( ServicesRegistry.CandlePatternProvider.Init );
            LoggingHelper.DoWithLog<string>( ServicesRegistry.MappingStorage.Init );
            LoggingHelper.DoWithLog<IExtendedInfoStorageItem>( ServicesRegistry.ExtendedInfoStorage.Init );
        }

        public static void InitProviders()
        {
            LoggingHelper.DoWithLog( new Action( ( ( IMappingMessageAdapterProvider<Tuple<SecurityId, DataType>> ) ServicesRegistry.SecurityAdapterProvider ).Init ) );
            LoggingHelper.DoWithLog( new Action( ( ( IMappingMessageAdapterProvider<string> ) ServicesRegistry.PortfolioAdapterProvider ).Init ) );
        }

        public static void SendSelect<T>( this T value, object listener, bool canEdit = false )
        {
            new SelectCommand<T>( value, canEdit ).Process( listener, false );
        }

        public static void TrySendNotification(
          this AlertNotifications? notification,
          StudioNotificationSettings settings,
          LogLevels lvl,
          string caption,
          string text = null,
          DateTimeOffset? time = null )
        {
            if ( settings == null )
                throw new ArgumentNullException( nameof( settings ) );
            if ( !notification.HasValue )
                return;
            LoggingHelper.ObserveErrorAndLog( AlertServicesRegistry.NotificationService.NotifyAsync( notification.GetValueOrDefault(), ( settings.TelegramChannel ?? ( ITelegramChannel ) WebApiHelper.DefaultClientSocial )?.Id, lvl, caption, text ?? caption, time ?? DateTimeOffset.Now, new CancellationToken() ).AsTask() );
        }
    }
}
