using Ecng.Collections;
using Ecng.Common;
using StockSharp.Algo;
using StockSharp.Algo.History.Russian.Finam;
using StockSharp.Algo.Storages;
using StockSharp.BusinessEntities;
using StockSharp.Localization;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

internal sealed class Class27
{
    private static readonly SynchronizedDictionary< TimeSpan, int > synchronizedDictionary_0 = new SynchronizedDictionary< TimeSpan, int >( )
  {
    {
      TimeSpan.Zero,
      1
    },
    {
      TimeSpan.FromMinutes( 1.0 ),
      2
    },
    {
      TimeSpan.FromMinutes( 5.0 ),
      3
    },
    {
      TimeSpan.FromMinutes( 10.0 ),
      4
    },
    {
      TimeSpan.FromMinutes( 15.0 ),
      5
    },
    {
      TimeSpan.FromMinutes( 30.0 ),
      6
    },
    {
      TimeSpan.FromMinutes( 60.0 ),
      7
    },
    {
      TimeSpan.FromDays( 1.0 ),
      8
    }
  };
    private readonly Dictionary< Class27.Enum0, Func< string > > dictionary_0 = new Dictionary< Class27.Enum0, Func< string > >( );

    public Uri method_0( )
    {
        return ( "http://export.finam.ru/TempData.txt?d=d&f=TempData&e=.txt&dtf=1&tmf=1&MSOR=0&cn=SECURITYCODE&sep=3&sep2=1&" + this.dictionary_0.Select< KeyValuePair< Class27.Enum0, Func< string > >, string >( Class27.Class29.func_0 ?? ( Class27.Class29.func_0 = new Func< KeyValuePair< Class27.Enum0, Func< string > >, string >( Class27.Class29.class29_0.method_0 ) ) ).Join( "&" ) ).To< Uri >( );
    }

    public Class27 method_1( DateTime dateTime_0 )
    {
        this.dictionary_0[0] = new Func< string >( new Class27.Class32( )
    {
      dateTime_0 = dateTime_0
    }.method_0 );
        return this;
    }

    public Class27 method_2( DateTime dateTime_0 )
    {
        this.dictionary_0[ ( Class27.Enum0 ) 1 ] = new Func< string >( new Class27.Class28( )
    {
      dateTime_0 = dateTime_0
    }.method_0 );
        return this;
    }

    public Class27 method_3( TimeSpan timeSpan_0, bool bool_0 )
    {
        Class27.Class30 class30 = new Class27.Class30( );
        class30.timeSpan_0 = timeSpan_0;
        class30.bool_0 = bool_0;
        if( !Class27.synchronizedDictionary_0.TryGetValue( class30.timeSpan_0, out class30.int_0 ) )
        {
            throw new ArgumentOutOfRangeException( "tf", class30.timeSpan_0, LocalizedStrings.Str2101 );
        }

        this.dictionary_0[ ( Class27.Enum0 ) 2 ] = new Func< string >( class30.method_0 );
        return this;
    }

    public Class27 method_4( Security security_0, INativeIdStorage inativeIdStorage_0 )
    {
        if( security_0 == null )
        {
            throw new ArgumentNullException( "security" );
        }

        if( inativeIdStorage_0 == null )
        {
            throw new ArgumentNullException( "nativeIdStorage" );
        }

        Tuple< long, long > bySecurityId = ( Tuple< long, long > ) inativeIdStorage_0.TryGetBySecurityId( "Finam", security_0.ToSecurityId( null ) );
        if( bySecurityId == null )
        {
            throw new ArgumentException( LocalizedStrings.Str2099Params.Put( security_0, "Finam" ) );
        }

        return this.method_6( bySecurityId.Item1, bySecurityId.Item2 );
    }

    public Class27 method_5( FinamSecurityInfo finamSecurityInfo_0 )
    {
        if( finamSecurityInfo_0 == null )
        {
            throw new ArgumentNullException( "securityInfo" );
        }

        return this.method_6( finamSecurityInfo_0.FinamMarketId, finamSecurityInfo_0.FinamSecurityId );
    }

    private Class27 method_6( long long_0, long long_1 )
    {
        this.dictionary_0[ ( Class27.Enum0 ) 3 ] = new Func< string >( new Class27.Class31( )
    {
      long_0 = long_0,
      long_1 = long_1
    }.method_0 );
        return this;
    }

    private sealed class Class28
    {
        public DateTime dateTime_0;

        internal string method_0( )
        {
            return "dt={0}&mt={1}&yt={2}".Put( this.dateTime_0.Day.ToString( "#0" ), ( this.dateTime_0.Month - 1 ).ToString( "#0" ), this.dateTime_0.Year.ToString( "0000" ) );
        }
    }

    private enum Enum0
    {
    }

    [Serializable]
  private sealed class Class29
  {
      public static readonly Class27.Class29 class29_0 = new Class27.Class29( );
      public static Func< KeyValuePair< Class27.Enum0, Func< string > >, string > func_0;

      internal string method_0(
      KeyValuePair< Class27.Enum0, Func< string > > keyValuePair_0 )
      {
          return keyValuePair_0.Value( );
      }
  }

    private sealed class Class30
    {
        public int int_0;
        public TimeSpan timeSpan_0;
        public bool bool_0;

        internal string method_0( )
        {
            return "p={0}&datf={1}".Put( int_0, this.timeSpan_0 == TimeSpan.Zero ? ( this.bool_0 ? 12 : 11 ) : 5 );
        }
    }

    private sealed class Class31
    {
        public long long_0;
        public long long_1;

        internal string method_0( )
        {
            return string.Format( "m={0}&em={1}", long_0, long_1 );
        }
    }

    private sealed class Class32
    {
        public DateTime dateTime_0;

        internal string method_0( )
        {
            return "df={0}&mf={1}&yf={2}".Put( this.dateTime_0.Day.ToString( "#0" ), ( this.dateTime_0.Month - 1 ).ToString( "#0" ), this.dateTime_0.Year.ToString( "0000" ) );
        }
    }
}
