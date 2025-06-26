using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public partial struct HewLong
    {
        public bool IsWave12345OrHigherDegree_ScanBackward( ElliottWaveCycle cycle )
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
        }


        public bool IsWave5OrHigherDegree_ScanBackward( ElliottWaveCycle cycle )
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

        }


        public bool HasWave2OfDegree_ScanBackward( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = waveCycleCount - 1; i >= 0; i-- )
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

        }

        public bool HasWaveAOfDegree_ScanBackward( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = waveCycleCount - 1; i >= 0; i-- )
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

        }


        public bool HasWaveBOfDegree_ScanBackward( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = waveCycleCount - 1; i >= 0; i-- )
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

        }

        public bool HasWaveCOfDegree( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo(content);

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle == cycle ) && ( ewavePoint.WaveName == ElliottWaveEnum.WaveC ) )
                    {
                        return true;
                    }
                }
            }

            return false;

        }

        public bool HasWaveCOfDegree_ScanBackward( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = waveCycleCount - 1; i >= 0; i-- )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo(content);

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle == cycle ) && ( ewavePoint.WaveName == ElliottWaveEnum.WaveC ) )
                    {
                        return true;
                    }
                }
            }


            return false;

        }

        public bool HasWaveWOfDegree( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle == cycle ) && ( ewavePoint.WaveName == ElliottWaveEnum.WaveW ) )
                    {
                        return true;
                    }
                }
            }


            return false;

        }

        public bool HasWaveWOfDegree_ScanBackward( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = waveCycleCount - 1; i >= 0; i-- )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle == cycle ) && ( ewavePoint.WaveName == ElliottWaveEnum.WaveW ) )
                    {
                        return true;
                    }
                }
            }

            return false;

        }

        public bool HasWaveYOfDegree( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle == cycle ) && ( ewavePoint.WaveName == ElliottWaveEnum.WaveY ) )
                    {
                        return true;
                    }
                }
            }

            return false;

        }

        public bool HasWaveYOfDegree_ScanBackward( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = waveCycleCount - 1; i >= 0; i-- )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle == cycle ) && ( ewavePoint.WaveName == ElliottWaveEnum.WaveY ) )
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool HasWaveZOfDegree( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle == cycle ) && ( ewavePoint.WaveName == ElliottWaveEnum.WaveZ ) )
                    {
                        return true;
                    }
                }
            }

            return false;

        }

        public bool HasWaveWYOfDegree( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = 0; i < waveCycleCount; i++ )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle == cycle ) && ( ewavePoint.WaveName == ElliottWaveEnum.WaveY || ewavePoint.WaveName == ElliottWaveEnum.WaveW ) )
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool HasWaveWYOfDegree_ScanBackward( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = waveCycleCount - 1; i >= 0; i-- )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle == cycle ) && ( ewavePoint.WaveName == ElliottWaveEnum.WaveY || ewavePoint.WaveName == ElliottWaveEnum.WaveW ) )
                    {
                        return true;
                    }
                }
            }

            return false;

        }


        public bool HasWaveZOfDegree_ScanBackward( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = waveCycleCount - 1; i >= 0; i-- )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle == cycle ) && ( ewavePoint.WaveName == ElliottWaveEnum.WaveZ ) )
                    {
                        return true;
                    }
                }
            }

            return false;

        }




        public bool HasWave024X_ScanBackward( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = waveCycleCount - 1; i >= 0; i-- )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle == cycle + GlobalConstants.OneWaveCycle ) )
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

        }

        public bool HasWave4_ScanBackward( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = waveCycleCount - 1; i >= 0; i-- )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle == cycle + GlobalConstants.OneWaveCycle ) )
                    {
                        return true;
                    }
                    else if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle == cycle ) )
                    {
                        if ( ewavePoint.WaveName == ElliottWaveEnum.Wave4 )
                        {
                            return true;
                        }
                    }
                }
            }

            return false;

        }


        public bool HasWave0X_ScanBackward( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = waveCycleCount - 1; i >= 0; i-- )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveCycle == cycle + GlobalConstants.OneWaveCycle ) )
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

        }



        public bool HasWaveEFA_ScanBackward( ElliottWaveCycle cycle )
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
                        if ( ewavePoint.WaveName == ElliottWaveEnum.WaveEFA )
                        {
                            return true;
                        }
                    }
                }
            }

            return false;

        }


        public bool HasWave1OfDegree_ScanBackward( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = waveCycleCount - 1; i >= 0; i-- )
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

        }

        public bool HasWave3OfDegree_ScanBackward( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = waveCycleCount - 1; i >= 0; i-- )
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

        }

        public bool IsBeginningWaveOfExpandedFlat_ScanBackward( ElliottWaveCycle cycle )
        {
            long content = 0;

            var waveCycleCount = GetWaveDegreeCount( );

            if ( waveCycleCount <= GlobalConstants.MaxCountPerLong )
            {
                for ( int i = waveCycleCount - 1; i >= 0; i-- )
                {
                    content = _hewBits >> ( GlobalConstants.NewHewBits * i );
                    var ewavePoint = new WaveInfo( content );

                    if ( ewavePoint.HasWave() && ( ewavePoint.WaveName == ElliottWaveEnum.Wave1 || ewavePoint.WaveName == ElliottWaveEnum.WaveA ) && ( ewavePoint.WaveCycle >= cycle + GlobalConstants.OneWaveCycle ) )
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}