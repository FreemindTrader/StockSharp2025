using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using fx.Collections;
using fx.Definitions.UndoRedo;

namespace fx.Definitions
{
    /// <summary>
    /// 
    /// 9 | 8765 | 43210
    /// Bit 9     = Wave Label Position, 0 = bottom, 1 = top
    /// Bit 5 - 8 = Wave Cycle.
    /// Bit 0 - 4 = Wave Name
    /// 
    /// Every ten Bits is a HewPointInfo
    /// </summary>

    public partial struct HewLong : IHarmonicElliottWave<long>, IEquatable<HewLong>
    {
        #region Variables        
        private long               _hewBits;

        #endregion

        /* --------------------------------------------------------------------------------------------------------------------
         * 
         * Since our wave can have a Maximum of 12 Cycles,  
         *      1) we would store 6 cycles in _hewBits
         *      2) and store another 6 cycles in _hewExtraBits
         * 
         * When the WaveLabelPosition is BOTH
         *      1) We would use the _hewBits to store the Wave Cycle on the top of the bar.
         *      2) and use the _hewExtraBitsto store the wave Cycle on the bottom of the bar
         *      
         * --------------------------------------------------------------------------------------------------------------------
        */

        public HewLong( HewLong other )
        {
            _hewBits = other.RawWave;
        }

        public HewLong( HewLong other, ElliottWaveCycle mininumCycle )
        {
            _hewBits = other.RawWave;

            var waves = other.GetAllWaves( mininumCycle );            

            foreach ( var wave in waves )
            {                
                AddHarmonicElliottWave( wave );
            }
        }

        public bool HasHigherCount
        {
            get
            {
                return BitHelper.IsBitSet( _hewBits, 32 );
            }

            set
            {
                _hewBits = BitHelper.TurnOnOffBit( _hewBits, 32, value );
            }
        }

        public HewLong( ElliottWaveCycle waveCycle, ElliottWaveEnum waveName, WaveLabelPosition waveLabelPosition )
        {
            _hewBits = 0;

            AddHarmonicElliottWave( waveCycle, waveName, waveLabelPosition );
        }

        public HewLong( PooledList<WaveInfo> waves )
        {
            _hewBits = 0;

            foreach ( WaveInfo wave in waves )
            {
                AddHarmonicElliottWave( wave.WaveCycle, wave.WaveName, wave.LabelPosition );
            }
        }

        public HewLong( WaveInfo wave, WaveLabelPosition waveLabelPosition )
        {
            _hewBits = 0;

            AddHarmonicElliottWave( wave.WaveCycle, wave.WaveName, wave.LabelPosition );
        }

        public HewLong( WaveInfo wave )
        {
            _hewBits = 0;

            AddHarmonicElliottWave( wave.WaveCycle, wave.WaveName, wave.LabelPosition );
        }

        public HewLong( long waveBit )
        {
            _hewBits = waveBit;
        }


        //public SmallHEW( SmallHEW waves )
        //{
        //    _hewBits           = waves._hewBits;

        //    _hewExtraBits     = waves._hewExtraBits;

        //    _waveLabelPosition= waves._waveLabelPosition;

        //    waveCycleCount   = 0;

        //    waveCycleCount   = GetWaveDegreeCount( );
        //}

        public void UpdateWaveCount()
        {
        }



        public int Count
        {
            get { return GetWaveDegreeCount(); }
        }

        //public WaveLabelPosition LabelPosition
        //{
        //    get { return _waveLabelPosition; }

        //    set { _waveLabelPosition = value; }
        //}

        //public void SetWaveLabelPosition( WaveLabelPosition wavePosition )
        //{
        //    _waveLabelPosition = wavePosition;
        //}

        public long RawWave
        {
            get { return _hewBits; }

            set { _hewBits = value; }
        }

        //public long RawWaveExtra
        //{
        //    get { return 0; }

        //    set {  }
        //}






        public void ResetWaves()
        {
            _hewBits = 0;
        }

        public bool AddHarmonicElliottWave( ElliottWaveCycle waveCycle, ElliottWaveEnum waveName, WaveLabelPosition waveLabelPosition )
        {
            long bitwiseWave = ( long ) waveCycle;

            bitwiseWave = bitwiseWave << ( GlobalConstants.NewHewBits - GlobalConstants.CycleBits - GlobalConstants.WaveLabelBit );

            bitwiseWave = bitwiseWave | ( long ) waveName;

            if ( waveLabelPosition == WaveLabelPosition.TOP )
            {
                bitwiseWave = bitwiseWave | GlobalConstants.NewHewLabelMask;
            }

            var waveCycleCount = GetWaveDegreeCount( );

            if ( WaveExists( bitwiseWave ) == false )
            {
                if ( waveCycleCount < GlobalConstants.MaxCountPerLong )
                {
                    _hewBits |= bitwiseWave << waveCycleCount * GlobalConstants.NewHewBits;
                }
            }

            return true;
        }

        private bool WaveExists( long inputBitWave )
        {
            long content = 0;
            int extractedWave = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    extractedWave = ( int ) content & GlobalConstants.NewHewBitsMask;

                    if ( ( extractedWave == inputBitWave ) || ( inputBitWave.SameWaveName( extractedWave ) ) )
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        public void AddHarmonicElliottWave( ref HewLong other )
        {
            _hewBits = other.RawWave;

            var waves = other.GetAllWaves( );

            foreach ( var wave in waves )
            {
                AddHarmonicElliottWave( wave );
            }
        }

        public bool AddHarmonicElliottWave( WaveInfo wave )
        {
            return AddHarmonicElliottWave( wave.WaveCycle, wave.WaveName, wave.LabelPosition );
        }

        public bool AddHarmonicElliottWave( ElliottWaveCycle waveCycle,
                                            PooledList<ElliottWaveEnum> waveNames,
                                            WaveLabelPosition waveLabelPosition )
        {
            var waveCycleCount = GetWaveDegreeCount( );

            foreach ( ElliottWaveEnum waveName in waveNames )
            {
                long bitwiseWave = ( long ) waveCycle;

                bitwiseWave = bitwiseWave << ( GlobalConstants.NewHewBits - GlobalConstants.CycleBits - GlobalConstants.WaveLabelBit );

                bitwiseWave = bitwiseWave | ( long ) waveName;

                if ( waveLabelPosition == WaveLabelPosition.TOP )
                {
                    bitwiseWave = bitwiseWave | GlobalConstants.NewHewLabelMask;
                }

                if ( WaveExists( bitwiseWave ) == false )
                {
                    if ( waveCycleCount < GlobalConstants.MaxCountPerLong )
                    {
                        _hewBits |= bitwiseWave << waveCycleCount * GlobalConstants.NewHewBits;
                    }

                }
            }


            return true;
        }

        public bool AddSpecialHarmonicElliottWave( ElliottWaveCycle waveCycle,
                                                   ElliottWaveEnum waveName,
                                                   WaveLabelPosition waveLabelPosition )
        {
            return AddHarmonicElliottWave( waveCycle, waveName, waveLabelPosition );
        }

        public bool ReplaceWave( 
                                    ElliottWaveCycle desiredCycle,
                                    ElliottWaveEnum oldElliottWave,
                                    ElliottWaveEnum newElliottWave )
        {
            ElliottWaveEnum wave = ElliottWaveEnum.NONE;
            ElliottWaveCycle cycle = ElliottWaveCycle.UNKNOWN;

            long content = 0;
            long newContent = 0;
            long bitsToClear = 0;

            WaveLabelPosition labelPosition = WaveLabelPosition.UNKNOWN;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    content = content & GlobalConstants.NewHewBitsMask;

                    wave = ( ElliottWaveEnum ) ( content & ( GlobalConstants.NewHewContentMask ) );
                    cycle = ( ElliottWaveCycle ) ( ( content & ( GlobalConstants.NewHewCycleMask ) ) >> ( GlobalConstants.NewHewBits - GlobalConstants.CycleBits - GlobalConstants.WaveLabelBit ) );
                    labelPosition = ( WaveLabelPosition ) ( ( content & ( GlobalConstants.NewHewLabelMask ) ) >> ( GlobalConstants.NewHewBits - GlobalConstants.WaveLabelBit ) );

                    if ( cycle == desiredCycle && wave == oldElliottWave )
                    {
                        // First we want to turn off the HEW bits which is 10 bits.
                        bitsToClear = GlobalConstants.NewHewBitsMask;

                        bitsToClear = bitsToClear << ( GlobalConstants.NewHewBits * i );

                        _hewBits = _hewBits & ( ~bitsToClear );

                        newContent = ( long ) desiredCycle;

                        newContent = newContent << ( GlobalConstants.NewHewBits - GlobalConstants.CycleBits - GlobalConstants.WaveLabelBit );

                        newContent = newContent | ( long ) newElliottWave;

                        if ( labelPosition == WaveLabelPosition.TOP )
                        {
                            newContent = newContent | GlobalConstants.NewHewLabelMask;
                        }

                        newContent = newContent << ( GlobalConstants.NewHewBits * i );

                        _hewBits = _hewBits | newContent;

                        return true;
                    }
                }
            }

            return false;
        }

        public bool ReplaceTopWave( long selectedBarTime,
                                    ElliottWaveCycle desiredCycle,
                                    ElliottWaveEnum oldElliottWave,
                                    ElliottWaveEnum newElliottWave )
        {
            var wave = ElliottWaveEnum.NONE;
            var cycle = ElliottWaveCycle.UNKNOWN;
            var labelPosition = WaveLabelPosition.UNKNOWN;

            long content = 0;
            long newContent = 0;
            long bitsToClear = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    content = content & GlobalConstants.NewHewBitsMask;

                    wave = ( ElliottWaveEnum ) ( content & ( GlobalConstants.NewHewContentMask ) );
                    cycle = ( ElliottWaveCycle ) ( ( content & ( GlobalConstants.NewHewCycleMask ) ) >> ( GlobalConstants.NewHewBits - GlobalConstants.CycleBits - GlobalConstants.WaveLabelBit ) );
                    labelPosition = ( WaveLabelPosition ) ( ( content & ( GlobalConstants.NewHewLabelMask ) ) >> ( GlobalConstants.NewHewBits - GlobalConstants.WaveLabelBit ) );

                    if ( cycle == desiredCycle && wave == oldElliottWave && labelPosition == WaveLabelPosition.TOP )
                    {
                        // First we want to turn off the HEW bits which is 10 bits.
                        bitsToClear = GlobalConstants.NewHewBitsMask;

                        bitsToClear = bitsToClear << ( GlobalConstants.NewHewBits * i );

                        _hewBits = _hewBits & ( ~bitsToClear );

                        newContent = ( long ) desiredCycle;

                        newContent = newContent << ( GlobalConstants.NewHewBits - GlobalConstants.CycleBits - GlobalConstants.WaveLabelBit );

                        newContent = newContent | ( long ) newElliottWave;

                        if ( labelPosition == WaveLabelPosition.TOP )
                        {
                            newContent = newContent | GlobalConstants.NewHewLabelMask;
                        }

                        newContent = newContent << ( GlobalConstants.NewHewBits * i );

                        _hewBits = _hewBits | newContent;

                        return true;
                    }
                }
            }





            return false;
        }

        public bool ReplaceBottomWave( long selectedBarTime,
                                       ElliottWaveCycle desiredCycle,
                                       ElliottWaveEnum oldElliottWave,
                                       ElliottWaveEnum newElliottWave )
        {
            var wave = ElliottWaveEnum.NONE;
            var cycle = ElliottWaveCycle.UNKNOWN;
            var labelPosition = WaveLabelPosition.UNKNOWN;

            long content = 0;
            long newContent = 0;
            long bitsToClear = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    content = content & GlobalConstants.NewHewBitsMask;

                    wave = ( ElliottWaveEnum ) ( content & ( GlobalConstants.NewHewContentMask ) );
                    cycle = ( ElliottWaveCycle ) ( ( content & ( GlobalConstants.NewHewCycleMask ) ) >> ( GlobalConstants.NewHewBits - GlobalConstants.CycleBits - GlobalConstants.WaveLabelBit ) );
                    labelPosition = ( WaveLabelPosition ) ( ( content & ( GlobalConstants.NewHewLabelMask ) ) >> ( GlobalConstants.NewHewBits - GlobalConstants.WaveLabelBit ) );

                    if ( cycle == desiredCycle && wave == oldElliottWave && labelPosition == WaveLabelPosition.BOTTOM )
                    {
                        // First we want to turn off the HEW bits which is 10 bits.
                        bitsToClear = GlobalConstants.NewHewBitsMask;

                        bitsToClear = bitsToClear << ( GlobalConstants.NewHewBits * i );

                        _hewBits = _hewBits & ( ~bitsToClear );

                        newContent = ( long ) desiredCycle;

                        newContent = newContent << ( GlobalConstants.NewHewBits - GlobalConstants.CycleBits - GlobalConstants.WaveLabelBit );

                        newContent = newContent | ( long ) newElliottWave;

                        if ( labelPosition == WaveLabelPosition.TOP )
                        {
                            newContent = newContent | GlobalConstants.NewHewLabelMask;
                        }

                        newContent = newContent << ( GlobalConstants.NewHewBits * i );

                        _hewBits = _hewBits | newContent;

                        return true;
                    }
                }
            }

            return false;
        }

        public PooledList<ElliottWaveEnum> GetWavesAtCycle( ElliottWaveCycle waveCycle )
        {
            var outputWave = new PooledList< ElliottWaveEnum >( );
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle == waveCycle ) )
                    {
                        outputWave.Add( ewavePoint.WaveName );
                    }
                }
            }

            return outputWave;
        }


        public WaveInfo? GetHewPointInfoAtCycle( ElliottWaveCycle waveCycle )
        {
            WaveInfo outputPointInfo = new WaveInfo( ElliottWaveCycle.UNKNOWN, ElliottWaveEnum.NONE, WaveLabelPosition.UNKNOWN );
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );
            var label = GetWaveLabelPosition( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle == waveCycle ) )
                    {
                        if ( label == WaveLabelPosition.BOTH )
                        {
                            outputPointInfo = ewavePoint;
                        }
                        else
                        {
                            return ewavePoint;
                        }
                    }
                }
            }


            if ( outputPointInfo.HasWave() )
            {
                return outputPointInfo;
            }

            return null;
        }

        public WaveInfo? GetHewPointInfoAtCycleOrHigherFirst( ElliottWaveCycle waveCycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle >= waveCycle ) )
                    {
                        return ewavePoint;
                    }
                }
            }

            return null;
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public WaveLabelPosition GetWaveLabelPosition()
        {
            long content = 0;
            var waveCycleCount = GetWaveDegreeCount( );
            WaveLabelPosition label = WaveLabelPosition.UNKNOWN;

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() )
                    {
                        if ( label != WaveLabelPosition.UNKNOWN )
                        {
                            if ( label != ewavePoint.LabelPosition )
                            {
                                return WaveLabelPosition.BOTH;
                            }

                        }
                        else
                        {
                            label = ewavePoint.LabelPosition;
                        }
                    }
                }
            }

            return label;
        }


        public WaveInfo? GetHewPointInfoAtCycleOrHigherLast( ElliottWaveCycle waveCycle )
        {
            WaveInfo outputPointInfo = new WaveInfo( ElliottWaveCycle.UNKNOWN, ElliottWaveEnum.NONE, WaveLabelPosition.UNKNOWN );
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );
            var label = GetWaveLabelPosition( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle >= waveCycle ) )
                    {
                        if ( label == WaveLabelPosition.BOTH )
                        {
                            outputPointInfo = ewavePoint;
                        }
                        else
                        {
                            return ewavePoint;
                        }
                    }
                }
            }


            if ( outputPointInfo.HasWave() )
            {
                return outputPointInfo;
            }

            return null;
        }

        public bool HasWaveAboveCycle( ElliottWaveCycle waveCycle )
        {
            if ( waveCycle == ElliottWaveCycle.UNKNOWN )
            {
                throw new ArgumentException();
            }

            var wave = ElliottWaveEnum.NONE;
            var cycle = ElliottWaveCycle.UNKNOWN;
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    content = content & GlobalConstants.NewHewBitsMask;

                    wave = ( ElliottWaveEnum ) ( content & ( GlobalConstants.NewHewContentMask ) );

                    cycle = ( ElliottWaveCycle ) ( ( content & ( GlobalConstants.NewHewCycleMask ) ) >> ( GlobalConstants.NewHewBits - GlobalConstants.CycleBits - GlobalConstants.WaveLabelBit ) );

                    if ( cycle > waveCycle )
                    {
                        return true;
                    }
                }
            }


            return false;
        }



        public bool HasWaveEqualOrAboveCycle( ElliottWaveCycle waveCycle )
        {
            if ( waveCycle == ElliottWaveCycle.UNKNOWN )
            {
                throw new ArgumentException();
            }

            var wave = ElliottWaveEnum.NONE;
            var cycle = ElliottWaveCycle.UNKNOWN;
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    content = content & GlobalConstants.NewHewBitsMask;

                    wave = ( ElliottWaveEnum ) ( content & ( GlobalConstants.NewHewContentMask ) );

                    cycle = ( ElliottWaveCycle ) ( ( content & ( GlobalConstants.NewHewCycleMask ) ) >> ( GlobalConstants.NewHewBits - GlobalConstants.CycleBits - GlobalConstants.WaveLabelBit ) );

                    if ( cycle >= waveCycle )
                    {
                        return true;
                    }
                }
            }


            return false;
        }

        public void CycleUpAllWaves()
        {
            var labelPosition = WaveLabelPosition.UNKNOWN;
            var wave = ElliottWaveEnum.NONE;
            var cycle = ElliottWaveCycle.UNKNOWN;
            long newHew = 0;
            int bitwiseWave = 0;
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    content = content & GlobalConstants.NewHewBitsMask;

                    wave = ( ElliottWaveEnum ) ( content & ( GlobalConstants.NewHewContentMask ) );
                    cycle = ( ElliottWaveCycle ) ( ( content & ( GlobalConstants.NewHewCycleMask ) ) >> ( GlobalConstants.NewHewBits - GlobalConstants.CycleBits - GlobalConstants.WaveLabelBit ) );
                    labelPosition = ( WaveLabelPosition ) ( ( content & ( GlobalConstants.NewHewLabelMask ) ) >> ( GlobalConstants.NewHewBits - GlobalConstants.WaveLabelBit ) );

                    if ( wave > ElliottWaveEnum.NONE )
                    {
                        bitwiseWave = ( int ) ( cycle + GlobalConstants.OneWaveCycle );

                        if ( bitwiseWave > ( int ) ElliottWaveCycle.GrandSupercycle )
                        {
                            bitwiseWave = ( int ) ElliottWaveCycle.GrandSupercycle;
                        }

                        bitwiseWave = bitwiseWave << ( GlobalConstants.NewHewBits - GlobalConstants.CycleBits - GlobalConstants.WaveLabelBit );

                        bitwiseWave = bitwiseWave | ( int ) wave;

                        if ( labelPosition == WaveLabelPosition.TOP )
                        {
                            bitwiseWave = bitwiseWave | GlobalConstants.NewHewLabelMask;
                        }

                        newHew |= ( long ) bitwiseWave << i * GlobalConstants.NewHewBits;
                    }
                }

                _hewBits = newHew;
            }

        }

        public void CycleDownAllWaves()
        {
            var wave = ElliottWaveEnum.NONE;
            var cycle = ElliottWaveCycle.UNKNOWN;
            long newHew = 0;
            int bitwiseWave = 0;
            var labelPosition = WaveLabelPosition.UNKNOWN;

            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    content = content & GlobalConstants.NewHewBitsMask;

                    wave = ( ElliottWaveEnum ) ( content & ( GlobalConstants.NewHewContentMask ) );
                    cycle = ( ElliottWaveCycle ) ( ( content & ( GlobalConstants.NewHewCycleMask ) ) >> ( GlobalConstants.NewHewBits - GlobalConstants.CycleBits - GlobalConstants.WaveLabelBit ) );
                    labelPosition = ( WaveLabelPosition ) ( ( content & ( GlobalConstants.NewHewLabelMask ) ) >> ( GlobalConstants.NewHewBits - GlobalConstants.WaveLabelBit ) );

                    if ( wave > ElliottWaveEnum.NONE )
                    {
                        bitwiseWave = ( int ) ( cycle - GlobalConstants.OneWaveCycle );

                        if ( bitwiseWave < 0 )
                        {
                            bitwiseWave = 0;
                        }

                        bitwiseWave = bitwiseWave << ( GlobalConstants.NewHewBits - GlobalConstants.CycleBits - GlobalConstants.WaveLabelBit );

                        bitwiseWave = bitwiseWave | ( int ) wave;

                        if ( labelPosition == WaveLabelPosition.TOP )
                        {
                            bitwiseWave = bitwiseWave | GlobalConstants.NewHewLabelMask;
                        }

                        newHew |= ( long ) bitwiseWave << i * GlobalConstants.NewHewBits;
                    }
                }

                _hewBits = newHew;
            }
        }


        public void CycleDownWave( WaveInfo waveInfo )
        {
            long newHew = 0;

            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() )
                    {
                        if ( ewavePoint == waveInfo )
                        {
                            newHew |= ewavePoint.SmallCycleDown( i );
                        }
                        else
                        {
                            newHew |= ewavePoint.SmallHewBits( i );
                        }


                    }
                }

                _hewBits = newHew;
            }
        }

        public void CycleDownWave( WaveInfo waveInfo, ElliottWaveCycle minimumCycle )
        {
            long newHew = 0;

            long content = 0;

            int newWaveCount = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() )
                    {
                        if ( ewavePoint == waveInfo )
                        {
                            if ( ewavePoint.WaveCycle > minimumCycle )
                            {
                                newHew |= ewavePoint.SmallCycleDown( i );

                                newWaveCount += 1;
                            }

                        }
                        else
                        {
                            newHew |= ewavePoint.SmallHewBits( i );

                            newWaveCount += 1;
                        }


                    }
                }

                _hewBits = newHew;
            }


            waveCycleCount = newWaveCount;
        }


        public void CycleUpWave( WaveInfo waveInfo )
        {
            long newHew = 0;

            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() )
                    {
                        if ( ewavePoint == waveInfo )
                        {
                            newHew |= ewavePoint.SmallCycleUp( i );
                        }
                        else
                        {
                            newHew |= ewavePoint.SmallHewBits( i );
                        }


                    }
                }

                _hewBits = newHew;
            }
        }



        public bool HasElliottWave
        {
            get { return _hewBits > 0; }
        }
                

        public PooledList<WaveInfo> GetAllWaves()
        {
            var output = new PooledList< WaveInfo >( 12 );

            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );

                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() )
                    {
                        output.Add( ewavePoint );
                    }
                    else
                    {
                        throw new InvalidProgramException();
                    }
                }
            }

            return output;
        }

        public PooledList<WaveInfo> GetAllWaves( bool isFilterAB, ElliottWaveCycle miniCycle )
        {
            var output = new PooledList< WaveInfo >( 12 );

            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( isFilterAB && waveCycleCount > 0 && waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );

                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ewavePoint.WaveCycle >= miniCycle)
                    {
                        output.Add( ewavePoint );
                    }                    
                }
            }

            return output;
        }

        public PooledList<WaveInfo> GetAllWaves( ElliottWaveCycle minimumWaveCycle )
        {
            var output = new PooledList< WaveInfo >( 12 );

            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );

                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ewavePoint.WaveCycle >= minimumWaveCycle )
                    {
                        output.Add( ewavePoint );
                    }
                }
            }

            return output;
        }

        public int GetNumOfWaveC()
        {
            var output = new PooledList< WaveInfo >( 12 );

            int count = 0;

            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );

                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() )
                    {
                        if ( ewavePoint.WaveName == ElliottWaveEnum.WaveC )
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
        }

        public PooledList<WaveInfo> GetTopWaves()
        {
            var output = new PooledList< WaveInfo >( 12 );

            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );

                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ewavePoint.LabelPosition == WaveLabelPosition.TOP )
                    {
                        output.Add( ewavePoint );
                    }
                }
            }


            return output;
        }

        public PooledList<WaveInfo> GetTopWaves( bool isFilterAB, ElliottWaveCycle miniCycle )
        {
            var topWaves = new PooledList< WaveInfo >( 12 );

            topWaves = GetTopWaves( miniCycle );

            if ( isFilterAB && topWaves.Count > 1 )
            {
                var highest      = GetHighestTopWave( );

                var mySortedList = topWaves.OrderByDescending( l => l.WaveCycle ).ThenByDescending( l => l.WaveName );

                var newOutput    = new PooledList< WaveInfo >( 12 );

                foreach ( WaveInfo wave in mySortedList )
                {
                    if ( wave == highest )
                    {
                        newOutput.Add( wave );
                    }

                    if ( wave.WaveName == ElliottWaveEnum.WaveA || wave.WaveName == ElliottWaveEnum.WaveB )
                    {
                        continue;
                    }

                    if ( wave.WaveName == ElliottWaveEnum.Wave1C )
                    {
                        var modifiedWave = new WaveInfo( wave.WaveCycle, ElliottWaveEnum.Wave1, wave.LabelPosition );
                        newOutput.Add( modifiedWave );
                    }
                    else if ( wave.WaveName == ElliottWaveEnum.Wave3C )
                    {
                        var modifiedWave = new WaveInfo( wave.WaveCycle, ElliottWaveEnum.Wave3, wave.LabelPosition );
                        newOutput.Add( modifiedWave );
                    }
                    else if ( wave.WaveName == ElliottWaveEnum.Wave5C )
                    {
                        var modifiedWave = new WaveInfo( wave.WaveCycle, ElliottWaveEnum.Wave5, wave.LabelPosition );
                        newOutput.Add( modifiedWave );
                    }
                }

                return newOutput;
            }


            return topWaves;
        }

        public PooledList<WaveInfo> GetTopWaves( ElliottWaveCycle minimumWaveCycle )
        {
            var output = new PooledList< WaveInfo >( 12 );

            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );

                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ewavePoint.WaveCycle >= minimumWaveCycle && ewavePoint.LabelPosition == WaveLabelPosition.TOP )
                    {
                        output.Add( ewavePoint );
                    }
                }
            }

            return output;
        }

        public WaveInfo? GetHighestTopWave()
        {
            WaveInfo outputPointInfo = new WaveInfo( ElliottWaveCycle.UNKNOWN, ElliottWaveEnum.NONE, WaveLabelPosition.UNKNOWN );

            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );

                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ewavePoint.LabelPosition == WaveLabelPosition.TOP )
                    {
                        if ( ewavePoint.WaveCycle > outputPointInfo.WaveCycle )
                        {
                            outputPointInfo = ewavePoint;
                        }
                    }
                }
            }


            if ( outputPointInfo.WaveCycle != ElliottWaveCycle.UNKNOWN )
                return outputPointInfo;

            return null;
        }

        public WaveInfo? GetLowestTopWave()
        {
            WaveInfo outputPointInfo = new WaveInfo( ElliottWaveCycle.GrandSupercycle, ElliottWaveEnum.NONE, WaveLabelPosition.UNKNOWN );

            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );

                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ewavePoint.LabelPosition == WaveLabelPosition.TOP )
                    {
                        if ( ewavePoint.WaveCycle < outputPointInfo.WaveCycle )
                        {
                            outputPointInfo = ewavePoint;
                        }
                    }
                }
            }

            if ( outputPointInfo.WaveCycle != ElliottWaveCycle.GrandSupercycle )
                return outputPointInfo;

            return null;
        }


        public PooledList<WaveInfo> GetHighestDegreeWaves()
        {
            var output = new PooledList< WaveInfo >( 12 );

            ElliottWaveCycle waveCycle = ElliottWaveCycle.UNKNOWN;

            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );

                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() )
                    {
                        if ( ewavePoint.WaveCycle > waveCycle )
                        {
                            output.Clear();

                            waveCycle = ewavePoint.WaveCycle;

                            output.Add( ewavePoint );
                        }
                        else if ( ewavePoint.WaveCycle == waveCycle )
                        {
                            output.Add( ewavePoint );
                        }


                    }

                }
            }

            return output;
        }

        public WaveInfo? GetHighestBottomWave()
        {
            WaveInfo outputPointInfo = new WaveInfo( ElliottWaveCycle.UNKNOWN, ElliottWaveEnum.NONE, WaveLabelPosition.UNKNOWN );

            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );

                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ewavePoint.LabelPosition == WaveLabelPosition.BOTTOM )
                    {
                        if ( ewavePoint.WaveCycle > outputPointInfo.WaveCycle )
                        {
                            outputPointInfo = ewavePoint;
                        }
                    }
                }
            }

            if ( outputPointInfo.WaveCycle != ElliottWaveCycle.UNKNOWN )
                return outputPointInfo;

            return null;
        }

        public WaveInfo? GetLowestBottomWave()
        {
            WaveInfo outputPointInfo = new WaveInfo( ElliottWaveCycle.GrandSupercycle, ElliottWaveEnum.NONE, WaveLabelPosition.UNKNOWN );

            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );

                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ewavePoint.LabelPosition == WaveLabelPosition.BOTTOM )
                    {
                        if ( ewavePoint.WaveCycle < outputPointInfo.WaveCycle )
                        {
                            outputPointInfo = ewavePoint;
                        }
                    }
                }
            }

            if ( outputPointInfo.WaveCycle != ElliottWaveCycle.GrandSupercycle )
                return outputPointInfo;

            return null;
        }

        public PooledList<WaveInfo> GetBottomWaves()
        {
            var output = new PooledList< WaveInfo >( 12 );

            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < GlobalConstants.MaxCountPerLong; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ewavePoint.LabelPosition == WaveLabelPosition.BOTTOM )
                    {
                        output.Add( ewavePoint );
                    }
                }
            }

            return output;
        }

        public PooledList<WaveInfo> GetBottomWaves( bool isFilterAB, ElliottWaveCycle miniCycle )
        {
            var bottomWaves = new PooledList< WaveInfo >( 12 );

            bottomWaves = GetBottomWaves( miniCycle );

            if ( isFilterAB && bottomWaves.Count > 1 )
            {
                var highest = GetHighestBottomWave( );

                var mySortedList = bottomWaves.OrderBy( l => l.WaveCycle ).ThenBy( l => l.WaveName );

                var newOutput = new PooledList< WaveInfo >( 12 );

                foreach ( WaveInfo wave in bottomWaves )
                {
                    if ( wave.WaveName == ElliottWaveEnum.WaveA || wave.WaveName == ElliottWaveEnum.WaveB )
                    {
                        continue;
                    }

                    if ( wave.WaveName == ElliottWaveEnum.Wave1C )
                    {
                        var modifiedWave = new WaveInfo( wave.WaveCycle, ElliottWaveEnum.Wave1, wave.LabelPosition );
                        newOutput.Add( modifiedWave );
                    }
                    else if ( wave.WaveName == ElliottWaveEnum.Wave3C )
                    {
                        var modifiedWave = new WaveInfo( wave.WaveCycle, ElliottWaveEnum.Wave3, wave.LabelPosition );
                        newOutput.Add( modifiedWave );
                    }
                    else if ( wave.WaveName == ElliottWaveEnum.Wave5C )
                    {
                        var modifiedWave = new WaveInfo( wave.WaveCycle, ElliottWaveEnum.Wave5, wave.LabelPosition );
                        newOutput.Add( modifiedWave );
                    }
                }

                if ( newOutput.Count == 0 )
                {
                    if ( highest.HasValue )
                    {
                        newOutput.Add( highest.Value );
                    }
                }

                return newOutput;
            }


            return bottomWaves;
        }

        public PooledList<WaveInfo> GetBottomWaves( ElliottWaveCycle minimumWaveCycle )
        {
            var output = new PooledList< WaveInfo >( 12 );

            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < GlobalConstants.MaxCountPerLong; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ewavePoint.WaveCycle >= minimumWaveCycle && ewavePoint.LabelPosition == WaveLabelPosition.BOTTOM )
                    {
                        output.Add( ewavePoint );
                    }
                }
            }

            return output;
        }


        public WaveInfo? GetLastWave()
        {
            var ewavePoint = new WaveInfo( _hewBits );

            if ( ewavePoint.HasWave() )
            {
                if ( ewavePoint.LabelPosition == WaveLabelPosition.TOP )
                {
                    var bottomWaves = GetBottomWaves( );

                    if ( bottomWaves.Count > 0 )
                    {
                        return bottomWaves[ bottomWaves.Count - 1 ];
                    }
                }
                else if ( ewavePoint.LabelPosition == WaveLabelPosition.BOTTOM )
                {
                    var topWaves = GetTopWaves( );

                    if ( topWaves.Count > 0 )
                    {
                        return topWaves[ topWaves.Count - 1 ];
                    }
                }
            }

            return null;
        }

        public WaveInfo? GetFirstOppositeWave()
        {
            var ewavePoint = new WaveInfo( _hewBits );

            if ( ewavePoint.HasWave() )
            {
                if ( ewavePoint.LabelPosition == WaveLabelPosition.TOP )
                {
                    var bottomWaves = GetBottomWaves( );

                    if ( bottomWaves.Count > 0 )
                    {
                        return bottomWaves[ 0 ];
                    }
                }
                else if ( ewavePoint.LabelPosition == WaveLabelPosition.BOTTOM )
                {
                    var topWaves = GetTopWaves( );

                    if ( topWaves.Count > 0 )
                    {
                        return topWaves[ 0 ];
                    }
                }
            }

            return null;
        }


        public WaveInfo? GetLowestDegreeWaveInfo()
        {
            WaveInfo lowestCycleWave = new WaveInfo( ElliottWaveCycle.GrandSupercycle, ElliottWaveEnum.NONE, WaveLabelPosition.UNKNOWN );

            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );

                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() )
                    {
                        if ( ewavePoint.WaveCycle < lowestCycleWave.WaveCycle )
                        {
                            lowestCycleWave = ewavePoint;
                        }
                    }
                }
            }

            if ( lowestCycleWave.HasWave() )
            {
                return lowestCycleWave;
            }

            return null;
        }

        public WaveInfo? GetFirstWave()
        {
            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount < 1 )
                return null;

            var ewavePoint = new WaveInfo( _hewBits );

            if ( ewavePoint.HasWave() )
            {
                return ewavePoint;
            }

            return null;
        }

        public WaveInfo? GetSecondaryWave()
        {
            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount < 1 )
                return null;

            var ewavePoint = new WaveInfo( _hewBits );

            if ( ewavePoint.HasWave() )
            {
                var currentWaveLabel = ewavePoint.LabelPosition;
                var secondaryWavePos = WaveLabelPosition.UNKNOWN;

                if ( currentWaveLabel == WaveLabelPosition.TOP )
                {
                    secondaryWavePos = WaveLabelPosition.BOTTOM;
                }
                else if ( currentWaveLabel == WaveLabelPosition.BOTTOM )
                {
                    secondaryWavePos = WaveLabelPosition.TOP;
                }
                else
                {
                    return null;
                }

                long content = 0;

                if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
                {
                    for ( int i = 0; i < waveCycleCount; i++ )
                    {
                        content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                        ewavePoint = new WaveInfo( content );

                        if ( ewavePoint.HasWave() )
                        {
                            if ( ewavePoint.LabelPosition == secondaryWavePos )
                            {
                                return ewavePoint;
                            }
                        }
                    }
                }
            }

            return null;
        }

        public WaveInfo? GetLastHighestWaveDegree()
        {
            WaveInfo highestCycleWave = new WaveInfo( ElliottWaveCycle.UNKNOWN, ElliottWaveEnum.NONE, WaveLabelPosition.UNKNOWN );

            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() )
                    {
                        if ( ewavePoint.WaveCycle >= highestCycleWave.WaveCycle )
                        {
                            highestCycleWave = ewavePoint;
                        }
                    }
                }
            }

            if ( highestCycleWave.HasWave() )
            {
                return highestCycleWave;
            }

            return null;
        }

        public WaveInfo? GetFirstHighestWaveInfo()
        {
            WaveInfo highestCycleWave = new WaveInfo( ElliottWaveCycle.UNKNOWN, ElliottWaveEnum.NONE, WaveLabelPosition.UNKNOWN );

            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() )
                    {
                        if ( ewavePoint.WaveCycle > highestCycleWave.WaveCycle )
                        {
                            highestCycleWave = ewavePoint;
                        }
                    }
                }
            }


            if ( highestCycleWave.HasWave() )
            {
                return highestCycleWave;
            }

            return null;
        }



        public bool HasWave( ElliottWaveCycle waveCycle )
        {
            ElliottWaveEnum wave = ElliottWaveEnum.NONE;
            ElliottWaveCycle cycle = ElliottWaveCycle.UNKNOWN;

            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    content = content & GlobalConstants.NewHewBitsMask;

                    wave    = ( ElliottWaveEnum ) ( content & ( GlobalConstants.NewHewContentMask ) );

                    cycle   = ( ElliottWaveCycle ) ( ( content & ( GlobalConstants.NewHewCycleMask ) ) >> ( GlobalConstants.NewHewBits - GlobalConstants.CycleBits - GlobalConstants.WaveLabelBit ) );

                    if ( cycle == waveCycle )
                    {
                        return true;
                    }
                }
            }


            return false;
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public int GetWaveDegreeCount()
        {
            int count = 0;

            ElliottWaveEnum wave = ElliottWaveEnum.NONE;
            ElliottWaveCycle cycle = ElliottWaveCycle.UNKNOWN;

            long content = 0;

            int counter = ( int ) Math.Round( ( double ) ( sizeof( long ) * 8 / GlobalConstants.NewHewBits ) );

            for ( int i = 0; i < counter; i++ )
            {
                content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                content = content & GlobalConstants.NewHewBitsMask;

                wave = ( ElliottWaveEnum ) ( content & ( GlobalConstants.NewHewContentMask ) );

                cycle = ( ElliottWaveCycle ) ( ( content & ( GlobalConstants.NewHewCycleMask ) ) >> ( GlobalConstants.NewHewBits - GlobalConstants.CycleBits - GlobalConstants.WaveLabelBit ) );

                if ( wave != ElliottWaveEnum.NONE )
                {
                    count++;
                }
            }

            return count;
        }



        public bool isValidPreviousWave( ElliottWaveCycle waveCycle,
                                         ElliottWaveEnum previousWave )
        {
            if ( previousWave == ElliottWaveEnum.NONE )
            {
                return false;
            }

            var lowestCycleWave = GetLowestDegreeWaveInfo( );

            if ( lowestCycleWave.HasValue )
            {
                if ( waveCycle != lowestCycleWave.Value.WaveCycle )
                {
                    return false;
                }
            }


            return false;
        }

        public bool IsWave5OrHigherDegree( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle >= cycle + GlobalConstants.OneWaveCycle ) )
                    {
                        return true;
                    }
                    else if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle == cycle ) )
                    {
                        if (
                             ewavePoint.WaveName == ElliottWaveEnum.Wave5 ||
                             ewavePoint.WaveName == ElliottWaveEnum.Wave5C )
                        {
                            return true;
                        }
                    }
                }
            }


            return false;
            //wave.Hew.GetWaveAtCycle( nextCycle ) != ElliottWave.NONE ||
        }

        public bool IsWaveX( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle == cycle ) )
                    {
                        if ( ewavePoint.WaveName == ElliottWaveEnum.WaveX )
                        {
                            return true;
                        }
                    }
                }
            }


            return false;
        }

        public bool IsAnyWaveOfOneHigherDegree( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle >= cycle + GlobalConstants.OneWaveCycle ) )
                    {
                        return true;
                    }
                }
            }


            return false;
            //wave.Hew.GetWaveAtCycle( nextCycle ) != ElliottWave.NONE ||
        }

        public bool IsAnyWaveOfOneHigherDegree_ScanBackward( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = waveCycleCount - 1; i >= 0; i-- )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle >= cycle + GlobalConstants.OneWaveCycle ) )
                    {
                        return true;
                    }
                }
            }


            return false;

        }

        public bool IsWaveX_ScanBackward( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = waveCycleCount - 1; i >= 0; i-- )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle == cycle ) )
                    {
                        if ( ewavePoint.WaveName == ElliottWaveEnum.WaveX )
                        {
                            return true;
                        }
                    }
                }
            }


            return false;

        }

        public bool HasHigherDegreeWave( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle > cycle ) )
                    {
                        return true;
                    }
                }
            }


            return false;
            //wave.Hew.GetWaveAtCycle( nextCycle ) != ElliottWave.NONE ||
        }


        public bool HasWave0X_OrHigher( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle >= cycle + GlobalConstants.OneWaveCycle ) )
                    {
                        return true;
                    }
                    else if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle == cycle ) )
                    {
                        if ( ewavePoint.WaveName == ElliottWaveEnum.WaveX )
                        {
                            return true;
                        }
                    }
                }
            }


            return false;
            //wave.Hew.GetWaveAtCycle( nextCycle ) != ElliottWave.NONE ||
        }

        public bool HasWaveEFA( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle == cycle ) )
                    {
                        if ( ewavePoint.WaveName == ElliottWaveEnum.WaveEFA )
                        {
                            return true;
                        }
                    }
                }
            }


            return false;
            //wave.Hew.GetWaveAtCycle( nextCycle ) != ElliottWave.NONE ||
        }


        public bool HasWave024XOrHigher( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle >= cycle + GlobalConstants.OneWaveCycle ) )
                    {
                        return true;
                    }
                    else if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle == cycle ) )
                    {
                        if (
                             ewavePoint.WaveName == ElliottWaveEnum.WaveX ||
                             ewavePoint.WaveName == ElliottWaveEnum.Wave2 ||
                             ewavePoint.WaveName == ElliottWaveEnum.Wave4 )
                        {
                            return true;
                        }
                    }
                }
            }


            return false;
            //wave.Hew.GetWaveAtCycle( nextCycle ) != ElliottWave.NONE ||
        }

        public bool HasWave2OfDegree( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle == cycle ) && ( ewavePoint.WaveName == ElliottWaveEnum.Wave2 ) )
                    {
                        return true;
                    }
                }
            }


            return false;
            //wave.Hew.GetWaveAtCycle( nextCycle ) != ElliottWave.NONE ||
        }

        public bool HasWave1OfDegree( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle == cycle ) && ( ewavePoint.WaveName == ElliottWaveEnum.Wave1 || ewavePoint.WaveName == ElliottWaveEnum.Wave1C ) )
                    {
                        return true;
                    }
                }
            }


            return false;
            //wave.Hew.GetWaveAtCycle( nextCycle ) != ElliottWave.NONE ||
        }

        public bool HasWave3OfDegree( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle == cycle ) && ( ewavePoint.WaveName == ElliottWaveEnum.Wave3 || ewavePoint.WaveName == ElliottWaveEnum.Wave3C ) )
                    {
                        return true;
                    }
                }
            }


            return false;
            //wave.Hew.GetWaveAtCycle( nextCycle ) != ElliottWave.NONE ||
        }

        public bool HasWave4OfDegree( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle == cycle ) && ( ewavePoint.WaveName == ElliottWaveEnum.Wave4 ) )
                    {
                        return true;
                    }
                }
            }


            return false;
            //wave.Hew.GetWaveAtCycle( nextCycle ) != ElliottWave.NONE ||
        }

        public bool HasWaveAOfDegree( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle == cycle ) && ( ewavePoint.WaveName == ElliottWaveEnum.WaveA ) )
                    {
                        return true;
                    }
                }
            }


            return false;
            //wave.Hew.GetWaveAtCycle( nextCycle ) != ElliottWave.NONE ||
        }


        public bool HasWaveBOfDegree( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle == cycle ) && ( ewavePoint.WaveName == ElliottWaveEnum.WaveB ) )
                    {
                        return true;
                    }
                }
            }


            return false;
            //wave.Hew.GetWaveAtCycle( nextCycle ) != ElliottWave.NONE ||
        }



        public bool IsBeginningWaveOfExpandedFlat( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() )
                    {
                        if ( ( ewavePoint.WaveName == ElliottWaveEnum.Wave1 ||                  // We are beginning of Wave 2
                               ewavePoint.WaveName == ElliottWaveEnum.Wave1C ||
                               ewavePoint.WaveName == ElliottWaveEnum.Wave3 ||
                               ewavePoint.WaveName == ElliottWaveEnum.Wave3C ||                 // We are beginning of Wave 4
                               ewavePoint.WaveName == ElliottWaveEnum.WaveA ||                  // We are beginning of Wave B
                               ewavePoint.WaveName == ElliottWaveEnum.WaveC                     // We are beginning of Wave X
                             )
                             && ( ewavePoint.WaveCycle >= cycle + GlobalConstants.OneWaveCycle )
                           )
                        {
                            return true;
                        }

                    }
                }
            }


            return false;
        }


        /// <summary>
        /// Corrections will develop in the following waves, wave (ii), Wave (iv), Wave (b) and Wave (x).
        /// All types of corrections can take the form of any corrective structure with the exception of Wave( ii) that will never develop as a triangle.
        /// </summary>
        /// <param name="cycle"></param>
        /// <returns></returns>
        public bool IsBeginningWaveOfTriangle( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveName == ElliottWaveEnum.WaveX || ewavePoint.WaveName == ElliottWaveEnum.Wave3 || ewavePoint.WaveName == ElliottWaveEnum.WaveA || ewavePoint.WaveName == ElliottWaveEnum.WaveC ) && ( ewavePoint.WaveCycle >= cycle + GlobalConstants.OneWaveCycle ) )
                    {
                        return true;
                    }
                }
            }


            return false;
        }


        public bool IsWaveAOfTriangle( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveName == ElliottWaveEnum.WaveTA && ewavePoint.WaveCycle == cycle ) )
                    {
                        return true;
                    }
                }
            }


            return false;
        }


        public bool IsWaveAOfTriangle_ScanBackward( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = waveCycleCount - 1; i >= 0; i-- )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveName == ElliottWaveEnum.WaveTA && ewavePoint.WaveCycle == cycle ) )
                    {
                        return true;
                    }
                }
            }


            return false;
        }


        public bool IsWaveBOfTriangle( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveName == ElliottWaveEnum.WaveTB && ewavePoint.WaveCycle == cycle ) )
                    {
                        return true;
                    }
                }
            }


            return false;
        }


        public bool IsWaveBOfTriangle_ScanBackward( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = waveCycleCount - 1; i >= 0; i-- )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveName == ElliottWaveEnum.WaveTB && ewavePoint.WaveCycle == cycle ) )
                    {
                        return true;
                    }
                }
            }


            return false;
        }



        public bool IsWaveCOfTriangle( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveName == ElliottWaveEnum.WaveTC && ewavePoint.WaveCycle == cycle ) )
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        public bool IsWaveCOfTriangle_ScanBackward( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = waveCycleCount - 1; i >= 0; i-- )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveName == ElliottWaveEnum.WaveTC && ewavePoint.WaveCycle == cycle ) )
                    {
                        return true;
                    }
                }
            }


            return false;
        }


        public bool IsWaveDOfTriangle( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveName == ElliottWaveEnum.WaveTD && ewavePoint.WaveCycle == cycle ) )
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        public bool IsWaveDOfTriangle_ScanBackward( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = waveCycleCount - 1; i >= 0; i-- )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveName == ElliottWaveEnum.WaveTD && ewavePoint.WaveCycle == cycle ) )
                    {
                        return true;
                    }
                }
            }

            return false;
        }



        public bool IsBeginningWaveOfTriangle_ScanBackward( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = waveCycleCount - 1; i >= 0; i-- )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveName == ElliottWaveEnum.Wave3 || ewavePoint.WaveName == ElliottWaveEnum.WaveA || ewavePoint.WaveName == ElliottWaveEnum.WaveC ) && ( ewavePoint.WaveCycle >= cycle + GlobalConstants.OneWaveCycle ) )
                    {
                        return true;
                    }
                }
            }


            return false;
        }


        public bool IsWave12345OrHigherDegree( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle >= cycle + GlobalConstants.OneWaveCycle ) )
                    {
                        return true;
                    }
                    else if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle == cycle ) )
                    {
                        if (
                             ewavePoint.WaveName == ElliottWaveEnum.Wave1 ||
                             ewavePoint.WaveName == ElliottWaveEnum.Wave1C ||
                             ewavePoint.WaveName == ElliottWaveEnum.Wave2 ||
                             ewavePoint.WaveName == ElliottWaveEnum.Wave3 ||
                             ewavePoint.WaveName == ElliottWaveEnum.Wave3C ||
                             ewavePoint.WaveName == ElliottWaveEnum.Wave4 ||
                             ewavePoint.WaveName == ElliottWaveEnum.Wave5 ||
                             ewavePoint.WaveName == ElliottWaveEnum.Wave5C )
                        {
                            return true;
                        }
                    }
                }
            }


            return false;
            //wave.Hew.GetWaveAtCycle( nextCycle ) != ElliottWave.NONE ||
        }

        public WaveInfo? GetWavePointInfo( ElliottWaveEnum beginningWaveName,
                                           ElliottWaveCycle waveCycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.Equals( waveCycle, beginningWaveName ) )
                    {
                        return ewavePoint;
                    }
                }
            }


            return null;
        }

        public override string ToString()
        {
            string output = "";

            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() )
                    {
                        output += ewavePoint.ToString() + " ";
                    }
                }
            }


            return output;
        }

        public void MergeWave( long waveBit )
        {
            if ( waveBit != _hewBits )
            {
                var higherTimeFrameWave = new HewLong( waveBit );

                var allWaves = GetAllWaves( );

                var higherWaves = higherTimeFrameWave.GetAllWaves( );

                foreach ( var higherWave in higherWaves )
                {
                    var index = allWaves.FindIndex( x => x.Equals( higherWave ) );

                    if ( index == -1 )
                    {
                        AddHarmonicElliottWave( higherWave );
                    }
                }
            }
        }

        public void MergeWavesList( PooledList<WaveInfo> topWaves )
        {
            var allWaves = GetAllWaves( );

            foreach ( WaveInfo wave in topWaves )
            {
                var index = allWaves.FindIndex( x => x.Equals( wave ) );

                if ( index == -1 )
                {
                    AddHarmonicElliottWave( wave );
                }
            }
        }

        public void MergeWave( WaveInfo wave )
        {
            var allWaves = GetAllWaves( );

            var index = allWaves.FindIndex( x => x.Equals( wave ) );

            if ( index == -1 )
            {
                AddHarmonicElliottWave( wave );
            }
        }



        

        public void RemoveWaves( ref HewLong hew )
        {
            var tempWave = new HewLong( );

            var otherWaves = hew.GetAllWaves( );

            var myWaves = GetAllWaves( );

            foreach ( var myWave in myWaves )
            {
                var index = otherWaves.FindIndex( x => x.Equals( myWave ) );

                if ( index == -1 )
                {
                    tempWave.AddHarmonicElliottWave( myWave );
                }
            }

            if ( tempWave.Count > 0 )
            {
                CopyFrom( ref tempWave );
            }
            else
            {
                ResetWaves();
            }
        }

        public bool RemoveMatchedWaves( IHarmonicElliottWave<uint> hew )
        {
            if ( Count == 0 ) return false;

            var myWaves    = GetAllWaves( );
            bool found     = false;
            var tempWave   = new HewLong( );
            var otherWaves = hew.GetAllWaves( );

            foreach ( var myWave in myWaves )
            {
                // Find waves that are not the same as input waves
                var index = otherWaves.FindIndex( x => x.Equals( myWave ) );

                if ( index == -1 )
                {
                    tempWave.AddHarmonicElliottWave( myWave );
                }
                else
                {
                    found = true;
                }
            }

            if ( tempWave.Count > 0 )
            {
                CopyFrom( ref tempWave );
            }
            else
            {
                ResetWaves();
            }


            return found;
        }


        public bool RemoveMatchedWaves( IHarmonicElliottWave<long> hew )
        {
            if ( Count == 0 ) return false;

            var myWaves    = GetAllWaves( );
            bool found     = false;
            var tempWave   = new HewLong( );
            var otherWaves = hew.GetAllWaves( );

            foreach ( var myWave in myWaves )
            {
                // Find waves that are not the same as input waves
                var index = otherWaves.FindIndex( x => x.Equals( myWave ) );

                if ( index == -1 )
                {
                    tempWave.AddHarmonicElliottWave( myWave );
                }
                else
                {
                    found = true;
                }
            }

            if ( tempWave.Count > 0 )
            {
                CopyFrom( ref tempWave );
            }
            else
            {
                ResetWaves();
            }


            return found;
        }

        
        public void CopyFrom( ref HewLong otherWave )
        {
            if ( otherWave.RawWave < long.MaxValue )
            {
                _hewBits = otherWave.RawWave;
            }
            else
            {
                var allWaves = otherWave.GetAllWaves( );

                var sortedWave = allWaves.OrderByDescending( x => x );

                foreach ( var wave in allWaves )
                {
                    var existing = GetWavesAtCycle( wave.WaveCycle );

                    if ( existing.Count == 0 )
                    {
                        AddHarmonicElliottWave( wave.WaveCycle, wave.WaveName, wave.LabelPosition );
                    }
                    else
                    {
                        if ( wave.WaveName.IsBetterThan( existing[ 0 ] ) )
                        {
                            ReplaceWave( wave.WaveCycle, existing[ 0 ], wave.WaveName );
                        }
                    }


                }

                //throw new ArgumentException();
            }
        }



        public WaveLabelPosition GetLabelPositionFromHew()
        {
            long content          = 0;
            var labelPosition     = WaveLabelPosition.UNKNOWN;
            var lastLabelPosition = WaveLabelPosition.UNKNOWN;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    content = content & GlobalConstants.NewHewBitsMask;
                    labelPosition = ( WaveLabelPosition ) ( ( content & ( GlobalConstants.NewHewLabelMask ) ) >> ( GlobalConstants.NewHewBits - GlobalConstants.WaveLabelBit ) );

                    if ( lastLabelPosition == WaveLabelPosition.UNKNOWN )
                    {
                        lastLabelPosition = labelPosition;
                    }

                    if ( lastLabelPosition != labelPosition )
                    {
                        lastLabelPosition = WaveLabelPosition.BOTH;
                    }
                }
            }


            return labelPosition;
        }

        public PooledDictionary<ElliottWaveCycle, PooledList<WaveInfo>> GetWavesByDegreeDesc()
        {
            var outputWave = new PooledDictionary<ElliottWaveCycle, PooledList<WaveInfo>>();
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() )
                    {
                        if ( outputWave.ContainsKey( ewavePoint.WaveCycle ) )
                        {
                            var mylist = outputWave[ ewavePoint.WaveCycle ];
                            mylist.Add( ewavePoint );
                        }
                        else
                        {
                            var mylist = new PooledList< WaveInfo >();
                            mylist.Add( ewavePoint );

                            outputWave.Add( ewavePoint.WaveCycle, mylist );
                        }
                    }
                }
            }


            return outputWave;
        }

        public bool MatchesWave( WaveInfo matchedWaves )
        {
            var outputWave = new PooledList< ElliottWaveEnum >( );
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint == matchedWaves ) )
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void MergeInLowerTimeFrameWaves( ref HewLong otherHew, ElliottWaveCycle minimumCycle )
        {
            var allWaves = GetAllWaves( );

            var otherWaves = otherHew.GetAllWaves( minimumCycle );

            foreach ( var wave in otherWaves )
            {
                var index = allWaves.FindIndex( x => x.Equals( wave ) );

                if ( index == -1 )
                {
                    AddHarmonicElliottWave( wave );
                }
            }
        }

        
        public override bool Equals( object obj )
        {
            if ( obj is HewLong )
            {
                return Equals( ( HewLong ) obj );
            }

            return base.Equals( obj );
        }

        public static bool operator ==( HewLong first, HewLong second )
        {
            return first.Equals( second );
        }

        public static bool operator !=( HewLong first, HewLong second )
        {
            return !( first == second );
        }

        public bool Equals( HewLong other )
        {
            return _hewBits.Equals( other._hewBits );
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 47;
                hashCode = ( hashCode * 53 ) ^ _hewBits.GetHashCode();
                
                return hashCode;
            }
        }
    }
}
