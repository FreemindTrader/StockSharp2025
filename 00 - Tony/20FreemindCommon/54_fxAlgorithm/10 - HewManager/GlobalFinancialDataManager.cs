using fx.Collections;
using fx.Database;
using Ecng.Collections;

using fx.Definitions;
using StockSharp.Algo;
using StockSharp.BusinessEntities;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fx.Bars;

namespace fx.Algorithm
{
    /// <summary>
    /// Class contains general helper functionality for financial classes and operation.
    /// </summary>
    public static class GFMgr
    {
        private readonly static ThreadSafeDictionary< string, AdvancedAnalysisManager >_taManagerForSymbol          = new ThreadSafeDictionary< string, AdvancedAnalysisManager >( 5 );

        public static AdvancedAnalysisManager GetTAManager( Security instructment )
        {
            var symbol = instructment.Code;

            return GetTAManager( symbol );
        }

        public static AdvancedAnalysisManager GetTAManager( string symbol )
        {
            AdvancedAnalysisManager result = null;

            

            if ( _taManagerForSymbol.TryGetValue( symbol, out result ) )
            {
                return result;
            }

            var taManager = new AdvancedAnalysisManager( symbol );

            if ( _taManagerForSymbol.TryAdd( symbol, taManager ) )
            {
                return taManager;
            }
            else
            {
                if ( _taManagerForSymbol.TryGetValue( symbol, out result ) )
                {
                    return result;
                }
            }

            throw new KeyNotFoundException( );
        }

        static GFMgr()
        {
            Initialize();
        }

        // explicit "constructor"
        public static void Initialize()
        {
            
        }


        private readonly static PooledList< DbSymbolsInfo > SymbolsOffer = new PooledList< DbSymbolsInfo >( 65 );

        

        

        private readonly static ThreadSafeDictionary< TimeSpan, fxHistoricBarsRepo > _databarRepos = new ThreadSafeDictionary< TimeSpan, fxHistoricBarsRepo >( );

        public static bool AddDatabarRepo( TimeSpan period, fxHistoricBarsRepo historicBarRepo )
        {
            if( _databarRepos.TryAdd( period, historicBarRepo ) )
            {
                return true;
            }
            else
            {
                if( _databarRepos.ContainsKey( period ) )
                {
                    return true;
                }
            }

            return false;
        }

        public static fxHistoricBarsRepo GetDatabarRepo( TimeSpan period )
        {
            fxHistoricBarsRepo historicBarRepo = null;

            if( _databarRepos.TryGetValue( period, out historicBarRepo ) )
            {
                return historicBarRepo;
            }

            return null;
        }
    }
}