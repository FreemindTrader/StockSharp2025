using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Definitions
{
    public class BaziAlgorithm
    {
        //==================================
        //以下为 http://blog.csdn.net/panaimin/article/details/8544489
        //计算五行
        // https://blog.csdn.net/lyx_zhl/article/details/53939889


        double _yiLei;
        double _tongLei;
        private char _belongsTo;
        const string TianGan = "甲乙丙丁戊己庚辛壬癸";
        const string DiZhi = "子丑寅卯辰巳午未申酉戌亥";

        private double[ ] _strengthResult = null;


        public bool CheckBazi( string bazi )
        {
            int baziLen;
            int i, j;

            baziLen = bazi.Length;

            if ( baziLen != 6 && baziLen != 8 ) return false;

            for ( i = 0; i < baziLen; )
            {
                char ch = bazi[ i ];

                for ( j = 0; j < 10; j++ )
                {
                    if ( ch == TianGan[ j ] ) break;
                }

                if ( j >= 10 ) return false;

                i++;

                ch = bazi[ i ];

                for ( j = 0; j < 12; j++ )
                {
                    if ( ch == DiZhi[ j ] ) break;
                }

                if ( j >= 12 ) return false;

                i++;
            }

            return true;
        }






        /*


        根据出生日子的天干，通过下表来查算时辰干支：


        时辰干支查算表


        时间时辰                             五行纪日干支


                               甲己     乙庚     丙辛     丁壬     戊癸


        23－01 子/水           甲子     丙子     戊子     庚子     壬子

        01－03 丑/土           乙丑     丁丑     己丑     辛丑     癸丑

        03－05 寅/木           丙寅     戊寅     庚寅     壬寅     甲寅

        05－07 卯/木           丁卯     己卯     辛卯     癸卯     乙卯

        07－09 辰/土           戊辰     庚辰     壬辰     甲辰     丙辰

        09－11 巳/火           己巳     辛巳     癸巳     己巳     丁巳

        11－13 午/火           庚午     壬午     甲午     丙午     戊午

        13－15 未/土           辛未     癸未     乙未     丁未     己未

        15－17 申/金           壬申     甲申     丙申     戊申     庚申

        17－19 酉/金           癸酉     乙酉     丁酉     己酉     辛酉

        19－21 戊/土           甲戌     丙戌     戊戌     庚戌     壬戌

        21－23 亥/水           乙亥     丁亥     己亥     辛亥     癸亥

        */


        string[ ][ ] cTimeGanZhi_Table = new string[ 12 ][ ]
                                                            {
                                                                new string[] {"甲子","丙子","戊子","庚子","壬子"},
                                                                new string[] {"乙丑","丁丑","己丑","辛丑","癸丑"},
                                                                new string[] {"丙寅","戊寅","庚寅","壬寅","甲寅"},
                                                                new string[] {"丁卯","己卯","辛卯","癸卯","乙卯"},
                                                                new string[] {"戊辰","庚辰","壬辰","甲辰","丙辰"},
                                                                new string[] {"己巳","辛巳","癸巳","己巳","丁巳"},
                                                                new string[] {"庚午","壬午","甲午","丙午","戊午"},
                                                                new string[] {"辛未","癸未","乙未","丁未","己未"},
                                                                new string[] {"壬申","甲申","丙申","戊申","庚申"},
                                                                new string[] {"癸酉","乙酉","丁酉","己酉","辛酉"},
                                                                new string[] {"甲戌","丙戌","戊戌","庚戌","壬戌"},
                                                                new string[] {"乙亥","丁亥","己亥","辛亥","癸亥"}
                                                            };






        static string sBuf;        // 用作八字结果缓冲区






        // 根据出生年月日的干支计算时辰干支


        // 输入参数：bazi，年月日的干支，即八字中的前六个字


        // 输入参数：hour，出生时间的小时数，-1~22


        // 输出结果：八字字符串，Unicode编码


        public string ComputeTimeGan( string bazi, int hour )
        {
            if ( hour > 22 ) hour -= 24;

            char dayGan = bazi[ 4 ];

            int indexX, indexY;

            int i;

            for ( i = 0; i < 10; i++ )
            {
                if ( dayGan == TianGan[ i ] ) break;
            }

            if ( i >= 10 ) return "";

            indexX = i;

            if ( indexX >= 5 ) indexX -= 5;

            indexY = ( hour + 1 ) / 2;

            sBuf = cTimeGanZhi_Table[ indexY ][ indexX ];             

            return sBuf;
        }






        /*


        十二月份天干强度表


        生月\四柱天干        甲              乙              丙              丁              戊              己              庚              辛              壬              癸


        子月                            1.2             1.2             1.0             1.0             1.0             1.0             1.0             1.0             1.2             1.2


        丑月                            1.06 1.06 1.0             1.0             1.1             1.1             1.14 1.14 1.1             1.1


        寅月                            1.14 1.14 1.2             1.2             1.06 1.06 1.0             1.0             1.0             1.0


        卯月                            1.2             1.2             1.2             1.2             1.0             1.0             1.0             1.0             1.0             1.0


        辰月                            1.1             1.1             1.06 1.06 1.1             1.1             1.1             1.1             1.04 1.04


        巳月                            1.0             1.0             1.14 1.14 1.14 1.14 1.06 1.06 1.06 1.06


        午月                            1.0             1.0             1.2             1.2             1.2             1.2             1.0             1.0             1.0             1.0


        未月                            1.04 1.04 1.1             1.1             1.16 1.16 1.1             1.1             1.0             1.0


        申月                            1.06 1.06 1.0             1.0             1.0             1.0             1.14 1.14 1.2             1.2


        酉月                            1.0             1.0             1.0             1.0             1.0             1.0             1.2             1.2             1.2             1.2


        戌月                            1.0             1.0             1.04 1.04 1.14 1.14 1.16 1.16 1.06 1.06


        亥月                            1.2             1.2             1.0             1.0             1.0             1.0             1.0             1.0             1.14 1.14


        */






        double[ ][ ] TianGan_Strength = new double[ 12 ][ ]
        {
            new double[] {1.2,  1.2,  1.0,  1.0,  1.0,  1.0,  1.0,  1.0,  1.2,  1.2},
            new double[] {1.06, 1.06, 1.0,  1.0,  1.1,  1.1,  1.14, 1.14, 1.1,  1.1},
            new double[] {1.14, 1.14, 1.2,  1.2,  1.06, 1.06, 1.0,  1.0,  1.0,  1.0},
            new double[] {1.2,  1.2,  1.2,  1.2,  1.0,  1.0,  1.0,  1.0,  1.0,  1.0},
            new double[] {1.1,  1.1,  1.06, 1.06, 1.1,  1.1,  1.1,  1.1,  1.04, 1.04},
            new double[] {1.0,  1.0,  1.14, 1.14, 1.14, 1.14, 1.06, 1.06, 1.06, 1.06},
            new double[] {1.0,  1.0,  1.2,  1.2,  1.2,  1.2,  1.0,  1.0,  1.0,  1.0},
            new double[] {1.04, 1.04, 1.1,  1.1,  1.16, 1.16, 1.1,  1.1,  1.0,  1.0},
            new double[] {1.06, 1.06, 1.0,  1.0,  1.0,  1.0,  1.14, 1.14, 1.2,  1.2},
            new double[] {1.0,  1.0,  1.0,  1.0,  1.0,  1.0,  1.2,  1.2,  1.2,  1.2},
            new double[] {1.0,  1.0,  1.04, 1.04, 1.14, 1.14, 1.16, 1.16, 1.06, 1.06},
            new double[] {1.2,  1.2,  1.0,  1.0,  1.0,  1.0,  1.0,  1.0,  1.14, 1.14}
        };






        /*


        十二月份地支强度表






                                生月       子月         丑月         寅月         卯月         辰月         巳月         午月         未月         申月         酉月         戌月         亥月        


        地支         支藏


        子              癸                       1.2             1.1             1.0             1.0             1.04 1.06 1.0             1.0             1.2             1.2             1.06 1.14


        丑              癸                       0.36 0.33 0.3             0.3             0.312        0.318        0.3             0.3             0.36 0.36 0.318        0.342


        丑              辛                       0.2             0.228        0.2             0.2             0.23 0.212        0.2             0.22 0.228        0.248        0.232        0.2           


        丑              己                       0.5             0.55 0.53 0.5             0.55 0.57 0.6             0.58 0.5             0.5             0.57 0.5            


        寅              丙                       0.3             0.3             0.36 0.36 0.318        0.342        0.36 0.33 0.3             0.3             0.342        0.318       


        寅              甲                       0.84 0.742        0.798        0.84 0.77 0.7             0.7             0.728        0.742        0.7             0.7             0.84


        卯              乙                       1.2             1.06 1.14 1.2             1.1             1.0             1.0             1.04 1.06 1.0             1.0             1.2            


        辰              乙                       0.36 0.318        0.342        0.36 0.33 0.3             0.3             0.312        0.318        0.3             0.3             0.36


        辰              癸                       0.24 0.22 0.2             0.2             0.208        0.2             0.2             0.2             0.24 0.24 0.212        0.228       


        辰              戊                       0.5             0.55 0.53 0.5             0.55 0.6             0.6             0.58 0.5             0.5             0.57 0.5            


        巳              庚                       0.3             0.342        0.3             0.3             0.33 0.3             0.3             0.33 0.342        0.36 0.348        0.3            


        巳              丙                       0.7             0.7             0.84 0.84 0.742        0.84 0.84 0.798        0.7             0.7             0.728        0.742       


        午              丁                       1.0             1.0             1.2             1.2             1.06 1.14 1.2             1.1             1.0             1.0             1.04 1.06


        未              丁                       0.3             0.3             0.36 0.36 0.318        0.342        0.36 0.33 0.3             0.3             0.312        0.318       


        未              乙                       0.24 0.212        0.228        0.24 0.22 0.2             0.2             0.208        0.212        0.2             0.2             0.24


        未              己                       0.5             0.55 0.53 0.5             0.55 0.57 0.6             0.58 0.5             0.5             0.57 0.5            


        申              壬                       0.36 0.33 0.3             0.3             0.312        0.318        0.3             0.3             0.36 0.36 0.318        0.342       


        申              庚                       0.7             0.798        0.7             0.7             0.77 0.742        0.7             0.77 0.798        0.84 0.812        0.7            


        酉              辛                       1.0             1.14 1.0             1.0             1.1             1.06 1.0             1.1             1.14 1.2             1.16 1.0            


        戌              辛                       0.3             0.342        0.3             0.3             0.33 0.318        0.3             0.33 0.342        0.36 0.348        0.3            


        戌              丁                       0.2             0.2             0.24 0.24 0.212        0.228        0.24 0.22 0.2             0.2             0.208        0.212       


        戌              戊                       0.5             0.55 0.53 0.5             0.55 0.57 0.6             0.58 0.5             0.5             0.57 0.5            


        亥              甲                       0.36 0.318        0.342        0.36 0.33 0.3             0.3             0.312        0.318        0.3             0.3             0.36


        亥              壬                        0.84 0.77 0.7             0.7             0.728        0.742        0.7             0.7             0.84 0.84 0.724        0.798       


        */


        struct ZISTRENGTH
        {
            public char diZhi;
            public char zhiCang;
            public double[ ] strength;


            public ZISTRENGTH( char a, char b, double[ ] c )
            {
                diZhi = a; zhiCang = b; strength = c;
            }
        };




        ZISTRENGTH[ ] DiZhi_Strength = new ZISTRENGTH[ ]
        {
            new ZISTRENGTH('子', '癸', new double[]  {1.2 , 1.1,   1.0,   1.0,  1.04,  1.06,  1.0,  1.0,   1.2,   1.2,   1.06,   1.14}),
            new ZISTRENGTH('丑', '癸', new double[]  {0.36, 0.33,  0.3,   0.3,  0.312, 0.318, 0.3,  0.3,   0.36,  0.36,  0.318,  0.342}),
            new ZISTRENGTH('丑', '辛', new double[]  {0.2,  0.228, 0.2,   0.2,  0.23,  0.212, 0.2,  0.22,  0.228, 0.248, 0.232,  0.2}),
            new ZISTRENGTH('丑', '己', new double[]  {0.5,  0.55,  0.53,  0.5,  0.55,  0.57,  0.6,  0.58,  0.5,   0.5,   0.57,   0.5}),
            new ZISTRENGTH('寅', '丙', new double[]  {0.3,  0.3,   0.36,  0.36, 0.318, 0.342, 0.36, 0.33,  0.3,   0.3,   0.342,  0.318}),
            new ZISTRENGTH('寅', '甲', new double[]  {0.84, 0.742, 0.798, 0.84, 0.77,  0.7,   0.7,  0.728, 0.742, 0.7,   0.7,    0.84}),
            new ZISTRENGTH('卯', '乙', new double[]  {1.2,  1.06,  1.14,  1.2,  1.1,   1.0,   1.0,  1.04,  1.06,  1.0,   1.0,    1.2}),
            new ZISTRENGTH('辰', '乙', new double[]  {0.36, 0.318, 0.342, 0.36, 0.33,  0.3,   0.3,  0.312, 0.318, 0.3,   0.3,    0.36}),
            new ZISTRENGTH('辰', '癸', new double[]  {0.24, 0.22,  0.2,   0.2,  0.208, 0.2,   0.2,  0.2,   0.24,  0.24,  0.212,  0.228}),
            new ZISTRENGTH('辰', '戊', new double[]  {0.5,  0.55,  0.53,  0.5,  0.55,  0.6,   0.6,  0.58,  0.5,   0.5,   0.57,   0.5}),
            new ZISTRENGTH('巳', '庚', new double[]  {0.3,  0.342, 0.3,   0.3,  0.33,  0.3,   0.3,  0.33,  0.342, 0.36,  0.348,  0.3}),
            new ZISTRENGTH('巳', '丙', new double[]  {0.7,  0.7,   0.84,  0.84, 0.742, 0.84,  0.84, 0.798, 0.7,   0.7,   0.728,  0.742}),
            new ZISTRENGTH('午', '丁', new double[]  {1.0,  1.0,   1.2,   1.2,  1.06,  1.14,  1.2,  1.1,   1.0,   1.0,   1.04,   1.06}),
            new ZISTRENGTH('未', '丁', new double[]  {0.3,  0.3,   0.36,  0.36, 0.318, 0.342, 0.36, 0.33,  0.3,   0.3,   0.312,  0.318}),
            new ZISTRENGTH('未', '乙', new double[]  {0.24, 0.212, 0.228, 0.24, 0.22,  0.2,   0.2,  0.208, 0.212, 0.2,   0.2,    0.24}),
            new ZISTRENGTH('未', '己', new double[]  {0.5,  0.55,  0.53,  0.5,  0.55,  0.57,  0.6,  0.58,  0.5,   0.5,   0.57,   0.5}),
            new ZISTRENGTH('申', '壬', new double[]  {0.36, 0.33,  0.3,   0.3,  0.312, 0.318, 0.3,  0.3,   0.36,  0.36,  0.318,  0.342}),
            new ZISTRENGTH('申', '庚', new double[]  {0.7,  0.798, 0.7,   0.7,  0.77,  0.742, 0.7,  0.77,  0.798, 0.84,  0.812,  0.7}),
            new ZISTRENGTH('酉', '辛', new double[]  {1.0,  1.14,  1.0,   1.0,  1.1,   1.06,  1.0,  1.1,   1.14,  1.2,   1.16,   1.0}),
            new ZISTRENGTH('戌', '辛', new double[]  {0.3,  0.342, 0.3,   0.3,  0.33,  0.318, 0.3,  0.33,  0.342, 0.36,  0.348,  0.3}),
            new ZISTRENGTH('戌', '丁', new double[]  {0.2,  0.2,   0.24,  0.24, 0.212, 0.228, 0.24, 0.22,  0.2,   0.2,   0.208,  0.212}),
            new ZISTRENGTH('戌', '戊', new double[]  {0.5,  0.55,  0.53,  0.5,  0.55,  0.57,  0.6,  0.58,  0.5,   0.5,   0.57,   0.5}),
            new ZISTRENGTH('亥', '甲', new double[]  {0.36, 0.318, 0.342, 0.36, 0.33,  0.3,   0.3,  0.312, 0.318, 0.3,   0.3,    0.36}),
            new ZISTRENGTH('亥', '壬', new double[]  {0.84, 0.77,  0.7,   0.7,  0.728, 0.742, 0.7,  0.7,   0.84,  0.84,  0.724,  0.798})
        };

        /*
                 金 --- 0
                 木 --- 1
                 水 --- 2
                 火 --- 3
                 土 --- 4
        */




        char[ ] WuXingTable = new char[ ] { '金', '木', '水', '火', '土' };

        /*
                 天干地支的五行属性表
                 天干： 甲-木、乙-木、丙-火、丁－火、戊－土、己－土、庚－金、辛－金、壬－水、癸－水
                 地支：子-水、丑-土、寅-木、卯－木、辰－土、巳－火、午－火、未－土、申－金、酉－金、戌－土、亥－水
        */






        int[ ] TianGan_WuXingProp    = new int[ 10 ] { 1, 1, 3, 3, 4, 4, 0, 0, 2, 2       };
        int[ ] DiZhi_WuXingProp      = new int[ 12 ] { 2, 4, 1, 1, 4, 3, 3, 4, 0, 0, 4, 2 };
        int[ ] GenerationSourceTable = new int[ 5 ]  { 4, 2, 0, 1, 3                      };

        int ComputeGanIndex( char gan )
        {
            int i;


            for ( i = 0; i < 10; i++ )
                if ( TianGan[ i ] == gan ) break;


            if ( i >= 10 ) return -1;


            return i;
        }

        int ComputeZhiIndex( char zhi )
        {
            int i;


            for ( i = 0; i < 12; i++ )
                if ( DiZhi[ i ] == zhi ) break;


            if ( i >= 12 ) return -1;


            return i;
        }


        public char BelongsTo
        {
            get { return _belongsTo; }
            set
            {
                _belongsTo = value;
            }
        }


        public double GetStrength( int index )
        {
            if ( index > _strengthResult.Length )
            {
                throw new NotImplementedException( );
            }

            return _strengthResult[ index ];
        }


        public double TongLei
        {
            get { return _tongLei; }
            set
            {
                _tongLei = value;
            }
        }

        
        public double YiLei
        {
            get { return _yiLei; }
            set
            {
                _yiLei = value;
            }
        }
        


        static string sResultBuf;   // 用作八字测算结果返回的字符缓冲区


        // 根据八字计算五行平衡
        // 输入参数：bazi，年月日时的干支，即俗称的八字
        // 输出结果：分析结果字符串，Unicode编码
        public string EvalBazi( string bazi )
        {
            _strengthResult = new double[ 5 ];

            int monthIndex = ComputeZhiIndex( bazi[ 3 ] );


            if ( monthIndex == -1 ) return "";

            sResultBuf = bazi;
            sResultBuf += "\n\n五行强度：天干 + 支藏\n";


            for ( int wuXing = 0; wuXing < 5; wuXing++ )
            {
                double value1 = 0.0, value2 = 0.0;
                int i;


                //扫描4个天干
                for ( i = 0; i < 8; i += 2 )
                {
                    char gan = bazi[ i ];
                    int index = ComputeGanIndex( gan );
                    if ( index == -1 ) return "";

                    if ( TianGan_WuXingProp[ index ] == wuXing )
                    {
                        value1 += TianGan_Strength[ monthIndex ][ index ];
                    }
                        
                }

                //扫描支藏
                for ( i = 1; i < 8; i += 2 )
                {
                    char zhi = bazi[ i ];


                    for ( int j = 0; j < DiZhi_Strength.Length; j++ )
                    {


                        if ( DiZhi_Strength[ j ].diZhi == zhi )
                        {


                            int zhiCangIndex = ComputeGanIndex( DiZhi_Strength[ j ].zhiCang );


                            if ( zhiCangIndex == -1 ) return "";


                            if ( TianGan_WuXingProp[ zhiCangIndex ] == wuXing )
                            {
                                value2 += DiZhi_Strength[ j ].strength[ monthIndex ];
                                break;
                            }
                        }
                    }
                }


                _strengthResult[ wuXing ] = value1 + value2;

                //输出一行计算结果
                {
                    string preStr;
                    string tmpBuf;


                    tmpBuf = value1.ToString( "0.00" ) + " + " + value2.ToString( "0.00" ) + " = " + ( value1 + value2 ).ToString( "0.00" ) + "\n";

                    preStr = WuXingTable[ wuXing ] + ":\t";
                    sResultBuf += preStr;
                    sResultBuf += tmpBuf;
                }
            }


            //根据日干求命里属性
            int fateProp, srcProp;
            {
                string tmpWBuf;


                fateProp = TianGan_WuXingProp[ ComputeGanIndex( bazi[ 4 ] ) ];


                if ( fateProp == -1 ) return "";

                _belongsTo = WuXingTable[ fateProp ];

                tmpWBuf = "\n命属" + _belongsTo + "\n\n";
                sResultBuf += tmpWBuf;
            }


            //求同类和异类的强度值
            srcProp = GenerationSourceTable[ fateProp ];
            {
                string preStr;
                string tmpBuf;
                _tongLei = _strengthResult[ fateProp ] + _strengthResult[ srcProp ];
                _yiLei = 0.0;


                for ( int i = 0; i < 5; i++ ) _yiLei += _strengthResult[ i ];


                _yiLei -= _tongLei;

                tmpBuf = _strengthResult[ fateProp ].ToString( "0.00" ) + " + " + _strengthResult[ srcProp ].ToString( "0.00" ) + " = " + _tongLei.ToString( "0.00" ) + "\n";
                preStr = "同类：" + WuXingTable[ fateProp ] + "+" + WuXingTable[ srcProp ] + "，";


                sResultBuf += preStr;
                sResultBuf += tmpBuf;


                tmpBuf = _yiLei.ToString( "0.00" ) + "\n";
                sResultBuf += "异类：总和为 " + tmpBuf;
            }
            return sResultBuf;
        }
    }
}
