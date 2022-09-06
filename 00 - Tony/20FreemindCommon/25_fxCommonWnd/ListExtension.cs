using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Common
{
    public static class ListExtension
    {
        public static void BubbleSort( this IList<SRlevel> o )
        {
            for ( int i = o.Count - 1; i >= 0; i-- )
            {
                for ( int j = 1; j <= i; j++ )
                {
                    object o1 = o[ j - 1 ];
                    object o2 = o[ j ];
                    if ( ( ( IComparable )o1 ).CompareTo( o2 ) > 0 )
                    {
                        o.Remove( ( SRlevel ) o1 );
                        o.Insert( j, ( SRlevel )o1 );
                    }
                }
            }
        }
    }
}
