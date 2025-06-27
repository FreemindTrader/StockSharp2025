using SciChart.Charting.ChartModifiers;
using SciChart.Charting.Model.ChartData;
using System;
using System.Collections.Generic; using fx.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace fx.Charting.ATony
{
    public enum Modifier : byte
    {
        Rollover,
        Cursor,
        Tooltip,
        VerticalSlice,
    }

    public static class SeriesInfoUtil
    {
        internal static void RemoveWhere<T>( this IList<T> collection, Predicate<T> predicate )
        {
            for ( int index = 0; index < collection.Count; ++index )
            {
                if ( predicate( collection[ index ] ) )
                {
                    collection.RemoveAt( index-- );
                }
            }
        }

        
    }
}
