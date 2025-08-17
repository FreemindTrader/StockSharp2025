using fx.Collections;
using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace fx.Bars
{
    public class AdvBarInfo : IEquatable<AdvBarInfo>
    {
        public static readonly AdvBarInfo EmptyBarInfo = new AdvBarInfo();

        public AdvBarInfo()
        {

        }

        public IBarList Parent
        {
            get
            {
                return _parent;
            }

            set
            {
                _parent = value;
            }
        }

        public AdvBarInfo( IBarList parent )
        {
            _parent = parent;
        }


        #region Variables

        private IBarList _parent;
        private uint _sbarAdvanced;

        public HewLong MainElliottWave;
        public HewLong AltWaveScenario01;
        public HewLong AltWaveScenario02;
        public HewLong AltWaveScenario03;

        public WavePriceTimeInfo   MainPriceTime;
        public WavePriceTimeInfo   AltPriceTime01;
        public WavePriceTimeInfo   AltPriceTime02;
        public WavePriceTimeInfo   AltPriceTime03;

        public string BaZiString;

        private TACandle _candleStickPatterns;                            // 4    => 64                      

        #endregion
        public static HewLong EmptyHew  = new HewLong( );
        public static WavePriceTimeInfo   EmptyFibR = new WavePriceTimeInfo( );


        public TACandle CandlePatterns
        {
            get
            {
                return _candleStickPatterns;
            }

            set
            {
                var temp = _candleStickPatterns | value;

                _candleStickPatterns = temp;
            }
        }
        /* -------------------------------------------------------------------------------------------------------------------------------------------
         * 
         * BitPos 32      = 1 bit for Speical Bar
         * BitPos 31 - 29 = 3 Bit reserved for WaveDirtyEnum
         * BitPos  8 - 01 = 8 Bit for CandlePatten
         * 
         * ------------------------------------------------------------------------------------------------------------------------------------------- */


        public bool IsSpecial
        {
            get
            {
                return BitHelper.IsBitSet( _sbarAdvanced, 32 );
            }
            set
            {
                _sbarAdvanced = BitHelper.TurnOnOffBit( _sbarAdvanced, 32, value );
            }
        }


        public WaveDirtyEnum WaveDirty
        {
            get
            {
                var dirty = BitHelper.GetBits( _sbarAdvanced, 29, 3, false );
                return ( WaveDirtyEnum ) dirty;
            }
            set
            {
                var setDirty = ( uint ) value;
                _sbarAdvanced = BitHelper.SetBits( _sbarAdvanced, setDirty, 1, 29, 3 );
            }
        }

        public bool HasWaves 
        {
            get { return WaveScenariosCount > 0;  } 
        }
        public int WaveScenariosCount
        {
            get
            {
                int count = 0;
                if ( MainElliottWave.HasElliottWave )
                {
                    count++;
                }

                if ( AltWaveScenario01.HasElliottWave )
                {
                    count++;
                }

                if ( AltWaveScenario02.HasElliottWave )
                {
                    count++;
                }

                if ( AltWaveScenario03.HasElliottWave )
                {
                    count++;
                }

                return count;
            }
        }



        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public ref HewLong GetWaveFromScenario( int waveScenarioNo )
        {
            switch( waveScenarioNo )
            {
                case 1:
                    return ref MainElliottWave;
                case 2:
                    return ref AltWaveScenario01;
                case 3:
                    return ref AltWaveScenario02;
                case 4:
                    return ref AltWaveScenario03;
            }

            return ref EmptyHew;
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public ref HewLong GetNextWaveFromScenario( int waveScenarioNo )
        {
            switch ( waveScenarioNo )
            {
                case 1:
                    return ref AltWaveScenario01;
                case 2:
                    return ref AltWaveScenario02;
                case 3:
                    return ref AltWaveScenario03;                
            }

            return ref EmptyHew;
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public ref WavePriceTimeInfo GetPriceTimeFromScenario( int waveScenarioNo )
        {
            switch ( waveScenarioNo )
            {
                case 1:
                    return ref MainPriceTime;
                case 2:
                    return ref AltPriceTime01;
                case 3:
                    return ref AltPriceTime02;
                case 4:
                    return ref AltPriceTime03;
            }

            return ref EmptyFibR;
        }

        public void RemoveWavesFromDatabar( int waveScenarioNo )
        {
            ref var hew = ref GetWaveFromScenario( waveScenarioNo );

            if ( hew.Count > 0 )
            {
                hew.ResetWaves();

                IsSpecial = hew.GetTopWaves().Count > 0 && hew.GetBottomWaves().Count > 0;

                WaveDirty = WaveDirtyEnum.DeleteAll;
            }
        }

        public void RemoveWavesFromDatabar( int waveScenarioNo, HewLong otherHew )
        {
            ref var hew = ref GetWaveFromScenario( waveScenarioNo );

            if ( hew.Count > 0 )
            {
                hew.RemoveWaves( ref otherHew );

                IsSpecial = hew.GetTopWaves().Count > 0 && hew.GetBottomWaves().Count > 0;

                WaveDirty = WaveDirtyEnum.DeleteSingle;
            }
        }

        

        public bool RemoveMatchedWavesFromDatabar( int waveScenarioNo, HewLong otherHew )
        {
            ref var hew = ref GetWaveFromScenario( waveScenarioNo );

            if ( hew.Count > 0 )
            {
                if ( hew.RemoveMatchedWaves( otherHew ) )
                {
                    IsSpecial = hew.GetTopWaves().Count > 0 && hew.GetBottomWaves().Count > 0;

                    WaveDirty = WaveDirtyEnum.DeleteSingle;

                    return true;
                }
            }

            return false;
        }

        public WaveLabelPosition GetRetracmentInfoPos( long linuxTime )
        {
            if ( MainPriceTime.HasPriceTimeInfo() )
            {
                return ( MainPriceTime.RetracmentInfoPosition( linuxTime ) );
            }

            return WaveLabelPosition.UNKNOWN;
        }

        public string GetPriceTimeInfoString( int waveScenarioNo, long linuxTime )
        {
            ref WavePriceTimeInfo ptinfo = ref GetPriceTimeFromScenario( waveScenarioNo );
            
            if ( ptinfo != EmptyFibR )
            {
                if ( ptinfo.HasPriceTimeInfo() )
                {
                    return ( ptinfo.GetPriceTimeInfoString( linuxTime ) );
                }
            }

            return null;
        }

        public void MergeHigherTimeFrameWaves( int waveScenarioNo, long waveBit )
        {
            ref var hew = ref GetWaveFromScenario( waveScenarioNo );

            if ( hew.Count > 0 )
            {
                hew.MergeWave( waveBit );                
            }
            else
            {
                hew = new HewLong( waveBit );
            }
        }

        public void MergeHigherTimeFrameWaves( int waveScenarioNo, PooledList<WaveInfo> waves )
        {
            ref var hew = ref GetWaveFromScenario( waveScenarioNo );

            if ( hew.Count > 0 )
            {
                hew.MergeWavesList( waves );                
            }
            else
            {
                hew = new HewLong( waves );                
            }
        }

        public void MergeWaves( int waveScenarioNo, HewLong waves, TimeSpan Period )
        {
            ref var hew = ref GetWaveFromScenario( waveScenarioNo );

            ElliottWaveCycle minimumCycle = FinancialHelper.GetMinimumWavesToMerge( Period );

            if ( hew.Count > 0 )
            {
                if ( hew != waves )
                {
                    hew.MergeInLowerTimeFrameWaves( ref waves, minimumCycle );                    
                }
            }
            else
            {
                hew = new HewLong( waves, minimumCycle );
            }
        }



        public void MergeWave( int waveScenarioNo, ref WaveInfo wave, TimeSpan Period )
        {
            ref var hew = ref GetWaveFromScenario( waveScenarioNo );

            ElliottWaveCycle minimumCycle = FinancialHelper.GetMinimumWavesToMerge( Period );

            if ( wave.WaveCycle >= minimumCycle )
            {
                if ( hew.Count > 0 )
                {
                    hew.MergeWave( wave );
                }
                else
                {
                    hew = new HewLong( wave );
                }
            }
        }

        public void MergeHigherTimeFrameWaves( int waveScenarioNo, ref WaveInfo wave )
        {
            ref var hew = ref GetWaveFromScenario( waveScenarioNo );

            if ( hew.Count > 0 )
            {
                hew.MergeWave( wave );
            }
            else
            {
                hew = new HewLong( wave );                
            }
        }
        
        

        

        public void SwapWaves( int waveScenarioNoX, int waveScenaioY )
        {
            ref var hew0X = ref GetWaveFromScenario( waveScenarioNoX );
            ref var hew0Y = ref GetWaveFromScenario( waveScenaioY );

            var temp   = hew0X;
            hew0X.CopyFrom( ref hew0Y );
            hew0Y.CopyFrom( ref temp );          
        }

       

        public void SwapMainWavesWithAlternative( IHarmonicElliottWave<long> wavesToBeChanged )
        {
            var primaryWave = MainElliottWave;

            primaryWave.RemoveMatchedWaves( wavesToBeChanged );

            var alternative = AltWaveScenario01;

            var wavesToMerge = wavesToBeChanged.GetAllWaves( );

            alternative.MergeWavesList( wavesToMerge );
        }

        public void SwapMainWavesWithAlternative( IHarmonicElliottWave<uint> wavesToBeChanged )
        {
            var primaryWave = MainElliottWave;

            primaryWave.RemoveMatchedWaves( wavesToBeChanged );

            var alternative = AltWaveScenario01;

            var wavesToMerge = wavesToBeChanged.GetAllWaves( );

            alternative.MergeWavesList( wavesToMerge );
        }

        public override bool Equals( object obj )
        {
            if ( obj is AdvBarInfo )
            {
                return Equals( ( AdvBarInfo ) obj );
            }

            return base.Equals( obj );
        }

        public static bool operator ==( AdvBarInfo first, AdvBarInfo second )
        {
            if ( ( object ) first == null )
            {
                return ( object ) second == null;
            }

            return first.Equals( second );
        }

        public static bool operator !=( AdvBarInfo first, AdvBarInfo second )
        {
            return !( first == second );
        }

        public bool Equals( AdvBarInfo other )
        {
            if ( ReferenceEquals( null, other ) )
            {
                return false;
            }

            if ( ReferenceEquals( this, other ) )
            {
                return true;
            }

            return Equals( _parent, other._parent ) && _sbarAdvanced.Equals( other._sbarAdvanced ) && MainElliottWave.Equals( other.MainElliottWave ) && AltWaveScenario01.Equals( other.AltWaveScenario01 ) && MainPriceTime.Equals( other.MainPriceTime ) && Equals( BaZiString, other.BaZiString ) && WaveDirty.Equals( other.WaveDirty );
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 47;
                if ( _parent != null )
                {
                    hashCode = ( hashCode * 53 ) ^ EqualityComparer<IBarList>.Default.GetHashCode( _parent );
                }

                hashCode = ( hashCode * 53 ) ^ _sbarAdvanced.GetHashCode();
                hashCode = ( hashCode * 53 ) ^ EqualityComparer<HewLong>.Default.GetHashCode( MainElliottWave );
                hashCode = ( hashCode * 53 ) ^ EqualityComparer<HewLong>.Default.GetHashCode( AltWaveScenario01 );
                hashCode = ( hashCode * 53 ) ^ EqualityComparer<WavePriceTimeInfo>.Default.GetHashCode( MainPriceTime );
                if ( BaZiString != null )
                {
                    hashCode = ( hashCode * 53 ) ^ EqualityComparer<string>.Default.GetHashCode( BaZiString );
                }

                hashCode = ( hashCode * 53 ) ^ ( int ) WaveDirty;
                return hashCode;
            }
        }
    }
}
