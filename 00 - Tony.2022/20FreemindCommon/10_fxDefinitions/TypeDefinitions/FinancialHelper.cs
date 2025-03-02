using fx.Collections;
using fx.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    public static class FinancialHelper
    {
        //private static PooledList< TimeSpan > _addedTimeSpan = new PooledList< TimeSpan >( );

        //public static void AddSupportedTimeSpan( TimeSpan support )
        //{
        //    _addedTimeSpan.Add( support );            
        //}

        ////public static PooledList<TimeSpan> SupportedTimeSpan
        ////{
        ////    get
        ////    {
        ////        return _addedTimeSpan;
        ////    }
        ////}




        public static string GetHarmonicWaveStringHtml<T>( IHarmonicElliottWave<T> hew )
        {
            string output = "";

            //int count = 0;

            PooledList<ElliottWaveEnum> wave;

            wave = hew.GetWavesAtCycle( ElliottWaveCycle.Miniscule );

            if ( wave.Count > 0 )
            {
                output += String.Format( "Miniscule: {0}\r\n", GetWaveString( ElliottWaveCycle.Miniscule, wave ) );

            }

            wave = hew.GetWavesAtCycle( ElliottWaveCycle.Submicro );

            if ( wave.Count > 0 )
            {
                output += String.Format( "Submicro: {0}\r\n", GetWaveString( ElliottWaveCycle.Submicro, wave ) );
            }

            wave = hew.GetWavesAtCycle( ElliottWaveCycle.Micro );

            if ( wave.Count > 0 )
            {
                output += String.Format( "Micro: {0}\r\n", GetWaveString( ElliottWaveCycle.Micro, wave ) );
            }

            wave = hew.GetWavesAtCycle( ElliottWaveCycle.Subminuette );

            if ( wave.Count > 0 )
            {
                output += String.Format( "Subminuette: {0}\r\n", GetWaveString( ElliottWaveCycle.Subminuette, wave ) );
            }


            wave = hew.GetWavesAtCycle( ElliottWaveCycle.Minuette );

            if ( wave.Count > 0 )
            {
                output += String.Format( "Minuette: {0}\r\n", GetWaveString( ElliottWaveCycle.Minuette, wave ) );
            }


            wave = hew.GetWavesAtCycle( ElliottWaveCycle.Minute );

            if ( wave.Count > 0 )
            {
                output += String.Format( "Minute: {0}\r\n", GetWaveString( ElliottWaveCycle.Minute, wave ) );
            }

            wave = hew.GetWavesAtCycle( ElliottWaveCycle.SubMinor );

            if ( wave.Count > 0 )
            {
                output += String.Format( "SubMinor: {0}\r\n", GetWaveString( ElliottWaveCycle.SubMinor, wave ) );
            }

            wave = hew.GetWavesAtCycle( ElliottWaveCycle.Minor );

            if ( wave.Count > 0 )
            {
                output += String.Format( "Minor: {0}\r\n", GetWaveString( ElliottWaveCycle.Minor, wave ) );
            }

            wave = hew.GetWavesAtCycle( ElliottWaveCycle.SubIntermediate );

            if ( wave.Count > 0 )
            {
                output += String.Format( "SubIntermediate: {0}\r\n", GetWaveString( ElliottWaveCycle.SubIntermediate, wave ) );
            }


            wave = hew.GetWavesAtCycle( ElliottWaveCycle.Intermediate );

            if ( wave.Count > 0 )
            {
                output += String.Format( "Intermediate: {0}\r\n", GetWaveString( ElliottWaveCycle.Intermediate, wave ) );
            }


            wave = hew.GetWavesAtCycle( ElliottWaveCycle.Primary );

            if ( wave.Count > 0 )
            {
                output += String.Format( "Primary: {0}\r\n", GetWaveString( ElliottWaveCycle.Primary, wave ) );
            }


            wave = hew.GetWavesAtCycle( ElliottWaveCycle.Cycle );

            if ( wave.Count > 0 )
            {
                output += String.Format( "Cycle: {0}\r\n", GetWaveString( ElliottWaveCycle.Cycle, wave ) );
            }


            wave = hew.GetWavesAtCycle( ElliottWaveCycle.Supercycle );

            if ( wave.Count > 0 )
            {
                output += String.Format( "Supercycle: {0}\r\n", GetWaveString( ElliottWaveCycle.Supercycle, wave ) );
            }

            wave = hew.GetWavesAtCycle( ElliottWaveCycle.GrandSupercycle );

            if ( wave.Count > 0 )
            {
                output += String.Format( "GrandSupercycle: {0}\r\n", GetWaveString( ElliottWaveCycle.GrandSupercycle, wave ) );
            }


            return output;
        }

        public static string GetHarmonicWaveStringText( HewLong hew )
        {
            string output = "";

            //int count = 0;

            PooledList<ElliottWaveEnum> wave;

            wave = hew.GetWavesAtCycle( ElliottWaveCycle.GrandSupercycle );

            if ( wave.Count > 0 )
            {
                output += String.Format( "GrandSupercycle: {0}", GetWaveString( ElliottWaveCycle.GrandSupercycle, wave ) );
            }

            wave = hew.GetWavesAtCycle( ElliottWaveCycle.Supercycle );

            if ( wave.Count > 0 )
            {
                if ( !string.IsNullOrEmpty( output ) )
                {
                    output += ", ";
                }

                output += String.Format( "Supercycle: {0}", GetWaveString( ElliottWaveCycle.Supercycle, wave ) );
            }

            wave = hew.GetWavesAtCycle( ElliottWaveCycle.Cycle );

            if ( wave.Count > 0 )
            {
                if ( !string.IsNullOrEmpty( output ) )
                {
                    output += ", ";
                }

                output += String.Format( "Cycle: {0}", GetWaveString( ElliottWaveCycle.Cycle, wave ) );
            }

            wave = hew.GetWavesAtCycle( ElliottWaveCycle.Primary );

            if ( wave.Count > 0 )
            {
                if ( !string.IsNullOrEmpty( output ) )
                {
                    output += ", ";
                }

                output += String.Format( "Primary: {0}", GetWaveString( ElliottWaveCycle.Primary, wave ) );
            }

            wave = hew.GetWavesAtCycle( ElliottWaveCycle.Intermediate );

            if ( wave.Count > 0 )
            {
                if ( !string.IsNullOrEmpty( output ) )
                {
                    output += ", ";
                }

                output += String.Format( "Intermediate: {0}", GetWaveString( ElliottWaveCycle.Intermediate, wave ) );
            }

            wave = hew.GetWavesAtCycle( ElliottWaveCycle.Minor );

            if ( wave.Count > 0 )
            {
                if ( !string.IsNullOrEmpty( output ) )
                {
                    output += ", ";
                }

                output += String.Format( "Minor: {0}", GetWaveString( ElliottWaveCycle.Minor, wave ) );
            }

            wave = hew.GetWavesAtCycle( ElliottWaveCycle.Minute );

            if ( wave.Count > 0 )
            {
                if ( !string.IsNullOrEmpty( output ) )
                {
                    output += ", ";
                }

                output += String.Format( "Minute: {0}", GetWaveString( ElliottWaveCycle.Minute, wave ) );
            }

            wave = hew.GetWavesAtCycle( ElliottWaveCycle.Minuette );

            if ( wave.Count > 0 )
            {
                if ( !string.IsNullOrEmpty( output ) )
                {
                    output += ", ";
                }

                output += String.Format( "Minuette: {0}", GetWaveString( ElliottWaveCycle.Minuette, wave ) );
            }

            wave = hew.GetWavesAtCycle( ElliottWaveCycle.Subminuette );

            if ( wave.Count > 0 )
            {
                if ( !string.IsNullOrEmpty( output ) )
                {
                    output += ", ";
                }

                output += String.Format( "Subminuette: {0}", GetWaveString( ElliottWaveCycle.Subminuette, wave ) );
            }

            wave = hew.GetWavesAtCycle( ElliottWaveCycle.Micro );

            if ( wave.Count > 0 )
            {
                if ( !string.IsNullOrEmpty( output ) )
                {
                    output += ", ";
                }

                output += String.Format( "Micro: {0}", GetWaveString( ElliottWaveCycle.Micro, wave ) );
            }

            wave = hew.GetWavesAtCycle( ElliottWaveCycle.Submicro );

            if ( wave.Count > 0 )
            {
                if ( !string.IsNullOrEmpty( output ) )
                {
                    output += ", ";
                }

                output += String.Format( "Submicro: {0}", GetWaveString( ElliottWaveCycle.Submicro, wave ) );
            }

            wave = hew.GetWavesAtCycle( ElliottWaveCycle.Miniscule );

            if ( wave.Count > 0 )
            {
                if ( !string.IsNullOrEmpty( output ) )
                {
                    output += ", ";
                }

                output += String.Format( "Miniscule: {0}", GetWaveString( ElliottWaveCycle.Miniscule, wave ) );
            }


            return output;


        }

        public static string GetWaveString( ElliottWaveCycle cycle, PooledList<ElliottWaveEnum> waveNames )
        {
            string output = string.Empty;

            foreach ( ElliottWaveEnum waveName in waveNames )
            {
                output += GetWaveString( cycle, waveName ) + " ";
            }

            return output;
        }

        public static string GetWaveString( ElliottWaveCycle cycle, ElliottWaveEnum waveName )
        {
            string output = string.Empty;

            switch ( cycle )
            {
                case ElliottWaveCycle.Miniscule:
                {
                    switch ( waveName )
                    {
                        case ElliottWaveEnum.Wave1:
                        return "1";


                        case ElliottWaveEnum.Wave2:
                        return "2";


                        case ElliottWaveEnum.Wave3:
                        return "3";


                        case ElliottWaveEnum.Wave4:
                        return "4";


                        case ElliottWaveEnum.Wave5:
                        return "5";

                        case ElliottWaveEnum.Wave1C:
                        return "1c";                       

                        case ElliottWaveEnum.Wave3C:
                        return "3c";                        


                        case ElliottWaveEnum.Wave5C:
                        return "5c";


                        case ElliottWaveEnum.WaveA:
                        return "a";


                        case ElliottWaveEnum.WaveB:
                        return "b";


                        case ElliottWaveEnum.WaveC:
                        return "c";

                        case ElliottWaveEnum.WaveTA:
                        return "^a";


                        case ElliottWaveEnum.WaveTB:
                        return "^b";


                        case ElliottWaveEnum.WaveTC:
                        return "^c";

                        case ElliottWaveEnum.WaveTD:
                        return "^d";


                        case ElliottWaveEnum.WaveTE:
                        return "^e";

                        case ElliottWaveEnum.WaveW:
                        return "w";
                        
                        case ElliottWaveEnum.WaveX:
                        return "x";

                        case ElliottWaveEnum.WaveY:
                        return "y";

                        case ElliottWaveEnum.WaveZ:
                        return "z";

                        case ElliottWaveEnum.WaveEFA:
                        return "efa";

                        case ElliottWaveEnum.WaveEFB:
                        return "efb";

                        case ElliottWaveEnum.WaveEFC:
                        return "efc";

                    }
                }
                break;


                case ElliottWaveCycle.Submicro:
                {
                    switch ( waveName )
                    {
                        case ElliottWaveEnum.Wave1:
                            return "1";

                        case ElliottWaveEnum.Wave2:
                            return "2";

                        case ElliottWaveEnum.Wave3:
                            return "3";

                        case ElliottWaveEnum.Wave4:
                            return "4";

                        case ElliottWaveEnum.Wave5:
                            return "5";

                        case ElliottWaveEnum.Wave1C:
                            return "1c";

                        case ElliottWaveEnum.Wave3C:
                            return "3c";

                        case ElliottWaveEnum.Wave5C:
                            return "5c";

                        case ElliottWaveEnum.WaveA:
                            return "a";

                        case ElliottWaveEnum.WaveB:
                            return "b";

                        case ElliottWaveEnum.WaveC:
                            return "c";

                        case ElliottWaveEnum.WaveTA:
                            return "^a";

                        case ElliottWaveEnum.WaveTB:
                            return "^b";

                        case ElliottWaveEnum.WaveTC:
                            return "^c";

                        case ElliottWaveEnum.WaveTD:
                            return "^d";

                        case ElliottWaveEnum.WaveTE:
                            return "^e";

                        case ElliottWaveEnum.WaveW:
                            return "w";

                        case ElliottWaveEnum.WaveX:
                            return "x";

                        case ElliottWaveEnum.WaveY:
                            return "y";

                        case ElliottWaveEnum.WaveZ:
                            return "z";

                        case ElliottWaveEnum.WaveEFA:
                            return "efa";

                        case ElliottWaveEnum.WaveEFB:
                            return "efb";

                        case ElliottWaveEnum.WaveEFC:
                            return "efc";
                        
                    }
                }
                break;

                case ElliottWaveCycle.Micro:
                {
                    switch ( waveName )
                    {
                        case ElliottWaveEnum.Wave1:
                            return "I";

                        case ElliottWaveEnum.Wave2:
                            return "II";

                        case ElliottWaveEnum.Wave3:
                            return "III";

                        case ElliottWaveEnum.Wave4:
                            return "IV";

                        case ElliottWaveEnum.Wave5:
                            return "V";

                        case ElliottWaveEnum.Wave1C:
                            return "IC";

                        case ElliottWaveEnum.Wave3C:
                            return "IIIC";

                        case ElliottWaveEnum.Wave5C:
                            return "VC";

                        case ElliottWaveEnum.WaveA:
                            return "A";

                        case ElliottWaveEnum.WaveB:
                            return "B";

                        case ElliottWaveEnum.WaveC:
                            return "C";

                        case ElliottWaveEnum.WaveTA:
                            return "^A";

                        case ElliottWaveEnum.WaveTB:
                            return "^B";

                        case ElliottWaveEnum.WaveTC:
                            return "^C";

                        case ElliottWaveEnum.WaveTD:
                            return "^D";

                        case ElliottWaveEnum.WaveTE:
                            return "^E";

                        case ElliottWaveEnum.WaveW:
                            return "W";

                        case ElliottWaveEnum.WaveX:
                            return "X";

                        case ElliottWaveEnum.WaveY:
                            return "Y";

                        case ElliottWaveEnum.WaveZ:
                            return "Z";

                        case ElliottWaveEnum.WaveEFA:
                            return "EFA";

                        case ElliottWaveEnum.WaveEFB:
                            return "EFB";

                        case ElliottWaveEnum.WaveEFC:
                            return "EFC";
                    }
                }
                break;

                case ElliottWaveCycle.Subminuette:
                {
                    switch ( waveName )
                    {
                        case ElliottWaveEnum.Wave1:
                            return "-1-";

                        case ElliottWaveEnum.Wave2:
                            return "-2-";

                        case ElliottWaveEnum.Wave3:
                            return "-3-";

                        case ElliottWaveEnum.Wave4:
                            return "-4-";

                        case ElliottWaveEnum.Wave5:
                            return "-5-";

                        case ElliottWaveEnum.Wave1C:
                            return "-1c-";

                        case ElliottWaveEnum.Wave3C:
                            return "-3c-";

                        case ElliottWaveEnum.Wave5C:
                            return "-5c-";

                        case ElliottWaveEnum.WaveA:
                            return "-a-";

                        case ElliottWaveEnum.WaveB:
                            return "-b-";

                        case ElliottWaveEnum.WaveC:
                            return "-c-";

                        case ElliottWaveEnum.WaveTA:
                            return "-^a-";

                        case ElliottWaveEnum.WaveTB:
                            return "-^b-";

                        case ElliottWaveEnum.WaveTC:
                            return "-^c-";

                        case ElliottWaveEnum.WaveTD:
                            return "-^d-";

                        case ElliottWaveEnum.WaveTE:
                            return "-^e-";

                        case ElliottWaveEnum.WaveW:
                            return "-w-";

                        case ElliottWaveEnum.WaveX:
                            return "-x-";

                        case ElliottWaveEnum.WaveY:
                            return "-y-";

                        case ElliottWaveEnum.WaveZ:
                            return "-z-";

                        case ElliottWaveEnum.WaveEFA:
                            return "-efa-";

                        case ElliottWaveEnum.WaveEFB:
                            return "-efb-";

                        case ElliottWaveEnum.WaveEFC:
                            return "-efc-";

                    }
                }
                break;

                case ElliottWaveCycle.Minuette:
                {
                    switch ( waveName )
                    {
                        case ElliottWaveEnum.Wave1:
                            return "-I-";

                        case ElliottWaveEnum.Wave2:
                            return "-II-";

                        case ElliottWaveEnum.Wave3:
                            return "-III-";

                        case ElliottWaveEnum.Wave4:
                            return "-IV-";

                        case ElliottWaveEnum.Wave5:
                            return "-V-";

                        case ElliottWaveEnum.Wave1C:
                            return "-IC-";

                        case ElliottWaveEnum.Wave3C:
                            return "-IIIC-";

                        case ElliottWaveEnum.Wave5C:
                            return "-VC-";

                        case ElliottWaveEnum.WaveA:
                            return "-A-";

                        case ElliottWaveEnum.WaveB:
                            return "-B-";

                        case ElliottWaveEnum.WaveC:
                            return "-C-";

                        case ElliottWaveEnum.WaveTA:
                            return "-^A-";

                        case ElliottWaveEnum.WaveTB:
                            return "-^B-";

                        case ElliottWaveEnum.WaveTC:
                            return "-^C-";

                        case ElliottWaveEnum.WaveTD:
                            return "-^D-";

                        case ElliottWaveEnum.WaveTE:
                            return "-^E-";

                        case ElliottWaveEnum.WaveW:
                            return "-W-";

                        case ElliottWaveEnum.WaveX:
                            return "-X-";

                        case ElliottWaveEnum.WaveY:
                            return "-Y-";

                        case ElliottWaveEnum.WaveZ:
                            return "-Z-";

                        case ElliottWaveEnum.WaveEFA:
                            return "-EFA-";

                        case ElliottWaveEnum.WaveEFB:
                            return "-EFB-";

                        case ElliottWaveEnum.WaveEFC:
                            return "-EFC-";
                        
                    }
                }
                break;



                case ElliottWaveCycle.SubMinute:
                {
                    switch ( waveName )
                    {
                        case ElliottWaveEnum.Wave1:
                            return "(1)";

                        case ElliottWaveEnum.Wave2:
                            return "(2)";

                        case ElliottWaveEnum.Wave3:
                            return "(3)";

                        case ElliottWaveEnum.Wave4:
                            return "(4)";

                        case ElliottWaveEnum.Wave5:
                            return "(5)";

                        case ElliottWaveEnum.Wave1C:
                            return "(1c)";

                        case ElliottWaveEnum.Wave3C:
                            return "(3c)";

                        case ElliottWaveEnum.Wave5C:
                            return "(5c)";

                        case ElliottWaveEnum.WaveA:
                            return "(a)";

                        case ElliottWaveEnum.WaveB:
                            return "(b)";

                        case ElliottWaveEnum.WaveC:
                            return "(c)";

                        case ElliottWaveEnum.WaveTA:
                            return "(^a)";

                        case ElliottWaveEnum.WaveTB:
                            return "(^b)";

                        case ElliottWaveEnum.WaveTC:
                            return "(^c)";

                        case ElliottWaveEnum.WaveTD:
                            return "(^d)";

                        case ElliottWaveEnum.WaveTE:
                            return "(^e)";

                        case ElliottWaveEnum.WaveW:
                            return "(w)";

                        case ElliottWaveEnum.WaveX:
                            return "(x)";

                        case ElliottWaveEnum.WaveY:
                            return "(y)";

                        case ElliottWaveEnum.WaveZ:
                            return "(z)";

                        case ElliottWaveEnum.WaveEFA:
                            return "(efa)";

                        case ElliottWaveEnum.WaveEFB:
                            return "(efb)";

                        case ElliottWaveEnum.WaveEFC:
                            return "(efc)";

                    }
                }
                break;

                case ElliottWaveCycle.Minute:
                {
                    switch ( waveName )
                    {
                        case ElliottWaveEnum.Wave1:
                        return "(I)";

                        case ElliottWaveEnum.Wave2:
                        return "(II)";

                        case ElliottWaveEnum.Wave3:
                        return "(III)";

                        case ElliottWaveEnum.Wave4:
                        return "(IV)";

                        case ElliottWaveEnum.Wave5:
                        return "(V)";

                        case ElliottWaveEnum.Wave1C:
                        return "(IC)";

                        case ElliottWaveEnum.Wave3C:
                        return "(IIIC)";

                        case ElliottWaveEnum.Wave5C:
                        return "(VC)";

                        case ElliottWaveEnum.WaveA:
                        return "(A)";

                        case ElliottWaveEnum.WaveB:
                        return "(B)";

                        case ElliottWaveEnum.WaveC:
                        return "(C)";

                        case ElliottWaveEnum.WaveTA:
                        return "(^A)";

                        case ElliottWaveEnum.WaveTB:
                        return "(^B)";

                        case ElliottWaveEnum.WaveTC:
                        return "(^C)";

                        case ElliottWaveEnum.WaveTD:
                        return "(^D)";

                        case ElliottWaveEnum.WaveTE:
                        return "(^E)";

                        case ElliottWaveEnum.WaveW:
                            return "(W)";

                        case ElliottWaveEnum.WaveX:
                            return "(X)";

                        case ElliottWaveEnum.WaveY:
                            return "(Y)";

                        case ElliottWaveEnum.WaveZ:
                            return "(Z)";

                        case ElliottWaveEnum.WaveEFA:
                        return "(EFA)";

                        case ElliottWaveEnum.WaveEFB:
                        return "(EFB)";

                        case ElliottWaveEnum.WaveEFC:
                        return "(EFC)";
                        
                    }
                }
                break;

                case ElliottWaveCycle.SubMinor:
                {
                    switch ( waveName )
                    {
                        case ElliottWaveEnum.Wave1:
                            return "{1}";

                        case ElliottWaveEnum.Wave2:
                            return "{2}";

                        case ElliottWaveEnum.Wave3:
                            return "{3}";

                        case ElliottWaveEnum.Wave4:
                            return "{4}";

                        case ElliottWaveEnum.Wave5:
                            return "{5}";

                        case ElliottWaveEnum.Wave1C:
                            return "{1c}";

                        case ElliottWaveEnum.Wave3C:
                            return "{3c}";

                        case ElliottWaveEnum.Wave5C:
                            return "{5c}";

                        case ElliottWaveEnum.WaveA:
                            return "{a}";

                        case ElliottWaveEnum.WaveB:
                            return "{b}";

                        case ElliottWaveEnum.WaveC:
                            return "{c}";

                        case ElliottWaveEnum.WaveTA:
                            return "{^a}";

                        case ElliottWaveEnum.WaveTB:
                            return "{^b}";

                        case ElliottWaveEnum.WaveTC:
                            return "{^c}";

                        case ElliottWaveEnum.WaveTD:
                            return "{^d}";

                        case ElliottWaveEnum.WaveTE:
                            return "{^e}";

                        case ElliottWaveEnum.WaveW:
                            return "{w}";

                        case ElliottWaveEnum.WaveX:
                            return "{x}";

                        case ElliottWaveEnum.WaveY:
                            return "{y}";

                        case ElliottWaveEnum.WaveZ:
                            return "{z}";

                        case ElliottWaveEnum.WaveEFA:
                            return "{efa}";

                        case ElliottWaveEnum.WaveEFB:
                            return "{efb}";

                        case ElliottWaveEnum.WaveEFC:
                            return "{efc}";
                            
                    }
                }
                break;

                case ElliottWaveCycle.Minor:
                {
                    switch ( waveName )
                    {
                        case ElliottWaveEnum.Wave1:
                            return "{I}";

                        case ElliottWaveEnum.Wave2:
                            return "{II}";

                        case ElliottWaveEnum.Wave3:
                            return "{III}";

                        case ElliottWaveEnum.Wave4:
                            return "{IV}";

                        case ElliottWaveEnum.Wave5:
                            return "{V}";

                        case ElliottWaveEnum.Wave1C:
                            return "{IC}";
                        
                        case ElliottWaveEnum.Wave3C:
                            return "{IIIC}";

                        case ElliottWaveEnum.Wave5C:
                            return "{VC}";

                        case ElliottWaveEnum.WaveA:
                            return "{A}";

                        case ElliottWaveEnum.WaveB:
                            return "{B}";

                        case ElliottWaveEnum.WaveC:
                            return "{C}";

                        case ElliottWaveEnum.WaveTA:
                            return "{^A}";

                        case ElliottWaveEnum.WaveTB:
                            return "{^B}";

                        case ElliottWaveEnum.WaveTC:
                            return "{^C}";

                        case ElliottWaveEnum.WaveTD:
                            return "{^D}";

                        case ElliottWaveEnum.WaveTE:
                            return "{^E}";

                        case ElliottWaveEnum.WaveW:
                            return "{W}";

                        case ElliottWaveEnum.WaveX:
                            return "{X}";

                        case ElliottWaveEnum.WaveY:
                            return "{Y}";

                        case ElliottWaveEnum.WaveZ:
                            return "{Z}";

                        case ElliottWaveEnum.WaveEFA:
                            return "{EFA}";

                        case ElliottWaveEnum.WaveEFB:
                            return "{EFB}";

                        case ElliottWaveEnum.WaveEFC:
                            return "{EFC}";
                        
                    }
                }
                break;

                case ElliottWaveCycle.SubIntermediate:
                {
                    switch ( waveName )
                    {
                        case ElliottWaveEnum.Wave1:
                            return "<1>";

                        case ElliottWaveEnum.Wave2:
                            return "<2>";

                        case ElliottWaveEnum.Wave3:
                            return "<3>";

                        case ElliottWaveEnum.Wave4:
                            return "<4>";

                        case ElliottWaveEnum.Wave5:
                            return "<5>";

                        case ElliottWaveEnum.Wave1C:
                            return "<1c>";

                        case ElliottWaveEnum.Wave3C:
                            return "<3c>";

                        case ElliottWaveEnum.Wave5C:
                            return "<5c>";

                        case ElliottWaveEnum.WaveA:
                            return "<a>";

                        case ElliottWaveEnum.WaveB:
                            return "<b>";

                        case ElliottWaveEnum.WaveC:
                            return "<c>";

                        case ElliottWaveEnum.WaveTA:
                            return "<^a>";

                        case ElliottWaveEnum.WaveTB:
                            return "<^b>";

                        case ElliottWaveEnum.WaveTC:
                            return "<^c>";

                        case ElliottWaveEnum.WaveTD:
                            return "<^d>";

                        case ElliottWaveEnum.WaveTE:
                            return "<^e>";

                        case ElliottWaveEnum.WaveW:
                            return "<w>";

                        case ElliottWaveEnum.WaveX:
                            return "<x>";

                        case ElliottWaveEnum.WaveY:
                            return "<y>";

                        case ElliottWaveEnum.WaveZ:
                            return "<z>";

                        case ElliottWaveEnum.WaveEFA:
                            return "<efa>";

                        case ElliottWaveEnum.WaveEFB:
                            return "<efb>";

                        case ElliottWaveEnum.WaveEFC:
                            return "<efc>";                           
                    }
                }
                break;

                case ElliottWaveCycle.Intermediate:
                {
                    switch ( waveName )
                    {
                        case ElliottWaveEnum.Wave1:
                        return "<I>";


                        case ElliottWaveEnum.Wave2:
                        return "<II>";


                        case ElliottWaveEnum.Wave3:
                        return "<III>";


                        case ElliottWaveEnum.Wave4:
                        return "<IV>";


                        case ElliottWaveEnum.Wave5:
                        return "<V>";

                        case ElliottWaveEnum.Wave1C:
                        return "<IC>";
                        
                        case ElliottWaveEnum.Wave3C:
                        return "<IIIC>";
                        
                        case ElliottWaveEnum.Wave5C:
                        return "<VC>";

                        case ElliottWaveEnum.WaveA:
                        return "<A>";


                        case ElliottWaveEnum.WaveB:
                        return "<B>";


                        case ElliottWaveEnum.WaveC:
                        return "<C>";

                        case ElliottWaveEnum.WaveTA:
                        return "<^A>";


                        case ElliottWaveEnum.WaveTB:
                        return "<^B>";


                        case ElliottWaveEnum.WaveTC:
                        return "<^C>";

                        case ElliottWaveEnum.WaveTD:
                        return "<^D>";

                        case ElliottWaveEnum.WaveTE:
                        return "<^E>";

                        case ElliottWaveEnum.WaveW:
                            return "<W>";

                        case ElliottWaveEnum.WaveX:
                            return "<X>";

                        case ElliottWaveEnum.WaveY:
                            return "<Y>";

                        case ElliottWaveEnum.WaveZ:
                            return "<Z>";

                        case ElliottWaveEnum.WaveEFA:
                        return "<EFA>";

                        case ElliottWaveEnum.WaveEFB:
                        return "<EFB>";

                        case ElliottWaveEnum.WaveEFC:
                        return "<EFC>";
                    }
                }
                break;

                case ElliottWaveCycle.Primary:
                {
                    switch ( waveName )
                    {
                        case ElliottWaveEnum.Wave1:
                        return "((I))";


                        case ElliottWaveEnum.Wave2:
                        return "((II))";


                        case ElliottWaveEnum.Wave3:
                        return "((III))";


                        case ElliottWaveEnum.Wave4:
                        return "((IV))";


                        case ElliottWaveEnum.Wave5:
                        return "((V))";

                        case ElliottWaveEnum.Wave1C:
                        return "((IC))";

                        case ElliottWaveEnum.Wave3C:
                        return "((IIIC))";

                        case ElliottWaveEnum.Wave5C:
                        return "((VC))";

                        case ElliottWaveEnum.WaveA:
                        return "((A))";


                        case ElliottWaveEnum.WaveB:
                        return "((B))";


                        case ElliottWaveEnum.WaveC:
                        return "((C))";

                        case ElliottWaveEnum.WaveTA:
                        return "((^A))";


                        case ElliottWaveEnum.WaveTB:
                        return "((^B))";


                        case ElliottWaveEnum.WaveTC:
                        return "((^C))";

                        case ElliottWaveEnum.WaveTD:
                        return "((^D))";

                        case ElliottWaveEnum.WaveTE:
                        return "((^E))";

                        case ElliottWaveEnum.WaveW:
                            return "((W))";

                        case ElliottWaveEnum.WaveX:
                            return "((X))";

                        case ElliottWaveEnum.WaveY:
                            return "((Y))";

                        case ElliottWaveEnum.WaveZ:
                            return "((Z))";

                        case ElliottWaveEnum.WaveEFA:
                        return "((EFA))";

                        case ElliottWaveEnum.WaveEFB:
                        return "((EFB))";

                        case ElliottWaveEnum.WaveEFC:
                        return "((EFC))";

                    }
                }
                break;

                case ElliottWaveCycle.Cycle:
                {
                    switch ( waveName )
                    {
                        case ElliottWaveEnum.Wave1:
                        return "[[I]]";


                        case ElliottWaveEnum.Wave2:
                        return "[[II]]";


                        case ElliottWaveEnum.Wave3:
                        return "[[III]]";


                        case ElliottWaveEnum.Wave4:
                        return "[[IV]]";


                        case ElliottWaveEnum.Wave5:
                        return "[[V]]";

                        case ElliottWaveEnum.Wave1C:
                        return "[[IC]]";                        

                        case ElliottWaveEnum.Wave3C:
                        return "[[IIIC]]";

                        case ElliottWaveEnum.Wave5C:
                        return "[[VC]]";

                        case ElliottWaveEnum.WaveA:
                        return "[[A]]";


                        case ElliottWaveEnum.WaveB:
                        return "[[B]]";


                        case ElliottWaveEnum.WaveC:
                        return "[[C]]";

                        case ElliottWaveEnum.WaveTA:
                        return "[[^A]]";


                        case ElliottWaveEnum.WaveTB:
                        return "[[^B]]";


                        case ElliottWaveEnum.WaveTC:
                        return "[[^C]]";

                        case ElliottWaveEnum.WaveTD:
                        return "[[^D]]";

                        case ElliottWaveEnum.WaveTE:
                        return "[[^E]]";

                        case ElliottWaveEnum.WaveW:
                            return "[[W]]";

                        case ElliottWaveEnum.WaveX:
                            return "[[X]]";

                        case ElliottWaveEnum.WaveY:
                            return "[[Y]]";

                        case ElliottWaveEnum.WaveZ:
                            return "[[Z]]";

                        case ElliottWaveEnum.WaveEFA:
                        return "[[EFA]]";

                        case ElliottWaveEnum.WaveEFB:
                        return "[[EFB]]";

                        case ElliottWaveEnum.WaveEFC:
                        return "[[EFC]]";

                    }
                }
                break;

                case ElliottWaveCycle.Supercycle:
                {
                    switch ( waveName )
                    {
                        case ElliottWaveEnum.Wave1:
                        return "{{I}}";


                        case ElliottWaveEnum.Wave2:
                        return "{{II}}";


                        case ElliottWaveEnum.Wave3:
                        return "{{III}}";


                        case ElliottWaveEnum.Wave4:
                        return "{{IV}}";


                        case ElliottWaveEnum.Wave5:
                        return "{{V}}";

                        case ElliottWaveEnum.Wave1C:
                        return "{{IC}}";
                        

                        case ElliottWaveEnum.Wave3C:
                        return "{{IIIC}}";

                        case ElliottWaveEnum.Wave5C:
                        return "{{VC}}";

                        case ElliottWaveEnum.WaveA:
                        return "{{A}}";


                        case ElliottWaveEnum.WaveB:
                        return "{{B}}";


                        case ElliottWaveEnum.WaveC:
                        return "{{C}}";

                        case ElliottWaveEnum.WaveTA:
                        return "{{^A}}";


                        case ElliottWaveEnum.WaveTB:
                        return "{{^B}}";


                        case ElliottWaveEnum.WaveTC:
                        return "{{^C}}";

                        case ElliottWaveEnum.WaveTD:
                        return "{{^D}}";

                        case ElliottWaveEnum.WaveTE:
                        return "{{^E}}";

                        case ElliottWaveEnum.WaveW:
                            return "{{W}}";

                        case ElliottWaveEnum.WaveX:
                            return "{{X}}";

                        case ElliottWaveEnum.WaveY:
                            return "{{Y}}";

                        case ElliottWaveEnum.WaveZ:
                            return "{{Z}}";

                        case ElliottWaveEnum.WaveEFA:
                        return "{{EFA}}";

                        case ElliottWaveEnum.WaveEFB:
                        return "{{EFB}}";

                        case ElliottWaveEnum.WaveEFC:
                        return "{{EFC}}";

                    }
                }
                break;

                case ElliottWaveCycle.GrandSupercycle:
                {
                    switch ( waveName )
                    {
                        case ElliottWaveEnum.Wave1:
                        return "<<I>>";


                        case ElliottWaveEnum.Wave2:
                        return "<<II>>";


                        case ElliottWaveEnum.Wave3:
                        return "<<III>>";


                        case ElliottWaveEnum.Wave4:
                        return "<<IV>>";


                        case ElliottWaveEnum.Wave5:
                        return "<<IV>>";

                        case ElliottWaveEnum.Wave1C:
                        return "<<IC>>";
                        


                        case ElliottWaveEnum.Wave3C:
                        return "<<IIIC>>";
                        
                        case ElliottWaveEnum.Wave5C:
                        return "<<VC>>";
                        
                        case ElliottWaveEnum.WaveA:
                        return "<<A>>";


                        case ElliottWaveEnum.WaveB:
                        return "<<B>>";


                        case ElliottWaveEnum.WaveC:
                        return "<<C>>";

                        case ElliottWaveEnum.WaveTA:
                        return "<<^A>>";


                        case ElliottWaveEnum.WaveTB:
                        return "<<^B>>";


                        case ElliottWaveEnum.WaveTC:
                        return "<<^C>>";

                        case ElliottWaveEnum.WaveTD:
                        return "<<^D>>";

                        case ElliottWaveEnum.WaveTE:
                        return "<<^E>>";

                        case ElliottWaveEnum.WaveW:
                            return "<<W>>";

                        case ElliottWaveEnum.WaveX:
                            return "<<X>>";

                        case ElliottWaveEnum.WaveY:
                            return "<<Y>>";

                        case ElliottWaveEnum.WaveZ:
                            return "<<Z>>";

                        case ElliottWaveEnum.WaveEFA:
                        return "<<EFA>>";

                        case ElliottWaveEnum.WaveEFB:
                        return "<<EFB>>";

                        case ElliottWaveEnum.WaveEFC:
                        return "<<EFC>>";

                    }
                }
                break;
            }

            return output;
        }

        public static int GetWaveImportanceDegreeLower( int degree )
        {
            if ( degree == 144 )
            {
                return 89;
            }
            else if ( degree == 89 )
            {
                return 55;
            }
            else if ( degree == 55 )
            {
                return 34;
            }
            else if ( degree == 34 )
            {
                return 21;
            }
            else if ( degree == 21 )
            {
                return 13;
            }
            else if ( degree == 13 )
            {
                return 8;
            }
            else if ( degree == 8 )
            {
                return 5;
            }

            return -1;
        }

        public static SymbolsEnum GetSymbolEnum( string input )
        {
            SymbolsEnum mysmbol;

            switch ( input )
            {
                case "EUR/USD":
                mysmbol = SymbolsEnum.EURUSD;
                break;

                case "CHF/JPY":
                mysmbol = SymbolsEnum.CHFJPY;
                break;

                case "GBP/CHF":
                mysmbol = SymbolsEnum.GBPCHF;
                break;

                case "EUR/AUD":
                mysmbol = SymbolsEnum.EURAUD;
                break;

                case "EUR/CAD":
                mysmbol = SymbolsEnum.EURCAD;
                break;

                case "AUD/CAD":
                mysmbol = SymbolsEnum.AUDCAD;
                break;

                case "CAD/JPY":
                mysmbol = SymbolsEnum.CADJPY;
                break;
                case "NZD/JPY":
                mysmbol = SymbolsEnum.NZDJPY;
                break;

                case "GBP/CAD":
                mysmbol = SymbolsEnum.GBPCAD;
                break;

                case "AUD/NZD":
                mysmbol = SymbolsEnum.AUDNZD;

                break;
                case "USD/SEK":
                mysmbol = SymbolsEnum.USDSEK;

                break;
                case "USD/DDK":
                mysmbol = SymbolsEnum.USDDDK;

                break;
                case "EUR/SEK":
                mysmbol = SymbolsEnum.EURSEK;
                break;

                case "EUR/NOK":
                mysmbol = SymbolsEnum.EURNOK;

                break;

                case "USD/NOK":
                mysmbol = SymbolsEnum.USDNOK;

                break;
                case "USD/MXN":
                mysmbol = SymbolsEnum.USDMXN;

                break;
                case "AUD/CHF":
                mysmbol = SymbolsEnum.AUDCHF;

                break;
                case "EUR/NZD":
                mysmbol = SymbolsEnum.EURNZD;

                break;
                case "EUR/PLN":
                mysmbol = SymbolsEnum.EURPLN;

                break;
                case "USD/PLN":
                mysmbol = SymbolsEnum.USDPLN;

                break;
                case "EUR/CZK":
                mysmbol = SymbolsEnum.EURCZK;

                break;
                case "USD/CZK":
                mysmbol = SymbolsEnum.USDCZK;

                break;
                case "USD/ZAR":
                mysmbol = SymbolsEnum.USDZAR;
                break;

                case "USD/SGD":
                mysmbol = SymbolsEnum.USDSGD;
                break;

                case "USD/HKD":
                mysmbol = SymbolsEnum.USDHKD;
                break;

                case "EUR/DKK":
                mysmbol = SymbolsEnum.EURDKK;
                break;

                case "GBP/SEK":
                mysmbol = SymbolsEnum.GBPSEK;
                break;

                case "NOK/JPY":
                mysmbol = SymbolsEnum.NOKJPY;
                break;

                case "SEK/JPY":
                mysmbol = SymbolsEnum.SEKJPY;
                break;

                case "SGD/JPY":
                mysmbol = SymbolsEnum.SGDJPY;
                break;

                case "HKD/JPY":
                mysmbol = SymbolsEnum.HKDJPY;
                break;

                case "ZAR/JPY":
                mysmbol = SymbolsEnum.ZARJPY;
                break;

                case "USD/TRY":
                mysmbol = SymbolsEnum.USDTRY;
                break;

                case "EUR/TRY":
                mysmbol = SymbolsEnum.EURTRY;
                break;

                case "NZD/CHF":
                mysmbol = SymbolsEnum.NZDCHF;
                break;

                case "CAD/CHF":
                mysmbol = SymbolsEnum.CADCHF;
                break;

                case "NZD/CAD":
                mysmbol = SymbolsEnum.NZDCAD;
                break;


                case "CHF/SEK":
                mysmbol = SymbolsEnum.CHFSEK;
                break;

                case "CHF/NOK":
                mysmbol = SymbolsEnum.CHFNOK;
                break;

                case "EUR/HUF":
                mysmbol = SymbolsEnum.EURHUF;
                break;

                case "USD/HUF":
                mysmbol = SymbolsEnum.USDHUF;
                break;

                case "TRY/JPY":
                mysmbol = SymbolsEnum.TRYJPY;
                break;

                case "GBP/USD":
                mysmbol = SymbolsEnum.GBPUSD;
                break;

                case "USD/CNH":
                mysmbol = SymbolsEnum.USDCNH;

                break;

                case "EUR/JPY":
                mysmbol = SymbolsEnum.EURJPY;
                break;

                case "USD/JPY":
                mysmbol = SymbolsEnum.USDJPY;
                break;

                case "GBP/JPY":
                mysmbol = SymbolsEnum.GBPJPY;
                break;

                case "AUD/JPY":
                mysmbol = SymbolsEnum.AUDJPY;
                break;

                case "USD/CHF":
                mysmbol = SymbolsEnum.USDCHF;
                break;

                case "AUD/USD":
                mysmbol = SymbolsEnum.AUDUSD;
                break;

                case "EUR/CHF":
                mysmbol = SymbolsEnum.EURCHF;
                break;

                case "EUR/GBP":
                mysmbol = SymbolsEnum.EURGBP;
                break;

                case "NZD/USD":
                mysmbol = SymbolsEnum.NZDUSD;
                break;

                case "USD/CAD":
                mysmbol = SymbolsEnum.USDCAD;
                break;

                case "GBP/AUD":
                mysmbol = SymbolsEnum.GBPAUD;
                break;

                case "XAG/USD":
                mysmbol = SymbolsEnum.XAGUSD;
                break;

                case "XAU/USD":
                mysmbol = SymbolsEnum.XAUUSD;
                break;

                case "UK100":
                mysmbol = SymbolsEnum.UK100;
                break;

                case "USDOLLAR":
                mysmbol = SymbolsEnum.USDOLLAR;
                break;

                case "GER30":
                mysmbol = SymbolsEnum.GER30;
                break;

                case "FRA40":
                mysmbol = SymbolsEnum.FRA40;
                break;

                case "AUS200":
                mysmbol = SymbolsEnum.AUS200;
                break;

                case "ESP35":
                mysmbol = SymbolsEnum.ESP35;
                break;

                case "HKG33":
                mysmbol = SymbolsEnum.HKG33;
                break;

                case "ITA40":
                mysmbol = SymbolsEnum.ITA40;
                break;

                case "JPN225":
                mysmbol = SymbolsEnum.JPN225;
                break;

                case "NAS100":
                mysmbol = SymbolsEnum.NAS100;
                break;

                case "SPX500":
                mysmbol = SymbolsEnum.SPX500;
                break;

                case "SUI20":
                mysmbol = SymbolsEnum.SUI20;
                break;

                case "Copper":
                mysmbol = SymbolsEnum.COPPER;
                break;

                case "EUSTX50":
                mysmbol = SymbolsEnum.EUSTX50;
                break;

                case "US30":
                mysmbol = SymbolsEnum.US30;
                break;

                case "USOIL":
                mysmbol = SymbolsEnum.USOIL;
                break;

                case "UKOIL":
                mysmbol = SymbolsEnum.UKOIL;
                break;

                case "NGAS":
                mysmbol = SymbolsEnum.NGAS;
                break;

                case "XPD/USD":
                mysmbol = SymbolsEnum.XPDUSD;
                break;

                case "XPT/USD":
                mysmbol = SymbolsEnum.XPTUSD;
                break;

                case "BUND":
                mysmbol = SymbolsEnum.BUND;
                break;

                case "CHN50":
                mysmbol = SymbolsEnum.CHN50;
                break;

                case "US2000":
                mysmbol = SymbolsEnum.US2000;
                break;

                case "SOYF":
                mysmbol = SymbolsEnum.SOYF;
                break;

                case "WHEATF":
                mysmbol = SymbolsEnum.WHEATF;
                break;

                case "CORNF":
                mysmbol = SymbolsEnum.CORNF;
                break;

                case "BTC/USD":
                mysmbol = SymbolsEnum.BTCUSD;
                break;

                case "ETH/USD":
                mysmbol = SymbolsEnum.ETHUSD;
                break;

                case "LTC/USD":
                mysmbol = SymbolsEnum.LTCUSD;
                break;



                default:
                mysmbol = SymbolsEnum.EURUSD;
                break;
            }

            return mysmbol;

        }

        public static string GetSpecialNumber( int number )
        {
            string specialNumber = string.Empty;

            if ( Array.IndexOf( WaveRotation.FibsSeq, number ) != -1 )
            {
                return "Fib";
            }

            if ( Array.IndexOf( WaveRotation.LucasSeq, number ) != -1 )
            {
                return "Lucas";
            }

            if ( Array.IndexOf( WaveRotation.FibRatio, number ) != -1 )
            {
                return "Fib R";
            }

            if ( Array.IndexOf( WaveRotation.SqRootNumbers, number ) != -1 )
            {
                return "Sq Rt";
            }

            if ( Array.IndexOf( WaveRotation.DerRootNumbers, number ) != -1 )
            {
                return "Der Rt";
            }

            if ( Array.IndexOf( WaveRotation.Numbers144, number ) != -1 )
            {
                return "144";
            }

            return specialNumber;
        }

        public static string GetNormalizedSymbol( string instrument )
        {
            string output = instrument;

            output.Replace( @"\", "" );

            output.ToUpper( );

            return output;
        }

        public static TimeSpecialNumbersType IsSpecialTimeNumber( int number )
        {
            string specialNumber = string.Empty;

            if ( Array.IndexOf( WaveRotation.FibsSeq, number ) != -1 )
            {
                return TimeSpecialNumbersType.FibSeq;
            }

            if ( Array.IndexOf( WaveRotation.DoubleFibsSeq, number ) != -1 )
            {
                return TimeSpecialNumbersType.DoubleFibsSeq;
            }


            if ( Array.IndexOf( WaveRotation.LucasSeq, number ) != -1 )
            {
                return TimeSpecialNumbersType.LucasSeq;
            }

            if ( Array.IndexOf( WaveRotation.DoubleLucasSeq, number ) != -1 )
            {
                return TimeSpecialNumbersType.DoubleLucasSeq;
            }

            return TimeSpecialNumbersType.NONE;
        }

        public static TimeSpecialNumbersType IsNearSpecialTimeNumber( int number )
        {
            string specialNumber = string.Empty;

            if ( Array.IndexOf( WaveRotation.FibsSeqNearby, number ) != -1 )
            {
                return TimeSpecialNumbersType.FibsSeqNearby;
            }

            if ( Array.IndexOf( WaveRotation.LucasSeqNearby, number ) != -1 )
            {
                return TimeSpecialNumbersType.LucasSeqNearby;
            }

            if ( Array.IndexOf( WaveRotation.FibRatio, number ) != -1 )
            {
                return TimeSpecialNumbersType.FibRatio;
            }

            if ( Array.IndexOf( WaveRotation.SqRootNumbers, number ) != -1 )
            {
                return TimeSpecialNumbersType.SqRootNumbers;
            }

            if ( Array.IndexOf( WaveRotation.DerRootNumbers, number ) != -1 )
            {
                return TimeSpecialNumbersType.DerRootNumbers;
            }

            if ( Array.IndexOf( WaveRotation.Numbers144, number ) != -1 )
            {
                return TimeSpecialNumbersType.Numbers144;
            }

            return TimeSpecialNumbersType.NONE;
        }


        public static double DoTheySquareAt45Dg( double firstDegree, double secondDegree )
        {
            var degrees = new PooledList<double>( );

            degrees.Add( firstDegree );

            double otherDegree = firstDegree;

            do
            {
                otherDegree = otherDegree - 45;

                if ( otherDegree > 0 )
                {
                    degrees.Add( otherDegree );
                }

            } while ( otherDegree >= 0 );

            otherDegree = firstDegree;

            do
            {
                otherDegree = otherDegree + 45;

                if ( otherDegree <= 360 )
                {
                    degrees.Add( otherDegree );
                }

            } while ( otherDegree <= 360 );

            degrees.Sort( );

            var index = degrees.IndexOf( secondDegree );

            if ( index != -1 )
            {
                return Math.Abs( firstDegree - secondDegree );
            }

            return -1;
        }

        public static double DoTheySquareAt45DgNearby( TimeSpan period, double firstDegree, double secondDegree, bool isMajor )
        {
            var degrees = new PooledList<double>( );

            degrees.Add( firstDegree );

            double otherDegree = firstDegree;

            do
            {
                otherDegree = otherDegree - 45;

                if ( otherDegree > 0 )
                {
                    degrees.Add( otherDegree );
                }

            } while ( otherDegree >= 0 );

            otherDegree = firstDegree;

            do
            {
                otherDegree = otherDegree + 45;

                if ( otherDegree <= 360 )
                {
                    degrees.Add( otherDegree );
                }

            } while ( otherDegree <= 360 );

            degrees.Sort( );

            var offsetBars = GetGannSqureBarsOffset( period, isMajor );

            foreach ( var degree in degrees )
            {
                var diff = Math.Abs( degree - secondDegree );

                if ( diff <= offsetBars )
                {
                    return Math.Abs( firstDegree - secondDegree );
                }

            }

            return -1;
        }

        public static int GetGannSqureBarsOffset( TimeSpan period, bool isMajor )
        {
            int offsetBars = 0;

            if ( period == TimeSpan.FromMinutes( 1 ) )
            {
                offsetBars = isMajor ? 3 : 1;
            }
            else if ( period == TimeSpan.FromMinutes( 4 ) )
            {
                offsetBars = isMajor ? 3 : 1;
            }
            else if ( period == TimeSpan.FromMinutes( 5 ) )
            {
                offsetBars = isMajor ? 3 : 1;
            }
            else if ( period == TimeSpan.FromMinutes( 15 ) )
            {
                offsetBars = isMajor ? 3 : 1;
            }
            else if ( period == TimeSpan.FromMinutes( 30 ) )
            {
                offsetBars = 3;
            }
            else if ( period >= TimeSpan.FromHours( 1 ) )
            {
                offsetBars = 3;
            }

            return offsetBars;
        }

        public static double DoTheySquareAt30DgNearby( TimeSpan period, double firstDegree, double secondDegree, bool isMajor )
        {
            var degrees = new PooledList<double>( );

            degrees.Add( firstDegree );

            var otherDegree = firstDegree;

            do
            {
                otherDegree = otherDegree - 30;

                if ( otherDegree > 0 )
                {
                    degrees.Add( otherDegree );
                }

            } while ( otherDegree >= 0 );

            otherDegree = firstDegree;

            do
            {
                otherDegree = otherDegree + 30;

                if ( otherDegree <= 360 )
                {
                    degrees.Add( otherDegree );
                }

            } while ( otherDegree <= 360 );

            degrees.Sort( );

            var offsetBars = GetGannSqureBarsOffset( period, isMajor );

            foreach ( var degree in degrees )
            {
                var diff = Math.Abs( degree - secondDegree );

                if ( diff <= offsetBars )
                {
                    return Math.Abs( firstDegree - secondDegree );
                }

            }

            return -1;
        }

        public static double DoTheySquareAt30Dg( double firstDegree, double secondDegree )
        {
            var degrees = new PooledList<double>( );

            degrees.Add( firstDegree );

            var otherDegree = firstDegree;

            do
            {
                otherDegree = otherDegree - 30;

                if ( otherDegree > 0 )
                {
                    degrees.Add( otherDegree );
                }

            } while ( otherDegree >= 0 );

            otherDegree = firstDegree;

            do
            {
                otherDegree = otherDegree + 30;

                if ( otherDegree <= 360 )
                {
                    degrees.Add( otherDegree );
                }

            } while ( otherDegree <= 360 );

            degrees.Sort( );

            if ( degrees.IndexOf( secondDegree ) != -1 )
            {
                return Math.Abs( firstDegree - secondDegree );
            }

            return -1;
        }

        public static double DoTheySquareAtSpecialGannSeq( double firstDegree, double secondDegree )
        {
            var diff = Math.Abs( firstDegree - secondDegree );

            int index = Array.IndexOf( WaveRotation.SpecialGannSeq, diff );

            if ( index != -1 )
            {
                return WaveRotation.SpecialGannSeq[ index ];
            }

            return -1;
        }


        public static ElliottWaveCycle GetMinimumWavesToMerge( BarPeriod period )
        {
            var output = ElliottWaveCycle.UNKNOWN;

            switch ( period )
            {
                case BarPeriod.t1:
                output = ElliottWaveCycle.Miniscule;
                break;

                case BarPeriod.m1:
                output = ElliottWaveCycle.Miniscule;
                break;

                case BarPeriod.m5:
                output = ElliottWaveCycle.Miniscule;
                break;

                case BarPeriod.m15:
                output = ElliottWaveCycle.Miniscule;
                break;

                case BarPeriod.m30:
                output = ElliottWaveCycle.Miniscule;
                break;

                case BarPeriod.H1:
                output = ElliottWaveCycle.Minor;
                break;

                case BarPeriod.H2:
                output = ElliottWaveCycle.Minor;
                break;

                case BarPeriod.H4:
                output = ElliottWaveCycle.Minor;
                break;

                case BarPeriod.D1:
                output = ElliottWaveCycle.SubIntermediate;
                break;

                case BarPeriod.W1:
                output = ElliottWaveCycle.Intermediate;
                break;

                case BarPeriod.M1:
                output = ElliottWaveCycle.Intermediate;
                break;
            }

            return output;
        }

        public static ElliottWaveCycle GetMinimumWavesToShow( TimeSpan period )
        {
            ElliottWaveCycle output = ElliottWaveCycle.Miniscule;

            if ( period == TimeSpan.FromSeconds( 1 ) )
            {
                output = ElliottWaveCycle.Miniscule;
            }
            else if ( period == TimeSpan.FromMinutes( 1 ) )
            {
                output = ElliottWaveCycle.Miniscule;
            }
            else if ( period == TimeSpan.FromMinutes( 4 ) )
            {
                output = ElliottWaveCycle.Miniscule;
            }
            else if ( period == TimeSpan.FromMinutes( 5 ) )
            {
                output = ElliottWaveCycle.Miniscule;
            }
            else if ( period == TimeSpan.FromMinutes( 15 ) )
            {
                output = ElliottWaveCycle.Miniscule;
            }
            else if ( period == TimeSpan.FromMinutes( 30 ) )
            {
                output = ElliottWaveCycle.Miniscule;
            }
            else if ( period == TimeSpan.FromHours( 1 ) )
            {
                output = ElliottWaveCycle.SubMinor;
            }
            else if ( period == TimeSpan.FromHours( 2 ) )
            {
                output = ElliottWaveCycle.SubMinor;
            }
            else if ( period == TimeSpan.FromHours( 3 ) )
            {
                output = ElliottWaveCycle.SubMinor;
            }
            else if ( period == TimeSpan.FromHours( 4 ) )
            {
                output = ElliottWaveCycle.SubMinor;
            }
            else if ( period == TimeSpan.FromHours( 6 ) )
            {
                output = ElliottWaveCycle.SubMinor;
            }
            else if ( period == TimeSpan.FromHours( 8 ) )
            {
                output = ElliottWaveCycle.SubMinor;
            }
            else if ( period == TimeSpan.FromDays( 1 ) )
            {
                output = ElliottWaveCycle.SubIntermediate;
            }
            else if ( period == TimeSpan.FromDays( 7 ) )
            {
                output = ElliottWaveCycle.Primary;
            }
            else if ( period == TimeSpan.FromDays( 30 ) )
            {
                output = ElliottWaveCycle.Primary;
            }
            else
            {
                output = ElliottWaveCycle.Primary;
            }                      

            return output;
        }

        public static string GetPeriodString( TimeSpan period )
        {
            string sPeriodId = "t1";

            if ( period.Days == 30 )
            {
                sPeriodId = "Monthly";
            }
            else if ( period.Days == 7 )
            {
                sPeriodId = "Weekly";
            }
            else if ( period.Days == 1 )
            {
                sPeriodId = "Daily";
            }
            else if ( period.Hours == 1 )
            {
                sPeriodId = "Hourly";
            }
            else if ( period.Hours == 2 )
            {
                sPeriodId = "Two Hours";
            }
            else if ( period.Hours == 3 )
            {
                sPeriodId = "Three Hours";
            }
            else if ( period.Hours == 4 )
            {
                sPeriodId = "Four Hours";
            }
            else if ( period.Minutes == 1 )
            {
                sPeriodId = "One Minute";
            }
            else if ( period.Minutes == 4 )
            {
                sPeriodId = "Four Minutes";
            }
            else if ( period.Minutes == 5 )
            {
                sPeriodId = "Five Minutes";
            }
            else if ( period.Minutes == 15 )
            {
                sPeriodId = "Fifteen Minutes";
            }
            else if ( period.Minutes == 30 )
            {
                sPeriodId = "Half Hour";
            }
            else if ( period.Ticks == 1 )
            {
                sPeriodId = "Tick";
            }

            return sPeriodId;
        }

        public static TimeSpan GetTimeSpanFromString( string timeString )
        {
            if ( timeString == "M1" )
            {
                return TimeSpan.FromDays( 30 );
            }
            else if ( timeString == "W1" )
            {
                return TimeSpan.FromDays( 7 );
            }
            else if ( timeString == "D1" )
            {
                return TimeSpan.FromDays( 1 );
            }
            else if ( timeString == "H1" )
            {
                return TimeSpan.FromHours( 1 );
            }
            else if ( timeString == "H2" )
            {
                return TimeSpan.FromHours( 2 );
            }
            else if ( timeString == "H3" )
            {
                return TimeSpan.FromHours( 3 );
            }
            else if ( timeString == "H4" )
            {
                return TimeSpan.FromHours( 4 );
            }
            else if ( timeString == "H6" )
            {
                return TimeSpan.FromHours( 6 );
            }
            else if ( timeString == "H8" )
            {
                return TimeSpan.FromHours( 8 );
            }
            else if ( timeString == "m30" )
            {
                return TimeSpan.FromMinutes( 30 );
            }
            else if ( timeString == "m15" )
            {
                return TimeSpan.FromMinutes( 15 );
            }
            else if ( timeString == "m5" )
            {
                return TimeSpan.FromMinutes( 5 );
            }
            else if ( timeString == "m4" )
            {
                return TimeSpan.FromMinutes( 4 );
            }
            else if ( timeString == "m1" )
            {
                return TimeSpan.FromMinutes( 1 );
            }
            else if ( timeString == "t1" )
            {
                return TimeSpan.FromTicks( 1 );
            }

            return TimeSpan.FromTicks( 0 );
        }

        public static int CalculateStorageSize( TimeSpan period )
        {
            int totalStorge = 0;

            if ( period == TimeSpan.FromTicks( 1 ) )
            {
                return ( GlobalConstants.TICKS_BARS_PER_DAY );
            }

            int extraBars = GetBarsBetweenLastBarAndEndOfWeek( DateTime.UtcNow, period );
            var hourlyBars = ( GlobalConstants.HOURLY_BARS_QUATERLY + GlobalConstants.HOURLY_BARS_PER_WEEK + extraBars + 1 );

            if ( period == TimeSpan.FromSeconds( 1 ) )
            {
                totalStorge = 20000;
            }
            else if ( period == TimeSpan.FromMinutes( 1 ) )
            {
                totalStorge = hourlyBars * 5;
            }
            else if ( period == TimeSpan.FromMinutes( 4 ) )
            {
                totalStorge = hourlyBars * 5;
            }
            else if ( period == TimeSpan.FromMinutes( 5 ) )
            {
                totalStorge = hourlyBars * 5;
            }
            else if ( period == TimeSpan.FromMinutes( 15 ) )
            {
                totalStorge = hourlyBars * 2;
            }
            else if ( period == TimeSpan.FromMinutes( 30 ) )
            {
                totalStorge = hourlyBars * 2 ;
            }
            else if ( period == TimeSpan.FromHours( 1 ) )
            {
                totalStorge = ( GlobalConstants.DAILY_BARS_50YEARS + GlobalConstants.DAILY_BARS_PER_WEEK + extraBars + 1 ) * 24;
            }
            else if ( period == TimeSpan.FromHours( 2 ) )
            {
                totalStorge = hourlyBars;
            }
            else if ( period == TimeSpan.FromHours( 3 ) )
            {
                totalStorge = hourlyBars;
            }
            else if ( period == TimeSpan.FromHours( 4 ) )
            {
                totalStorge = hourlyBars;
            }
            else if ( period == TimeSpan.FromHours( 6 ) )
            {
                totalStorge = hourlyBars;
            }
            else if ( period == TimeSpan.FromHours( 8 ) )
            {
                totalStorge = hourlyBars;
            }
            else if ( period == TimeSpan.FromDays( 1 ) )
            {
                totalStorge = GlobalConstants.DAILY_BARS_50YEARS + GlobalConstants.DAILY_BARS_PER_WEEK + extraBars + 1;
            }
            else if ( period == TimeSpan.FromDays( 7 ) )
            {
                totalStorge = ( GlobalConstants.WEEKLY_BARS_30YEARS + 1 );
            }
            else if ( period == TimeSpan.FromDays( 30 ) )
            {
                totalStorge = GlobalConstants.MONTHLY_BARS_30YEARS;
            }
            else
            {
                totalStorge = GlobalConstants.ONE_MINUTES_BARS_PER_MONTH * 2 + 1;
            }

            return totalStorge;
        }

        public static int CalculateStorageSize( TimeSpan period, int miniBarCount )
        {
            int totalStorge = CalculateStorageSize( period );

            if ( totalStorge < miniBarCount && period != TimeSpan.FromTicks( 1 ) )
            {
                totalStorge = miniBarCount + GetBarsBetweenLastBarAndEndOfWeek( DateTime.UtcNow, period );
            }


            return totalStorge;
        }

        public static int GetBarsBetweenLastBarAndEndOfWeek( DateTime lastBarTime, TimeSpan period )
        {
            var comingFridayUTC = DateTimeHelper.ReturnNextNthWeekdaysOfMonth( DateTime.UtcNow, DayOfWeek.Friday, 1 ).First( );
            comingFridayUTC     = comingFridayUTC.AddHours( 13 ); // now we have 1 pm UTC on certain Friday

            comingFridayUTC     = DateTime.SpecifyKind( comingFridayUTC, DateTimeKind.Utc );
            var tz              = TimeZoneInfo.FindSystemTimeZoneById( "Eastern Standard Time" );
            var barTime         = TimeZoneInfo.ConvertTimeFromUtc( comingFridayUTC, tz );

            while ( barTime.Hour < 17 )
            {
                barTime = barTime.AddHours( 1 );
            }

            var closingTime = barTime.Date.AddHours( barTime.Hour );

            var utcTimeZone = TimeZoneInfo.FindSystemTimeZoneById( "UTC" );
            var estTimeZone = TimeZoneInfo.FindSystemTimeZoneById( "Eastern Standard Time" );

            var marketClosingTime = TimeZoneInfo.ConvertTime( closingTime, estTimeZone, utcTimeZone );

            var differenceTimeSpan = marketClosingTime - lastBarTime;

            long remainder;
            int count = ( int )Math.DivRem( differenceTimeSpan.Ticks, period.Ticks, out remainder );

            return count;
        }


        //public static void GetStartAndEndDateForDatabar( TimeSpan period, out DateTime startDate, out DateTime endDate )
        //{
        //    startDate = DateTime.UtcNow.Date;
        //    endDate = DateTime.UtcNow.AddMinutes( 5 );

        //    var totalbars = FinancialHelper.CalculateStorageSize( period );

        //    if ( period.Days == 30 )
        //    {
        //        startDate = DateTime.Today.AddMonths( -totalbars );
        //        endDate = DateTime.Now.AddDays( 1 );
        //    }
        //    else if ( period.Days == 7 )
        //    {
        //        startDate = DateTime.Today.AddDays( -totalbars * period.TotalDays );
        //    }
        //    else if ( period.Days == 1 )
        //    {
        //        startDate = DateTime.Today.AddDays( -totalbars * period.TotalDays );
        //    }
        //    else if ( period.Ticks == 1 )
        //    {
        //        endDate = DateTime.UtcNow.AddMinutes( 5 );

        //        startDate = endDate.AddHours( -6 );
        //    }
        //    else
        //    {
        //        var d = DateTime.Today.AddMinutes( -( totalbars * period.TotalMinutes ) );
        //        startDate = new DateTime( d.Year, d.Month, d.Day, d.Hour, 0, 0, DateTimeKind.Utc );
        //    }
        //}

        

        public static string GetPeriodId( this TimeSpan period )
        {
            string sPeriodId = "t1";

            if ( period.Days == 30 )
            {
                sPeriodId = "M1";
            }
            else if ( period.Days == 7 )
            {
                sPeriodId = "W1";
            }
            else if ( period.Days == 1 )
            {
                sPeriodId = "D1";
            }
            else if ( period.Hours == 1 )
            {
                sPeriodId = "H1";
            }
            else if ( period.Hours == 2 )
            {
                sPeriodId = "H2";
            }
            else if ( period.Hours == 3 )
            {
                sPeriodId = "H3";
            }
            else if ( period.Hours == 4 )
            {
                sPeriodId = "H4";
            }
            else if ( period.Minutes == 1 )
            {
                sPeriodId = "m1";
            }
            else if ( period.Minutes == 4 )
            {
                sPeriodId = "m4";
            }
            else if ( period.Minutes == 5 )
            {
                sPeriodId = "m5";
            }
            else if ( period.Minutes == 15 )
            {
                sPeriodId = "m15";
            }
            else if ( period.Minutes == 30 )
            {
                sPeriodId = "m30";
            }
            else if ( period.Seconds == 1 )
            {
                sPeriodId = "s1";
            }
            else if ( period.Ticks == 1 )
            {
                sPeriodId = "t1";
            }

            return sPeriodId;
        }

        public static ElliottWaveCycle GetMinimumWavesToMerge( TimeSpan responsibleForWhatTimeFrame )
        {
            if ( responsibleForWhatTimeFrame == TimeSpan.FromDays( 30 ) )
            {
                return ElliottWaveCycle.Intermediate;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromDays( 7 ) )
            {
                return ElliottWaveCycle.Intermediate;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromDays( 1 ) )
            {
                return ElliottWaveCycle.SubIntermediate;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 1 ) )
            {
                return ElliottWaveCycle.SubMinor;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 2 ) )
            {
                return ElliottWaveCycle.SubMinor;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 3 ) )
            {
                return ElliottWaveCycle.SubMinor;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 4 ) )
            {
                return ElliottWaveCycle.SubMinor;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 6 ) )
            {
                return ElliottWaveCycle.SubMinor;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromHours( 8 ) )
            {
                return ElliottWaveCycle.SubMinor;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 30 ) )
            {
                return ElliottWaveCycle.Miniscule;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 15 ) )
            {
                return ElliottWaveCycle.Miniscule;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 5 ) )
            {
                return ElliottWaveCycle.Miniscule;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 4 ) )
            {
                return ElliottWaveCycle.Miniscule;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromMinutes( 1 ) )
            {
                return ElliottWaveCycle.Miniscule;
            }
            else if ( responsibleForWhatTimeFrame == TimeSpan.FromTicks( 1 ) )
            {
                return ElliottWaveCycle.Miniscule;
            }


            throw new ArgumentException( );
        }
    }
}
