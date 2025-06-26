using System;
using fx.Definitions;
using SciChart.Charting.Model.DataSeries;
using fx.Collections;
using Ecng.Configuration;
using fx.Bars;
using System.Diagnostics;

#pragma warning disable 067

namespace fx.Bars
{
    [DebuggerDisplay( "{DebuggerDisplay,nq}" )]
    public partial struct SBar
    {
        public static HewLong EmptyHew = new HewLong( );

        public ref HewLong MainElliottWave
        {
            get
            {
                if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
                {
                    return ref info.MainElliottWave;
                }

                return ref AdvBarInfo.EmptyHew;
            }
        }

        public UpperShadowRangeF UpperShadow
        {
            get
            {
                var output = UpperShadowRangeF.Create( ( Close >= Open ? Close : Open ), High, SymbolHelper.GetInstrumentPointSize( SymbolEx.OfferId ) );
                return output;
            }
        }

        public LowerShadowRangeF LowerShadow
        {
            get
            {
                var output = LowerShadowRangeF.Create( Low, ( Close >= Open ? Open : Close ), SymbolHelper.GetInstrumentPointSize( SymbolEx.OfferId )  );
                return output;
            }
        }

        public RealBodyRangeF RealBody
        {
            get
            {
                if ( Close > Open )
                {
                    return RealBodyRangeF.Create( Open, Close );
                }
                else
                {
                    return RealBodyRangeF.Create( Close, Open );
                }
            }
        }

        public WholeCandleRangeF WholeCandle
        {
            get
            {
                var output = WholeCandleRangeF.Create( Low, High );
                return output;
            }
        }


        public float UpperShadowLength => ( High - ( Close >= Open ? Close : Open ) );

        public float PointSize
        {
            get
            {
                return SymbolHelper.GetInstrumentPointSize( SymbolEx.OfferId );
            }
        }

        public float UpperShadowLengthAsPip
        {
            get
            {
                var instrumentPointSize = PointSize;

                if ( instrumentPointSize > 0 )
                {
                    return ( float ) Math.Round( UpperShadowLength / instrumentPointSize, 1 );
                }

                return -1;
            }
        }

        public float LowerShadowLength => ( ( Close >= Open ? Open : Close ) - Low );

        public float LowerShadowLengthAsPip
        {
            get
            {
                var instrumentPointSize = PointSize;

                if ( instrumentPointSize > 0 )
                {
                    return ( float ) Math.Round( LowerShadowLength / instrumentPointSize, 1 );
                }

                return -1;
            }
        }

        public float RealBodyLength => ( Close > Open ) ? ( Close - Open ) : ( Open - Close );

        public float RealBodyAsPip
        {
            get
            {
                var instrumentPointSize = PointSize;

                if ( instrumentPointSize > 0 )
                {
                    return ( float ) Math.Round( RealBodyLength / instrumentPointSize, 1 );
                }

                return -1;
            }
        }

        public float CandleLength => ( High - Low );

        public float CandleLengthAsPip
        {
            get
            {
                var instrumentPointSize = PointSize;

                if ( instrumentPointSize > 0 )
                {
                    return ( float ) Math.Round( CandleLength / instrumentPointSize, 1 );
                }

                return -1;
            }
        }

        public int CandleColor => ( Close > Open ? 1 : -1 );

        public bool HasCandleStickPattern => CandlePatterns != TACandle.NONE;

        #region NOT YET DONE


        public string CandleStickPatten
        {
            get
            {
                return CandlePatterns.ToDescription();
            }
        }

        public void ClearPattern()
        {
            CandlePatterns = 0;
        }

        public TACandle CandlePatterns
        {
            get
            {
                if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
                {
                    return info.CandlePatterns;
                }
                
                return TACandle.NONE;
            }

            set
            {
                AdvBarInfo info = _parent.GetOrAllocate( _barIndex );

                info.CandlePatterns = value;                
            }
        }

        #endregion

        public bool HasSignal => Features.HasSignal;

        public bool HasDivergence => Features.HasDivergence;

        public bool HasWaveRotation => Features.HasWaveRotation;

        public bool HasGannSquare => Features.HasGannSquare;

        public bool HasPivotRelation => Features.HasPivotRelation;

        public void AddSignal( TASignal signal )
        {
            var dataBarSignals = Features.TechnicalAnalysisSignal;

            var newSignal = dataBarSignals | signal;

            Features.TechnicalAnalysisSignal = newSignal;
        }


        public bool HasStructureLabel
        {
            get
            {
                return Features.HasStructureLabel;
            }

            set
            {
                Features.HasStructureLabel = value;
            }
        }


        public TASignal TechnicalAnalysisSignal
        {
            get
            {


                return Features.TechnicalAnalysisSignal;
            }

            set
            {
                Features.TechnicalAnalysisSignal = value;
            }
        }

        public string BarSignal
        {
            get
            {
                return GetBarSignalString();
            }
        }

        public string GetBarSignalString()
        {
            var dataBarSignals = Features.TechnicalAnalysisSignal;

            if ( dataBarSignals != 0 )
            {
                string output = "\r\n" + dataBarSignals.ToDescription( );

                var symbolMgr = ConfigManager.GetService<ISymbolsMgr>();

                if ( symbolMgr == null ) return output;



                var aa = symbolMgr.GetOrCreateAdvancedAnalysis( SymbolEx );

                IHewManager waveManager = null;

                if ( aa != null )
                {
                    waveManager = aa.HewManager;
                }
                else
                {
                    return "";
                }

                bool found = false;

                if ( dataBarSignals.HasFlag( TASignal.WAVE_PEAK ) || dataBarSignals.HasFlag( TASignal.WAVE_TROUGH ) )
                {
                    var importance = waveManager.GetWaveImportanceExt( SymbolEx.Period, LinuxTime );

                    if ( importance != null )
                    {
                        output += "\r\n";
                        output += "Wave Impt = " + importance.WaveImportance.ToString() + "\r\n";
                        output += "Wave Htf  = " + importance.HighestTimeframe.ToShortForm() + "\r\n";
                        found = true;
                    }
                }

                if ( dataBarSignals.HasFlag( TASignal.GANN_PEAK ) || dataBarSignals.HasFlag( TASignal.GANN_TROUGH ) )
                {
                    if ( !found )
                    {
                        output += "\r\n";
                    }

                    var gannImpt = waveManager.GetGannImportanceExt( SymbolEx.Period, LinuxTime  );

                    if ( gannImpt != null )
                    {
                        output += "Gann Impt = " + gannImpt.WaveImportance.ToString() + "\r\n";
                        output += "Gann Htf  = " + gannImpt.HighestTimeframe.ToShortForm() + "\r\n";
                        found = true;
                    }
                }

                if ( found )
                {
                    output += "\r\n";
                }

                return output;
            }

            return "";
        }

        public void ClearSignal()
        {
            Features.ClearSignal();
        }

        public void RemoveSignals( TASignal signal1, TASignal signal2 )
        {
            var dataBarSignals = Features.TechnicalAnalysisSignal;

            TASignal toBeDisable = signal1 | signal2;

            var newSignal = dataBarSignals & ( ~toBeDisable );

            Features.TechnicalAnalysisSignal = newSignal;
        }

        public void RemoveSignals( TASignal signal )
        {
            var dataBarSignals = Features.TechnicalAnalysisSignal;

            Features.TechnicalAnalysisSignal = dataBarSignals & ( ~signal );
        }

        public int TechnicalAnalysisSignalCount( ref PooledList<TASignal> signalList )
        {
            var dataBarSignals = Features.TechnicalAnalysisSignal;
            if ( dataBarSignals == TASignal.NONE )
            {
                return 0;
            }

            TASignal input = dataBarSignals;

            int count = 0;

            if ( input.HasFlag( TASignal.GANN_PEAK ) )
            {
                signalList.Add( TASignal.GANN_PEAK );
                count++;
            }

            if ( input.HasFlag( TASignal.GANN_TROUGH ) )
            {
                signalList.Add( TASignal.GANN_TROUGH );
                count++;
            }

            if ( input.HasFlag( TASignal.WAVE_PEAK ) )
            {
                signalList.Add( TASignal.WAVE_PEAK );
                count++;
            }

            if ( input.HasFlag( TASignal.WAVE_TROUGH ) )
            {
                signalList.Add( TASignal.WAVE_TROUGH );
                count++;
            }

            if ( input.HasFlag( TASignal.HAS_STRUCT_LABEL ) )
            {
                signalList.Add( TASignal.HAS_STRUCT_LABEL );
                count++;
            }

            if ( input.HasFlag( TASignal.HAS_BOTTOMING_SIGNAL ) )
            {
                signalList.Add( TASignal.HAS_BOTTOMING_SIGNAL );
                count++;
            }
            else if ( input.HasFlag( TASignal.HAS_TOPPING_SIGNAL ) )
            {
                signalList.Add( TASignal.HAS_TOPPING_SIGNAL );
                count++;
            }

            if ( input.HasFlag( TASignal.HAS_DIVERGENCE ) )
            {
                signalList.Add( TASignal.HAS_DIVERGENCE );
                count++;
            }

            if ( input.HasFlag( TASignal.HAS_GANN_SQUARE ) )
            {
                signalList.Add( TASignal.HAS_GANN_SQUARE );
                count++;
            }

            if ( input.HasFlag( TASignal.HAS_PIVOT_RELATION ) )
            {
                signalList.Add( TASignal.HAS_PIVOT_RELATION );
                count++;
            }

            if ( input.HasFlag( TASignal.HAS_TIME_ROTATION ) )
            {
                signalList.Add( TASignal.HAS_TIME_ROTATION );
                count++;
            }

            if ( input.HasFlag( TASignal.ExitOverBought ) )
            {
                signalList.Add( TASignal.ExitOverBought );
                count++;
            }
            else if ( input.HasFlag( TASignal.ExitOverSold ) )
            {
                signalList.Add( TASignal.ExitOverSold );
                count++;
            }

            return count;
        }

        public DateTime BarTime => LinuxTime.FromLinuxTime();

        public bool isValidBar
        {
            get
            {
                return Open != 0 && Close != 0 && High != 0 && Low != 0;
            }
        }

        public (DateTime, float) OpenTimeValue => (BarTime, ( float ) Open);

        public (DateTime, float) CloseTimeValue => (BarTime, ( float ) Close);

        public (DateTime, float) HighTimeValue => (BarTime, ( float ) High);

        public (DateTime, float) LowTimeValue => (BarTime, ( float ) Low);

        public DateTime LocalTime
        {
            get
            {
                DateTime output = BarTime;

                string offerId = SymbolHelper.GetSymbolFromOfferId( SymbolEx.OfferId );

                switch ( offerId )
                {
                    case "EUR/USD":
                    {
                        return LondonTime;
                    }

                    case "USD/CAD":
                    case "USD/MXN":
                    {
                        return NewYorkTime;
                    }

                    case "USD/SEK":
                    case "USD/DDK":
                    case "USD/NOK":
                    case "USD/PLN":
                    case "USD/CZK":
                    case "USD/ZAR":
                    case "USD/TRY":
                    case "USD/HUF":
                    case "USD/CHF":
                    {
                        return FrankfurtTime;
                    }

                    case "AUD/USD":
                    {
                        return WellingtonTime;
                    }

                    case "USD/SGD":
                    case "USD/CNH":
                    case "USD/HKD":
                    {
                        return ChinaTime;
                    }

                    case "USD/JPY":
                    {
                        return TokyoTime;
                    }

                    case "GBP/USD":
                    {
                        return LondonTime;
                    }

                    default:
                    {
                        return NewYorkTime;
                    }

                }
            }
        }

        public DateTime LondonTime
        {
            get
            {
                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById( "GMT Standard Time" );
                DateTime barTime = TimeZoneInfo.ConvertTimeFromUtc( BarTime, tz );

                return barTime;
            }
        }

        public DateTime ChinaTime
        {
            get
            {
                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById( "China Standard Time" );
                DateTime barTime = TimeZoneInfo.ConvertTimeFromUtc( BarTime, tz );

                return barTime;
            }
        }

        public DateTime FrankfurtTime
        {
            get
            {
                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById( "Central European Standard Time" );
                DateTime barTime = TimeZoneInfo.ConvertTimeFromUtc( BarTime, tz );

                return barTime;
            }
        }

        public DateTime NewYorkTime
        {
            get
            {
                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById( "Eastern Standard Time" );
                DateTime barTime = TimeZoneInfo.ConvertTimeFromUtc( BarTime, tz );

                return barTime;
            }
        }

        public DateTime WellingtonTime
        {
            get
            {
                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById( "New Zealand Standard Time" );
                DateTime barTime = TimeZoneInfo.ConvertTimeFromUtc( BarTime, tz );

                return barTime;
            }
        }

        public DateTime TokyoTime
        {
            get
            {
                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById( "Tokyo Standard Time" );
                DateTime barTime = TimeZoneInfo.ConvertTimeFromUtc( BarTime, tz );

                return barTime;
            }
        }

        public TimeSpan BarPeriod => SymbolEx.Period;


        public double GetValue( DataBarProperty valueSource )
        {
            switch ( valueSource )
            {
                case DataBarProperty.High:
                    return High;

                case DataBarProperty.Low:
                    return Low;

                case DataBarProperty.Open:
                    return Open;

                case DataBarProperty.Volume:
                    return Volume;

                case DataBarProperty.Close:
                    return Close;
            }

            return 0;
        }

        

        public bool HasElliottWave => Features.HasElliottWave;

        public bool HasFibRetExpInfo => Features.HasFibRetExpInfo;

        public string WaveString
        {
            get
            {
                string waveInfo = string.Empty;
                

                if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
                {
                    if ( info.MainElliottWave.Count > 0 )
                    {
                        string replacement = FinancialHelper.GetHarmonicWaveStringHtml( info.MainElliottWave );

                        waveInfo = "\r\nMain: " + replacement;
                    }

                    if ( info.AltWaveScenario01.Count > 0 )
                    {
                        string replacement = FinancialHelper.GetHarmonicWaveStringHtml( info.AltWaveScenario01 );

                        waveInfo += "\r\nAlt: " + replacement;
                    }

                    if ( info.AltWaveScenario02.Count > 0 )
                    {
                        string replacement = FinancialHelper.GetHarmonicWaveStringHtml( info.AltWaveScenario02 );

                        waveInfo += "\r\nAlt: " + replacement;
                    }

                    if ( info.AltWaveScenario03.Count > 0 )
                    {
                        string replacement = FinancialHelper.GetHarmonicWaveStringHtml( info.AltWaveScenario02 );

                        waveInfo += "\r\nAlt: " + replacement;
                    }
                }

                return waveInfo;
            }
        }


        public WavePriceTimeInfo MainPriceTimeInfo
        {
            get
            {
                if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
                {
                    return info.MainPriceTime;
                }                

                return AdvBarInfo.EmptyFibR;
            }

            set
            {
                AdvBarInfo info = _parent.GetOrAllocate( _barIndex );

                info.MainPriceTime = value;                
            }
        }

        public WavePriceTimeInfo Atl01PriceTimeInfo
        {
            get
            {
                if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
                {
                    return info.AltPriceTime01;
                }

                return AdvBarInfo.EmptyFibR;
            }

            set
            {
                AdvBarInfo info = _parent.GetOrAllocate( _barIndex );

                info.AltPriceTime01 = value;
            }
        }

        public WavePriceTimeInfo Atl02PriceTimeInfo
        {
            get
            {
                if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
                {
                    return info.AltPriceTime02;
                }

                return AdvBarInfo.EmptyFibR;
            }

            set
            {
                AdvBarInfo info = _parent.GetOrAllocate( _barIndex );

                info.AltPriceTime02 = value;
            }
        }

        public WavePriceTimeInfo Atl03PriceTimeInfo
        {
            get
            {
                if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
                {
                    return info.AltPriceTime03;
                }

                return AdvBarInfo.EmptyFibR;
            }

            set
            {
                AdvBarInfo info = _parent.GetOrAllocate( _barIndex );

                info.AltPriceTime03 = value;
            }
        }


        public ref HewLong AltElliottWave01
        {
            get
            {
                if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
                {
                    return ref info.AltWaveScenario01;
                }

                return ref AdvBarInfo.EmptyHew;                
            }
        }

        public ref HewLong AltElliottWave02
        {
            get
            {
                if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
                {
                    return ref info.AltWaveScenario02;
                }

                return ref AdvBarInfo.EmptyHew;
            }
        }

        public ref HewLong AltElliottWave03
        {
            get
            {
                if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
                {
                    return ref info.AltWaveScenario03;
                }

                return ref AdvBarInfo.EmptyHew;
            }
        }


        public WaveDirtyEnum WaveDirty
        {
            get
            {
                if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
                {
                    return info.WaveDirty;
                }

                return WaveDirtyEnum.NONE;                
            }

            set
            {
                AdvBarInfo info = _parent.GetOrAllocate( _barIndex );

                info.WaveDirty = value;                
            }
        }

        public string WaveRotationInfoString
        {
            get
            {
                string output = "\r\n";

                //var symbolMgr = ConfigManager.GetService<ISymbolsMgr>();

                //if ( symbolMgr == null ) return output;

                //var aa = symbolMgr.GetOrCreateAdvancedAnalysis( this.SymbolEx );

                //IPeriodXTaManager taManager = null;

                //if ( aa != null )
                //{
                //    taManager = aa.GetPeriodXTa( SymbolEx.Period );
                //}
                //else
                //{
                //    return "";
                //}

                //var waveRotations = taManager.GetWaveImportantTimeInfo( ref this );

                //if ( waveRotations != null )
                //{
                //    foreach ( var wave in waveRotations )
                //    {
                //        output += wave.ToString() + "\r\n\r\n";
                //    }
                //}

                return output;
            }
        }


        public string GannSquareInfoString
        {
            get
            {
                string output = "\r\n";

                var symbolMgr = ConfigManager.GetService<ISymbolsMgr>();

                if ( symbolMgr == null ) return output;

                var aa = symbolMgr.GetOrCreateAdvancedAnalysis( SymbolEx );

                IPeriodXTaManager taManager = null;

                if ( aa != null )
                {
                    taManager = aa.GetPeriodXTa( SymbolEx.Period );
                }
                else
                {
                    return "";
                }

                //var gannInfos = taManager.GetGannPriceTimeInfo( ref this );

                //if ( gannInfos != null )
                //{
                //    foreach ( var wave in gannInfos )
                //    {
                //        output += wave.ToString() + "\r\n\r\n";
                //    }
                //}

                return output;
            }
        }

        public string DivergenceInfoString
        {
            get
            {
                string output = "\r\n";

                var symbolMgr = ConfigManager.GetService<ISymbolsMgr>();

                if ( symbolMgr == null ) return output;

                var aa = symbolMgr.GetOrCreateAdvancedAnalysis( SymbolEx );

                IPeriodXTaManager taManager = null;

                if ( aa != null )
                {
                    taManager = aa.GetPeriodXTa( SymbolEx.Period );
                }
                else
                {
                    return "";
                }

                //var divergences = taManager.GetDivergenceInfo( ref this );

                //if ( divergences != null )
                //{
                //    foreach ( var divergence in divergences )
                //    {
                //        output += divergence.ToString() + "\r\n";
                //    }
                //}

                return output;
            }
        }

        public string BaZiString
        {
            get
            {
                if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
                {
                    return info.BaZiString;
                }

                return null;
            }

            set
            {
                AdvBarInfo info = _parent.GetOrAllocate( _barIndex );

                info.BaZiString = value;
            }            
        }

        public bool IsSpecialBar
        {
            get
            {
                if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
                {
                    return info.IsSpecial;
                }

                return false;
            }

            set
            {
                AdvBarInfo info = _parent.GetOrAllocate( _barIndex );

                info.IsSpecial = value;
            }            
        }
        public bool SelectionChanged { get => Features.SelectionChanged; set => Features.SelectionChanged = value; }

        public bool HasBaZi { get => Features.HasBazi; set => Features.HasBazi = value; }

        public double PeakTroughValue
        {
            get
            {
                return IsPeak() ? High : Low;
            }
        }
        

       
        

        //public void DetachWaveFromDatabar()
        //{
        //    if ( SimpleHew.Count > 0 )
        //    {
        //        SimpleHew.ResetWaves();
        //        IsSpecialBar = false;
        //    }

        //    ExtraBarInfo?.DetachWaveFromDatabar();
        //}

        

        

        public PooledList<WaveInfo> GetAllWaves( bool filterWaves, int waveScenarioNo )
        {
            ref var hew = ref GetWaveFromScenario( waveScenarioNo );

            if ( hew == AdvBarInfo.EmptyHew )
            {
                return null;
            }

            if ( filterWaves )
            {
                ElliottWaveCycle wavesToShow = FinancialHelper.GetMinimumWavesToShow( SymbolEx.Period );

                return hew.GetAllWaves( wavesToShow );
            }
            else
            {
                return hew.GetAllWaves();
            }            
        }

        

        public PooledList<WaveInfo> GetBottomWaves()
        {
            if ( MainElliottWave.Count > 0 )
            {
                ElliottWaveCycle wavesToShow = FinancialHelper.GetMinimumWavesToShow( SymbolEx.Period );

                return MainElliottWave.GetBottomWaves( wavesToShow );
            }

            return null;
        }

        public PooledList<WaveInfo> GetBottomWaves( bool filterWaves )
        {
            if ( MainElliottWave.Count > 0 )
            {
                ElliottWaveCycle wavesToShow = FinancialHelper.GetMinimumWavesToShow( SymbolEx.Period );

                return MainElliottWave.GetBottomWaves( filterWaves, wavesToShow );
            }

            return null;
        }

        

        
        public PooledList<int> GetDivergenceIndex()
        {
            PooledList< int > output = new PooledList< int >( );

            //var symbolMgr = ConfigManager.GetService<ISymbolsMgr>();

            //if ( symbolMgr == null ) return output;

            //var aa = symbolMgr.GetOrCreateAdvancedAnalysis( this.SymbolEx );

            //IPeriodXTaManager taManager = null;

            //if ( aa != null )
            //{
            //    taManager = aa.GetPeriodXTa( SymbolEx.Period );
            //}
            //else
            //{
            //    return null;
            //}

            //var divergences = taManager.GetDivergenceInfo( ref this );

            //if ( divergences != null )
            //{
            //    foreach ( var div in divergences )
            //    {
            //        output.Add( ( int ) div.StartIndex );
            //    }
            //}

            return output;
        }

        public TASignal GetExtremumType()
        {
            if ( IsWavePeak() )
            {
                return TASignal.WAVE_PEAK;
            }

            if ( IsGannPeak() )
            {
                return TASignal.GANN_PEAK;
            }

            if ( IsWaveTrough() )
            {
                return TASignal.WAVE_TROUGH;
            }

            if ( IsGannTrough() )
            {
                return TASignal.GANN_TROUGH;
            }

            return TASignal.NONE;
        }

        public WaveInfo? GetFirstWave( int waveScenarioNo )
        {
            ref var hew = ref GetWaveFromScenario( waveScenarioNo );

            if ( hew != AdvBarInfo.EmptyHew )
            {
                return hew.GetFirstWave();
            }

            return null;
        }


        public PooledList<int> GetGannSquareIndex()
        {
            PooledList< int > output = new PooledList< int >( );

            //var symbolMgr = ConfigManager.GetService<ISymbolsMgr>();

            //if ( symbolMgr == null ) return output;


            //var aa = symbolMgr.GetOrCreateAdvancedAnalysis( this.SymbolEx );

            //IPeriodXTaManager taManager = null;

            //if ( aa != null )
            //{
            //    taManager = aa.GetPeriodXTa( SymbolEx.Period );
            //}
            //else
            //{
            //    return null;
            //}

            //var gannSquareInfo = taManager.GetGannPriceTimeInfo( ref this );

            //if ( gannSquareInfo != null )
            //{
            //    foreach ( var info in gannSquareInfo )
            //    {
            //        output.Add( ( int ) info.BeginBarIndex );
            //    }
            //}

            return output;
        }

        public TASignal GetGannType()
        {
            if ( IsGannPeak() )
            {
                return TASignal.GANN_PEAK;
            }

            if ( IsGannTrough() )
            {
                return TASignal.GANN_TROUGH;
            }

            return TASignal.NONE;
        }

        public WaveInfo? GetLowestCycleWave()
        {
            if ( MainElliottWave.Count > 0 )
            {
                return MainElliottWave.GetLowestDegreeWaveInfo();
            }

            return null;
        }

        public string GetPivotRelationString()
        {
            string output = "";

            //var symbolMgr = ConfigManager.GetService<ISymbolsMgr>();

            //if ( symbolMgr == null ) return output;

            //var aa = symbolMgr.GetOrCreateAdvancedAnalysis( this.SymbolEx );

            //IPeriodXTaManager taManager = null;

            //if ( aa != null )
            //{
            //    taManager = aa.GetPeriodXTa( SymbolEx.Period );
            //}
            //else
            //{
            //    return "";
            //}

            //var pivotRelations = taManager.GetPivotReltations( ref this );

            //foreach ( var relation in pivotRelations )
            //{
            //    output += relation.ToString() + "\r\n";
            //}

            return output;
        }

        

        public WaveLabelPosition GetRetracmentInfoPos()
        {
            if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
            {
                return info.GetRetracmentInfoPos( LinuxTime );
            }
            
            return WaveLabelPosition.UNKNOWN;
        }

        public PooledList<WaveInfo> GetTopWaves()
        {
            if ( MainElliottWave.Count > 0 )
            {
                ElliottWaveCycle wavesToShow = FinancialHelper.GetMinimumWavesToShow( SymbolEx.Period );

                return MainElliottWave.GetTopWaves( wavesToShow );
            }

            return null;
        }

        public PooledList<WaveInfo> GetTopWaves( bool isFilterAB )
        {
            if ( MainElliottWave.Count > 0 )
            {
                ElliottWaveCycle wavesToShow = FinancialHelper.GetMinimumWavesToShow( SymbolEx.Period );

                return MainElliottWave.GetTopWaves( isFilterAB, wavesToShow );
            }

            return null;
        }

        
        

        public ElliottWaveEnum GetWaveCount( ElliottWaveCycle waveCycle )
        {
            if ( MainElliottWave.Count > 0 )
            {
                MainElliottWave.GetWavesAtCycle( waveCycle );
            }

            return ElliottWaveEnum.NONE;
        }

        //public WaveLabelPosition GetWaveLabelPosition()
        //{
        //    if ( MainElliottWave.Count > 0 )
        //    {
        //        return ( MainElliottWave.LabelPosition );
        //    }

        //    return WaveLabelPosition.UNKNOWN;
        //}

        public WaveLabelPosition GetWaveLabelPosition( int waveScenarioNo )
        {            
            ref var hew = ref GetWaveFromScenario( waveScenarioNo );

            if ( hew != AdvBarInfo.EmptyHew )
            {
                return hew.GetWaveLabelPosition();
            }

            return WaveLabelPosition.UNKNOWN;
        }

        //public WaveLabelPosition GetWaveLabelPosition( ElliottWaveCountType waveCountType )
        //{
        //    HewLong elliottWave = default;

        //    switch ( waveCountType )
        //    {
        //        case ElliottWaveCountType.INVALID:
        //        case ElliottWaveCountType.PRIMARY:
        //        {
        //            if ( MainElliottWave.Count > 0 )
        //            {
        //                return ( MainElliottWave.GetWaveLabelPosition() );
        //            }
        //        }
        //        break;

        //        case ElliottWaveCountType.ALTERNATIVE:
        //        {
        //            if ( AltElliottWave.Count > 0 )
        //            {
        //                return ( AltElliottWave.GetWaveLabelPosition() );
        //            }
        //        }
        //        break;

        //        case ElliottWaveCountType.CONFIRMED:

        //            break;
        //    }

        //    if ( elliottWave.Count > 0 )
        //    {
        //        return ( elliottWave.GetWaveLabelPosition() );
        //    }

        //    return WaveLabelPosition.UNKNOWN;
        //}


        public PooledList<int> GetWaveRotationIndex()
        {
            PooledList< int > output = new PooledList< int >( );

            //var symbolMgr = ConfigManager.GetService<ISymbolsMgr>();

            //if ( symbolMgr == null ) return output;

            //var aa = symbolMgr.GetOrCreateAdvancedAnalysis( this.SymbolEx );

            //IPeriodXTaManager taManager = null;

            //if ( aa != null )
            //{
            //    taManager = aa.GetPeriodXTa( SymbolEx.Period );
            //}
            //else
            //{
            //    return null;
            //}

            //var waveRotations = taManager.GetWaveImportantTimeInfo( ref this );

            //foreach ( var wave in waveRotations )
            //{
            //    output.Add( wave.BeginBarIndex );
            //}

            return output;
        }

        public TASignal GetWaveType()
        {
            if ( IsWavePeak() )
            {
                return TASignal.WAVE_PEAK;
            }

            if ( IsWaveTrough() )
            {
                return TASignal.WAVE_TROUGH;
            }

            return TASignal.NONE;
        }

        public bool IsExtremum()
        {
            return IsPeak() || IsTrough();
        }

        public bool IsGannPeak()
        {
            if ( HasSignal )
            {
                TASignal signal = TechnicalAnalysisSignal;

                if ( signal.HasFlag( TASignal.GANN_PEAK ) )
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsGannTrough()
        {
            if ( HasSignal )
            {
                TASignal signal = TechnicalAnalysisSignal;

                if ( signal.HasFlag( TASignal.GANN_TROUGH ) )
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsPeak()
        {
            return IsWavePeak() || IsGannPeak();
        }

        public bool IsTrough()
        {
            return IsWaveTrough() || IsGannTrough();
        }

        public bool IsWavePeak()
        {
            if ( HasSignal )
            {
                TASignal signal = TechnicalAnalysisSignal;

                if ( signal.HasFlag( TASignal.WAVE_PEAK ) )
                {
                    return true;
                }
            }

            return false;
        }


        public bool IsWaveTrough()
        {
            if ( HasSignal )
            {
                TASignal signal = TechnicalAnalysisSignal;

                if ( signal.HasFlag( TASignal.WAVE_TROUGH ) )
                {
                    return true;
                }
            }

            return false;
        }

        public bool MatchesWave( ref WaveInfo value )
        {
            if ( MainElliottWave.Count > 0 )
            {
                if ( MainElliottWave.MatchesWave( value ) )
                {
                    return true;
                }
            }

            return false;
        }

        public void MergeHigherTimeFrameWaves( int waveScenarioNo, long waveBit )
        {
            if ( !_parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
            {
                info = _parent.GetOrAllocate( _barIndex );
            }

            info.MergeHigherTimeFrameWaves( waveScenarioNo, waveBit );

            WaveDirty = WaveDirtyEnum.Add;

            Features.HasElliottWave = info.HasWaves;
        }

        public void MergeHigherTimeFrameWaves( int waveScenarioNo, PooledList<WaveInfo> waves )
        {
            if ( !_parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
            {
                info = _parent.GetOrAllocate( _barIndex );
            }

            info.MergeHigherTimeFrameWaves( waveScenarioNo, waves );

            WaveDirty = WaveDirtyEnum.Add;

            Features.HasElliottWave = info.HasWaves;
        }

        public void MergeHigherTimeFrameWaves( int waveScenarioNo, ref WaveInfo wave )
        {
            if ( !_parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
            {
                info = _parent.GetOrAllocate( _barIndex );
            }

            info.MergeHigherTimeFrameWaves( waveScenarioNo, ref wave );

            WaveDirty = WaveDirtyEnum.Add;

            Features.HasElliottWave = info.HasWaves;
        }
        

        public void MergeWave( int waveScenarioNo, ref WaveInfo wave )
        {
            if ( !_parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
            {
                info = _parent.GetOrAllocate( _barIndex );
            }

            info.MergeWave( waveScenarioNo, ref wave, SymbolEx.Period );

            WaveDirty = WaveDirtyEnum.Add;

            Features.HasElliottWave = info.HasWaves;
        }
        
        public void MergeWaves( int waveScenarioNo, ref HewLong waves )
        {
            if ( !_parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
            {
                info = _parent.GetOrAllocate( _barIndex );
            }

            info.MergeWaves( waveScenarioNo, waves, SymbolEx.Period );

            WaveDirty = WaveDirtyEnum.Add;

            Features.HasElliottWave = info.HasWaves;
        }

        //public void MergeWaves( bool isMainCount, HewShort waves, WaveLabelPosition waveLabelPosition )
        //{
        //    if ( _extraBarInfo != null )
        //    {
        //        _extraBarInfo.MergeWaves( isMainCount, waves.ToLarger(), waveLabelPosition, Symbol.Period );
        //    }
        //}

        

        //public void RestoreElliottWave( HewShort main, HewShort alt )
        //{            
        //    if ( alt.HasElliottWave )
        //    {
        //        var symbolMgr = ConfigManager.GetService<ISymbolsMgr>();

        //        if ( symbolMgr == null ) return;

        //        var barInfo = symbolMgr.CreateBarInfoManager( Symbol );

        //        ExtraBarInfo = barInfo.CreateOrGetExtraInfo( ref this );
        //        
        //        ExtraBarInfo.AltElliottWave.CopyFrom( ref alt );
        //        ExtraBarInfo.SetWaveLabelPosition( main.LabelPosition );
        //        
        //        if ( alt.HasElliottWave && alt.LabelPosition == WaveLabelPosition.BOTH )
        //        {
        //            ExtraBarInfo.IsSpecialBar = true;
        //        }
        //    }

        //    this.SimpleHew = main;

        //    if ( main.HasElliottWave && main.LabelPosition == WaveLabelPosition.BOTH )
        //    {
        //        if ( ExtraBarInfo != null && ExtraBarInfo != AdvBarInfo.EmptyBarInfo )
        //        {
        //            ExtraBarInfo.IsSpecialBar = true;
        //        }
        //    }
        //}

        public void RestoreElliottWave( ref HewLong main, ref HewLong alt01, ref HewLong alt02, ref HewLong alt03 )
        {
            AdvBarInfo info = _parent.GetOrAllocate( _barIndex );

            info.MainElliottWave.CopyFrom( ref main );

            if ( alt01 != AdvBarInfo.EmptyHew )
            {
                info.AltWaveScenario01.CopyFrom( ref alt01 );
            }

            if ( alt02 != AdvBarInfo.EmptyHew )
            {
                info.AltWaveScenario02.CopyFrom( ref alt02 );
            }

            if ( alt03 != AdvBarInfo.EmptyHew )
            {
                info.AltWaveScenario03.CopyFrom( ref alt03 );
            }

            WaveDirty = WaveDirtyEnum.Add;

            Features.HasElliottWave = info.HasWaves;
        }





        public void SwapWavesAndZeroOut()
        {
            if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
            {
                if ( info.AltWaveScenario01.Count > 0 )
                {
                    info.AltWaveScenario01.ResetWaves();
                }

                if ( info.MainElliottWave.Count > 0 )
                {
                    info.AltWaveScenario01.CopyFrom( ref info.MainElliottWave );
                    info.MainElliottWave.ResetWaves();
                }

                WaveDirty = WaveDirtyEnum.Change;

                Features.HasElliottWave = info.HasWaves;                
            }


            
        }

        

        

        public void SwapWaves( int waveScenarioNoX, int waveScenarioNoY )
        {
            if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
            {
                info.SwapWaves( waveScenarioNoX, waveScenarioNoY );
            }            
        }

        public void SwapMainWavesWithAlternative( HewLong wavesToBeChanged )
        {
            if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
            {
                info.SwapMainWavesWithAlternative( wavesToBeChanged );
            }            
        }

        
        public void SwapMainWavesWithAlternative( IHarmonicElliottWave<long> wavesToBeChanged )
        {
            if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
            {
                info.SwapMainWavesWithAlternative( wavesToBeChanged );
            }
        }

        public void SwapMainWavesWithAlternative( IHarmonicElliottWave<uint> wavesToBeChanged )
        {
            if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
            {
                info.SwapMainWavesWithAlternative( wavesToBeChanged );
            }             
        }        

        public void ReplaceWave( int waveScenarioNo, ref HewLong copy )
        {
            if ( _parent.HasExtraInfo( _barIndex, out AdvBarInfo info ) )
            {
                ref var hew = ref info.GetWaveFromScenario( waveScenarioNo );
                
                hew.CopyFrom( ref copy );

                Features.HasElliottWave = info.HasWaves;
            }
            else
            {
                var barInfo = _parent.GetOrAllocate( _barIndex );

                ref var hew = ref info.GetWaveFromScenario( waveScenarioNo );

                hew.CopyFrom( ref copy );

                Features.HasElliottWave = barInfo.HasWaves;
            }

            WaveDirty = WaveDirtyEnum.Change;
        }

        private string DebuggerDisplay
        {
            get
            {
                return ToString();
            }
        }
    }
}
