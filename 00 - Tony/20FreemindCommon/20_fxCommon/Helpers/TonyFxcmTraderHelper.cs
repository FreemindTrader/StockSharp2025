using Ecng.Collections;
using StockSharp.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Common
{
    public static class TonyFxcmTraderHelper
    {
        public static IEnumerable<ExecutionMessage> ToFxcmTicks( this IEnumerable<Level1ChangeMessage> level1 )
        {
            return new FxcmTickEnumerable( level1 );
        }

        public static ExecutionMessage ToFxcmTick( this Level1ChangeMessage level1 )
        {
            if ( level1 == null )
                throw new ArgumentNullException( nameof( level1 ) );

            var bestBid = ( decimal? )level1.Changes.TryGetValue( Level1Fields.BestBidPrice );
            var bestAsk = ( decimal? )level1.Changes.TryGetValue( Level1Fields.BestAskPrice );

            var price = ( bestBid + bestAsk ) / 2;

            return new ExecutionMessage
            {
                DataTypeEx = DataType.Ticks,
                SecurityId    = level1.SecurityId,
                TradeId       = ( long? )level1.Changes.TryGetValue( Level1Fields.LastTradeId ),
                TradePrice    = price,
                ServerTime    = ( DateTimeOffset? )level1.Changes.TryGetValue( Level1Fields.LastTradeTime ) ?? level1.ServerTime,
                LocalTime     = level1.LocalTime,
            };
        }

        private class FxcmTickEnumerable : SimpleEnumerable<ExecutionMessage>//, IEnumerableEx<ExecutionMessage>
        {
            private class FxcmTickEnumerator : IEnumerator<ExecutionMessage>
            {
                private readonly IEnumerator<Level1ChangeMessage> _level1Enumerator;

                public FxcmTickEnumerator( IEnumerator<Level1ChangeMessage> level1Enumerator )
                {
                    _level1Enumerator = level1Enumerator ?? throw new ArgumentNullException( nameof( level1Enumerator ) );
                }

                public ExecutionMessage Current { get; private set; }

                public void Reset( )
                {
                    _level1Enumerator.Reset( );
                    Current = null;
                }



                object IEnumerator.Current => Current;

                void IDisposable.Dispose( )
                {
                    Current = null;
                    _level1Enumerator.Dispose( );
                }

                public bool MoveNext( )
                {
                    while ( _level1Enumerator.MoveNext( ) )
                    {
                        var level1 = _level1Enumerator.Current;

                        //if ( !level1.IsContainsTick( ) )
                        //    continue;

                        Current = level1.ToFxcmTick( );
                        return true;
                    }

                    Current = null;
                    return false;
                }
            }

            //private readonly IEnumerable<Level1ChangeMessage> _level1;

            public FxcmTickEnumerable( IEnumerable<Level1ChangeMessage> level1 )
                : base( ( ) => new FxcmTickEnumerator( level1.GetEnumerator( ) ) )
            {
                if ( level1 == null )
                    throw new ArgumentNullException( nameof( level1 ) );

                //_level1 = level1;
            }



            //int IEnumerableEx.Count => _level1.Count;
        }

    }
}
