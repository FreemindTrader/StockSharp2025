using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.ComponentModel;
using System.Data;
using fx.Definitions.Collections;
using fx.Collections;

namespace fx.Definitions
{
    /// <summary>
    /// Class contains general helper functionality for financial classes and operation.
    /// </summary>
    public static class WaveHelper
    {
        public static bool IsImpulsiveWave( ElliottWaveEnum waveName )
        {
            if ( waveName == ElliottWaveEnum.Wave1 || waveName == ElliottWaveEnum.Wave1C || waveName == ElliottWaveEnum.Wave3 || waveName == ElliottWaveEnum.Wave3C || waveName == ElliottWaveEnum.Wave5 || waveName == ElliottWaveEnum.Wave5C )
                return true;

            return false;
        }


        public static bool SameWaveName( this ElliottWaveEnum firstWave, ElliottWaveEnum secondWave )
        {
            if ( firstWave == secondWave )
            {
                return true;
            }

            if ( IsImpulsiveWave( firstWave ) && IsImpulsiveWave( secondWave ) )
            {
                switch ( firstWave )
                {
                    case ElliottWaveEnum.Wave1:
                    case ElliottWaveEnum.Wave1C:
                        if ( secondWave == ElliottWaveEnum.Wave1 || secondWave == ElliottWaveEnum.Wave1C ) return true;
                        break;

                    case ElliottWaveEnum.Wave3:
                    case ElliottWaveEnum.Wave3C:
                        if ( secondWave == ElliottWaveEnum.Wave3 || secondWave == ElliottWaveEnum.Wave3C ) return true;
                        break;

                    case ElliottWaveEnum.Wave5:
                    case ElliottWaveEnum.Wave5C:
                        if ( secondWave == ElliottWaveEnum.Wave5 || secondWave == ElliottWaveEnum.Wave5C ) return true;
                        break;
                }
            }

            return false;
        }


        public static bool SameWaveName( this long first, long second )
        {
            if ( first == second )
            {
                return true;
            }

            ElliottWaveEnum firstWave = ( ElliottWaveEnum ) first;
            ElliottWaveEnum secondWave = ( ElliottWaveEnum ) second;

            if ( IsImpulsiveWave( firstWave ) && IsImpulsiveWave( secondWave ) )
            {
                switch ( firstWave )
                {
                    case ElliottWaveEnum.Wave1:
                    case ElliottWaveEnum.Wave1C:
                        if ( secondWave == ElliottWaveEnum.Wave1 || secondWave == ElliottWaveEnum.Wave1C ) return true;
                        break;

                    case ElliottWaveEnum.Wave3:
                    case ElliottWaveEnum.Wave3C:
                        if ( secondWave == ElliottWaveEnum.Wave3 || secondWave == ElliottWaveEnum.Wave3C ) return true;
                        break;

                    case ElliottWaveEnum.Wave5:
                    case ElliottWaveEnum.Wave5C:
                        if ( secondWave == ElliottWaveEnum.Wave5 || secondWave == ElliottWaveEnum.Wave5C ) return true;
                        break;
                }
            }

            return false;
        }

        public static bool SameWaveName( this uint first, int second )
        {
            if ( first == second )
            {
                return true;
            }

            ElliottWaveEnum firstWave = ( ElliottWaveEnum ) first;
            ElliottWaveEnum secondWave = ( ElliottWaveEnum ) second;

            if ( IsImpulsiveWave( firstWave ) && IsImpulsiveWave( secondWave ) )
            {
                switch ( firstWave )
                {
                    case ElliottWaveEnum.Wave1:
                    case ElliottWaveEnum.Wave1C:
                        if ( secondWave == ElliottWaveEnum.Wave1 || secondWave == ElliottWaveEnum.Wave1C ) return true;
                        break;

                    case ElliottWaveEnum.Wave3:
                    case ElliottWaveEnum.Wave3C:
                        if ( secondWave == ElliottWaveEnum.Wave3 || secondWave == ElliottWaveEnum.Wave3C ) return true;
                        break;

                    case ElliottWaveEnum.Wave5:
                    case ElliottWaveEnum.Wave5C:
                        if ( secondWave == ElliottWaveEnum.Wave5 || secondWave == ElliottWaveEnum.Wave5C ) return true;
                        break;
                }
            }

            return false;
        }

    }
}
    


