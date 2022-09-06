// Decompiled with JetBrains decompiler
// Type: StockSharp.Studio.Core.Configuration.BaseAppConfig`2
// Assembly: StockSharp.Studio.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2AA452FA-4D77-495D-BC00-1781FF5794A8
// Assembly location: T:\00-StockSharp\Designer\StockSharp.Studio.Core.dll

using Ecng.Collections;
using Ecng.Common;
using StockSharp.Configuration;
using StockSharp.Logging;
using System;
using System.Collections;
using System.Collections.Generic;

namespace StockSharp.Studio.Core.Configuration
{
    public abstract class BaseAppConfig<TConfig, TSection> : Disposable
      where TConfig : BaseAppConfig<TConfig, TSection>, new()
      where TSection : StudioSection
    {
        private static readonly Lazy<TConfig> _instance = new Lazy<TConfig>( ( Func<TConfig> )( () => new TConfig() ) );
        private readonly CachedSynchronizedList<Type> _strategyControls = new CachedSynchronizedList<Type>();
        private readonly CachedSynchronizedList<Type> _toolControls = new CachedSynchronizedList<Type>();

        public static TConfig Instance
        {
            get
            {
                return BaseAppConfig<TConfig, TSection>._instance.Value;
            }
        }

        public IEnumerable<Type> StrategyControls
        {
            get
            {
                return ( IEnumerable<Type> )this._strategyControls.Cache;
            }
        }

        public IEnumerable<Type> ToolControls
        {
            get
            {
                return ( IEnumerable<Type> )this._toolControls.Cache;
            }
        }

        protected BaseAppConfig()
        {
            TSection rootSection = this.RootSection;
            BaseAppConfig<TConfig, TSection>.SafeAdd<ControlElement>( ( IEnumerable )rootSection.StrategyControls, ( Action<ControlElement> )( elem => this._strategyControls.Add( elem.Type.To<Type>() ) ) );
            BaseAppConfig<TConfig, TSection>.SafeAdd<ControlElement>( ( IEnumerable )rootSection.ToolControls, ( Action<ControlElement> )( elem => this._toolControls.Add( elem.Type.To<Type>() ) ) );
        }

        protected TSection RootSection
        {
            get
            {
                return ( TSection )Extensions.RootSection;
            }
        }

        public string FixServerAddress
        {
            get
            {
                return this.RootSection.FixServerAddress;
            }
        }

        private static void SafeAdd<T1>( IEnumerable from, Action<T1> action )
        {
            foreach ( T1 obj in from )
            {
                try
                {
                    action( obj );
                }
                catch ( Exception ex )
                {
                    ex.LogError( ( string )null );
                }
            }
        }
    }
}
