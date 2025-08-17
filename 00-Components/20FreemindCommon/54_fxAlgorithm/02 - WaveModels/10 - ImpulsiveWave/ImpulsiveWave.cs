using fx.Bars;
using fx.Definitions;
using System;
using System.Collections.Generic;

namespace fx.Algorithm
{
    public enum Wave3Type : byte
    {
        UNKNOWN,
        Classic,
        Extended,
        SuperExtended
    }

    

    public struct ImpulsiveWaveKey
    {
        public TrendDirection WaveDirection {  get; }
        public RangeEx< DateTime>  WaveRange { get; set; }
        

        public ImpulsiveWaveKey( TrendDirection direction, DateTime begin, DateTime end )
        {
            WaveDirection = direction;
            WaveRange     = new RangeEx< DateTime >( begin, end );    
        }
    }

    public class ImpulsiveWave : IEquatable<ImpulsiveWave>
    {
        TrendDirection _dir = TrendDirection.NoTrend;
        private SBar _bar0  = default;
        private SBar _bar1a = default;
        private SBar _bar1b = default;
        private SBar _bar1c = default;
        private SBar _bar2  = default;
        private SBar _bar3a = default;
        private SBar _bar3b = default;
        private SBar _bar3c = default;
        private SBar _bar4  = default;
        private SBar _bar5a = default;
        private SBar _bar5b = default;
        private SBar _bar5c = default;

        double len_0_1        = 0;
        double len_1_2        = 0;
        double len_2_3        = 0;
        double len_0_3        = 0;
        double len_2_3a       = 0;
        double len_3a_3b      = 0;
        double len_3b_3c      = 0;
        double len_3_4        = 0;
        double len_4_5a       = 0;
        double len_5a_5b      = 0;
        double len_5b_5c      = 0;
        double len_4_5        = 0;

        double _wave2Ret      = -1;
        double _wave4Ret      = -1;
        double _wave3bRet     = -1;
        double _wave5bRet     = -1;

        double _wave3Exp      = -1;
        double _wave3cExp     = -1;
        double _wave5Exp      = -1;
        double _wave5cExp     = -1;

        public ImpulsiveWaveKey WaveKey
        {
            get
            {
                return new ImpulsiveWaveKey( _dir, _bar0.BarTime, _bar5c.BarTime );
            }
        }

        public double Wave2Ret
        {
            get
            {
                return _wave2Ret;
            }
        }

        public double Wave4Ret
        {
            get
            {
                return _wave4Ret;
            }
        }

        public double Wave3bRet
        {
            get
            {
                return _wave3bRet;
            }
        }

        public double Wave5bRet
        {
            get
            {
                return _wave5bRet;
            }
        }

        public double Wave3Exp
        {
            get
            {
                return _wave3Exp;
            }
        }

        public double Wave3cExp
        {
            get
            {
                return _wave3cExp;
            }
        }

        public double Wave5Exp
        {
            get
            {
                return _wave5Exp;
            }
        }

        public double Wave5cExp
        {
            get
            {
                return _wave5cExp;
            }
        }

        public bool HasAlternation()
        {
            var sum = _wave2Ret + _wave4Ret;

            if ( sum >= 80 && sum <= 100 )
            {
                return true;
            }

            return false;
        }

        public bool HasDeepWaveB( )
        {
            if ( _wave3bRet >= 66.6 || _wave5bRet >= 66.6 )
            {
                return true;
            }

            return false;
        }


        public ImpulsiveWave( TrendDirection tendDirection, ref SBar bar0, ref SBar bar1, ref SBar bar2, ref SBar bar3, ref SBar bar4, ref SBar bar5 )
        {
            _dir = tendDirection;
            _bar0  = bar0;
            _bar1c = bar1;
            _bar2  = bar2;
            _bar3c = bar3;
            _bar4  = bar4;
            _bar5c = bar5;

            CalculateProjectionAndRetracement( );
        }

        

        private void CalculateProjectionAndRetracement( )
        {
            if ( _bar0 != default && _bar1c != default && _bar2 != default )
            {
                if ( _dir == TrendDirection.Uptrend )
                {
                    len_0_1 = _bar1c.High - _bar0.Low;
                    len_1_2 = _bar1c.High - _bar2.Low;
                }
                else if ( _dir == TrendDirection.DownTrend )
                {
                    len_0_1 = _bar0.High - _bar1c.Low;
                    len_1_2 = _bar2.High - _bar1c.Low;
                }

                if ( len_0_1 > 0 )
                {
                    _wave2Ret = ( len_1_2 / len_0_1 ) * 100;
                }

                if ( _bar3c != default )
                {
                    if ( _dir == TrendDirection.Uptrend )
                    {
                        len_2_3 = _bar3c.High - _bar2.Low;
                        len_0_3 = _bar3c.High - _bar0.Low;
                    }
                    else if ( _dir == TrendDirection.DownTrend )
                    {
                        len_2_3 = _bar2.High - _bar3c.Low;
                        len_0_3 = _bar0.High - _bar3c.Low;
                    }

                    if ( len_0_1 > 0 )
                    {
                        _wave3Exp = ( len_2_3 / len_0_1 ) * 100;
                    }

                    if ( _bar3a != default && _bar3b != default )
                    {
                        if ( _dir == TrendDirection.Uptrend )
                        {
                            len_2_3a  = _bar3a.High - _bar2.Low;
                            len_3a_3b = _bar3a.High - _bar3b.Low;
                            len_3b_3c = _bar3c.High - _bar3b.Low;
                        }
                        else if ( _dir == TrendDirection.DownTrend )
                        {
                            len_2_3a  = _bar2.High - _bar3a.Low;
                            len_3a_3b = _bar3b.High - _bar3a.Low;
                            len_3b_3c = _bar3b.High - _bar3c.Low;
                        }

                        if ( len_2_3a  > 0 )
                        {
                            _wave3bRet = len_3a_3b / len_2_3a * 100;
                            _wave3cExp = len_3b_3c / len_2_3a * 100;
                        }
                    }

                    if ( _bar4 != default )
                    {
                        if ( _dir == TrendDirection.Uptrend )
                        {
                            len_3_4 = _bar3c.High - _bar4.Low;
                        }
                        else if ( _dir == TrendDirection.DownTrend )
                        {
                            len_3_4 = _bar4.High - _bar3c.Low;
                        }

                        if ( len_0_3 > 0 )
                        {
                            _wave4Ret = len_3_4 / len_0_3 * 100;
                        }

                        if ( _bar5c != default )
                        {
                            if ( _dir == TrendDirection.Uptrend )
                            {
                                len_4_5 = _bar5c.High - _bar4.Low;
                            }
                            else if ( _dir == TrendDirection.DownTrend )
                            {
                                len_4_5 = _bar4.High - _bar5c.Low;
                            }

                            if ( len_0_3 > 0 )
                            {
                                _wave5Exp = len_4_5 / len_0_3 * 100;

                            }

                            if ( _bar5a != default && _bar5b != default )
                            {
                                if ( _dir == TrendDirection.Uptrend )
                                {
                                    len_4_5a  = _bar5a.High - _bar4.Low;
                                    len_5a_5b = _bar5a.High - _bar5b.Low;
                                    len_5b_5c = _bar5c.High - _bar5b.Low;
                                }
                                else if ( _dir == TrendDirection.DownTrend )
                                {
                                    len_4_5a  = _bar4.High - _bar5a.Low;
                                    len_5a_5b = _bar5b.High - _bar5a.Low;
                                    len_5b_5c = _bar5b.High - _bar5c.Low;
                                }

                                if ( len_4_5a > 0 )
                                {
                                    _wave5bRet = len_5a_5b / len_4_5a * 100;
                                    _wave5cExp = len_5b_5c / len_4_5a * 100;
                                }
                            }
                        }
                    }
                }
            }
        }

        public ImpulsiveWave( TrendDirection trendDirection, ref SBar bar0, ref SBar bar1a, ref SBar bar1b, ref SBar bar1c, ref SBar bar2, ref SBar bar3a, ref SBar bar3b, ref SBar bar3c, ref SBar bar4, ref SBar bar5a, ref SBar bar5b, ref SBar bar5c )
        {
            _dir = trendDirection;

            _bar0  = bar0;
            _bar1a = bar1a;
            _bar1b = bar1b;
            _bar1c = bar1c;
            _bar2  = bar2;
            _bar3a = bar3a;
            _bar3b = bar3b;
            _bar3c = bar3c;
            _bar4  = bar4;
            _bar5a = bar5a;
            _bar5b = bar5b;
            _bar5c = bar5c;

            CalculateProjectionAndRetracement( );
        }

        public ImpulsiveWave( TrendDirection trendDirection, ref SBar bar0, ref SBar bar1, ref SBar bar2, ref SBar bar3a, ref SBar bar3b, ref SBar bar3c, ref SBar bar4, ref SBar bar5 )
        {
            _dir = trendDirection;

            _bar0  = bar0;
            _bar1a = default;
            _bar1b = default;
            _bar1c = bar1;
            _bar2  = bar2;
            _bar3a = bar3a;
            _bar3b = bar3b;
            _bar3c = bar3c;
            _bar4  = bar4;
            _bar5a = default;
            _bar5b = default;
            _bar5c = bar5;

            CalculateProjectionAndRetracement( );
        }

       
        

        public override string ToString( )
        {
            string output = "";

            if ( _dir == TrendDirection.Uptrend )
            {
                output = "[" + GlobalConstants.UpTrend + "] " + _bar0.BarIndex  + GlobalConstants.UpTrendArrow + _bar1c.BarIndex + GlobalConstants.UpTrendRetracement + _bar2.BarIndex  + GlobalConstants.UpTrendArrow;

                if ( _bar3a != SBar.EmptySBar && _bar3b != SBar.EmptySBar)
                {
                    output += string.Format( "{0}{1}{2}{3}", _bar3a.BarIndex, GlobalConstants.UpTrendRetracement, _bar3b.BarIndex, GlobalConstants.UpTrendArrow );                    
                }

                output += string.Format( "{0}{1}{2}{3}", _bar3c.BarIndex, GlobalConstants.UpTrendRetracement , _bar4.BarIndex , GlobalConstants.UpTrendArrow );

                if ( _bar5a != SBar.EmptySBar && _bar5b != SBar.EmptySBar)
                {
                    output += string.Format( "{0}{1}{2}{3}", _bar5a.BarIndex, GlobalConstants.UpTrendRetracement, _bar5b.BarIndex, GlobalConstants.UpTrendArrow );                    
                }

                output += string.Format( "{0}", _bar5c.BarIndex );
            }
            else
            {
                output = "[" + GlobalConstants.DownTrend + "] " + _bar0.BarIndex  + GlobalConstants.DownTrendRetracement + _bar1c.BarIndex + GlobalConstants.DownTrendArrow + _bar2.BarIndex  + GlobalConstants.DownTrendRetracement;

                if ( _bar3a != SBar.EmptySBar && _bar3b != SBar.EmptySBar)
                {
                    output += string.Format( "{0}{1}{2}{3}", _bar3a.BarIndex, GlobalConstants.DownTrendRetracement, _bar3b.BarIndex, GlobalConstants.DownTrendArrow );                    
                }

                output += _bar3c.BarIndex + GlobalConstants.DownTrendRetracement + _bar4.BarIndex + GlobalConstants.DownTrendArrow;

                if ( _bar5a != SBar.EmptySBar && _bar5b != SBar.EmptySBar)
                {                    
                    output += string.Format( "{0}{1}{2}{3}", _bar5a.BarIndex, GlobalConstants.DownTrendRetracement, _bar5b.BarIndex, GlobalConstants.DownTrendArrow );
                }

                output += string.Format( "{0}", _bar5c.BarIndex);
            }

            return output;
        }

        public override bool Equals(object obj)
        {
            if (obj is ImpulsiveWave)
            {
                return Equals((ImpulsiveWave)obj);
            }

            return base.Equals(obj);
        }

        public static bool operator ==(ImpulsiveWave first, ImpulsiveWave second)
        {
            if (first == null)
            {
                return second == null;
            }

            return first.Equals(second);
        }

        public static bool operator !=(ImpulsiveWave first, ImpulsiveWave second)
        {
            return !(first == second);
        }

        public bool Equals(ImpulsiveWave other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return _dir.Equals(other._dir) && _bar0.Equals(other._bar0) && _bar1a.Equals(other._bar1a) && _bar1b.Equals(other._bar1b) && _bar1c.Equals(other._bar1c) && _bar2.Equals(other._bar2) && _bar3a.Equals(other._bar3a) && _bar3b.Equals(other._bar3b) && _bar3c.Equals(other._bar3c) && _bar4.Equals(other._bar4) && _bar5a.Equals(other._bar5a) && _bar5b.Equals(other._bar5b) && _bar5c.Equals(other._bar5c) && len_0_1.Equals(other.len_0_1) && len_1_2.Equals(other.len_1_2) && len_2_3.Equals(other.len_2_3) && len_0_3.Equals(other.len_0_3) && len_2_3a.Equals(other.len_2_3a) && len_3a_3b.Equals(other.len_3a_3b) && len_3b_3c.Equals(other.len_3b_3c) && len_3_4.Equals(other.len_3_4) && len_4_5a.Equals(other.len_4_5a) && len_5a_5b.Equals(other.len_5a_5b) && len_5b_5c.Equals(other.len_5b_5c) && len_4_5.Equals(other.len_4_5) && _wave2Ret.Equals(other._wave2Ret) && _wave4Ret.Equals(other._wave4Ret) && _wave3bRet.Equals(other._wave3bRet) && _wave5bRet.Equals(other._wave5bRet) && _wave3Exp.Equals(other._wave3Exp) && _wave3cExp.Equals(other._wave3cExp) && _wave5Exp.Equals(other._wave5Exp) && _wave5cExp.Equals(other._wave5cExp);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 47;
                hashCode = (hashCode * 53) ^ (int)_dir;
                hashCode = (hashCode * 53) ^ EqualityComparer<SBar>.Default.GetHashCode(_bar0);
                hashCode = (hashCode * 53) ^ EqualityComparer<SBar>.Default.GetHashCode(_bar1a);
                hashCode = (hashCode * 53) ^ EqualityComparer<SBar>.Default.GetHashCode(_bar1b);
                hashCode = (hashCode * 53) ^ EqualityComparer<SBar>.Default.GetHashCode(_bar1c);
                hashCode = (hashCode * 53) ^ EqualityComparer<SBar>.Default.GetHashCode(_bar2);
                hashCode = (hashCode * 53) ^ EqualityComparer<SBar>.Default.GetHashCode(_bar3a);
                hashCode = (hashCode * 53) ^ EqualityComparer<SBar>.Default.GetHashCode(_bar3b);
                hashCode = (hashCode * 53) ^ EqualityComparer<SBar>.Default.GetHashCode(_bar3c);
                hashCode = (hashCode * 53) ^ EqualityComparer<SBar>.Default.GetHashCode(_bar4);
                hashCode = (hashCode * 53) ^ EqualityComparer<SBar>.Default.GetHashCode(_bar5a);
                hashCode = (hashCode * 53) ^ EqualityComparer<SBar>.Default.GetHashCode(_bar5b);
                hashCode = (hashCode * 53) ^ EqualityComparer<SBar>.Default.GetHashCode(_bar5c);
                hashCode = (hashCode * 53) ^ len_0_1.GetHashCode();
                hashCode = (hashCode * 53) ^ len_1_2.GetHashCode();
                hashCode = (hashCode * 53) ^ len_2_3.GetHashCode();
                hashCode = (hashCode * 53) ^ len_0_3.GetHashCode();
                hashCode = (hashCode * 53) ^ len_2_3a.GetHashCode();
                hashCode = (hashCode * 53) ^ len_3a_3b.GetHashCode();
                hashCode = (hashCode * 53) ^ len_3b_3c.GetHashCode();
                hashCode = (hashCode * 53) ^ len_3_4.GetHashCode();
                hashCode = (hashCode * 53) ^ len_4_5a.GetHashCode();
                hashCode = (hashCode * 53) ^ len_5a_5b.GetHashCode();
                hashCode = (hashCode * 53) ^ len_5b_5c.GetHashCode();
                hashCode = (hashCode * 53) ^ len_4_5.GetHashCode();
                hashCode = (hashCode * 53) ^ _wave2Ret.GetHashCode();
                hashCode = (hashCode * 53) ^ _wave4Ret.GetHashCode();
                hashCode = (hashCode * 53) ^ _wave3bRet.GetHashCode();
                hashCode = (hashCode * 53) ^ _wave5bRet.GetHashCode();
                hashCode = (hashCode * 53) ^ _wave3Exp.GetHashCode();
                hashCode = (hashCode * 53) ^ _wave3cExp.GetHashCode();
                hashCode = (hashCode * 53) ^ _wave5Exp.GetHashCode();
                hashCode = (hashCode * 53) ^ _wave5cExp.GetHashCode();
                return hashCode;
            }
        }
    }
}
