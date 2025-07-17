using System;
using System.Collections.Generic;
using System.Linq;
namespace Ecng.Xaml.Charting
{
    internal class TradeChartTickCalculator
    {
        private readonly static IList<TimeSpan> _dateDeltas;

        static TradeChartTickCalculator()
        {
            TradeChartTickCalculator._dateDeltas = ( IList<TimeSpan> ) ( new TimeSpan[ ] { TimeSpan.FromDays( 1 ), TimeSpan.FromDays( 2 ), TimeSpan.FromDays( 7 ), TimeSpan.FromDays( 14 ), TimeSpanExtensions.FromMonths( 1 ), TimeSpanExtensions.FromMonths( 3 ), TimeSpanExtensions.FromMonths( 6 ), TimeSpanExtensions.FromYears( 1 ), TimeSpanExtensions.FromYears( 2 ), TimeSpanExtensions.FromYears( 5 ), TimeSpanExtensions.FromYears( 10 ) } );
        }

        public TradeChartTickCalculator()
        {
        }

        private static int[ ] GetMinorTickIndices( IndexRange indexRange, int[ ] majorTickIndices, int minorsPerMajor )
        {
            List<int> nums = new List<int>();
            int num = 0;
            int num1 = 0;
            int num2 = 1;
            while ( num2 < ( int ) majorTickIndices.Length )
            {
                num = num + ( majorTickIndices[ num2 ] - majorTickIndices[ num2 - 1 ] );
                num2++;
                num1++;
            }
            num = ( int ) Math.Ceiling( ( double ) num / ( double ) ( num1 * minorsPerMajor ) );
            num = Math.Max( num, 1 );
            int min = num - indexRange.Min % num;
            for ( int i = indexRange.Min + min ; i <= indexRange.Max ; i += num )
            {
                nums.Add( i );
            }
            return nums.ToArray();
        }

        internal static TickCoordinates GetTickCoordinates( IndexRange indexRange, DateRange dateRange, ICategoryCoordinateCalculator coordCalc, int minorsPerMajor, int maxAutoTicks )
        {
            TimeSpanDelta timeSpanDeltum;
            TimeSpan max = dateRange.Max - dateRange.Min;
            TimeSpan timeSpan = new TimeSpan(max.Ticks);
            TimeSpan timeSpan1 = TradeChartTickCalculator._dateDeltas.FirstOrDefault<TimeSpan>((TimeSpan x) => new TimeSpan(x.Ticks * (long)maxAutoTicks) > timeSpan);
            if ( timeSpan1.Equals( TimeSpan.Zero ) )
            {
                double ticks = (double)max.Ticks;
                TimeSpan timeSpan2 = TimeSpanExtensions.FromYears(1);
                NiceDoubleScale niceDoubleScale = new NiceDoubleScale(0, ticks / (double)timeSpan2.Ticks, minorsPerMajor, (uint)maxAutoTicks);
                niceDoubleScale.CalculateDelta();
                DoubleAxisDelta tickSpacing = niceDoubleScale.TickSpacing;
                timeSpanDeltum = new TimeSpanDelta( TimeSpanExtensions.FromYears( ( int ) tickSpacing.MinorDelta ), TimeSpanExtensions.FromYears( ( int ) tickSpacing.MajorDelta ) );
            }
            int num = TradeChartTickCalculator._dateDeltas.IndexOf(timeSpan1) - 2;
            if ( num < 0 )
            {
                NiceDoubleScale niceDoubleScale1 = new NiceDoubleScale(0, (double)max.Ticks, minorsPerMajor, (uint)maxAutoTicks);
                niceDoubleScale1.CalculateDelta();
                DoubleAxisDelta doubleAxisDeltum = niceDoubleScale1.TickSpacing;
                timeSpanDeltum = new TimeSpanDelta( TimeSpan.FromTicks( ( long ) doubleAxisDeltum.MinorDelta ), TimeSpan.FromTicks( ( long ) doubleAxisDeltum.MajorDelta ) );
            }
            else
            {
                timeSpanDeltum = new TimeSpanDelta( TradeChartTickCalculator._dateDeltas[ num ], timeSpan1 );
            }
            double[] doubleArray = (new DateTimeTickProvider()).GetMajorTicks(new TradeChartParams()

            {
                VisibleRange = dateRange,
                MajorDelta = timeSpanDeltum.MajorDelta,
                MinorDelta = timeSpanDeltum.MinorDelta
            }).ToDoubleArray<IComparable>();
            int[] array = (
                from x in doubleArray
                select coordCalc.TransformDataToIndex(x.ToDateTime())).ToArray<int>();
            float[] singleArray = (
                from x in doubleArray
                select (float)coordCalc.GetCoordinate((double)coordCalc.TransformDataToIndex(x.ToDateTime()))).ToArray<float>();
            int[] minorTickIndices = TradeChartTickCalculator.GetMinorTickIndices(indexRange, array, minorsPerMajor);
            double[] numArray = (
                from x in (IEnumerable<int>)minorTickIndices
                select coordCalc.TransformIndexToData(x).ToDouble()).ToArray<double>();
            float[] array1 = (
                from x in (IEnumerable<int>)minorTickIndices
                select (float)coordCalc.GetCoordinate((double)x)).ToArray<float>();
            return new TickCoordinates( numArray, doubleArray, array1, singleArray );
        }
    }
}
