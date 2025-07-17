// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.Numerics.CoordinateCalculators.CoordinateCalculatorFactory
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
namespace Ecng.Xaml.Charting
{
    internal sealed class CoordinateCalculatorFactory : ICoordinateCalculatorFactory
    {
        public ICoordinateCalculator<double> New( AxisParams arg )
        {
            ICoordinateCalculator<double> coordinateCalculator;
            if ( arg.IsCategoryAxis && arg.CategoryPointSeries != null )
                coordinateCalculator = ( ICoordinateCalculator<double> ) new CategoryCoordinateCalculator( arg.DataPointStep, arg.DataPointPixelSize, arg.Size, arg.CategoryPointSeries, new IndexRange( ( int ) arg.VisibleMin.RoundOff(), ( int ) arg.VisibleMax.RoundOff() ), arg.IsHorizontal, arg.BaseXValues, arg.IsBaseXValuesSorted );
            else if ( arg.IsLogarithmicAxis )
                coordinateCalculator = ( ICoordinateCalculator<double> ) new LogarithmicDoubleCoordinateCalculator( arg.Size, arg.VisibleMin, arg.VisibleMax, arg.LogarithmicBase, arg.IsXAxis, arg.IsHorizontal, arg.FlipCoordinates );
            else if ( arg.IsPolarAxis )
            {
                if ( arg.IsXAxis )
                {
                    coordinateCalculator = ( ICoordinateCalculator<double> ) new PolarCoordinateCalculator( arg.VisibleMin, arg.VisibleMax, arg.IsXAxis, arg.IsHorizontal, arg.FlipCoordinates );
                }
                else
                {
                    bool shouldFlip = !arg.FlipCoordinates;
                    coordinateCalculator = CoordinateCalculatorFactory.GetDoulbeCoordinateCalculator( arg, shouldFlip );
                }
            }
            else
            {
                bool shouldFlip = arg.IsXAxis ^ arg.FlipCoordinates;
                coordinateCalculator = CoordinateCalculatorFactory.GetDoulbeCoordinateCalculator( arg, shouldFlip );
            }
            if ( coordinateCalculator == null )
                throw new InvalidOperationException( string.Format( "Unable to create a tick calculator instance." ) );
            ( ( CoordinateCalculatorBase ) coordinateCalculator ).CoordinatesOffset = arg.Offset;
            ( ( CoordinateCalculatorBase ) coordinateCalculator ).IsPolarAxisCalculator = arg.IsPolarAxis;
            return coordinateCalculator;
        }

        private static ICoordinateCalculator<double> GetDoulbeCoordinateCalculator( AxisParams arg, bool shouldFlip )
        {
            if ( shouldFlip )
                return ( ICoordinateCalculator<double> ) new FlippedDoubleCoordinateCalculator( arg.Size, arg.VisibleMin, arg.VisibleMax, arg.IsXAxis, arg.IsHorizontal, arg.FlipCoordinates );
            return ( ICoordinateCalculator<double> ) new DoubleCoordinateCalculator( arg.Size, arg.VisibleMin, arg.VisibleMax, arg.IsXAxis, arg.IsHorizontal, arg.FlipCoordinates );
        }
    }
}
