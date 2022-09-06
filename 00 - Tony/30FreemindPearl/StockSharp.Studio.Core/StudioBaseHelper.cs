// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.StudioBaseHelper
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using Ecng.Common;
using Ecng.Configuration;
using Ecng.Serialization;
using StockSharp.Algo;
using StockSharp.Algo.Storages;
using StockSharp.Algo.Strategies;
using StockSharp.Diagram;
using StockSharp.Studio.Core.Commands;
using StockSharp.Studio.Core.Configuration;
using StockSharp.Studio.Core.Services;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace StockSharp.Studio.Core
{
    public static class StudioBaseHelper
    {
        public static string CreateKey( this Type controlType )
        {
            return string.Format( "_{0:N}", ( object )( controlType.GetAttribute<GuidAttribute>( true ) != null ? controlType.GUID : Guid.NewGuid() ) );
        }

        public static void InitializeDriveCache()
        {
            DriveCache cache = new DriveCache( ServicesRegistry.StorageRegistry.DefaultDrive );
            ConfigManager.RegisterService<DriveCache>( cache );
            StudioUserConfig config = BaseUserConfig<StudioUserConfig>.Instance;
            if ( config.GetIsFirstRun() )
                cache.GetDrive( RemoteMarketDataDrive.DefaultAddress.To<string>() );
            else
                config.TryLoadSettings( "DriveCache", new Action<SettingsStorage>( cache.Load ) );
            cache.Changed += ( Action )( () => config.SetDriveCache( cache.Save() ) );
        }

        public static EntityCommand<TEntity> ToCommand<TEntity>(
          this Subscription subscription,
          TEntity entity )
          where TEntity : class
        {
            return new EntityCommand<TEntity>( subscription, entity );
        }

        public static EntityCommand<TEntity> ToCommand<TEntity>(
          this Subscription subscription,
          IEnumerable<TEntity> entities )
          where TEntity : class
        {
            return new EntityCommand<TEntity>( subscription, entities );
        }

        public static string GetTypeId( this Strategy strategy )
        {
            if ( strategy == null )
                throw new ArgumentNullException( nameof( strategy ) );
            DiagramStrategy diagramStrategy = strategy as DiagramStrategy;
            if ( diagramStrategy != null )
                return diagramStrategy.Composition.TypeId.To<string>();
            return strategy.GetType().GetTypeName( false );
        }
    }
}
