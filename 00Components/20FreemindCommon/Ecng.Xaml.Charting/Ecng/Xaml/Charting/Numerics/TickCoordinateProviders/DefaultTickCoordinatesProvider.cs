// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Numerics.TickCoordinateProviders.DefaultTickCoordinatesProvider
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\StockSharp.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using StockSharp.Xaml.Charting.Common.Extensions;
using StockSharp.Xaml.Charting.Numerics.CoordinateCalculators;
using StockSharp.Xaml.Charting.Numerics.CoordinateProviders;
using StockSharp.Xaml.Charting.Rendering.Common;

namespace StockSharp.Xaml.Charting.Numerics.TickCoordinateProviders
{
    public class DefaultTickCoordinatesProvider : ProviderBase, ITickCoordinatesProvider
    {
        protected readonly List<double> _minorTicks = new List<double>();
        protected readonly List<double> _majorTicks = new List<double>();
        protected readonly List<float> _minorTickCoords = new List<float>();
        protected readonly List<float> _majorTickCoords = new List<float>();
        protected const double Precision = -1E-08;
        protected const double MinTickDistance = 4.94065645841247E-324;
        private double _currOffset;
        private double _currAxisSize;

        public virtual TickCoordinates GetTickCoordinates( double[ ] minorTicks, double[ ] majorTicks )
        {
            this._minorTicks.Clear();
            this._majorTicks.Clear();
            this._minorTickCoords.Clear();
            this._majorTickCoords.Clear();
            this._currAxisSize = this.GetAxisSize();
            ICoordinateCalculator<double> coordinateCalculator = this.ParentAxis.GetCurrentCoordinateCalculator();
            if ( coordinateCalculator != null && Math.Abs( this._currAxisSize ) >= double.Epsilon )
            {
                this._currOffset = this.ParentAxis.GetAxisOffset();
                if ( !( ( IEnumerable<double> ) minorTicks ).IsNullOrEmpty<double>() )
                    this.CalculateTickCoords( minorTicks, coordinateCalculator, false );
                if ( !( ( IEnumerable<double> ) majorTicks ).IsNullOrEmpty<double>() )
                    this.CalculateTickCoords( majorTicks, coordinateCalculator, true );
            }
            return new TickCoordinates( this._minorTicks.ToArray(), this._majorTicks.ToArray(), this._minorTickCoords.ToArray(), this._majorTickCoords.ToArray() );
        }

        private double GetAxisSize()
        {
            double num = this.ParentAxis.IsHorizontalAxis ? this.ParentAxis.ActualWidth : this.ParentAxis.ActualHeight;
            if ( Math.Abs( num ) < double.Epsilon && this.ParentAxis.ParentSurface != null )
            {
                IRenderSurface renderSurface = this.ParentAxis.ParentSurface.RenderSurface;
                if ( renderSurface != null )
                    num = this.ParentAxis.IsHorizontalAxis ? renderSurface.ActualWidth : renderSurface.ActualHeight;
            }
            return num;
        }

        protected virtual void CalculateTickCoords( double[ ] ticks, ICoordinateCalculator<double> coordinateCalculator, bool isMajor )
        {
            List<double> doubleList = isMajor ? this._majorTicks : this._minorTicks;
            List<float> floatList = isMajor ? this._majorTickCoords : this._minorTickCoords;
            double num1 = ticks[0];
            double dataValue = num1;
            double num2 = coordinateCalculator.GetCoordinate(dataValue);
            double coord = num2;
            if ( this.IsInBounds( coord ) )
            {
                floatList.Add( ( float ) coord );
                doubleList.Add( dataValue );
            }
            for ( int index = 1 ; index < ticks.Length ; ++index )
            {
                double tick = ticks[index];
                if ( Math.Abs( tick - num1 ) > double.Epsilon )
                {
                    double coordinate = coordinateCalculator.GetCoordinate(ticks[index]);
                    if ( Math.Abs( coordinate - num2 ) > double.Epsilon && this.IsInBounds( coordinate ) )
                    {
                        doubleList.Add( tick );
                        floatList.Add( ( float ) coordinate );
                        num1 = tick;
                        num2 = coordinate;
                    }
                }
            }
        }

        protected virtual bool IsInBounds( double coord )
        {
            coord -= this._currOffset;
            if ( coord >= -1E-08 )
                return coord < this._currAxisSize - -1E-08;
            return false;
        }
    }
}
