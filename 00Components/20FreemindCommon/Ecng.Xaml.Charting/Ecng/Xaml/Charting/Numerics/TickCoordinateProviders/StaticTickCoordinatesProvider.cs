// Decompiled with JetBrains decompiler
// Type: StockSharp.Xaml.Charting.Numerics.TickCoordinateProviders.StaticTickCoordinatesProvider
// Assembly: StockSharp.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: 5D7395C1-836A-4A9B-B006-2FBF7EC25A8F
// Assembly location: T:\00 - Programming\StockSharp\References\StockSharp.Xaml.Charting.dll

using System;
using StockSharp.Xaml.Charting.Common.Extensions;

namespace StockSharp.Xaml.Charting.Numerics.TickCoordinateProviders
{
    public class StaticTickCoordinatesProvider : DefaultTickCoordinatesProvider
    {
        private TickCoordinates _tickCoords;
        private IRange _prevRange;
        private double _prevSize;

        public override TickCoordinates GetTickCoordinates( double[ ] minorTicks, double[ ] majorTicks )
        {
            bool flag = !this.ParentAxis.VisibleRange.Equals((object) this._prevRange);
            if ( this._tickCoords.IsEmpty | ( uint ) this.ParentAxis.ActualWidth.CompareTo( this._prevSize ) > 0U )
                this._tickCoords = base.GetTickCoordinates( minorTicks, majorTicks );
            else if ( flag )
                this.OverrideTickValues( this._tickCoords );
            else
                this.OverrideTickCoordinates( this._tickCoords );
            this._prevRange = this.ParentAxis.VisibleRange;
            this._prevSize = this.ParentAxis.ActualWidth;
            return this._tickCoords;
        }

        private void OverrideTickValues( TickCoordinates tickCoords )
        {
            if ( this.ParentAxis.GetCurrentCoordinateCalculator() == null )
                return;
            for ( int index = 0 ; index < tickCoords.MinorTickCoordinates.Length ; ++index )
            {
                IComparable dataValue = this.ParentAxis.GetDataValue((double) tickCoords.MinorTickCoordinates[index]);
                tickCoords.MinorTicks[ index ] = dataValue.ToDouble();
            }
            for ( int index = 0 ; index < tickCoords.MajorTickCoordinates.Length ; ++index )
            {
                IComparable dataValue = this.ParentAxis.GetDataValue((double) tickCoords.MajorTickCoordinates[index]);
                tickCoords.MajorTicks[ index ] = dataValue.ToDouble();
            }
        }

        private void OverrideTickCoordinates( TickCoordinates tickCoords )
        {
            if ( this.ParentAxis.GetCurrentCoordinateCalculator() == null )
                return;
            for ( int index = 0 ; index < tickCoords.MinorTickCoordinates.Length ; ++index )
            {
                float coordinate = (float) this.ParentAxis.GetCoordinate((IComparable) tickCoords.MinorTicks[index]);
                tickCoords.MinorTickCoordinates[ index ] = coordinate;
            }
            for ( int index = 0 ; index < tickCoords.MajorTickCoordinates.Length ; ++index )
            {
                float coordinate = (float) this.ParentAxis.GetCoordinate((IComparable) tickCoords.MajorTicks[index]);
                tickCoords.MajorTickCoordinates[ index ] = coordinate;
            }
        }
    }
}
