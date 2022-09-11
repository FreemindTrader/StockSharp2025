using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using fx.Definitions; 
using System.ComponentModel;
using DevExpress.Mvvm;
using fx.Definitions.UndoRedo;
using fx.Bars;
using System.Runtime.CompilerServices;

namespace fx.Database
{
    public partial class DbElliottWave : BindableBase,
                                         IElliottWave,
                                         IFxcm,
                                         IEquatable< DbElliottWave >
    {
        public override bool Equals( object obj )
        {
            if ( obj is DbElliottWave )
                return Equals( ( DbElliottWave ) obj );
            return base.Equals( obj );
        }

        public static bool operator ==( DbElliottWave first, DbElliottWave second )
        {
            if ( ( object ) first == null )
                return ( object ) second == null;
            return first.Equals( second );
        }

        public static bool operator !=( DbElliottWave first, DbElliottWave second )
        {
            return !( first == second );
        }

        public bool Equals( DbElliottWave other )
        {
            if ( ReferenceEquals( null, other ) )
                return false;
            if ( ReferenceEquals( this, other ) )
                return true;

            return 
                   _startDate.Equals( other._startDate ) &&
                   Id.Equals( other.Id );
        }

        public override int GetHashCode( )
        {
            unchecked
            {
                int hashCode = 47;
                hashCode = ( hashCode * 53 ) ^ _startDate.GetHashCode( );
                hashCode = ( hashCode * 53 ) ^ Id.GetHashCode( );
                
                return hashCode;
            }
        }
        
        private long               _startDate;
        private int                _offerId;        
        private ElliottWaveCycle   _highestWaveCycle;
        private WaveLabelPosition  _waveLabelPosition;
        
        private string             _period    = String.Empty;        
        private int                _barIndex  = -1;
        private fxHistoricBarsRepo _bars      = null;
        private HewLong            _hew01;
        private HewLong            _hew02;
        private HewLong            _hew03;
        private HewLong            _hew04;

        public DbElliottWave( )
        {            
            _hew01 = new HewLong();
            _hew02 = new HewLong();
            _hew03 = new HewLong();
            _hew04 = new HewLong();
        }

        public DbElliottWave( int offerId, fxHistoricBarsRepo bars, ref SBar owningBar )
        {
            _hew01      = new HewLong();
            _hew02      = new HewLong();
            _hew03      = new HewLong();
            _hew04      = new HewLong();

            _offerId    = offerId;
            _bars       = bars;
            _barIndex   = owningBar.Index;

            ref var hew = ref owningBar.GetWaveFromScenario( 1 );

            _startDate  = owningBar.LinuxTime;
            _period     = owningBar.BarPeriod.GetPeriodId();

            var highestWaveCycle = ElliottWaveCycle.UNKNOWN;

            if ( hew.HasElliottWave )
            {                
                _hew01.CopyFrom ( ref hew );

                if ( hew.GetFirstHighestWaveInfo().Value.WaveCycle > highestWaveCycle )
                {
                    highestWaveCycle = hew.GetFirstHighestWaveInfo().Value.WaveCycle;
                }
            }

            hew = ref owningBar.GetWaveFromScenario( 2 );

            if ( hew.HasElliottWave )
            {                
                _hew02.CopyFrom( ref hew );

                if ( hew.GetFirstHighestWaveInfo().Value.WaveCycle > highestWaveCycle )
                {
                    highestWaveCycle = hew.GetFirstHighestWaveInfo().Value.WaveCycle;
                }
            }

            hew = ref owningBar.GetWaveFromScenario( 3 );

            if ( hew.HasElliottWave )
            {
                _hew03.CopyFrom( ref hew );

                if ( hew.GetFirstHighestWaveInfo().Value.WaveCycle > highestWaveCycle )
                {
                    highestWaveCycle = hew.GetFirstHighestWaveInfo().Value.WaveCycle;
                }
            }

            hew = ref owningBar.GetWaveFromScenario( 4 );

            if ( hew.HasElliottWave )
            {
                _hew04.CopyFrom( ref hew );

                if ( hew.GetFirstHighestWaveInfo().Value.WaveCycle > highestWaveCycle )
                {
                    highestWaveCycle = hew.GetFirstHighestWaveInfo().Value.WaveCycle;
                }
            }

            _highestWaveCycle = highestWaveCycle;
            _waveLabelPosition = hew.GetWaveLabelPosition();
        }

        public DbElliottWave( long id, int offerId, fxHistoricBarsRepo bars, ref SBar owningBar )
        {
            _hew01               = new HewLong();
            _hew02               = new HewLong();
            _hew03               = new HewLong();
            _hew04               = new HewLong();

            Id                   = id;
            _offerId             = offerId;
            _bars             = bars;
            _barIndex            = owningBar.Index;

            ref var hew          = ref owningBar.GetWaveFromScenario( 1 );

            _startDate           = owningBar.LinuxTime;
            _period              = owningBar.BarPeriod.GetPeriodId();

            ElliottWaveCycle highestWaveCycle = ElliottWaveCycle.UNKNOWN;

            if ( hew.HasElliottWave )
            {
                _hew01.CopyFrom( ref hew );
                highestWaveCycle = hew.GetFirstHighestWaveInfo().Value.WaveCycle;
            }

            hew = ref owningBar.GetWaveFromScenario( 2 );

            if ( hew.HasElliottWave )
            {
                _hew02.CopyFrom( ref hew );

                if ( hew.GetFirstHighestWaveInfo().Value.WaveCycle > highestWaveCycle )
                {
                    highestWaveCycle = hew.GetFirstHighestWaveInfo().Value.WaveCycle;
                }
            }

            hew = ref owningBar.GetWaveFromScenario( 3 );

            if ( hew.HasElliottWave )
            {
                _hew03.CopyFrom( ref hew );

                if ( hew.GetFirstHighestWaveInfo().Value.WaveCycle > highestWaveCycle )
                {
                    highestWaveCycle = hew.GetFirstHighestWaveInfo().Value.WaveCycle;
                }
            }

            hew = ref owningBar.GetWaveFromScenario( 4 );

            if ( hew.HasElliottWave )
            {
                _hew04.CopyFrom( ref hew );

                if ( hew.GetFirstHighestWaveInfo().Value.WaveCycle > highestWaveCycle )
                {
                    highestWaveCycle = hew.GetFirstHighestWaveInfo().Value.WaveCycle;
                }
            }

            _highestWaveCycle = highestWaveCycle;
            _waveLabelPosition = hew.GetWaveLabelPosition();
        }

        public void SyncWithBar( ref SBar owningBar )
        {
            _hew01.CopyFrom( ref owningBar.MainElliottWave );
            _hew02.CopyFrom( ref owningBar.AltElliottWave01 );
            _hew03.CopyFrom( ref owningBar.AltElliottWave02 );
            _hew04.CopyFrom( ref owningBar.AltElliottWave03 );
        }



        [ Key,
        DatabaseGenerated( DatabaseGeneratedOption.Identity ) ]
        public long Id { get; set; }


        public long HarmonicElliottWaveBit
        {
            get { return _hew01.RawWave; }

            set
            {
                if ( _hew01.RawWave == value )
                    return;

                _hew01.RawWave = value;                

                RaisePropertyChanged( nameof( HarmonicElliottWaveBit ) );                
            }
        }


        public long? AlternativeHewBit
        {
            get { return _hew02.RawWave; }
            set
            {
                if ( value != null )
                {
                    if ( _hew02.RawWave == value.Value )
                        return;

                    _hew02.RawWave = value.Value;                    
                }

                RaisePropertyChanged( nameof( AlternativeHewBit ) );                
            }
        }

        public long? HarmonicElliottWaveExtraBit
        {
            get { return _hew03.RawWave; }
            set
            {
                if ( value != null )
                {
                    if ( _hew03.RawWave == value.Value )
                        return;

                    _hew03.RawWave = value.Value;
                }

                RaisePropertyChanged( nameof( HarmonicElliottWaveExtraBit ) );
            }
        }

        public long? AlternativeHewExtraBit
        {
            get { return _hew04.RawWave; }
            set
            {
                if ( value != null )
                {
                    if ( _hew04.RawWave == value.Value )
                        return;

                    _hew04.RawWave = value.Value;
                }

                RaisePropertyChanged( nameof( AlternativeHewExtraBit ) );
            }
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public ref HewLong GetWaveFromScenario( int waveScenarioNo )
        {
            switch ( waveScenarioNo )
            {
                case 1:
                    return ref _hew01;
                case 2:
                    return ref _hew02;
                case 3:
                    return ref _hew03;
                case 4:
                    return ref _hew04;
            }

            return ref AdvBarInfo.EmptyHew;
        }

        [MethodImpl( MethodImplOptions.AggressiveInlining )]
        public long? GetBitsFromScenario( int waveScenarioNo )
        {
            switch ( waveScenarioNo )
            {
                case 1:
                    return HarmonicElliottWaveBit;
                case 2:
                    return AlternativeHewBit;
                case 3:
                    return HarmonicElliottWaveExtraBit;
                case 4:
                    return AlternativeHewExtraBit;
            }

            throw new NotImplementedException();
        }

        

        //public ref HewLong GetAlternativeWave()
        //{
        //    return ref _alterHew01;
        //}

        DateTimeOffset? _waveDate;
        public DateTimeOffset ? WaveDate
        {
            get
            {
                return _waveDate;
            }

            set
            {
                _waveDate = value;
            }
        }

        bool? _isLocked;
        public bool? IsLocked
        {
            get
            {
                return _isLocked;
            }

            set
            {
                _isLocked = value;
            }
        }

        public long StartDate
        {
            get { return _startDate; }

            set { _startDate = value; }
        }

        public DateTime BeginTimeUTC
        {
            get { return _startDate.FromLinuxTime(); }

            set { _startDate = value.ToLinuxTime(); }
        }

        

        public WaveLabelPosition WaveLabelPosition
        {
            get
            {
                if ( _waveLabelPosition == WaveLabelPosition.UNKNOWN )
                {
                    if ( _hew01.HasElliottWave )
                    {
                        _waveLabelPosition = _hew01.GetWaveLabelPosition();                        
                    }
                    else if ( _hew02.HasElliottWave )
                    {
                        _waveLabelPosition = _hew02.GetWaveLabelPosition();                        
                    }
                }
                
                return _waveLabelPosition;
                                
            }

            set { _waveLabelPosition = value; }
        }


        public int OfferId
        {
            get { return _offerId; }

            set { _offerId = value; }
        }

        public ElliottWaveCycle HighestWaveCycle
        {
            get { return _highestWaveCycle; }

            set { _highestWaveCycle = value; }
        }

        public string Period
        {
            get { return _period; }

            set { _period = value; }
        }
        
        public bool IsSpecialWave( )
        {
            return _hew01.GetWaveLabelPosition() == WaveLabelPosition.BOTH;
        }
       
        //public void AttachOwningBar( fxHistoricBarsRepo bars, ref SBar tmpBar )
        //{                        
        //    if ( _mainHew.HasElliottWave && _mainHew.LabelPosition == WaveLabelPosition.UNKNOWN )
        //    {
        //        _mainHew.LabelPosition = _mainHew.GetLabelPositionFromHew();
        //    }

        //    if ( _alterHew.HasElliottWave && _alterHew.LabelPosition == WaveLabelPosition.UNKNOWN )
        //    {
        //        _alterHew.LabelPosition = _alterHew.GetLabelPositionFromHew();
        //    }

        //    tmpBar.RestoreElliottWave( _mainHew, _alterHew );

        //    _bars  = bars;
        //    _barIndex = tmpBar.Index;
        //}

        public void AttachOwningBar( fxHistoricBarsRepo bars, ref SBar tmpBar )
        {                   
            _bars = bars;
            _barIndex = tmpBar.Index;
        }

        public bool HasOwingBar()
        {
            return _bars != null && _barIndex > -1;
        }

        public DateTime GetMarginDateTime()
        {
            if ( _barIndex != -1 )
            {
                var bar = _bars.GetBarByIndex( _barIndex );
                var margin = TimeSpan.FromTicks( bar.BarPeriod.Ticks * 20 );

                return ( bar.BarTime + margin );
            }


            return DateTime.Now;            
        }


        //public void ChangeOwningBar( ref SBar tmpBar )
        //{            
        //    tmpBar.SimpleHew.CopyFrom( ref _mainHew );
        //    tmpBar.AltElliottWave.CopyFrom( ref _alterHew );

        //    tmpBar.SetWaveLabelPosition( _waveLabelPosition );

        //    if ( _mainHew.HasElliottWave )
        //    {
        //        if ( _mainHew.LabelPosition == WaveLabelPosition.BOTH )
        //        {
        //            tmpBar.IsSpecialBar = true;
        //        }
        //        else
        //        {
        //            if ( _mainHew.LabelPosition == WaveLabelPosition.UNKNOWN )
        //            {
        //                _mainHew.LabelPosition = _mainHew.GetLabelPositionFromHew( );
        //            }
        //        }
        //    }

        //    if ( _alterHew.HasElliottWave )
        //    {
        //        if ( _alterHew.LabelPosition == WaveLabelPosition.BOTH )
        //        {
        //            tmpBar.IsSpecialBar = true;
        //        }
        //        else
        //        {
        //            if ( _alterHew.LabelPosition == WaveLabelPosition.UNKNOWN )
        //            {
        //                _alterHew.LabelPosition = _alterHew.GetLabelPositionFromHew( );
        //            }
        //        }
        //    }

        //    _barIndex = tmpBar;

        //    // Now that it has been attached to a different bar, it should use the new bar's time as the startDate.
        //    StartDate = tmpBar.LinuxTime;
        //}

        public override string ToString( )
        {
            string output = BeginTimeUTC.ToString() + " " + GetWaveFromScenario( 1 ).ToString( );

            return output;
        }


        public void SwapWaves( fxHistoricBarsRepo bars, int waveScenarioNoX, int waveScenarioNoY )
        {
            ref var hew0X = ref GetWaveFromScenario( waveScenarioNoX );
            ref var hew0Y = ref GetWaveFromScenario( waveScenarioNoY );

            var temp   = hew0X;
            hew0X.CopyFrom( ref hew0Y );
            hew0Y.CopyFrom( ref temp );            

            if ( _barIndex != -1 )
            {
                var bar = bars.GetBarByIndex( _barIndex );
                bar.SwapWaves( waveScenarioNoX, waveScenarioNoY );
                bar.WaveDirty = WaveDirtyEnum.Change;
            }            
        }


        public void SwapMainWavesWithAlternative( HewLong wavesToBeChanged )
        {            
            _hew01.RemoveMatchedWaves( wavesToBeChanged );

            var alternative = _hew02;

            var wavesToMerge = wavesToBeChanged.GetAllWaves();

            alternative.MergeWavesList( wavesToMerge );

            if ( _barIndex != -1 )
            {
                var bar = _bars.GetBarByIndex( _barIndex );
                bar.SwapMainWavesWithAlternative( wavesToBeChanged );
            }            
        }

        public bool HasWaves()
        {
            return _hew01.HasElliottWave || _hew02.HasElliottWave || _hew03.HasElliottWave || _hew04.HasElliottWave;
        }
    }
}
