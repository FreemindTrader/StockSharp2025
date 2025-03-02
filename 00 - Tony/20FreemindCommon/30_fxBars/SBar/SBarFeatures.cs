using fx.Definitions;
using StockSharp.Messages;
using System;

namespace fx.Bars
{
    /// <summary>
    /// 

    /// Bit 32  - 8 = Used for other TASignal
    /// Bit 7 - 4 = 4 Bit reserved for BarSession
    /// Bit 3      = IsSelected
    /// Bit 2      = Selection Changed
    /// Bit 1      = Is Active Or Finished Bar
    public struct SBarFeatures 
    {
        private uint _sbarFeatures;

        public TASignal TechnicalAnalysisSignal
        {
            get
            {
                var myValue = BitHelper.GetBits( _sbarFeatures, BarBits.POS_TASIGNAL_BEGIN, BarBits.LENGTH_SIGNAL );
                return ( TASignal ) myValue;
            }

            set
            {
                var newValue = (int) value;

                _sbarFeatures = (uint) BitHelper.SetBits( _sbarFeatures, newValue, 1, BarBits.POS_TASIGNAL_BEGIN, BarBits.LENGTH_SIGNAL );
            }
        }

        public SessionEnum BarSession
        {
            get
            {
                var myValue = BitHelper.GetBits( _sbarFeatures, BarBits.POS_BAR_SESSION, 4 );
                return ( SessionEnum ) myValue;
            }

            set
            {
                var newValue = (int) value;
                _sbarFeatures = ( uint ) BitHelper.SetBits( _sbarFeatures, newValue, 1, BarBits.POS_BAR_SESSION, 4 );
            }
        }

        public CandleStates State
        {
            get
            {
                var myValue = BitHelper.GetBits( _sbarFeatures, BarBits.POS_BAR_STATE, 1 );
                return ( CandleStates ) ( myValue + 1 );
            }

            set
            {
                var newValue = Math.Max( (int) value - 1, 0 ) ;
                _sbarFeatures = ( uint ) BitHelper.SetBits( _sbarFeatures, newValue, 1, BarBits.POS_BAR_STATE, 1 );
            }
        }


        public void ClearSignal()
        {
            _sbarFeatures = _sbarFeatures & BarBits.TurnOff27Bits;
        }

        

        public bool HasElliottWave
        {
            get
            {
                return BitHelper.IsBitSet( _sbarFeatures, BarBits.POS_ELLIOTT_WAVE );
            }
            set
            {
                _sbarFeatures = BitHelper.TurnOnOffBit( _sbarFeatures, BarBits.POS_ELLIOTT_WAVE, value );
            }
        }

        public bool HasFibRetExpInfo
        {
            get
            {
                return BitHelper.IsBitSet( _sbarFeatures, BarBits.POS_ELLIOTT_WAVE );
            }
            set
            {
                _sbarFeatures = BitHelper.TurnOnOffBit( _sbarFeatures, BarBits.POS_ELLIOTT_WAVE, value );
            }
        }

        public bool HasCandleStickPattern
        {
            get
            {
                return BitHelper.IsBitSet( _sbarFeatures, BarBits.POS_CANDLEPATTERN );
            }
            set
            {
                _sbarFeatures = BitHelper.TurnOnOffBit( _sbarFeatures, BarBits.POS_CANDLEPATTERN, value );
            }
        }


        public bool IsSelected
        {
            get
            {
                return BitHelper.IsBitSet( _sbarFeatures, BarBits.POS_IsSelected );
            }

            set
            {
                _sbarFeatures = BitHelper.TurnOnOffBit( _sbarFeatures, BarBits.POS_IsSelected, value );
            }
        }

        public bool SelectionChanged
        {
            get
            {
                return BitHelper.IsBitSet( _sbarFeatures, BarBits.POS_SelectionChanged );
            }

            set
            {
                _sbarFeatures = BitHelper.TurnOnOffBit( _sbarFeatures, BarBits.POS_SelectionChanged, value );
            }
        }

        public bool HasBazi
        {
            get
            {
                return BitHelper.IsBitSet( _sbarFeatures, BarBits.POS_BAZI );
            }

            set
            {
                _sbarFeatures = BitHelper.TurnOnOffBit( _sbarFeatures, BarBits.POS_BAZI, value );
            }
        }

        public bool HasDivergence
        {
            get
            {
                return BitHelper.IsBitSet( _sbarFeatures, BarBits.POS_DIVERGENCE );
            }

            set
            {
                _sbarFeatures = BitHelper.TurnOnOffBit( _sbarFeatures, BarBits.POS_DIVERGENCE, value );
            }
        }

        public bool HasStructureLabel
        {
            get
            {
                return BitHelper.IsBitSet( _sbarFeatures, BarBits.POS_STRUCT_LABEL );
            }

            set
            {
                _sbarFeatures = BitHelper.TurnOnOffBit( _sbarFeatures, BarBits.POS_STRUCT_LABEL, value );
            }
        }

        

        public bool HasWaveRotation
        {
            get
            {
                return BitHelper.IsBitSet( _sbarFeatures, BarBits.POS_TIME_ROTATION );
            }

            set
            {
                _sbarFeatures = BitHelper.TurnOnOffBit( _sbarFeatures, BarBits.POS_TIME_ROTATION, value );
            }
        }


        public bool HasGannSquare
        {
            get
            {
                return BitHelper.IsBitSet( _sbarFeatures, BarBits.POS_GANN_SQUARE );
            }

            set
            {
                _sbarFeatures = BitHelper.TurnOnOffBit( _sbarFeatures, BarBits.POS_GANN_SQUARE, value );
            }
        }

        public bool HasPivotRelation
        {
            get
            {
                return BitHelper.IsBitSet( _sbarFeatures, BarBits.POS_PIVOT_RELATION );
            }

            set
            {
                _sbarFeatures = BitHelper.TurnOnOffBit( _sbarFeatures, BarBits.POS_PIVOT_RELATION, value );
            }
        }

        public bool HasSignal
        {
            get
            {
                var signal = _sbarFeatures & BarBits.TaSignalBitsMask;
                return signal != 0;
            }
        }
    }
}
