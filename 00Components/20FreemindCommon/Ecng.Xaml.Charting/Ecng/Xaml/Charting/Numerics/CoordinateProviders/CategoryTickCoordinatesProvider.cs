using System;
using System.Linq;
using Ecng.Xaml.Charting.Common.Extensions;
using Ecng.Xaml.Charting.Numerics.CoordinateCalculators;
using Ecng.Xaml.Charting.Numerics.TickCoordinateProviders;

namespace Ecng.Xaml.Charting.Numerics.CoordinateProviders
{
    public class CategoryTickCoordinatesProvider : DefaultTickCoordinatesProvider
    {
        public CategoryTickCoordinatesProvider()
        {
        }

        private double GetDataPointIndexAt( double coord, ICategoryCoordinateCalculator coordCalc )
        {
            int num = (int)Math.Round(coordCalc.GetDataValue(coord).ToDouble());

            return coordCalc.TransformIndexToData( num ).ToDouble();
        }

        public override TickCoordinates GetTickCoordinates( double[ ] minorTicks, double[ ] majorTicks )
        {
            TickCoordinates tickCoordinate = new TickCoordinates(new double[0], new double[0], new float[0], new float[0]);
            ICategoryCoordinateCalculator currentCoordinateCalculator = base.ParentAxis.GetCurrentCoordinateCalculator() as ICategoryCoordinateCalculator;
            if ( currentCoordinateCalculator != null )
            {
                TickCoordinates tickCoordinates = base.GetTickCoordinates(minorTicks, majorTicks);
                double[] array = (
                    from x in tickCoordinates.MinorTickCoordinates
                    select this.GetDataPointIndexAt((double)x, currentCoordinateCalculator)).ToArray<double>();
                double[] numArray = (
                    from x in tickCoordinates.MajorTickCoordinates
                    select this.GetDataPointIndexAt((double)x, currentCoordinateCalculator)).ToArray<double>();
                tickCoordinate = new TickCoordinates( array, numArray, tickCoordinates.MinorTickCoordinates, tickCoordinates.MajorTickCoordinates );
            }
            return tickCoordinate;
        }
    }
}
