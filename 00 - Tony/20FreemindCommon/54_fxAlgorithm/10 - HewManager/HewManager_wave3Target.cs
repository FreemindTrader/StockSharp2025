using System;
using fx.Database.Common.DataModel;
using fx.Database.ForexDatabarsDataModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Linq;
using fx.Definitions;
using fx.Database;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Collections;
using fx.Definitions.Collections;
using StockSharp.BusinessEntities;
using Ecng.Common;
using fx.TimePeriod;
using fx.Collections;
using fx.Bars;
using StockSharp.Logging;

namespace fx.Algorithm
{
    public partial class HewManager
    {
        
        public void ShrinkWave3Target()
        {
            switch( _wave3ProjectionType )
            {
                case FibonacciType.Wave3All:
                {
                    _wave3ProjectionType = FibonacciType.Wave3SuperExtended;
                }
                break;

                case FibonacciType.Wave3SuperExtended:
                {
                    _wave3ProjectionType = FibonacciType.Wave3Extended;
                }
                    break;

                case FibonacciType.Wave3Extended:
                {
                    _wave3ProjectionType = FibonacciType.Wave3ClassicProjection;
                }
                break;

                case FibonacciType.Wave3ClassicProjection:
                {
                    _wave3ProjectionType = FibonacciType.CompactWave3;
                }
                break;

                case FibonacciType.CompactWave3:
                {
                    _wave3ProjectionType = FibonacciType.CompactWave3;
                }
                    break;

                default:
                {
                    _wave3ProjectionType = FibonacciType.Wave3ClassicProjection;
                }
                    break;

            }            
        }

        public void ExtendedWave3Target()
        {
            switch ( _wave3ProjectionType )
            {
                case FibonacciType.Wave3SuperExtended:
                {
                    _wave3ProjectionType = FibonacciType.Wave3All;
                }
                break;

                case FibonacciType.Wave3Extended:
                {
                    _wave3ProjectionType = FibonacciType.Wave3SuperExtended;
                }
                break;

                case FibonacciType.Wave3ClassicProjection:
                {
                    _wave3ProjectionType = FibonacciType.Wave3Extended;
                }
                break;

                case FibonacciType.CompactWave3:
                {
                    _wave3ProjectionType = FibonacciType.Wave3ClassicProjection;
                }
                break;                

                default:
                {
                    _wave3ProjectionType = FibonacciType.Wave3ClassicProjection;
                }
                break;

            }
        }
    }
}
