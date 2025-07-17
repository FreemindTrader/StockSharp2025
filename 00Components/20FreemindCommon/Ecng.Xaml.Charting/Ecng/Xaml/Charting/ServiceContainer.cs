// Decompiled with JetBrains decompiler
// Type: fx.Xaml.Charting.ServiceContainer
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;
using System.Collections.Generic;

namespace fx.Xaml.Charting
{
    public class ServiceContainer : IServiceContainer
    {
        private readonly IDictionary<Type, object> _serviceInstances = (IDictionary<Type, object>) new Dictionary<Type, object>();

        internal bool HasService<T>()
        {
            return this._serviceInstances.ContainsKey( typeof( T ) );
        }

        public void RegisterService<T>( T instance )
        {
            this._serviceInstances[ typeof( T ) ] = ( object ) instance;
        }

        public void DeRegisterService<T>()
        {
            this._serviceInstances.Remove( typeof( T ) );
        }

        public T GetService<T>()
        {
            Type index = typeof (T);
            if ( !this.HasService<T>() )
                throw new Exception( string.Format( "The service instance of type {0} has not been registered with the container", ( object ) index ) );
            return ( T ) this._serviceInstances[ index ];
        }
    }
}
