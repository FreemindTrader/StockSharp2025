using fx.Collections;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;
using StockSharp.Messages;

namespace fx.Definitions
{
    public static class SymbolHelper
    {
        public static DictionarySlim<string, string > _offerIdSymbol  = new DictionarySlim<string, string>( 24 );
        public static DictionarySlim<string, int >    _symbol2OfferId = new DictionarySlim<string, int>( 24 );

        public static SymbolEx ToSymbolEx( Security sec, TimeSpan period )
        {
            return new SymbolEx( sec, sec.Type.Value, period );
        }

        public static int GetOfferId( Security instrument )
        {
            int offerId = 0;

            if ( _symbol2OfferId.TryGetValue( instrument.Code, out offerId ) )
                return offerId;

            var id = instrument.ToSecurityId( );

            var storage = ServicesRegistry.NativeIdStorage;

            string native = ( string ) storage.TryGetBySecurityId( "FxConnectFXCM", id );

            _symbol2OfferId.GetOrAddValueRef( instrument.Code ) = Int32.Parse( native );

            return Int32.Parse( native );
        }

        public static int GetOfferId( string instrutment )
        {
            int offerId = 0;

            if ( _symbol2OfferId.TryGetValue( instrutment, out offerId ) )
                return offerId;

            var id = new SecurityId
            {
                SecurityCode = instrutment,
                BoardCode = "Fxcm"
            };

            var storage = ServicesRegistry.NativeIdStorage;

            var native = ( string ) storage.TryGetBySecurityId( "FxConnectFXCM", id );

            _symbol2OfferId.GetOrAddValueRef( instrutment ) = Int32.Parse( native );

            return Int32.Parse( native );
        }

        public static string GetSymbolFromOfferId( string offerId )
        {
            string symbol = string.Empty;

            if ( _offerIdSymbol.TryGetValue( offerId, out symbol ) )
                return symbol;

            
            var ids       = ServicesRegistry.NativeIdStorage;

            var secIds = ids.Get( "FxConnectFXCM" );

            foreach ( var id in secIds )
            {
                string secId = ( string ) id.Item2;

                if ( secId == offerId )
                {
                    _offerIdSymbol.GetOrAddValueRef( offerId ) = id.Item1.BoardCode;
                    return id.Item1.BoardCode;
                }
            }

            return symbol;
        }

        public static string GetSymbolFromOfferId( int offerId )
        {            
            string idStr  = offerId.ToString( );

            string symbol = string.Empty;

            if ( _offerIdSymbol.TryGetValue( idStr, out symbol ) )
                return symbol;

            var ids       = ServicesRegistry.NativeIdStorage;

            var secIds = ids.Get( "FxConnectFXCM" );

            SecurityId matched = default;

            foreach ( var id in secIds )
            {
                string secId = ( string ) id.Item2;

                if ( secId == idStr )
                {
                    matched = id.Item1;

                    _offerIdSymbol.GetOrAddValueRef( idStr ) = matched.SecurityCode;

                    return matched.SecurityCode;
                }
            }

            return symbol;
        }

        public static int GetInstrumentDigits( string instrument )
        {
            var secs = ServicesRegistry.EntityRegistry.Securities;

            foreach ( var sec in secs )
            {
                var code = sec.Code;

                if ( instrument == code )
                {
                    return ( int ) sec.Decimals;
                }

            }

            return ( 0 );
        }

        public static float GetInstrumentPointSize( int offerId )
        {
            var symbol = GetSymbolFromOfferId( offerId );

            if ( !string.IsNullOrEmpty( symbol ) )
            {
                var secs = ServicesRegistry.EntityRegistry.Securities;

                foreach ( var sec in secs )
                {
                    var code = sec.Code;

                    if ( symbol == code )
                    {
                        return ( float ) sec.PriceStep;
                    }
                }
            }

            return 0.0001f;
        }

        public static double GetInstrumentPointSize( string instrument )
        {
            var secs = ServicesRegistry.EntityRegistry.Securities;

            foreach ( var sec in secs )
            {
                var code = sec.Code;

                if ( instrument == code )
                {
                    return ( double ) sec.PriceStep;
                }
            }

            return 0.0001;
        }
    }
}
