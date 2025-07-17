// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.StrategyManager.DefaultStrategyManager
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Windows;
using Ecng.Xaml.Charting.Common.Messaging;
using Ecng.Xaml.Charting.Visuals;

namespace Ecng.Xaml.Charting.StrategyManager
{
    internal class DefaultStrategyManager : IStrategyManager
    {
        private CoordinateSystem _chartCoordinateSystem;
        private ITransformationStrategy _transformationStrategy;

        public DefaultStrategyManager( UltrachartSurface surface )
        {
            this._chartCoordinateSystem = surface.IsPolarChart ? CoordinateSystem.Polar : CoordinateSystem.Cartesian;
            this.UpdateStrategies( new Size(), this._chartCoordinateSystem );
            IEventAggregator service = surface.Services.GetService<IEventAggregator>();
            service.Subscribe<RenderSurfaceResizedMessage>( new Action<RenderSurfaceResizedMessage>( this.OnRenderSurfaceResized ), true );
            service.Subscribe<CoordinateSystemMessage>( new Action<CoordinateSystemMessage>( this.OnCoordinateSystemChanged ), true );
        }

        private void UpdateStrategies( Size viewportSize, CoordinateSystem coordinateSystem )
        {
            if ( coordinateSystem != CoordinateSystem.Cartesian )
            {
                if ( coordinateSystem != CoordinateSystem.Polar )
                    throw new InvalidOperationException( string.Format( "Cannot update strategies for surface with CoordinateSystem.{0}", ( object ) coordinateSystem ) );
                this._transformationStrategy = ( ITransformationStrategy ) new PolarTransformationStrategy( viewportSize );
            }
            else
                this._transformationStrategy = ( ITransformationStrategy ) new CartesianTransformationStrategy( viewportSize );
        }

        private void OnRenderSurfaceResized( RenderSurfaceResizedMessage message )
        {
            this.UpdateStrategies( message.ViewportSize, this._chartCoordinateSystem );
        }

        private void OnCoordinateSystemChanged( CoordinateSystemMessage message )
        {
            this._chartCoordinateSystem = message.CoordinateSystem;
            this.UpdateStrategies( this._transformationStrategy.ViewportSize, this._chartCoordinateSystem );
        }

        public ITransformationStrategy GetTransformationStrategy()
        {
            return this._transformationStrategy;
        }
    }
}
