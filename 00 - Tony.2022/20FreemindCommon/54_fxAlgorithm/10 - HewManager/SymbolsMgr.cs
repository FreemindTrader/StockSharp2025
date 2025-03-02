using fx.Collections;
using fx.Database;
using Ecng.Collections;

using fx.Common;
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
using fx.TimePeriod;

namespace fx.Algorithm
{
    /// <summary>
    /// This class will contain everything related to the symbols
    /// 
    /// 1) FreemindIndicator
    /// 2) 
    /// </summary>
    public class SymbolsMgr : ISymbolsMgr
    {
        private static SymbolsMgr _instance=null;

        public static SymbolsMgr Instance
        {
            get
            {
                if ( _instance == null )
                {
                    _instance = new SymbolsMgr();
                }
                return _instance;
            }
        }

        private readonly  ThreadSafeDictionary< string, AdvancedAnalysisManager >_taManagerForSymbol = new ThreadSafeDictionary< string, AdvancedAnalysisManager >( 5 );
        
        private readonly  ThreadSafeDictionary< string, ThreadSafeDictionary< TimeSpan, fxHistoricBarsRepo > >    _databarRepos = new ThreadSafeDictionary< string, ThreadSafeDictionary< TimeSpan, fxHistoricBarsRepo > >( );

        //public  BarInfoManager CreateBarInfoManager( SymbolEx symbol )
        //{
        //    BarInfoManager result = null;

        //    if ( _barInfoForSymbolEx.TryGetValue( symbol, out result ) )
        //    {
        //        return result;
        //    }

        //    var barManager = new BarInfoManager( );

        //    if ( _barInfoForSymbolEx.TryAdd( symbol, barManager ) )
        //    {
        //        return barManager;
        //    }
        //    else
        //    {
        //        if ( _barInfoForSymbolEx.TryGetValue( symbol, out result ) )
        //        {
        //            return result;
        //        }
        //    }

        //    throw new KeyNotFoundException();
        //}

        //public  BarInfoManager CreateBarInfoManager( string symbol )
        //{
        //    BarInfoManager result = null;

        //    if ( _barInfoForSymbol.TryGetValue( symbol, out result ) )
        //    {
        //        return result;
        //    }

        //    var barManager = new BarInfoManager( );

        //    if ( _barInfoForSymbol.TryAdd( symbol, barManager ) )
        //    {
        //        return barManager;
        //    }
        //    else
        //    {
        //        if ( _barInfoForSymbol.TryGetValue( symbol, out result ) )
        //        {
        //            return result;
        //        }
        //    }

        //    throw new KeyNotFoundException();
        //}

        //public  BarInfoManager BarInfo( Security instructment )
        //{
        //    var symbol = instructment.Code;

        //    return BarInfo( symbol );
        //}

        //public  BarInfoManager BarInfo( string symbol )
        //{
        //    BarInfoManager result = null;

        //    if ( _barInfoForSymbol.TryGetValue( symbol, out result ) )
        //    {
        //        return result;
        //    }

        //    return null;
        //}

        public IAdvancedAnalysisManager GetOrCreateAdvancedAnalysis( string symbol )
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

        public IAdvancedAnalysisManager GetOrCreateAdvancedAnalysis( Security instructment )
        {
            var symbol = instructment.Code;

            return GetOrCreateAdvancedAnalysis( symbol );
        }

        public IAdvancedAnalysisManager GetOrCreateAdvancedAnalysis( SymbolEx instructment )
        {            
            var symbol = SymbolHelper.GetSymbolFromOfferId( instructment.OfferId );

            return GetOrCreateAdvancedAnalysis( symbol );
        }

        

        public  void RemoveSymbolsData( string symbol )
        {
            if ( _taManagerForSymbol.ContainsKey( symbol ) )
            {
                _taManagerForSymbol.Remove( symbol );
            }

            if ( _databarRepos.ContainsKey( symbol ) )
            {
                _databarRepos.Remove( symbol );
            }
        }

        private readonly  PooledList< DbSymbolsInfo > SymbolsOffer = new PooledList< DbSymbolsInfo >( 65 );

        public  int GetOfferId( Security instrument )
        {
            var id = instrument.ToSecurityId( );

            var storage = ServicesRegistry.NativeIdStorage;

            string native = ( string ) storage.TryGetBySecurityId( "FxConnectFXCM", id );

            return Int32.Parse( native );
        }

        public  int GetOfferId( string instrutment )
        {
            var id = new SecurityId
            {
                SecurityCode = instrutment,
                BoardCode = "Fxcm"
            };

            var storage = ServicesRegistry.NativeIdStorage;

            var native = ( string ) storage.TryGetBySecurityId( "FxConnectFXCM", id );

            return Int32.Parse( native );
        }

        

        public  fxHistoricBarsRepo CreateOrGetDatabarRepo( Security security, TimeSpan period )
        {
            ThreadSafeDictionary< TimeSpan, fxHistoricBarsRepo > symbolDatabars = null;

            if ( _databarRepos.TryGetValue( security.Code, out symbolDatabars ) )
            {
                fxHistoricBarsRepo bars = null;

                if ( symbolDatabars.TryGetValue( period, out bars ) )
                {
                    return bars;
                }

                var aa = Instance.GetOrCreateAdvancedAnalysis( security.Code );             

                var indicator = new IndicatorManager( );

                var newDatabarRepo = new fxHistoricBarsRepo( security, period, aa.HewManager, indicator );

                indicator.SelectedPeriodXProvider = newDatabarRepo;

                if ( symbolDatabars.TryAdd( period, newDatabarRepo ) )
                {
                    return newDatabarRepo;
                }
            }
            else
            {
                symbolDatabars = new ThreadSafeDictionary< TimeSpan, fxHistoricBarsRepo >( );

                var aa = Instance.GetOrCreateAdvancedAnalysis( security.Code );

                var indicator = new IndicatorManager( );

                var newDatabarRepo = new fxHistoricBarsRepo( security, period, aa.HewManager, indicator );

                indicator.SelectedPeriodXProvider = newDatabarRepo;

                if ( symbolDatabars.TryAdd( period, newDatabarRepo ) )
                {
                    if( _databarRepos.TryAdd( security.Code, symbolDatabars ) )
                    {
                        return newDatabarRepo;
                    }
                }
            }

            throw new InvalidProgramException( );
        }
        

        //public  fxHistoricBarsRepo GetDatabarRepo( TimeSpan period )
        //{
        //    fxHistoricBarsRepo historicBarRepo = null;

        //    if( _databarRepos.TryGetValue( period, out historicBarRepo ) )
        //    {
        //        return historicBarRepo;
        //    }

        //    return null;
        //}

        public  fxHistoricBarsRepo GetDatabarRepo( Security security, TimeSpan period )
        {
            ThreadSafeDictionary< TimeSpan, fxHistoricBarsRepo > symbolDatabars = null;

            if ( _databarRepos.TryGetValue( security.Code, out symbolDatabars ) )
            {
                fxHistoricBarsRepo bars = null;

                if ( symbolDatabars.TryGetValue( period, out bars ) )
                {
                    return bars;
                }                
            }

            return null;
        }

        public  fxHistoricBarsRepo GetDatabarRepo( SymbolEx security, TimeSpan period )
        {
            ThreadSafeDictionary< TimeSpan, fxHistoricBarsRepo > symbolDatabars = null;

            string symbol = SymbolHelper.GetSymbolFromOfferId( security.OfferId );

            if ( _databarRepos.TryGetValue( symbol, out symbolDatabars ) )
            {
                fxHistoricBarsRepo bars = null;

                if ( symbolDatabars.TryGetValue( period, out bars ) )
                {
                    return bars;
                }
            }

            return null;
        }

        public  fxHistoricBarsRepo GetDatabarRepo( string security, TimeSpan period )
        {
            ThreadSafeDictionary< TimeSpan, fxHistoricBarsRepo > symbolDatabars = null;

            if ( _databarRepos.TryGetValue( security, out symbolDatabars ) )
            {
                fxHistoricBarsRepo bars = null;

                if ( symbolDatabars.TryGetValue( period, out bars ) )
                {
                    return bars;
                }
            }


            throw null;
        }

        public  void UpdateMacd( Security security, TimeSpan period, DateTime? lastBarTime, double macd, double macdSig )
        {            
            var aa    = ( AdvancedAnalysisManager )Instance.GetOrCreateAdvancedAnalysis( security );                            
            aa.RaiseMacdValueChange( security, period, (macd, macdSig), lastBarTime != null ? lastBarTime.Value : DateTime.MinValue );
            
        }

        public  void UpdateSma( Security security, TimeSpan period, DateTime? lastBarTime  )
        {
            var aa    = ( AdvancedAnalysisManager )Instance.GetOrCreateAdvancedAnalysis( security );

            if( aa == null )
                return;

            var ta    = aa.TradingEventsBindingList;

            var sr    = aa.SupportResistanceBindingList;

            var sma50 = aa.GetCurrentSmaValue( period );      
            
            if ( sma50 > -1 )
            {
                sr.AddSRline( new SRlevel( ref TimeBlockEx.EmptyBlock, period, sma50, 0, SR1stType.MA, period.SmaToSr2nd( ), SR3rdType.SMA50 ), security.Decimals.Value );

                aa.RaiseSmaValueChange( security, period, sma50, lastBarTime != null ? lastBarTime.Value : DateTime.MinValue );
            }            
        }
    }
}