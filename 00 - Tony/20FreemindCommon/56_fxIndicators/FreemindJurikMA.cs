using fx.Algorithm;

using fx.Definitions;
using System;
using fx.Bars;
using System.Drawing;
using System.Collections.Generic;

namespace fx.Indicators
{
    /// <summary>
    /// This indicator is not tradeable since it changes its symbolPositionSummary and requres future looking.
    /// Indicator written manually no external lib used.
    /// </summary>
    [Serializable]
    [UserFriendlyName( "FreemindJMA" )]
    public class FreemindJurikMA : CustomPlatformIndicator
    {
        protected int? _lookbackCount = null;
        protected int _jmaLength   = 26;
        protected double _jmaPhase    = 0;
        protected int _lastJmaIndex = 0;


        //! wrk is a multi-dimensional array;
        //! Where each row correspond to the datarow.
        //! And every column has a special role
        //! 
        //! 0) First column stores the preliminary smoothing by adaptive EMA:
        //!           the wrk [ r ] [ 0 + s ] = price + alpha * ( wrk [ r - 1 ] [ 0 + s ] - price );
        //! 1) Second column stores the 2nd stage - one more preliminary smoothing by Kalman filter:
        //!           wrk [ r ] [ 1 + s ] = ( price - wrk [ r ] [ 0 + s ] ) * ( 1 - beta ) + beta * wrk [ r - 1 ] [ 1 + s ];
        //! 2) MA2 = MA1 + PR*Det0;
        //!           wrk [ r ] [ 2 + s ] = ( wrk [ r ] [ 0 + s ] + R * wrk [ r ] [ 1 + s ] );
        //! 3) 3rd stage - final smoothing by unique Jurik adaptive filter:
        //!    Det1 = ( MA2 - JMA [ 1 ] ) * ( 1 - alpha ) ^ 2 + alpha ^ 2 * Det1 [ 1 ]; 
        //!           wrk [ r ] [ 3 + s ] = ( wrk [ r ] [ 2 + s ] - wrk [ r - 1 ] [ 4 + s ] ) * Math.Pow( ( 1 - alpha ), 2 ) + Math.Pow( alpha, 2 ) * wrk [ r - 1 ] [ 3 + s ];
        //! 4) Finaly JMA Value
        //!           JMA = JMA [ 1 ] + Det1;
        //! 5) bsMax

        protected double [,] _multiWrk;
        protected double [,] _backupWrk;
        protected double [,] _workFil;


        protected int bsmax                = 5;
        protected int bsmin                = 6;
        protected int volty                = 7;
        protected int vsum                 = 8;
        protected int avolty               = 9;
        protected int myPrice              = 10;

        protected int _filterInstances     = 2;
        protected int _filterInstancesSize = 3;
        protected int _fchange             = 0;
        protected int _fachang             = 1;
        protected int _fprice              = 2;

        public FreemindJurikMA( ) : base( typeof( FreemindJurikMA ).Name, true, true, true, new string [ ] { "JMA" } )
        {

            //this.ChartSeries.OutputResultSetsPens [ "JMA" ]   = Pens.Red;

            //this.ChartSeries.Visible                          = true;
            //this.ChartSeries.InvolvedInDisplayAreaCalculation = false;

            //workFil = new double[ , _filterInstances * _filterInstancesSize ];
        }

        public FreemindJurikMA( string name, bool isIndicatorVisible, bool? isTradeable, bool? isShowInMasterPane, string [ ] resultSetNames ) : base( name, isIndicatorVisible, isTradeable, isShowInMasterPane , resultSetNames )
        {
            
        }

        protected override void OnCalculate( bool fullRecalculation, HistoricBarsUpdateEventArg e )
        {
            if ( e.UpdateType == DataBarUpdateType.CurrentBarUpdate )
            {
                return;
            }            

            int repoStartingIndex = 0;

            if ( fullRecalculation == false )
            {
                repoStartingIndex = Math.Max( 0, IndicatorResult.SetLength );
            }

            IList< double > closeArray;

            //repoStartingIndex = 0;

            int startIndex    = 0;
            int endIndex      = Bars.TotalBarCount - repoStartingIndex - 1;

            int outBeginIdx   = 0;
            int outNBElement = 0;
            int indexCount    = endIndex - startIndex + 1;

            lock ( this )
            {
                closeArray = Bars.GetDataBarSubset( DataBarProperty.Close, repoStartingIndex, indexCount );                        
            }

            var jurikMa = new double [ indexCount ];
            _multiWrk   = new double [ indexCount + 10 , 11 ];

            int beginIndex = 0;

            if ( IndicatorResult.SetLength > 10 )
            {
                RestoreLast10Rows( );
                beginIndex = 10;
            }
            else
            {
                beginIndex = repoStartingIndex;
            }

            for ( int i = startIndex; i < endIndex; i++ )
            {
                jurikMa[ i ] = iSmooth( beginIndex, closeArray [ i ], _jmaLength, _jmaPhase, i, 0 );
                outNBElement++;
            }

            _lastJmaIndex = endIndex ;

            BackupLast10Rows( endIndex );

            lock ( this )
            {
                //Results.AddSetValues( "PivotPoint", startIndex, closeArray.Length, true, Pivot );
                IndicatorResult.AddSetValues( "JMA", repoStartingIndex + outBeginIdx, outNBElement, true, jurikMa );                
            }
        }

        private void BackupLast10Rows( int endIndex )
        {
            if ( _multiWrk.GetLength( 0 ) > 10 )
            {
                if( endIndex < 10 )
                    endIndex += 10;

                _backupWrk = new double [ 10 , 11 ];

                int startIndex = endIndex - 10;

                int row = 0;

                if ( startIndex >= 0 )
                {
                    for ( int i = startIndex; i < endIndex; i++ )
                    {
                        for ( int j = 0; j < 11; ++j )
                        {
                            _backupWrk [row , j] = _multiWrk [ i , j ];
                        }

                        row++;
                    }
                }                
            }
        }

        private void RestoreLast10Rows( )
        {
            if ( _multiWrk.GetLength( 0 ) > 10 )
            {
                for ( int i = 0; i < 10; i++ )
                {
                    for ( int j = 0; j < 11; ++j )
                    {
                        _multiWrk [ i, j ] = _backupWrk [ i, j ];
                    }
                }
            }
        }

        public override PlatformIndicator OnSimpleClone( )
        {
            var result          = new FreemindJurikMA();
            result._description = _description;
            result._name = _name;


            return result;
        }

        protected void EstablishLookback( )
        {
            if ( _lookbackCount.HasValue == false )
            {
                lock ( this )
                {
                    _lookbackCount = TALib.Core.SmaLookback( _jmaLength );
                }
            }
        }

        protected override void OnFinalCalculate( bool fullRecalculation, DataBarUpdateType? updateType )
        {

        }

        public double iSmooth( int beginIndex, double price, double length, double phase, int i, int s = 0 )
        {
            if ( length <= 1 )
            {
                return ( price );
            }
            //if ( ArrayRange( wrk, 0 ) != Bars ) ArrayResize( wrk, Bars );

            int r = beginIndex + i;

            if ( r == 0 )       // For the first bar, we need to initialize the arrary
            {
                int k;

                //! assuming the close is 1.1673
                //!     1st     2nd     3rd     4th     5th     6th     7th     8th     9th     10th
                //! 1.1673  1.1673  1.1673  1.1673  1.1673  1.1673  1.1673      0       0       0

                for ( k = 0; k < 7; k++ )
                {
                    _multiWrk [ r , k + s ] = price;
                }

                for ( ; k < 10; k++ )
                {
                    _multiWrk [ r , k + s ] = 0;
                }

                return ( price );
            }

            //!where len1 - additional periodic factor:
            //!len1 = Log( SquareRoot( len ) ) / Log( 2.0 ) + 2(if len1 < 0 then len1 = 0).
            double len1   = Math.Max( Math.Log( Math.Sqrt( 0.5*( length-1 ))) / Math.Log(2.0)+2.0,0);

            //! pow1 - power of relative volatility with following formula:
            //! pow1 = len1 - 2(if pow1 < 0.5 then pow1 = 0.5),
            double pow1   = Math.Max( len1-2.0, 0.5 );

            //! del1 - distance between price and upper band del1 = Price - UpperBand
            double del1   = price - _multiWrk[ r-1,bsmax+s ];

            //! del2 - distance between price and lower band del2 = Price - LowerBand
            double del2   = price - _multiWrk[ r-1,bsmin+s ];

            double div    = 1.0/( 10.0 + 10.0*( Math.Min( Math.Max( length-10,0 ),100))/100);
            int    forBar = Math.Min(r,10);

            //! The formula for price volatility is
            //! Volty = max between Abs( del1) and Abs( del2), if Abs( del1 ) = Abs( del2 ) then Volty = 0,

            //! if Abs( del1 ) = Abs( del2 ) then Volty = 0
            _multiWrk [ r , volty + s ] = 0;

            if ( Math.Abs( del1 ) > Math.Abs( del2 ) )
            {
                //! Volty = max between Abs( del1) and Abs( del2),
                _multiWrk [ r , volty + s ] = Math.Abs( del1 );
            }

            if ( Math.Abs( del1 ) < Math.Abs( del2 ) )
            {
                //! Volty = max between Abs( del1) and Abs( del2),
                _multiWrk [ r , volty + s ] = Math.Abs( del2 );
            }

            //! vSum - incremental sum of (Volty - Volty[10])/10;
            _multiWrk [ r , vsum + s ] = _multiWrk [ r - 1 , vsum + s ] + ( _multiWrk [ r , volty + s ] - _multiWrk [ r - forBar , volty + s ] ) * div;


            //! AvgVolty - average volatility for which Jurik use difficult enough algorithm of calculation:
            //! AvgVolty = Average( vSum, AvgLen )
            //! AvgLen - period of average (Jurik use 65)
            _multiWrk [ r , avolty + s ] = _multiWrk [ r - 1 , avolty + s ] + ( 2.0 / ( Math.Max( 4.0 * length, 30 ) + 1.0 ) ) * ( _multiWrk [ r , vsum + s ] - _multiWrk [ r - 1 , avolty + s ] );

            double dVolty = 0;

            //! The formula for relative price volatility is
            //! rVolty = Volty / AvgVolty
            //! (if rVolty > len1 ^ ( 1 / pow1 ) then rVolty = len1^(1/pow1), if rVolty < 1 then rVolty = 1), where:
            if ( _multiWrk [ r , avolty + s ] > 0 )
            {
                dVolty = _multiWrk [ r , volty + s ] / _multiWrk [ r , avolty + s ];
            }

            if ( dVolty > Math.Pow( len1, 1.0 / pow1 ) )
            {
                dVolty = Math.Pow( len1, 1.0 / pow1 );
            }

            if ( dVolty < 1 )
            {
                dVolty = 1.0;
            }


            double pow2 = Math.Pow(dVolty, pow1);
            double len2 = Math.Sqrt(0.5*(length-1))*len1;

            //! Kv - volatility's factor Kv = bet ^ SquareRoot(pow2).
            double Kv   = Math.Pow(len2/(len2+1), Math.Sqrt(pow2));

            //! if del1 > 0 then UpperBand = Price else UpperBand = Price - Kv*del1
            if ( del1 > 0 )
            {
                _multiWrk [ r , bsmax + s ] = price;
            }
            else
            {
                _multiWrk [ r , bsmax + s ] = price - Kv * del1;
            }

            //! if del2 < 0 then LowerBand = Price else LowerBand = Price - Kv*del2
            if ( del2 < 0 )
            {
                _multiWrk [ r , bsmin + s ] = price;
            }
            else
            {
                _multiWrk [ r , bsmin + s ] = price - Kv * del2;
            }            

            //! PR - Phase Ratio: PR = Phase/100 + 1.5 (if Phase < -100 then PR=0.5, if Phase > 100 then PR=2.5).
            double R     = Math.Max(Math.Min(phase,100),-100)/100.0 + 1.5;

            //! beta - periodic ratio = 0.45*(Length-1)/(0.45*(Length-1)+2)
            double beta  = 0.45*(length-1)/(0.45*(length-1)+2);

            //! The Dynamic Factor is periodic factor (beta) raised to a power (pow):
            //! alpha = beta ^ Pow,            
            double alpha = Math.Pow(beta,pow2);

            //! 1st stage - preliminary smoothing by adaptive EMA:
            //! MA1 = (1-alpha) X Price + alpha X MA1[1]; 
            //! MA1 = price - alpha X price + alpha X MA[1] = price + alpha x ( MA[1] - price )
            _multiWrk [ r , 0 + s ] = price + alpha * ( _multiWrk [ r - 1 , 0 + s ] - price );

            //! 2nd stage - one more preliminary smoothing by Kalman filter:
            //! Det0 = ( Price - MA1 ) * ( 1 - beta ) + beta * Det0 [ 1 ];            
            _multiWrk [ r , 1 + s ] = ( price - _multiWrk [ r , 0 + s ] ) * ( 1 - beta ) + beta * _multiWrk [ r - 1 , 1 + s ];
            
            //! MA2 = MA1 + PR * Det0;
            _multiWrk [ r , 2 + s ] = ( _multiWrk [ r , 0 + s ] + R * _multiWrk [ r , 1 + s ] );


            //! 3rd stage - final smoothing by unique Jurik adaptive filter:
            //! Det1 = ( MA2 - JMA [ 1 ] ) * ( 1 - alpha ) ^ 2 + alpha ^ 2 * Det1 [ 1 ];            
            _multiWrk [ r , 3 + s ] = ( _multiWrk [ r , 2 + s ] - _multiWrk [ r - 1 , 4 + s ] ) * Math.Pow( ( 1 - alpha ), 2 ) + Math.Pow( alpha, 2 ) * _multiWrk [ r - 1 , 3 + s ];

            //! JMA = JMA [ 1 ] + Det1;
            _multiWrk [ r , 4 + s ] = ( _multiWrk [ r - 1 , 4 + s ] + _multiWrk [ r , 3 + s ] );

            _multiWrk [ r, myPrice + s ] = price;
            

            return ( _multiWrk [ r , 4 + s ] );
        }


        double iFilter( double tprice, double filter, int period, int i, int bars, int instanceNo = 0 )
        {
            if ( filter <= 0 )
            {
                return ( tprice );
            }
            //if ( ArrayRange( workFil, 0 ) != bars ) ArrayResize( workFil, bars ); 

            i = Bars.TotalBarCount - i - 1;
            instanceNo *= _filterInstancesSize;

            _workFil [ i , instanceNo + _fprice ] = tprice;

            if ( i < 1 )
            {
                return ( tprice );
            }

            _workFil [ i , instanceNo + _fchange ] = Math.Abs( _workFil [ i , instanceNo + _fprice ] - _workFil [ i - 1 , instanceNo + _fprice ] );
            _workFil [ i , instanceNo + _fachang ] = _workFil [ i , instanceNo + _fchange ];

            for ( int k = 1; k < period && ( i - k ) >= 0; k++ )
            {
                _workFil [ i , instanceNo + _fachang ] += _workFil [ i - k , instanceNo + _fchange ];
            }

            _workFil [ i , instanceNo + _fachang ] /= period;

            double stddev = 0;

            for ( int k = 0; k < period && ( i - k ) >= 0; k++ )
            {
                stddev += Math.Pow( _workFil [ i - k , instanceNo + _fchange ] - _workFil [ i - k , instanceNo + _fachang ], 2 );
            }

            stddev = Math.Sqrt( stddev / period );
            double filtev = filter * stddev;

            if ( Math.Abs( _workFil [ i , instanceNo + _fprice ] - _workFil [ i - 1 , instanceNo + _fprice ] ) < filtev )
            {
                _workFil [ i , instanceNo + _fprice ] = _workFil [ i - 1 , instanceNo + _fprice ];
            }

            return ( _workFil [ i , instanceNo + _fprice ] );
        }


    }
}

