using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Reflection;
using fx.Definitions.Messages;
using fx.Collections;

namespace fx.Definitions
{
    public enum WorkFlowStatus
    {
        NotStarted = 0,
        StartWork = 1,
        DoneWork = 2,
        ErrorInWork = 3
    }        


    public enum BarSpeed
    {
        Invalid = 0,
        Stopped = 1,
        SpeedingUp = 2,
        Steady = 3,
        SlowingDown = 4

    }

    

    public enum FibPercentage
    {
        Invalid = 0,
        [Description( "9.02%" )]
        Fib_9_02 = 1,
        [Description( "14.6%" )]
        Fib_14_6 = 2,
        [Description( "23.6%" )]
        Fib_23_6,
        [Description( "30.0%" )]
        Fib_30,
        [Description( "33.3%" )]
        Fib_33_3,
        [Description( "38.2%" )]
        Fib_38_2,
        [Description( "41.4%" )]
        Fib_41_4,
        [Description( "44.1%" )]
        Fib_44_1,
        [Description( "50%" )]
        Fib_50,
        [Description( "58.6%" )]
        Fib_58_6,
        [Description( "61.8%" )]
        Fib_61_8,
        [Description( "66.66%" )]
        Fib_66_66,
        [Description( "76.4%" )]
        Fib_76_4,
        [Description( "85.4%" )]
        Fib_85_4,
        [Description( "90.02%" )]
        Fib_90_02,
        [Description( "94.4%" )]
        Fib_94_4,
        [Description( "98.4%" )]
        Fib_98_4,
        [Description( "98.68%" )]
        Fib_98_68,

        [Description( "100%" )]
        Fib_100,
        [Description( "105.6%" )]
        Fib_105_6,
        [Description( "109.2%" )]
        Fib_109_2,
        [Description( "114.6%" )]
        Fib_114_6,
        [Description( "123.6%" )]
        Fib_123_6,
        [Description( "127.2%" )]
        Fib_127_2,
        [Description( "133.3%" )]
        Fib_133_3,
        [Description( "138.2%" )]
        Fib_138_2,
        [Description( "141.4%" )]
        Fib_141_4,
        [Description( "150%" )]
        Fib_150,
        [Description( "161.8%" )]
        Fib_161_8,
        [Description( "166.7%" )]
        Fib_166_7,
        [Description( "176.4%" )]
        Fib_176_4,
        [Description( "185.4%" )]
        Fib_185_4,
        [Description( "190.2%" )]
        Fib_190_2,
        [Description( "194.4%" )]
        Fib_194_4,
        [Description( "198.4%" )]
        Fib_198_4,
        [Description( "200.0%" )]
        Fib_200,
        [Description( "205.6%" )]
        Fib_205_6,
        [Description( "209.2%" )]
        Fib_209_2,
        [Description( "214.6%" )]
        Fib_214_6,
        [Description( "223.6%" )]
        Fib_223_6,
        [Description( "227.2%" )]
        Fib_227_2,
        [Description( "238.2%" )]
        Fib_238_2,
        [Description( "241.4%" )]
        Fib_241_4,
        [Description( "242.7%" )]
        Fib_242_7,
        [Description( "250.0%" )]
        Fib_250,
        [Description( "261.8%" )]
        Fib_261_8,
        [Description( "266.7%" )]
        Fib_266_7,
        [Description( "276.4%" )]
        Fib_276_4,
        [Description( "285.4%" )]
        Fib_285_4,
        [Description( "290.2%" )]
        Fib_290_2,
        [Description( "294.4%" )]
        Fib_294_4,
        [Description( "298.4%" )]
        Fib_298_4,



        [Description( "200.0%" )]
        Fib_300,
        [Description( "205.6%" )]
        Fib_305_6,
        [Description( "209.2%" )]
        Fib_309_2,
        [Description( "214.6%" )]
        Fib_314_6,
        [Description( "223.6%" )]
        Fib_323_6,
        [Description( "227.2%" )]
        Fib_327_2,
        [Description( "238.2%" )]
        Fib_338_2,
        [Description( "241.4%" )]
        Fib_341_4,
        [Description( "242.7%" )]
        Fib_342_7,
        [Description( "250.0%" )]
        Fib_350,
        [Description( "261.8%" )]
        Fib_361_8,
        [Description( "266.7%" )]
        Fib_366_7,
        [Description( "276.4%" )]
        Fib_376_4,
        [Description( "285.4%" )]
        Fib_385_4,
        [Description( "290.2%" )]
        Fib_390_2,
        [Description( "294.4%" )]
        Fib_394_4,
        [Description( "298.4%" )]
        Fib_398_4,

        [Description( "200.0%" )]
        Fib_400,
        [Description( "205.6%" )]
        Fib_405_6,
        [Description( "209.2%" )]
        Fib_409_2,
        [Description( "214.6%" )]
        Fib_414_6,
        [Description( "223.6%" )]
        Fib_423_6,
        [Description( "227.2%" )]
        Fib_427_2,
        [Description( "238.2%" )]
        Fib_438_2,
        [Description( "241.4%" )]
        Fib_441_4,
        [Description( "242.7%" )]
        Fib_442_7,
        [Description( "250.0%" )]
        Fib_450,
        [Description( "261.8%" )]
        Fib_461_8,
        [Description( "266.7%" )]
        Fib_466_7,
        [Description( "276.4%" )]
        Fib_476_4,
        [Description( "285.4%" )]
        Fib_485_4,
        [Description( "290.2%" )]
        Fib_490_2,
        [Description( "294.4%" )]
        Fib_494_4,
        [Description( "298.4%" )]
        Fib_498_4,

        [Description( "200.0%" )]
        Fib_500,
        [Description( "205.6%" )]
        Fib_505_6,
        [Description( "209.2%" )]
        Fib_509_2,
        [Description( "214.6%" )]
        Fib_514_6,
        [Description( "223.6%" )]
        Fib_523_6,
        [Description( "227.2%" )]
        Fib_527_2,
        [Description( "238.2%" )]
        Fib_538_2,
        [Description( "241.4%" )]
        Fib_541_4,
        [Description( "242.7%" )]
        Fib_542_7,
        [Description( "250.0%" )]
        Fib_550,
        [Description( "261.8%" )]
        Fib_561_8,
        [Description( "266.7%" )]
        Fib_566_7,
        [Description( "276.4%" )]
        Fib_576_4,
        [Description( "285.4%" )]
        Fib_585_4,
        [Description( "290.2%" )]
        Fib_590_2,
        [Description( "294.4%" )]
        Fib_594_4,
        [Description( "298.4%" )]
        Fib_598_4,


        [Description( "200.0%" )]
        Fib_600,
        [Description( "205.6%" )]
        Fib_605_6,
        [Description( "209.2%" )]
        Fib_609_2,
        [Description( "214.6%" )]
        Fib_614_6,
        [Description( "223.6%" )]
        Fib_623_6,
        [Description( "227.2%" )]
        Fib_627_2,
        [Description( "238.2%" )]
        Fib_638_2,
        [Description( "241.4%" )]
        Fib_641_4,
        [Description( "242.7%" )]
        Fib_642_7,
        [Description( "250.0%" )]
        Fib_650,
        [Description( "261.8%" )]
        Fib_661_8,
        [Description( "266.7%" )]
        Fib_666_7,
        [Description( "276.4%" )]
        Fib_676_4,
        [Description( "285.4%" )]
        Fib_685_4,
        [Description( "290.2%" )]
        Fib_690_2,
        [Description( "294.4%" )]
        Fib_694_4,
        [Description( "298.4%" )]
        Fib_698_4,



        [Description( "200.0%" )]
        Fib_700,
        [Description( "205.6%" )]
        Fib_705_6,
        [Description( "209.2%" )]
        Fib_709_2,
        [Description( "214.6%" )]
        Fib_714_6,
        [Description( "223.6%" )]
        Fib_723_6,
        [Description( "227.2%" )]
        Fib_727_2,
        [Description( "238.2%" )]
        Fib_738_2,
        [Description( "241.4%" )]
        Fib_741_4,
        [Description( "242.7%" )]
        Fib_742_7,
        [Description( "250.0%" )]
        Fib_750,
        [Description( "261.8%" )]
        Fib_761_8,
        [Description( "266.7%" )]
        Fib_766_7,
        [Description( "276.4%" )]
        Fib_776_4,
        [Description( "285.4%" )]
        Fib_785_4,
        [Description( "290.2%" )]
        Fib_790_2,
        [Description( "294.4%" )]
        Fib_794_4,
        [Description( "298.4%" )]
        Fib_798_4,

        [Description( "200.0%" )]
        Fib_800,
        [Description( "205.6%" )]
        Fib_805_6,
        [Description( "209.2%" )]
        Fib_809_2,
        [Description( "214.6%" )]
        Fib_814_6,
        [Description( "223.6%" )]
        Fib_823_6,
        [Description( "227.2%" )]
        Fib_827_2,
        [Description( "238.2%" )]
        Fib_838_2,
        [Description( "241.4%" )]
        Fib_841_4,
        [Description( "242.7%" )]
        Fib_842_7,
        [Description( "250.0%" )]
        Fib_850,
        [Description( "261.8%" )]
        Fib_861_8,
        [Description( "266.7%" )]
        Fib_866_7,
        [Description( "276.4%" )]
        Fib_876_4,
        [Description( "285.4%" )]
        Fib_885_4,
        [Description( "290.2%" )]
        Fib_890_2,
        [Description( "294.4%" )]
        Fib_894_4,
        [Description( "298.4%" )]
        Fib_898_4,

        [Description( "200.0%" )]
        Fib_900,
        [Description( "205.6%" )]
        Fib_905_6,
        [Description( "209.2%" )]
        Fib_909_2,
        [Description( "214.6%" )]
        Fib_914_6,
        [Description( "223.6%" )]
        Fib_923_6,
        [Description( "227.2%" )]
        Fib_927_2,
        [Description( "238.2%" )]
        Fib_938_2,
        [Description( "241.4%" )]
        Fib_941_4,
        [Description( "242.7%" )]
        Fib_942_7,
        [Description( "250.0%" )]
        Fib_950,
        [Description( "261.8%" )]
        Fib_961_8,
        [Description( "266.7%" )]
        Fib_966_7,
        [Description( "276.4%" )]
        Fib_976_4,
        [Description( "285.4%" )]
        Fib_985_4,
        [Description( "290.2%" )]
        Fib_990_2,
        [Description( "294.4%" )]
        Fib_994_4,
        [Description( "298.4%" )]
        Fib_998_4,

        [Description( "200.0%" )]
        Fib_1000,
        [Description( "205.6%" )]
        Fib_1005_6,
        [Description( "209.2%" )]
        Fib_1009_2,
        [Description( "214.6%" )]
        Fib_1014_6,
        [Description( "223.6%" )]
        Fib_1023_6,
        [Description( "227.2%" )]
        Fib_1027_2,
        [Description( "238.2%" )]
        Fib_1038_2,
        [Description( "241.4%" )]
        Fib_1041_4,
        [Description( "242.7%" )]
        Fib_1042_7,
        [Description( "250.0%" )]
        Fib_1050,
        [Description( "261.8%" )]
        Fib_1061_8,
        [Description( "266.7%" )]
        Fib_1066_7,
        [Description( "276.4%" )]
        Fib_1076_4,
        [Description( "285.4%" )]
        Fib_1085_4,
        [Description( "290.2%" )]
        Fib_1090_2,
        [Description( "294.4%" )]
        Fib_1094_4,
        [Description( "298.4%" )]
        Fib_1098_4,

        [Description( "100%" )]
        Fib_1100,
        [Description( "105.6%" )]
        Fib_1105_6,
        [Description( "109.2%" )]
        Fib_1109_2,
        [Description( "114.6%" )]
        Fib_1114_6,
        [Description( "123.6%" )]
        Fib_1123_6,
        [Description( "127.2%" )]
        Fib_1127_2,
        [Description( "133.3%" )]
        Fib_1133_3,
        [Description( "138.2%" )]
        Fib_1138_2,
        [Description( "141.4%" )]
        Fib_1141_4,
        [Description( "150%" )]
        Fib_1150,
        [Description( "161.8%" )]
        Fib_1161_8,
        [Description( "166.7%" )]
        Fib_1166_7,
        [Description( "176.4%" )]
        Fib_1176_4,
        [Description( "185.4%" )]
        Fib_1185_4,
        [Description( "190.2%" )]
        Fib_1190_2,
        [Description( "194.4%" )]
        Fib_1194_4,
        [Description( "198.4%" )]
        Fib_1198_4,
        [Description( "200.0%" )]
        Fib_1200,
        [Description( "205.6%" )]
        Fib_1205_6,
        [Description( "209.2%" )]
        Fib_1209_2,
        [Description( "214.6%" )]
        Fib_1214_6,
        [Description( "223.6%" )]
        Fib_1223_6,
        [Description( "227.2%" )]
        Fib_1227_2,
        [Description( "238.2%" )]
        Fib_1238_2,
        [Description( "241.4%" )]
        Fib_1241_4,
        [Description( "242.7%" )]
        Fib_1242_7,
        [Description( "250.0%" )]
        Fib_1250,
        [Description( "261.8%" )]
        Fib_1261_8,
        [Description( "266.7%" )]
        Fib_1266_7,
        [Description( "276.4%" )]
        Fib_1276_4,
        [Description( "285.4%" )]
        Fib_1285_4,
        [Description( "290.2%" )]
        Fib_1290_2,
        [Description( "294.4%" )]
        Fib_1294_4,
        [Description( "298.4%" )]
        Fib_1298_4,



        [Description( "200.0%" )]
        Fib_1300,
        [Description( "205.6%" )]
        Fib_1305_6,
        [Description( "209.2%" )]
        Fib_1309_2,
        [Description( "214.6%" )]
        Fib_1314_6,
        [Description( "223.6%" )]
        Fib_1323_6,
        [Description( "227.2%" )]
        Fib_1327_2,
        [Description( "238.2%" )]
        Fib_1338_2,
        [Description( "241.4%" )]
        Fib_1341_4,
        [Description( "242.7%" )]
        Fib_1342_7,
        [Description( "250.0%" )]
        Fib_1350,
        [Description( "261.8%" )]
        Fib_1361_8,
        [Description( "266.7%" )]
        Fib_1366_7,
        [Description( "276.4%" )]
        Fib_1376_4,
        [Description( "285.4%" )]
        Fib_1385_4,
        [Description( "290.2%" )]
        Fib_1390_2,
        [Description( "294.4%" )]
        Fib_1394_4,
        [Description( "298.4%" )]
        Fib_1398_4,

        [Description( "200.0%" )]
        Fib_1400,
        [Description( "205.6%" )]
        Fib_1405_6,
        [Description( "209.2%" )]
        Fib_1409_2,
        [Description( "214.6%" )]
        Fib_1414_6,
        [Description( "223.6%" )]
        Fib_1423_6,
        [Description( "227.2%" )]
        Fib_1427_2,
        [Description( "238.2%" )]
        Fib_1438_2,
        [Description( "241.4%" )]
        Fib_1441_4,
        [Description( "242.7%" )]
        Fib_1442_7,
        [Description( "250.0%" )]
        Fib_1450,
        [Description( "261.8%" )]
        Fib_1461_8,
        [Description( "266.7%" )]
        Fib_1466_7,
        [Description( "276.4%" )]
        Fib_1476_4,
        [Description( "285.4%" )]
        Fib_1485_4,
        [Description( "290.2%" )]
        Fib_1490_2,
        [Description( "294.4%" )]
        Fib_1494_4,
        [Description( "298.4%" )]
        Fib_1498_4,

        [Description( "200.0%" )]
        Fib_1500,
        [Description( "205.6%" )]
        Fib_1505_6,
        [Description( "209.2%" )]
        Fib_1509_2,
        [Description( "214.6%" )]
        Fib_1514_6,
        [Description( "223.6%" )]
        Fib_1523_6,
        [Description( "227.2%" )]
        Fib_1527_2,
        [Description( "238.2%" )]
        Fib_1538_2,
        [Description( "241.4%" )]
        Fib_1541_4,
        [Description( "242.7%" )]
        Fib_1542_7,
        [Description( "250.0%" )]
        Fib_1550,
        [Description( "261.8%" )]
        Fib_1561_8,
        [Description( "266.7%" )]
        Fib_1566_7,
        [Description( "276.4%" )]
        Fib_1576_4,
        [Description( "285.4%" )]
        Fib_1585_4,
        [Description( "290.2%" )]
        Fib_1590_2,
        [Description( "294.4%" )]
        Fib_1594_4,
        [Description( "298.4%" )]
        Fib_1598_4,


        [Description( "200.0%" )]
        Fib_1600,
        [Description( "205.6%" )]
        Fib_1605_6,
        [Description( "209.2%" )]
        Fib_1609_2,
        [Description( "214.6%" )]
        Fib_1614_6,
        [Description( "223.6%" )]
        Fib_1623_6,
        [Description( "227.2%" )]
        Fib_1627_2,
        [Description( "238.2%" )]
        Fib_1638_2,
        [Description( "241.4%" )]
        Fib_1641_4,
        [Description( "242.7%" )]
        Fib_1642_7,
        [Description( "250.0%" )]
        Fib_1650,
        [Description( "261.8%" )]
        Fib_1661_8,
        [Description( "266.7%" )]
        Fib_1666_7,
        [Description( "276.4%" )]
        Fib_1676_4,
        [Description( "285.4%" )]
        Fib_1685_4,
        [Description( "290.2%" )]
        Fib_1690_2,
        [Description( "294.4%" )]
        Fib_1694_4,
        [Description( "298.4%" )]
        Fib_1698_4,



        [Description( "200.0%" )]
        Fib_1700,
        [Description( "205.6%" )]
        Fib_1705_6,
        [Description( "209.2%" )]
        Fib_1709_2,
        [Description( "214.6%" )]
        Fib_1714_6,
        [Description( "223.6%" )]
        Fib_1723_6,
        [Description( "227.2%" )]
        Fib_1727_2,
        [Description( "238.2%" )]
        Fib_1738_2,
        [Description( "241.4%" )]
        Fib_1741_4,
        [Description( "242.7%" )]
        Fib_1742_7,
        [Description( "250.0%" )]
        Fib_1750,
        [Description( "261.8%" )]
        Fib_1761_8,
        [Description( "266.7%" )]
        Fib_1766_7,
        [Description( "276.4%" )]
        Fib_1776_4,
        [Description( "285.4%" )]
        Fib_1785_4,
        [Description( "290.2%" )]
        Fib_1790_2,
        [Description( "294.4%" )]
        Fib_1794_4,
        [Description( "298.4%" )]
        Fib_1798_4,

        [Description( "200.0%" )]
        Fib_1800,
        [Description( "205.6%" )]
        Fib_1805_6,
        [Description( "209.2%" )]
        Fib_1809_2,
        [Description( "214.6%" )]
        Fib_1814_6,
        [Description( "223.6%" )]
        Fib_1823_6,
        [Description( "227.2%" )]
        Fib_1827_2,
        [Description( "238.2%" )]
        Fib_1838_2,
        [Description( "241.4%" )]
        Fib_1841_4,
        [Description( "242.7%" )]
        Fib_1842_7,
        [Description( "250.0%" )]
        Fib_1850,
        [Description( "261.8%" )]
        Fib_1861_8,
        [Description( "266.7%" )]
        Fib_1866_7,
        [Description( "276.4%" )]
        Fib_1876_4,
        [Description( "285.4%" )]
        Fib_1885_4,
        [Description( "290.2%" )]
        Fib_1890_2,
        [Description( "294.4%" )]
        Fib_1894_4,
        [Description( "298.4%" )]
        Fib_1898_4,

        [Description( "200.0%" )]
        Fib_1900,
        [Description( "205.6%" )]
        Fib_1905_6,
        [Description( "209.2%" )]
        Fib_1909_2,
        [Description( "214.6%" )]
        Fib_1914_6,
        [Description( "223.6%" )]
        Fib_1923_6,
        [Description( "227.2%" )]
        Fib_1927_2,
        [Description( "238.2%" )]
        Fib_1938_2,
        [Description( "241.4%" )]
        Fib_1941_4,
        [Description( "242.7%" )]
        Fib_1942_7,
        [Description( "250.0%" )]
        Fib_1950,
        [Description( "261.8%" )]
        Fib_1961_8,
        [Description( "266.7%" )]
        Fib_1966_7,
        [Description( "276.4%" )]
        Fib_1976_4,
        [Description( "285.4%" )]
        Fib_1985_4,
        [Description( "290.2%" )]
        Fib_1990_2,
        [Description( "294.4%" )]
        Fib_1994_4,
        [Description( "298.4%" )]
        Fib_1998_4,

        [Description( "200.0%" )]
        Fib_2000,
        [Description( "205.6%" )]
        Fib_2005_6,
        [Description( "209.2%" )]
        Fib_2009_2,
        [Description( "214.6%" )]
        Fib_2014_6,
        [Description( "223.6%" )]
        Fib_2023_6,
        [Description( "227.2%" )]
        Fib_2027_2,
        [Description( "238.2%" )]
        Fib_2038_2,
        [Description( "241.4%" )]
        Fib_2041_4,
        [Description( "242.7%" )]
        Fib_2042_7,
        [Description( "250.0%" )]
        Fib_2050,
        [Description( "261.8%" )]
        Fib_2061_8,
        [Description( "266.7%" )]
        Fib_2066_7,
        [Description( "276.4%" )]
        Fib_2076_4,
        [Description( "285.4%" )]
        Fib_2085_4,
        [Description( "290.2%" )]
        Fib_2090_2,
        [Description( "294.4%" )]
        Fib_2094_4,
        [Description( "298.4%" )]
        Fib_2098_4,

        [Description( "100%" )]
        Fib_2100,
        [Description( "105.6%" )]
        Fib_2105_6,
        [Description( "109.2%" )]
        Fib_2109_2,
        [Description( "114.6%" )]
        Fib_2114_6,
        [Description( "123.6%" )]
        Fib_2123_6,
        [Description( "127.2%" )]
        Fib_2127_2,
        [Description( "133.3%" )]
        Fib_2133_3,
        [Description( "138.2%" )]
        Fib_2138_2,
        [Description( "141.4%" )]
        Fib_2141_4,
        [Description( "150%" )]
        Fib_2150,
        [Description( "161.8%" )]
        Fib_2161_8,
        [Description( "166.7%" )]
        Fib_2166_7,
        [Description( "176.4%" )]
        Fib_2176_4,
        [Description( "185.4%" )]
        Fib_2185_4,
        [Description( "190.2%" )]
        Fib_2190_2,
        [Description( "194.4%" )]
        Fib_2194_4,
        [Description( "198.4%" )]
        Fib_2198_4,
        [Description( "200.0%" )]
        Fib_2200,
        [Description( "205.6%" )]
        Fib_2205_6,
        [Description( "209.2%" )]
        Fib_2209_2,
        [Description( "214.6%" )]
        Fib_2214_6,
        [Description( "223.6%" )]
        Fib_2223_6,
        [Description( "227.2%" )]
        Fib_2227_2,
        [Description( "238.2%" )]
        Fib_2238_2,
        [Description( "241.4%" )]
        Fib_2241_4,
        [Description( "242.7%" )]
        Fib_2242_7,
        [Description( "250.0%" )]
        Fib_2250,
        [Description( "261.8%" )]
        Fib_2261_8,
        [Description( "266.7%" )]
        Fib_2266_7,
        [Description( "276.4%" )]
        Fib_2276_4,
        [Description( "285.4%" )]
        Fib_2285_4,
        [Description( "290.2%" )]
        Fib_2290_2,
        [Description( "294.4%" )]
        Fib_2294_4,
        [Description( "298.4%" )]
        Fib_2298_4,



        [Description( "200.0%" )]
        Fib_2300,
        [Description( "205.6%" )]
        Fib_2305_6,
        [Description( "209.2%" )]
        Fib_2309_2,
        [Description( "214.6%" )]
        Fib_2314_6,
        [Description( "223.6%" )]
        Fib_2323_6,
        [Description( "227.2%" )]
        Fib_2327_2,
        [Description( "238.2%" )]
        Fib_2338_2,
        [Description( "241.4%" )]
        Fib_2341_4,
        [Description( "242.7%" )]
        Fib_2342_7,
        [Description( "250.0%" )]
        Fib_2350,
        [Description( "261.8%" )]
        Fib_2361_8,
        [Description( "266.7%" )]
        Fib_2366_7,
        [Description( "276.4%" )]
        Fib_2376_4,
        [Description( "285.4%" )]
        Fib_2385_4,
        [Description( "290.2%" )]
        Fib_2390_2,
        [Description( "294.4%" )]
        Fib_2394_4,
        [Description( "298.4%" )]
        Fib_2398_4,

        [Description( "200.0%" )]
        Fib_2400,
        [Description( "205.6%" )]
        Fib_2405_6,
        [Description( "209.2%" )]
        Fib_2409_2,
        [Description( "214.6%" )]
        Fib_2414_6,
        [Description( "223.6%" )]
        Fib_2423_6,
        [Description( "227.2%" )]
        Fib_2427_2,
        [Description( "238.2%" )]
        Fib_2438_2,
        [Description( "241.4%" )]
        Fib_2441_4,
        [Description( "242.7%" )]
        Fib_2442_7,
        [Description( "250.0%" )]
        Fib_2450,
        [Description( "261.8%" )]
        Fib_2461_8,
        [Description( "266.7%" )]
        Fib_2466_7,
        [Description( "276.4%" )]
        Fib_2476_4,
        [Description( "285.4%" )]
        Fib_2485_4,
        [Description( "290.2%" )]
        Fib_2490_2,
        [Description( "294.4%" )]
        Fib_2494_4,
        [Description( "298.4%" )]
        Fib_2498_4,

        [Description( "200.0%" )]
        Fib_2500,
        [Description( "205.6%" )]
        Fib_2505_6,
        [Description( "209.2%" )]
        Fib_2509_2,
        [Description( "214.6%" )]
        Fib_2514_6,
        [Description( "223.6%" )]
        Fib_2523_6,
        [Description( "227.2%" )]
        Fib_2527_2,
        [Description( "238.2%" )]
        Fib_2538_2,
        [Description( "241.4%" )]
        Fib_2541_4,
        [Description( "242.7%" )]
        Fib_2542_7,
        [Description( "250.0%" )]
        Fib_2550,
        [Description( "261.8%" )]
        Fib_2561_8,
        [Description( "266.7%" )]
        Fib_2566_7,
        [Description( "276.4%" )]
        Fib_2576_4,
        [Description( "285.4%" )]
        Fib_2585_4,
        [Description( "290.2%" )]
        Fib_2590_2,
        [Description( "294.4%" )]
        Fib_2594_4,
        [Description( "298.4%" )]
        Fib_2598_4,


        [Description( "200.0%" )]
        Fib_2600,
        [Description( "205.6%" )]
        Fib_2605_6,
        [Description( "209.2%" )]
        Fib_2609_2,
        [Description( "214.6%" )]
        Fib_2614_6,
        [Description( "223.6%" )]
        Fib_2623_6,
        [Description( "227.2%" )]
        Fib_2627_2,
        [Description( "238.2%" )]
        Fib_2638_2,
        [Description( "241.4%" )]
        Fib_2641_4,
        [Description( "242.7%" )]
        Fib_2642_7,
        [Description( "250.0%" )]
        Fib_2650,
        [Description( "261.8%" )]
        Fib_2661_8,
        [Description( "266.7%" )]
        Fib_2666_7,
        [Description( "276.4%" )]
        Fib_2676_4,
        [Description( "285.4%" )]
        Fib_2685_4,
        [Description( "290.2%" )]
        Fib_2690_2,
        [Description( "294.4%" )]
        Fib_2694_4,
        [Description( "298.4%" )]
        Fib_2698_4,



        [Description( "200.0%" )]
        Fib_2700,
        [Description( "205.6%" )]
        Fib_2705_6,
        [Description( "209.2%" )]
        Fib_2709_2,
        [Description( "214.6%" )]
        Fib_2714_6,
        [Description( "223.6%" )]
        Fib_2723_6,
        [Description( "227.2%" )]
        Fib_2727_2,
        [Description( "238.2%" )]
        Fib_2738_2,
        [Description( "241.4%" )]
        Fib_2741_4,
        [Description( "242.7%" )]
        Fib_2742_7,
        [Description( "250.0%" )]
        Fib_2750,
        [Description( "261.8%" )]
        Fib_2761_8,
        [Description( "266.7%" )]
        Fib_2766_7,
        [Description( "276.4%" )]
        Fib_2776_4,
        [Description( "285.4%" )]
        Fib_2785_4,
        [Description( "290.2%" )]
        Fib_2790_2,
        [Description( "294.4%" )]
        Fib_2794_4,
        [Description( "298.4%" )]
        Fib_2798_4,

        [Description( "200.0%" )]
        Fib_2800,
        [Description( "205.6%" )]
        Fib_2805_6,
        [Description( "209.2%" )]
        Fib_2809_2,
        [Description( "214.6%" )]
        Fib_2814_6,
        [Description( "223.6%" )]
        Fib_2823_6,
        [Description( "227.2%" )]
        Fib_2827_2,
        [Description( "238.2%" )]
        Fib_2838_2,
        [Description( "241.4%" )]
        Fib_2841_4,
        [Description( "242.7%" )]
        Fib_2842_7,
        [Description( "250.0%" )]
        Fib_2850,
        [Description( "261.8%" )]
        Fib_2861_8,
        [Description( "266.7%" )]
        Fib_2866_7,
        [Description( "276.4%" )]
        Fib_2876_4,
        [Description( "285.4%" )]
        Fib_2885_4,
        [Description( "290.2%" )]
        Fib_2890_2,
        [Description( "294.4%" )]
        Fib_2894_4,
        [Description( "298.4%" )]
        Fib_2898_4,

        [Description( "200.0%" )]
        Fib_2900,
        [Description( "205.6%" )]
        Fib_2905_6,
        [Description( "209.2%" )]
        Fib_2909_2,
        [Description( "214.6%" )]
        Fib_2914_6,
        [Description( "223.6%" )]
        Fib_2923_6,
        [Description( "227.2%" )]
        Fib_2927_2,
        [Description( "238.2%" )]
        Fib_2938_2,
        [Description( "241.4%" )]
        Fib_2941_4,
        [Description( "242.7%" )]
        Fib_2942_7,
        [Description( "250.0%" )]
        Fib_2950,
        [Description( "261.8%" )]
        Fib_2961_8,
        [Description( "266.7%" )]
        Fib_2966_7,
        [Description( "276.4%" )]
        Fib_2976_4,
        [Description( "285.4%" )]
        Fib_2985_4,
        [Description( "290.2%" )]
        Fib_2990_2,
        [Description( "294.4%" )]
        Fib_2994_4,
        [Description( "298.4%" )]
        Fib_2998_4,


        [Description( "200.0%" )]
        Fib_3000,
        [Description( "205.6%" )]
        Fib_3005_6,
        [Description( "209.2%" )]
        Fib_3009_2,
        [Description( "214.6%" )]
        Fib_3014_6,
        [Description( "223.6%" )]
        Fib_3023_6,
        [Description( "227.2%" )]
        Fib_3027_2,
        [Description( "238.2%" )]
        Fib_3038_2,
        [Description( "241.4%" )]
        Fib_3041_4,
        [Description( "242.7%" )]
        Fib_3042_7,
        [Description( "250.0%" )]
        Fib_3050,
        [Description( "261.8%" )]
        Fib_3061_8,
        [Description( "266.7%" )]
        Fib_3066_7,
        [Description( "276.4%" )]
        Fib_3076_4,
        [Description( "285.4%" )]
        Fib_3085_4,
        [Description( "290.2%" )]
        Fib_3090_2,
        [Description( "294.4%" )]
        Fib_3094_4,
        [Description( "298.4%" )]
        Fib_3098_4,

        [Description( "100%" )]
        Fib_3100,
        [Description( "105.6%" )]
        Fib_3105_6,
        [Description( "109.2%" )]
        Fib_3109_2,
        [Description( "114.6%" )]
        Fib_3114_6,
        [Description( "123.6%" )]
        Fib_3123_6,
        [Description( "127.2%" )]
        Fib_3127_2,
        [Description( "133.3%" )]
        Fib_3133_3,
        [Description( "138.2%" )]
        Fib_3138_2,
        [Description( "141.4%" )]
        Fib_3141_4,
        [Description( "150%" )]
        Fib_3150,
        [Description( "161.8%" )]
        Fib_3161_8,
        [Description( "166.7%" )]
        Fib_3166_7,
        [Description( "176.4%" )]
        Fib_3176_4,
        [Description( "185.4%" )]
        Fib_3185_4,
        [Description( "190.2%" )]
        Fib_3190_2,
        [Description( "194.4%" )]
        Fib_3194_4,
        [Description( "198.4%" )]
        Fib_3198_4,
        [Description( "200.0%" )]
        Fib_3200,
        [Description( "205.6%" )]
        Fib_3205_6,
        [Description( "209.2%" )]
        Fib_3209_2,
        [Description( "214.6%" )]
        Fib_3214_6,
        [Description( "223.6%" )]
        Fib_3223_6,
        [Description( "227.2%" )]
        Fib_3227_2,
        [Description( "238.2%" )]
        Fib_3238_2,
        [Description( "241.4%" )]
        Fib_3241_4,
        [Description( "242.7%" )]
        Fib_3242_7,
        [Description( "250.0%" )]
        Fib_3250,
        [Description( "261.8%" )]
        Fib_3261_8,
        [Description( "266.7%" )]
        Fib_3266_7,
        [Description( "276.4%" )]
        Fib_3276_4,
        [Description( "285.4%" )]
        Fib_3285_4,
        [Description( "290.2%" )]
        Fib_3290_2,
        [Description( "294.4%" )]
        Fib_3294_4,
        [Description( "298.4%" )]
        Fib_3298_4,



        [Description( "200.0%" )]
        Fib_3300,
        [Description( "205.6%" )]
        Fib_3305_6,
        [Description( "209.2%" )]
        Fib_3309_2,
        [Description( "214.6%" )]
        Fib_3314_6,
        [Description( "223.6%" )]
        Fib_3323_6,
        [Description( "227.2%" )]
        Fib_3327_2,
        [Description( "238.2%" )]
        Fib_3338_2,
        [Description( "241.4%" )]
        Fib_3341_4,
        [Description( "242.7%" )]
        Fib_3342_7,
        [Description( "250.0%" )]
        Fib_3350,
        [Description( "261.8%" )]
        Fib_3361_8,
        [Description( "266.7%" )]
        Fib_3366_7,
        [Description( "276.4%" )]
        Fib_3376_4,
        [Description( "285.4%" )]
        Fib_3385_4,
        [Description( "290.2%" )]
        Fib_3390_2,
        [Description( "294.4%" )]
        Fib_3394_4,
        [Description( "298.4%" )]
        Fib_3398_4,

        [Description( "200.0%" )]
        Fib_3400,
        [Description( "205.6%" )]
        Fib_3405_6,
        [Description( "209.2%" )]
        Fib_3409_2,
        [Description( "214.6%" )]
        Fib_3414_6,
        [Description( "223.6%" )]
        Fib_3423_6,
        [Description( "227.2%" )]
        Fib_3427_2,
        [Description( "238.2%" )]
        Fib_3438_2,
        [Description( "241.4%" )]
        Fib_3441_4,
        [Description( "242.7%" )]
        Fib_3442_7,
        [Description( "250.0%" )]
        Fib_3450,
        [Description( "261.8%" )]
        Fib_3461_8,
        [Description( "266.7%" )]
        Fib_3466_7,
        [Description( "276.4%" )]
        Fib_3476_4,
        [Description( "285.4%" )]
        Fib_3485_4,
        [Description( "290.2%" )]
        Fib_3490_2,
        [Description( "294.4%" )]
        Fib_3494_4,
        [Description( "298.4%" )]
        Fib_3498_4,

        [Description( "200.0%" )]
        Fib_3500,
        [Description( "205.6%" )]
        Fib_3505_6,
        [Description( "209.2%" )]
        Fib_3509_2,
        [Description( "214.6%" )]
        Fib_3514_6,
        [Description( "223.6%" )]
        Fib_3523_6,
        [Description( "227.2%" )]
        Fib_3527_2,
        [Description( "238.2%" )]
        Fib_3538_2,
        [Description( "241.4%" )]
        Fib_3541_4,
        [Description( "242.7%" )]
        Fib_3542_7,
        [Description( "250.0%" )]
        Fib_3550,
        [Description( "261.8%" )]
        Fib_3561_8,
        [Description( "266.7%" )]
        Fib_3566_7,
        [Description( "276.4%" )]
        Fib_3576_4,
        [Description( "285.4%" )]
        Fib_3585_4,
        [Description( "290.2%" )]
        Fib_3590_2,
        [Description( "294.4%" )]
        Fib_3594_4,
        [Description( "298.4%" )]
        Fib_3598_4,


        [Description( "200.0%" )]
        Fib_3600,
        [Description( "205.6%" )]
        Fib_3605_6,
        [Description( "209.2%" )]
        Fib_3609_2,
        [Description( "214.6%" )]
        Fib_3614_6,
        [Description( "223.6%" )]
        Fib_3623_6,
        [Description( "227.2%" )]
        Fib_3627_2,
        [Description( "238.2%" )]
        Fib_3638_2,
        [Description( "241.4%" )]
        Fib_3641_4,
        [Description( "242.7%" )]
        Fib_3642_7,
        [Description( "250.0%" )]
        Fib_3650,
        [Description( "261.8%" )]
        Fib_3661_8,
        [Description( "266.7%" )]
        Fib_3666_7,
        [Description( "276.4%" )]
        Fib_3676_4,
        [Description( "285.4%" )]
        Fib_3685_4,
        [Description( "290.2%" )]
        Fib_3690_2,
        [Description( "294.4%" )]
        Fib_3694_4,
        [Description( "298.4%" )]
        Fib_3698_4,



        [Description( "200.0%" )]
        Fib_3700,
        [Description( "205.6%" )]
        Fib_3705_6,
        [Description( "209.2%" )]
        Fib_3709_2,
        [Description( "214.6%" )]
        Fib_3714_6,
        [Description( "223.6%" )]
        Fib_3723_6,
        [Description( "227.2%" )]
        Fib_3727_2,
        [Description( "238.2%" )]
        Fib_3738_2,
        [Description( "241.4%" )]
        Fib_3741_4,
        [Description( "242.7%" )]
        Fib_3742_7,
        [Description( "250.0%" )]
        Fib_3750,
        [Description( "261.8%" )]
        Fib_3761_8,
        [Description( "266.7%" )]
        Fib_3766_7,
        [Description( "276.4%" )]
        Fib_3776_4,
        [Description( "285.4%" )]
        Fib_3785_4,
        [Description( "290.2%" )]
        Fib_3790_2,
        [Description( "294.4%" )]
        Fib_3794_4,
        [Description( "298.4%" )]
        Fib_3798_4,

        [Description( "200.0%" )]
        Fib_3800,
        [Description( "205.6%" )]
        Fib_3805_6,
        [Description( "209.2%" )]
        Fib_3809_2,
        [Description( "214.6%" )]
        Fib_3814_6,
        [Description( "223.6%" )]
        Fib_3823_6,
        [Description( "227.2%" )]
        Fib_3827_2,
        [Description( "238.2%" )]
        Fib_3838_2,
        [Description( "241.4%" )]
        Fib_3841_4,
        [Description( "242.7%" )]
        Fib_3842_7,
        [Description( "250.0%" )]
        Fib_3850,
        [Description( "261.8%" )]
        Fib_3861_8,
        [Description( "266.7%" )]
        Fib_3866_7,
        [Description( "276.4%" )]
        Fib_3876_4,
        [Description( "285.4%" )]
        Fib_3885_4,
        [Description( "290.2%" )]
        Fib_3890_2,
        [Description( "294.4%" )]
        Fib_3894_4,
        [Description( "298.4%" )]
        Fib_3898_4,

        [Description( "200.0%" )]
        Fib_3900,
        [Description( "205.6%" )]
        Fib_3905_6,
        [Description( "209.2%" )]
        Fib_3909_2,
        [Description( "214.6%" )]
        Fib_3914_6,
        [Description( "223.6%" )]
        Fib_3923_6,
        [Description( "227.2%" )]
        Fib_3927_2,
        [Description( "238.2%" )]
        Fib_3938_2,
        [Description( "241.4%" )]
        Fib_3941_4,
        [Description( "242.7%" )]
        Fib_3942_7,
        [Description( "250.0%" )]
        Fib_3950,
        [Description( "261.8%" )]
        Fib_3961_8,
        [Description( "266.7%" )]
        Fib_3966_7,
        [Description( "276.4%" )]
        Fib_3976_4,
        [Description( "285.4%" )]
        Fib_3985_4,
        [Description( "290.2%" )]
        Fib_3990_2,
        [Description( "294.4%" )]
        Fib_3994_4,
        [Description( "298.4%" )]
        Fib_3998_4,
    }

    public static class GlobalConstants
    {
        public const int TICKS_TO_DOWNLOAD               = 25000;
        public const int TICKS_BARS_PER_DAY              = 86400;
        //public const int ONE_MINUTES_BARS_PER_MONTH      = 6600 * 5;        
        public const int ONE_MINUTES_BARS_PER_MONTH      = 3000;
        public const int FIVE_MINUTES_BARS_PER_MONTH     = 6600;
        public const int FIFTEEN_MINUTES_BARS_PER_WEEK   = 490;
        public const int FIFTEEN_MINUTES_BARS_PER_MONTH  = 2940;
        public const int HOURLY_BARS_QUATERLY            = 10000;
        public const int HOURLY_BARS_PER_WEEK            = 125;
        public const int DAILY_BARS_50YEARS              = 3650 * 5;
        public const int DAILY_BARS_PER_WEEK             = 6;
        public const int WEEKLY_BARS_30YEARS             = 1600;
        public const int MONTHLY_BARS_30YEARS            = 370;
        public const int ONE_MINUTES_BARS_PER_DAY        = 1440;
        public const int FIVE_MINUTES_BARS_PER_DAY       = 288;
        public const int FIFTEEN_MINUTES_BARS_PER_DAY    = 96;
        public const int HOUR_BARS_PER_DAY               = 24;
        public const int OneWaveCycle                    = 1;
        public const int HewBitsMask                     = 0x3F;
        public const int NewHewBitsMask                  = 0x3FF;
        public const int NewHewContentMask               = 0x1F;
        public const int NewHewCycleMask                 = 0x1E0;
        public const int NewHewLabelMask                 = 0x200;
        public const int NewHewBits                      = 10;
        public const int WaveLabelBit                    = 1;
        public const int WaveCountBit                    = 5;
        public const int CycleBits                       = 4;
        public const int MaxCountPerLong                 = 6;
        public const int MaxCountPerInt                  = 3;
        public const int SecondHalf                      = 0xF;
        public const int MaxAllowedMisses                = 50;
        public const double TIMEOFAYEAR                  = 365.242242;
        public const double GANN_DAILY_DEGREE            = 0.9856472;

        public const int MINS05IMPT = 5;
        public const int MINS15IMPT = 15;
        public const int MINS30IMPT = 30;
        public const int HRS01IMPT = 60;
        public const int HRS02IMPT = 120;
        public const int HRS04IMPT = 240;
        public const int HRS08IMPT = 480;
        public const int DAILYIMPT = 1440;
        public const int WEEKLYIMPT = 7200;
        public const int MONTHLYIMPT = 43200;

        public const char UpTrend                        = '\u2B9D';
        public const char Degree = '\u00B0';
        public const char DownTrend                      = '\u2B9F';
        public const char UpTrendArrow            = '\u2B08';
        public const char UpTrendRetracement             = '\u2BAF';
        //char UpTrendRetracement                        = '\u2B02';
        public const char DownTrendArrow          = '\u2B0A';
        public const char DownTrendRetracement           = '\u2BAD';
        //char DownTrendRetracement                      = '\u2B00';

        public const int FIFTEEN_MINUTES_BARS_DOWNLOAD = FIFTEEN_MINUTES_BARS_PER_MONTH * 3 + FIFTEEN_MINUTES_BARS_PER_WEEK + 1;
       


        #region Retracement of Waves
        public static readonly float [ ] Wave2RetracementLevels = new float [ ]
        {
                                                                9.02f,
                                                                14.6f,
                                                                23.6f,
                                                                33.3f,
                                                                38.2f,
                                                                41.4f,
                                                                50f,
                                                                58.6f,
                                                                61.8f,
                                                                66.66f,
                                                                76.40f,
                                                                85.40f,
                                                                90.02f,
                                                                98.68f
        };

        public static readonly FibPercentage [ ] Wave2RetracementFibLevelType = new FibPercentage [ ]
        {
                                                                FibPercentage.Fib_9_02,
                                                                FibPercentage.Fib_14_6,
                                                                FibPercentage.Fib_23_6,
                                                                FibPercentage.Fib_33_3,
                                                                FibPercentage.Fib_38_2,
                                                                FibPercentage.Fib_41_4,
                                                                FibPercentage.Fib_50,
                                                                FibPercentage.Fib_58_6,
                                                                FibPercentage.Fib_61_8,
                                                                FibPercentage.Fib_66_66,
                                                                FibPercentage.Fib_76_4,
                                                                FibPercentage.Fib_85_4,
                                                                FibPercentage.Fib_90_02,
                                                                FibPercentage.Fib_98_68
        };



        public static readonly int[ ] Wave2RetracementStrength = new int[ ]
        {
                                                                5,      // Fib_9_02
                                                                10,      // Fib_14_6
                                                                10,     // Fib_23_6
                                                                15,
                                                                15,
                                                                20,
                                                                30,
                                                                30,
                                                                30,
                                                                30,
                                                                40,
                                                                30,
                                                                10,
                                                                5
        };

        

        //public static readonly SolidColorBrush BaseColor   = new SolidColorBrush( Color.FromArgb( byte.MaxValue, 119, 119, 135 ) );
        //public static readonly SolidColorBrush Impt0Color  = new SolidColorBrush( Colors.LightGray );
        //public static readonly SolidColorBrush Impt10Color = new SolidColorBrush( Colors.Blue );
        //public static readonly SolidColorBrush Impt20Color = new SolidColorBrush( Colors.Red );
        //
        

        public static readonly float [ ] Wave4RetracementLevels = new float [ ]
        {
                                                                9.02f,
                                                                14.60f,
                                                                23.60f,
                                                                33.30f,
                                                                38.20f,
                                                                41.40f,
                                                                44.10f,
                                                                50.00f,
                                                                58.60f,
                                                                61.80f,
                                                                66.66f,
                                                                76.40f,
        };

        public static readonly FibPercentage [ ] Wave4RetracementFibLevelType = new FibPercentage [ ]
        {
                                                                FibPercentage.Fib_9_02,
                                                                FibPercentage.Fib_14_6,
                                                                FibPercentage.Fib_23_6,
                                                                FibPercentage.Fib_33_3,
                                                                FibPercentage.Fib_38_2,
                                                                FibPercentage.Fib_41_4,
                                                                FibPercentage.Fib_44_1,
                                                                FibPercentage.Fib_50,
                                                                FibPercentage.Fib_58_6,
                                                                FibPercentage.Fib_61_8,
                                                                FibPercentage.Fib_66_66,
                                                                FibPercentage.Fib_76_4
        };

        public static readonly int[ ] Wave4RetracementStrength = new int[ ]
        {
                                                                5,
                                                                5,
                                                                10,
                                                                10,
                                                                20,
                                                                30,
                                                                30,
                                                                30,
                                                                20,
                                                                10,
                                                                10,
                                                                5
        };

       

        




        #endregion

        #region Projection of Waves
        public static readonly float[ ] TonyDiscoveryLevels = new float[ ]
        {
                                                                109.20f,                                                                
                                                                127.20f,
                                                                150.00f,
                                                                161.80f,
                                                                200.00f,
                                                                209.20f,
                                                                214.60f,
                                                                227.20f,
                                                                250.00f,
                                                                261.80f,
                                                                
                                                                276.40f,
                                                                285.40f,
                                                                290.00f,
                                                                294.40f,
                                                                298.40f,

                                                                300.00f,
                                                                327.20f,
                                                                350.00f,
                                                                361.80f,
                                                                400.00f,
                                                                427.20f,
                                                                450.00f,
                                                                461.80f,
                                                                500.00f,
                                                                527.20f,
                                                                550.00f,
                                                                561.80f,
                                                                600.00f,
                                                                627.20f,
                                                                650.00f,
                                                                661.80f,
                                                                700.00f,
                                                                727.20f,
                                                                750.00f,
                                                                761.80f,
                                                                800.00f,
                                                                827.20f,
                                                                850.00f,
                                                                861.80f,
                                                                900.00f,
                                                                927.20f,
                                                                950.00f,
                                                                961.80f,
        };

        public static readonly FibPercentage [ ] TonyDiscoveryLevelType = new FibPercentage [ ]
        {
                                                                FibPercentage.Fib_109_2,
                                                                FibPercentage.Fib_127_2,
                                                                FibPercentage.Fib_150,
                                                                FibPercentage.Fib_161_8,
                                                                FibPercentage.Fib_200,
                                                                FibPercentage.Fib_209_2,
                                                                FibPercentage.Fib_214_6,
                                                                FibPercentage.Fib_227_2,
                                                                FibPercentage.Fib_250,
                                                                FibPercentage.Fib_261_8,

                                                                FibPercentage.Fib_276_4,
                                                                FibPercentage.Fib_285_4,
                                                                FibPercentage.Fib_290_2,
                                                                FibPercentage.Fib_294_4,
                                                                FibPercentage.Fib_298_4,

                                                                FibPercentage.Fib_300,
                                                                FibPercentage.Fib_327_2,
                                                                FibPercentage.Fib_350,
                                                                FibPercentage.Fib_361_8,
                                                                FibPercentage.Fib_400,
                                                                FibPercentage.Fib_427_2,
                                                                FibPercentage.Fib_450,
                                                                FibPercentage.Fib_461_8,
                                                                FibPercentage.Fib_500,
                                                                FibPercentage.Fib_527_2,
                                                                FibPercentage.Fib_550,
                                                                FibPercentage.Fib_561_8,
                                                                FibPercentage.Fib_600,
                                                                FibPercentage.Fib_627_2,
                                                                FibPercentage.Fib_650,
                                                                FibPercentage.Fib_661_8,
                                                                FibPercentage.Fib_700,
                                                                FibPercentage.Fib_727_2,
                                                                FibPercentage.Fib_750,
                                                                FibPercentage.Fib_761_8,
                                                                FibPercentage.Fib_800,
                                                                FibPercentage.Fib_827_2,
                                                                FibPercentage.Fib_850,
                                                                FibPercentage.Fib_861_8,
                                                                FibPercentage.Fib_900,
                                                                FibPercentage.Fib_927_2,
                                                                FibPercentage.Fib_950,
                                                                FibPercentage.Fib_961_8,
        };

        public static readonly int [ ] TonyDiscoveryLevelsStrength = new int [ ]
        {
                                                                25,     // Fib_109_2
                                                                35,     // Fib_127_2,
                                                                25,     // Fib_150
                                                                35,     // Fib_161_8
                                                                35,     // Fib_200
                                                                5,      // Fib_209_2
                                                                5,      // Fib_214_6
                                                                35,     // Fib_227_2
                                                                25,     // Fib_250
                                                                35,     // Fib_261_8
                                                                5,      //FibPercentage.Fib_276_4,
                                                                5,      //FibPercentage.Fib_285_4,
                                                                5,      //FibPercentage.Fib_290,
                                                                5,      // FibPercentage.Fib_294_4,
                                                                5,      // FibPercentage.Fib_298_4,

                                                                5,
                                                                5,
                                                                5,
                                                                5,
                                                                5,
                                                                5,
                                                                5,
                                                                5,
                                                                5,
                                                                5,
                                                                5,
                                                                5,
                                                                5,
                                                                5,
                                                                5,
                                                                5,
                                                                5,
                                                                5,
                                                                5,
                                                                5,
                                                                5,
                                                                5,
                                                                5,
                                                                5,
                                                                5,
                                                                5,
                                                                5,
                                                                5
        };


        public static readonly float[ ] Wave3ProjectionLevels = new float[ ]
        {
                                                                50f,            // Wave 3B Retracement Level
                                                                61.8f,          // Wave 3B Retracement Level
                                                                66.7f,          // Wave 3B Retracement Level
                                                                76.4f,          // Wave 3B Retracement Level
                                                                90.02f,
                                                                94.4f,
                                                                98.68f,
                                                                
                                                                100.0f,
                                                                105.6f,
                                                                109.20f,
                                                                114.6f,
                                                                123.60f,
                                                                138.20f,
                                                                141.40f,
                                                                150.00f,
                                                                161.80f,
                                                                166.70f,
                                                                176.40f,        // Projection Cluster
                                                                185.40f,
                                                                190.20f,
                                                                198.40f,

                                                                200.0f,
                                                                205.6f,
                                                                209.20f,
                                                                214.6f,
                                                                223.60f,
                                                                238.20f,
                                                                241.40f,
                                                                250.00f,
                                                                261.80f,
                                                                266.70f,
                                                                276.40f,        // Projection Cluster
                                                                285.40f,
                                                                290.20f,
                                                                298.40f,

                                                                300.0f,
                                                                305.6f,
                                                                309.20f,
                                                                314.6f,
                                                                323.60f,
                                                                338.20f,
                                                                341.40f,
                                                                350.00f,
                                                                361.80f,
                                                                366.70f,
                                                                376.40f,        // Projection Cluster
                                                                385.40f,
                                                                390.20f,
                                                                398.40f,

                                                                400.0f,
                                                                405.6f,
                                                                409.20f,
                                                                414.6f,
                                                                423.60f,
                                                                438.20f,
                                                                441.40f,
                                                                450.00f,
                                                                461.80f,
                                                                466.70f,
                                                                476.40f,        // Projection Cluster
                                                                485.40f,
                                                                490.20f,
                                                                498.40f,

                                                                500.0f,
                                                                505.6f,
                                                                509.20f,
                                                                514.6f,
                                                                523.60f,
                                                                538.20f,
                                                                541.40f,
                                                                550.00f,
                                                                561.80f,
                                                                566.70f,
                                                                576.40f,        // Projection Cluster
                                                                585.40f,
                                                                590.20f,
                                                                598.40f,

                                                                600.0f,
                                                                605.6f,
                                                                609.20f,
                                                                614.6f,
                                                                623.60f,
                                                                638.20f,
                                                                641.40f,
                                                                650.00f,
                                                                661.80f,
                                                                666.70f,
                                                                676.40f,        // Projection Cluster
                                                                685.40f,
                                                                690.20f,
                                                                698.40f,

                                                                700.0f,
                                                                705.6f,
                                                                709.20f,
                                                                714.6f,
                                                                723.60f,
                                                                738.20f,
                                                                741.40f,
                                                                750.00f,
                                                                761.80f,
                                                                766.70f,
                                                                776.40f,        // Projection Cluster
                                                                785.40f,
                                                                790.20f,
                                                                798.40f,

                                                                800.0f,
                                                                805.6f,
                                                                809.20f,
                                                                814.6f,
                                                                823.60f,
                                                                838.20f,
                                                                841.40f,
                                                                850.00f,
                                                                861.80f,
                                                                866.70f,
                                                                876.40f,        // Projection Cluster
                                                                885.40f,
                                                                890.20f,
                                                                898.40f,

                                                                900.0f,
                                                                905.6f,
                                                                909.20f,
                                                                914.6f,
                                                                923.60f,
                                                                938.20f,
                                                                941.40f,
                                                                950.00f,
                                                                961.80f,
                                                                966.70f,
                                                                976.40f,        // Projection Cluster
                                                                985.40f,
                                                                990.20f,
                                                                998.40f,

                                                                1000.0f,
                                                                1005.6f,
                                                                1009.20f,
                                                                1014.6f,
                                                                1023.60f,
                                                                1038.20f,
                                                                1041.40f,
                                                                1050.00f,
                                                                1061.80f,
                                                                1066.70f,
                                                                1076.40f,        // Projection Cluster
                                                                1085.40f,
                                                                1090.20f,
                                                                1098.40f,

                                                                1100.0f,
                                                                1105.6f,
                                                                1109.20f,
                                                                1114.6f,
                                                                1123.60f,
                                                                1138.20f,
                                                                1141.40f,
                                                                1150.00f,
                                                                1161.80f,
                                                                1166.70f,
                                                                1176.40f,        // Projection Cluster
                                                                1185.40f,
                                                                1190.20f,
                                                                1198.40f,

                                                                1200.0f,
                                                                1205.6f,
                                                                1209.20f,
                                                                1214.6f,
                                                                1223.60f,
                                                                1238.20f,
                                                                1241.40f,
                                                                1250.00f,
                                                                1261.80f,
                                                                1266.70f,
                                                                1276.40f,        // Projection Cluster
                                                                1285.40f,
                                                                1290.20f,
                                                                1298.40f,

                                                                1300.0f,
                                                                1305.6f,
                                                                1309.20f,
                                                                1314.6f,
                                                                1323.60f,
                                                                1338.20f,
                                                                1341.40f,
                                                                1350.00f,
                                                                1361.80f,
                                                                1366.70f,
                                                                1376.40f,        // Projection Cluster
                                                                1385.40f,
                                                                1390.20f,
                                                                1398.40f,

                                                                1400.0f,
                                                                1405.6f,
                                                                1409.20f,
                                                                1414.6f,
                                                                1423.60f,
                                                                1438.20f,
                                                                1441.40f,
                                                                1450.00f,
                                                                1461.80f,
                                                                1466.70f,
                                                                1476.40f,        // Projection Cluster
                                                                1485.40f,
                                                                1490.20f,
                                                                1498.40f,

                                                                1500.0f,
                                                                1505.6f,
                                                                1509.20f,
                                                                1514.6f,
                                                                1523.60f,
                                                                1538.20f,
                                                                1541.40f,
                                                                1550.00f,
                                                                1561.80f,
                                                                1566.70f,
                                                                1576.40f,        // Projection Cluster
                                                                1585.40f,
                                                                1590.20f,
                                                                1598.40f,

                                                                1600.0f,
                                                                1605.6f,
                                                                1609.20f,
                                                                1614.6f,
                                                                1623.60f,
                                                                1638.20f,
                                                                1641.40f,
                                                                1650.00f,
                                                                1661.80f,
                                                                1666.70f,
                                                                1676.40f,        // Projection Cluster
                                                                1685.40f,
                                                                1690.20f,
                                                                1698.40f,

                                                                1700.0f,
                                                                1705.6f,
                                                                1709.20f,
                                                                1714.6f,
                                                                1723.60f,
                                                                1738.20f,
                                                                1741.40f,
                                                                1750.00f,
                                                                1761.80f,
                                                                1766.70f,
                                                                1776.40f,        // Projection Cluster
                                                                1785.40f,
                                                                1790.20f,
                                                                1798.40f,

                                                                1800.0f,
                                                                1805.6f,
                                                                1809.20f,
                                                                1814.6f,
                                                                1823.60f,
                                                                1838.20f,
                                                                1841.40f,
                                                                1850.00f,
                                                                1861.80f,
                                                                1866.70f,
                                                                1876.40f,        // Projection Cluster
                                                                1885.40f,
                                                                1890.20f,
                                                                1898.40f,

                                                                1900.0f,
                                                                1905.6f,
                                                                1909.20f,
                                                                1914.6f,
                                                                1923.60f,
                                                                1938.20f,
                                                                1941.40f,
                                                                1950.00f,
                                                                1961.80f,
                                                                1966.70f,
                                                                1976.40f,        // Projection Cluster
                                                                1985.40f,
                                                                1990.20f,
                                                                1998.40f,

                                                                2000.0f,
                                                                2005.6f,
                                                                2009.20f,
                                                                2014.6f,
                                                                2023.60f,
                                                                2038.20f,
                                                                2041.40f,
                                                                2050.00f,
                                                                2061.80f,
                                                                2066.70f,
                                                                2076.40f,        // Projection Cluster
                                                                2085.40f,
                                                                2090.20f,
                                                                2098.40f,

                                                                2100.0f,
                                                                2105.6f,
                                                                2109.20f,
                                                                2114.6f,
                                                                2123.60f,
                                                                2138.20f,
                                                                2141.40f,
                                                                2150.00f,
                                                                2161.80f,
                                                                2166.70f,
                                                                2176.40f,        // Projection Cluster
                                                                2185.40f,
                                                                2190.20f,
                                                                2198.40f,

                                                                2200.0f,
                                                                2205.6f,
                                                                2209.20f,
                                                                2214.6f,
                                                                2223.60f,
                                                                2238.20f,
                                                                2241.40f,
                                                                2250.00f,
                                                                2261.80f,
                                                                2266.70f,
                                                                2276.40f,        // Projection Cluster
                                                                2285.40f,
                                                                2290.20f,
                                                                2298.40f,

                                                                2300.0f,
                                                                2305.6f,
                                                                2309.20f,
                                                                2314.6f,
                                                                2323.60f,
                                                                2338.20f,
                                                                2341.40f,
                                                                2350.00f,
                                                                2361.80f,
                                                                2366.70f,
                                                                2376.40f,        // Projection Cluster
                                                                2385.40f,
                                                                2390.20f,
                                                                2398.40f,

                                                                2400.0f,
                                                                2405.6f,
                                                                2409.20f,
                                                                2414.6f,
                                                                2423.60f,
                                                                2438.20f,
                                                                2441.40f,
                                                                2450.00f,
                                                                2461.80f,
                                                                2466.70f,
                                                                2476.40f,        // Projection Cluster
                                                                2485.40f,
                                                                2490.20f,
                                                                2498.40f,

                                                                2500.0f,
                                                                2505.6f,
                                                                2509.20f,
                                                                2514.6f,
                                                                2523.60f,
                                                                2538.20f,
                                                                2541.40f,
                                                                2550.00f,
                                                                2561.80f,
                                                                2566.70f,
                                                                2576.40f,        // Projection Cluster
                                                                2585.40f,
                                                                2590.20f,
                                                                2598.40f,

                                                                2600.0f,
                                                                2605.6f,
                                                                2609.20f,
                                                                2614.6f,
                                                                2623.60f,
                                                                2638.20f,
                                                                2641.40f,
                                                                2650.00f,
                                                                2661.80f,
                                                                2666.70f,
                                                                2676.40f,        // Projection Cluster
                                                                2685.40f,
                                                                2690.20f,
                                                                2698.40f,

                                                                2700.0f,
                                                                2705.6f,
                                                                2709.20f,
                                                                2714.6f,
                                                                2723.60f,
                                                                2738.20f,
                                                                2741.40f,
                                                                2750.00f,
                                                                2761.80f,
                                                                2766.70f,
                                                                2776.40f,        // Projection Cluster
                                                                2785.40f,
                                                                2790.20f,
                                                                2798.40f,

                                                                2800.0f,
                                                                2805.6f,
                                                                2809.20f,
                                                                2814.6f,
                                                                2823.60f,
                                                                2838.20f,
                                                                2841.40f,
                                                                2850.00f,
                                                                2861.80f,
                                                                2866.70f,
                                                                2876.40f,        // Projection Cluster
                                                                2885.40f,
                                                                2890.20f,
                                                                2898.40f,

                                                                2900.0f,
                                                                2905.6f,
                                                                2909.20f,
                                                                2914.6f,
                                                                2923.60f,
                                                                2938.20f,
                                                                2941.40f,
                                                                2950.00f,
                                                                2961.80f,
                                                                2966.70f,
                                                                2976.40f,        // Projection Cluster
                                                                2985.40f,
                                                                2990.20f,
                                                                2998.40f,

                                                                3000.0f,
                                                                3005.6f,
                                                                3009.20f,
                                                                3014.6f,
                                                                3023.60f,
                                                                3038.20f,
                                                                3041.40f,
                                                                3050.00f,
                                                                3061.80f,
                                                                3066.70f,
                                                                3076.40f,        // Projection Cluster
                                                                3085.40f,
                                                                3090.20f,
                                                                3098.40f,

                                                                3100.0f,
                                                                3105.6f,
                                                                3109.20f,
                                                                3114.6f,
                                                                3123.60f,
                                                                3138.20f,
                                                                3141.40f,
                                                                3150.00f,
                                                                3161.80f,
                                                                3166.70f,
                                                                3176.40f,        // Projection Cluster
                                                                3185.40f,
                                                                3190.20f,
                                                                3198.40f,

                                                                3200.0f,
                                                                3205.6f,
                                                                3209.20f,
                                                                3214.6f,
                                                                3223.60f,
                                                                3238.20f,
                                                                3241.40f,
                                                                3250.00f,
                                                                3261.80f,
                                                                3266.70f,
                                                                3276.40f,        // Projection Cluster
                                                                3285.40f,
                                                                3290.20f,
                                                                3298.40f,

                                                                3300.0f,
                                                                3305.6f,
                                                                3309.20f,
                                                                3314.6f,
                                                                3323.60f,
                                                                3338.20f,
                                                                3341.40f,
                                                                3350.00f,
                                                                3361.80f,
                                                                3366.70f,
                                                                3376.40f,        // Projection Cluster
                                                                3385.40f,
                                                                3390.20f,
                                                                3398.40f,

                                                                3400.0f,
                                                                3405.6f,
                                                                3409.20f,
                                                                3414.6f,
                                                                3423.60f,
                                                                3438.20f,
                                                                3441.40f,
                                                                3450.00f,
                                                                3461.80f,
                                                                3466.70f,
                                                                3476.40f,        // Projection Cluster
                                                                3485.40f,
                                                                3490.20f,
                                                                3498.40f,

                                                                3500.0f,
                                                                3505.6f,
                                                                3509.20f,
                                                                3514.6f,
                                                                3523.60f,
                                                                3538.20f,
                                                                3541.40f,
                                                                3550.00f,
                                                                3561.80f,
                                                                3566.70f,
                                                                3576.40f,        // Projection Cluster
                                                                3585.40f,
                                                                3590.20f,
                                                                3598.40f,

                                                                3600.0f,
                                                                3605.6f,
                                                                3609.20f,
                                                                3614.6f,
                                                                3623.60f,
                                                                3638.20f,
                                                                3641.40f,
                                                                3650.00f,
                                                                3661.80f,
                                                                3666.70f,
                                                                3676.40f,        // Projection Cluster
                                                                3685.40f,
                                                                3690.20f,
                                                                3698.40f,

                                                                3700.0f,
                                                                3705.6f,
                                                                3709.20f,
                                                                3714.6f,
                                                                3723.60f,
                                                                3738.20f,
                                                                3741.40f,
                                                                3750.00f,
                                                                3761.80f,
                                                                3766.70f,
                                                                3776.40f,        // Projection Cluster
                                                                3785.40f,
                                                                3790.20f,
                                                                3798.40f,

                                                                3800.0f,
                                                                3805.6f,
                                                                3809.20f,
                                                                3814.6f,
                                                                3823.60f,
                                                                3838.20f,
                                                                3841.40f,
                                                                3850.00f,
                                                                3861.80f,
                                                                3866.70f,
                                                                3876.40f,        // Projection Cluster
                                                                3885.40f,
                                                                3890.20f,
                                                                3898.40f,

                                                                3900.0f,
                                                                3905.6f,
                                                                3909.20f,
                                                                3914.6f,
                                                                3923.60f,
                                                                3938.20f,
                                                                3941.40f,
                                                                3950.00f,
                                                                3961.80f,
                                                                3966.70f,
                                                                3976.40f,        // Projection Cluster
                                                                3985.40f,
                                                                3990.20f,
                                                                3998.40f,

        };

        public static readonly int [ ] Wave3ProjectionStrength = new int [ ]
        {
                                                                0,                      // 50
                                                                0,                      // 61.8
                                                                0,                      // 66.7
                                                                0,                      // 76.4
                                                                10,                     //90.02f,
                                                                10,                     // 94.4f,
                                                                10,                     // 98.68f,


                                                                10,                     // 100.0f,
                                                                10,                     // 105.6f,
                                                                75,                     // 109.20
                                                                75,                     // 114.6
                                                                75,                     // 123.6
                                                                10,                     // 138.2
                                                                10,                     // 141.4
                                                                10,                     // 150.00
                                                                10,                     // 161.8
                                                                10,                     // 166.7
                                                                75,                     // 176.4
                                                                10,                     // 185.40
                                                                10,                     // 190.20
                                                                85,                     // 198.40

                                                                10,                     // 200.0f,
                                                                10,                     // 205.6f,
                                                                10,                     // 209.2f,
                                                                85,                     //214.60
                                                                85,                     // 223.60
                                                                10,                     // 238.20
                                                                85,                     // 241.40
                                                                10,                     // 250.0
                                                                10,                     // 261.80
                                                                10,                     // 266.7
                                                                10,                     // 276.40
                                                                85,                     // 285.40
                                                                10,                     // 290.00                                                                
                                                                10,                     // 298.40

                                                                10,                     // 300.0f,
                                                                10,                     // 305.6f,
                                                                75,                     // 309.20
                                                                75,                     // 314.6
                                                                75,                     // 323.6
                                                                10,                     // 338.2
                                                                10,                     // 341.4
                                                                10,                     // 350.00
                                                                10,                     // 361.8
                                                                10,                     // 366.7
                                                                75,                     // 376.4
                                                                10,                     // 385.40
                                                                10,                     // 390.20
                                                                85,                     // 398.40

                                                                10,                     // 400.0f,
                                                                10,                     // 405.6f,
                                                                10,                     // 409.2f,
                                                                85,                     // 414.60
                                                                85,                     // 423.60
                                                                10,                     // 438.20
                                                                85,                     // 441.40
                                                                10,                     // 450.0
                                                                10,                     // 461.80
                                                                10,                     // 466.7
                                                                10,                     // 476.40
                                                                85,                     // 485.40
                                                                10,                     // 490.00                                                                
                                                                10,                     // 498.40

                                                                10,                     // 500.0f,
                                                                10,                     // 505.6f,
                                                                75,                     // 509.20
                                                                75,                     // 514.6
                                                                75,                     // 523.6
                                                                10,                     // 538.2
                                                                10,                     // 541.4
                                                                10,                     // 550.00
                                                                10,                     // 561.8
                                                                10,                     // 566.7
                                                                75,                     // 576.4
                                                                10,                     // 585.40
                                                                10,                     // 590.20
                                                                85,                     // 598.40

                                                                10,                     // 600.0f,
                                                                10,                     // 605.6f,
                                                                10,                     // 609.2f,
                                                                85,                     // 614.60
                                                                85,                     // 623.60
                                                                10,                     // 638.20
                                                                85,                     // 641.40
                                                                10,                     // 650.0
                                                                10,                     // 661.80
                                                                10,                     // 666.7
                                                                10,                     // 676.40
                                                                85,                     // 685.40
                                                                10,                     // 690.00                                                                
                                                                10,                     // 698.40

                                                                10,                     // 700.0f,
                                                                10,                     // 705.6f,
                                                                75,                     // 709.20
                                                                75,                     // 714.6
                                                                75,                     // 723.6
                                                                10,                     // 738.2
                                                                10,                     // 741.4
                                                                10,                     // 750.00
                                                                10,                     // 761.8
                                                                10,                     // 766.7
                                                                75,                     // 776.4
                                                                10,                     // 785.40
                                                                10,                     // 790.20
                                                                85,                     // 798.40

                                                                10,                     // 800.0f,
                                                                10,                     // 805.6f,
                                                                10,                     // 809.2f,
                                                                85,                     // 814.60
                                                                85,                     // 823.60
                                                                10,                     // 838.20
                                                                85,                     // 841.40
                                                                10,                     // 850.0
                                                                10,                     // 861.80
                                                                10,                     // 866.7
                                                                10,                     // 876.40
                                                                85,                     // 885.40
                                                                10,                     // 890.00                                                                
                                                                10,                     // 898.40

                                                                10,                     // 900.0f,
                                                                10,                     // 905.6f,
                                                                75,                     // 909.20
                                                                75,                     // 914.6
                                                                75,                     // 923.6
                                                                10,                     // 938.2
                                                                10,                     // 941.4
                                                                10,                     // 950.00
                                                                10,                     // 961.8
                                                                10,                     // 966.7
                                                                75,                     // 976.4
                                                                10,                     // 985.40
                                                                10,                     // 990.20
                                                                85,                     // 998.40

                                                                10,                     // 1000.0f,
                                                                10,                     // 1005.6f,
                                                                75,                     // 1009.20
                                                                75,                     // 1014.6
                                                                75,                     // 1023.6
                                                                10,                     // 1038.2
                                                                10,                     // 1041.4
                                                                10,                     // 1050.00
                                                                10,                     // 1061.8
                                                                10,                     // 1066.7
                                                                75,                     // 1076.4
                                                                10,                     // 1085.40
                                                                10,                     // 1090.20
                                                                85,                     // 1098.40

                                                                10,                     // 1100.0f,
                                                                10,                     // 1105.6f,
                                                                75,                     // 1109.20
                                                                75,                     // 1114.6
                                                                75,                     // 1123.6
                                                                10,                     // 1138.2
                                                                10,                     // 1141.4
                                                                10,                     // 1150.00
                                                                10,                     // 1161.8
                                                                10,                     // 1166.7
                                                                75,                     // 1176.4
                                                                10,                     // 1185.40
                                                                10,                     // 1190.20
                                                                85,                     // 1198.40

                                                                10,                     // 1200.0f,
                                                                10,                     // 1205.6f,
                                                                10,                     // 1209.2f,
                                                                85,                     //214.60
                                                                85,                     // 1223.60
                                                                10,                     // 1238.20
                                                                85,                     // 1241.40
                                                                10,                     // 1250.0
                                                                10,                     // 1261.80
                                                                10,                     // 1266.7
                                                                10,                     // 1276.40
                                                                85,                     // 1285.40
                                                                10,                     // 1290.00                                                                
                                                                10,                     // 1298.40

                                                                10,                     // 1300.0f,
                                                                10,                     // 1305.6f,
                                                                75,                     // 1309.20
                                                                75,                     // 1314.6
                                                                75,                     // 1323.6
                                                                10,                     // 1338.2
                                                                10,                     // 1341.4
                                                                10,                     // 1350.00
                                                                10,                     // 1361.8
                                                                10,                     // 1366.7
                                                                75,                     // 1376.4
                                                                10,                     // 1385.40
                                                                10,                     // 1390.20
                                                                85,                     // 1398.40

                                                                10,                     // 1400.0f,
                                                                10,                     // 1405.6f,
                                                                10,                     // 1409.2f,
                                                                85,                     // 1414.60
                                                                85,                     // 1423.60
                                                                10,                     // 1438.20
                                                                85,                     // 1441.40
                                                                10,                     // 1450.0
                                                                10,                     // 1461.80
                                                                10,                     // 1466.7
                                                                10,                     // 1476.40
                                                                85,                     // 1485.40
                                                                10,                     // 1490.00                                                                
                                                                10,                     // 1498.40

                                                                10,                     // 1500.0f,
                                                                10,                     // 1505.6f,
                                                                75,                     // 1509.20
                                                                75,                     // 1514.6
                                                                75,                     // 1523.6
                                                                10,                     // 1538.2
                                                                10,                     // 1541.4
                                                                10,                     // 1550.00
                                                                10,                     // 1561.8
                                                                10,                     // 1566.7
                                                                75,                     // 1576.4
                                                                10,                     // 1585.40
                                                                10,                     // 1590.20
                                                                85,                     // 1598.40

                                                                10,                     // 1600.0f,
                                                                10,                     // 1605.6f,
                                                                10,                     // 1609.2f,
                                                                85,                     // 1614.60
                                                                85,                     // 1623.60
                                                                10,                     // 1638.20
                                                                85,                     // 1641.40
                                                                10,                     // 1650.0
                                                                10,                     // 1661.80
                                                                10,                     // 1666.7
                                                                10,                     // 1676.40
                                                                85,                     // 1685.40
                                                                10,                     // 1690.00                                                                
                                                                10,                     // 1698.40

                                                                10,                     // 1700.0f,
                                                                10,                     // 1705.6f,
                                                                75,                     // 1709.20
                                                                75,                     // 1714.6
                                                                75,                     // 1723.6
                                                                10,                     // 1738.2
                                                                10,                     // 1741.4
                                                                10,                     // 1750.00
                                                                10,                     // 1761.8
                                                                10,                     // 1766.7
                                                                75,                     // 1776.4
                                                                10,                     // 1785.40
                                                                10,                     // 1790.20
                                                                85,                     // 1798.40

                                                                10,                     // 1800.0f,
                                                                10,                     // 1805.6f,
                                                                10,                     // 1809.2f,
                                                                85,                     // 1814.60
                                                                85,                     // 1823.60
                                                                10,                     // 1838.20
                                                                85,                     // 1841.40
                                                                10,                     // 1850.0
                                                                10,                     // 1861.80
                                                                10,                     // 1866.7
                                                                10,                     // 1876.40
                                                                85,                     // 1885.40
                                                                10,                     // 1890.00                                                                
                                                                10,                     // 1898.40

                                                                10,                     // 1900.0f,
                                                                10,                     // 1905.6f,
                                                                75,                     // 1909.20
                                                                75,                     // 1914.6
                                                                75,                     // 1923.6
                                                                10,                     // 1938.2
                                                                10,                     // 1941.4
                                                                10,                     // 1950.00
                                                                10,                     // 1961.8
                                                                10,                     // 1966.7
                                                                75,                     // 1976.4
                                                                10,                     // 1985.40
                                                                10,                     // 1990.20
                                                                85,                     // 1998.40


                                                                10,                     // 2000.0f,
                                                                10,                     // 2005.6f,
                                                                75,                     // 2009.20
                                                                75,                     // 2014.6
                                                                75,                     // 2023.6
                                                                10,                     // 2038.2
                                                                10,                     // 2041.4
                                                                10,                     // 2050.00
                                                                10,                     // 2061.8
                                                                10,                     // 2066.7
                                                                75,                     // 2076.4
                                                                10,                     // 2085.40
                                                                10,                     // 2090.20
                                                                85,                     // 2098.40

                                                                10,                     // 2100.0f,
                                                                10,                     // 2105.6f,
                                                                75,                     // 2109.20
                                                                75,                     // 2114.6
                                                                75,                     // 2123.6
                                                                10,                     // 2138.2
                                                                10,                     // 2141.4
                                                                10,                     // 2150.00
                                                                10,                     // 2161.8
                                                                10,                     // 2166.7
                                                                75,                     // 2176.4
                                                                10,                     // 2185.40
                                                                10,                     // 2190.20
                                                                85,                     // 2198.40

                                                                10,                     // 2200.0f,
                                                                10,                     // 2205.6f,
                                                                10,                     // 2209.2f,
                                                                85,                     //214.60
                                                                85,                     // 2223.60
                                                                10,                     // 2238.20
                                                                85,                     // 2241.40
                                                                10,                     // 2250.0
                                                                10,                     // 2261.80
                                                                10,                     // 2266.7
                                                                10,                     // 2276.40
                                                                85,                     // 2285.40
                                                                10,                     // 2290.00                                                                
                                                                10,                     // 2298.40

                                                                10,                     // 2300.0f,
                                                                10,                     // 2305.6f,
                                                                75,                     // 2309.20
                                                                75,                     // 2314.6
                                                                75,                     // 2323.6
                                                                10,                     // 2338.2
                                                                10,                     // 2341.4
                                                                10,                     // 2350.00
                                                                10,                     // 2361.8
                                                                10,                     // 2366.7
                                                                75,                     // 2376.4
                                                                10,                     // 2385.40
                                                                10,                     // 2390.20
                                                                85,                     // 2398.40

                                                                10,                     // 2400.0f,
                                                                10,                     // 2405.6f,
                                                                10,                     // 2409.2f,
                                                                85,                     // 2414.60
                                                                85,                     // 2423.60
                                                                10,                     // 2438.20
                                                                85,                     // 2441.40
                                                                10,                     // 2450.0
                                                                10,                     // 2461.80
                                                                10,                     // 2466.7
                                                                10,                     // 2476.40
                                                                85,                     // 2485.40
                                                                10,                     // 2490.00                                                                
                                                                10,                     // 2498.40

                                                                10,                     // 2500.0f,
                                                                10,                     // 2505.6f,
                                                                75,                     // 2509.20
                                                                75,                     // 2514.6
                                                                75,                     // 2523.6
                                                                10,                     // 2538.2
                                                                10,                     // 2541.4
                                                                10,                     // 2550.00
                                                                10,                     // 2561.8
                                                                10,                     // 2566.7
                                                                75,                     // 2576.4
                                                                10,                     // 2585.40
                                                                10,                     // 2590.20
                                                                85,                     // 2598.40

                                                                10,                     // 2600.0f,
                                                                10,                     // 2605.6f,
                                                                10,                     // 2609.2f,
                                                                85,                     // 2614.60
                                                                85,                     // 2623.60
                                                                10,                     // 2638.20
                                                                85,                     // 2641.40
                                                                10,                     // 2650.0
                                                                10,                     // 2661.80
                                                                10,                     // 2666.7
                                                                10,                     // 2676.40
                                                                85,                     // 2685.40
                                                                10,                     // 2690.00                                                                
                                                                10,                     // 2698.40

                                                                10,                     // 2700.0f,
                                                                10,                     // 2705.6f,
                                                                75,                     // 2709.20
                                                                75,                     // 2714.6
                                                                75,                     // 2723.6
                                                                10,                     // 2738.2
                                                                10,                     // 2741.4
                                                                10,                     // 2750.00
                                                                10,                     // 2761.8
                                                                10,                     // 2766.7
                                                                75,                     // 2776.4
                                                                10,                     // 2785.40
                                                                10,                     // 2790.20
                                                                85,                     // 2798.40

                                                                10,                     // 2800.0f,
                                                                10,                     // 2805.6f,
                                                                10,                     // 2809.2f,
                                                                85,                     // 2814.60
                                                                85,                     // 2823.60
                                                                10,                     // 2838.20
                                                                85,                     // 2841.40
                                                                10,                     // 2850.0
                                                                10,                     // 2861.80
                                                                10,                     // 2866.7
                                                                10,                     // 2876.40
                                                                85,                     // 2885.40
                                                                10,                     // 2890.00                                                                
                                                                10,                     // 2898.40

                                                                10,                     // 2900.0f,
                                                                10,                     // 2905.6f,
                                                                75,                     // 2909.20
                                                                75,                     // 2914.6
                                                                75,                     // 2923.6
                                                                10,                     // 2938.2
                                                                10,                     // 2941.4
                                                                10,                     // 2950.00
                                                                10,                     // 2961.8
                                                                10,                     // 2966.7
                                                                75,                     // 2976.4
                                                                10,                     // 2985.40
                                                                10,                     // 2990.20
                                                                85,                     // 2998.40


                                                                10,                     // 3000.0f,
                                                                10,                     // 3005.6f,
                                                                75,                     // 3009.20
                                                                75,                     // 3014.6
                                                                75,                     // 3023.6
                                                                10,                     // 3038.2
                                                                10,                     // 3041.4
                                                                10,                     // 3050.00
                                                                10,                     // 3061.8
                                                                10,                     // 3066.7
                                                                75,                     // 3076.4
                                                                10,                     // 3085.40
                                                                10,                     // 3090.20
                                                                85,                     // 3098.40

                                                                10,                     // 3100.0f,
                                                                10,                     // 3105.6f,
                                                                75,                     // 3109.20
                                                                75,                     // 3114.6
                                                                75,                     // 3123.6
                                                                10,                     // 3138.2
                                                                10,                     // 3141.4
                                                                10,                     // 3150.00
                                                                10,                     // 3161.8
                                                                10,                     // 3166.7
                                                                75,                     // 3176.4
                                                                10,                     // 3185.40
                                                                10,                     // 3190.20
                                                                85,                     // 3198.40

                                                                10,                     // 3200.0f,
                                                                10,                     // 3205.6f,
                                                                10,                     // 3209.2f,
                                                                85,                     //214.60
                                                                85,                     // 3223.60
                                                                10,                     // 3238.20
                                                                85,                     // 3241.40
                                                                10,                     // 3250.0
                                                                10,                     // 3261.80
                                                                10,                     // 3266.7
                                                                10,                     // 3276.40
                                                                85,                     // 3285.40
                                                                10,                     // 3290.00                                                                
                                                                10,                     // 3298.40

                                                                10,                     // 3300.0f,
                                                                10,                     // 3305.6f,
                                                                75,                     // 3309.20
                                                                75,                     // 3314.6
                                                                75,                     // 3323.6
                                                                10,                     // 3338.2
                                                                10,                     // 3341.4
                                                                10,                     // 3350.00
                                                                10,                     // 3361.8
                                                                10,                     // 3366.7
                                                                75,                     // 3376.4
                                                                10,                     // 3385.40
                                                                10,                     // 3390.20
                                                                85,                     // 3398.40

                                                                10,                     // 3400.0f,
                                                                10,                     // 3405.6f,
                                                                10,                     // 3409.2f,
                                                                85,                     // 3414.60
                                                                85,                     // 3423.60
                                                                10,                     // 3438.20
                                                                85,                     // 3441.40
                                                                10,                     // 3450.0
                                                                10,                     // 3461.80
                                                                10,                     // 3466.7
                                                                10,                     // 3476.40
                                                                85,                     // 3485.40
                                                                10,                     // 3490.00                                                                
                                                                10,                     // 3498.40

                                                                10,                     // 3500.0f,
                                                                10,                     // 3505.6f,
                                                                75,                     // 3509.20
                                                                75,                     // 3514.6
                                                                75,                     // 3523.6
                                                                10,                     // 3538.2
                                                                10,                     // 3541.4
                                                                10,                     // 3550.00
                                                                10,                     // 3561.8
                                                                10,                     // 3566.7
                                                                75,                     // 3576.4
                                                                10,                     // 3585.40
                                                                10,                     // 3590.20
                                                                85,                     // 3598.40

                                                                10,                     // 3600.0f,
                                                                10,                     // 3605.6f,
                                                                10,                     // 3609.2f,
                                                                85,                     // 3614.60
                                                                85,                     // 3623.60
                                                                10,                     // 3638.20
                                                                85,                     // 3641.40
                                                                10,                     // 3650.0
                                                                10,                     // 3661.80
                                                                10,                     // 3666.7
                                                                10,                     // 3676.40
                                                                85,                     // 3685.40
                                                                10,                     // 3690.00                                                                
                                                                10,                     // 3698.40

                                                                10,                     // 3700.0f,
                                                                10,                     // 3705.6f,
                                                                75,                     // 3709.20
                                                                75,                     // 3714.6
                                                                75,                     // 3723.6
                                                                10,                     // 3738.2
                                                                10,                     // 3741.4
                                                                10,                     // 3750.00
                                                                10,                     // 3761.8
                                                                10,                     // 3766.7
                                                                75,                     // 3776.4
                                                                10,                     // 3785.40
                                                                10,                     // 3790.20
                                                                85,                     // 3798.40

                                                                10,                     // 3800.0f,
                                                                10,                     // 3805.6f,
                                                                10,                     // 3809.2f,
                                                                85,                     // 3814.60
                                                                85,                     // 3823.60
                                                                10,                     // 3838.20
                                                                85,                     // 3841.40
                                                                10,                     // 3850.0
                                                                10,                     // 3861.80
                                                                10,                     // 3866.7
                                                                10,                     // 3876.40
                                                                85,                     // 3885.40
                                                                10,                     // 3890.00                                                                
                                                                10,                     // 3898.40

                                                                10,                     // 3900.0f,
                                                                10,                     // 3905.6f,
                                                                75,                     // 3909.20
                                                                75,                     // 3914.6
                                                                75,                     // 3923.6
                                                                10,                     // 3938.2
                                                                10,                     // 3941.4
                                                                10,                     // 3950.00
                                                                10,                     // 3961.8
                                                                10,                     // 3966.7
                                                                75,                     // 3976.4
                                                                10,                     // 3985.40
                                                                10,                     // 3990.20
                                                                85,                     // 3998.40
        };

        public static readonly FibPercentage [ ] Wave3ProjectionFibLevelType = new FibPercentage [ ]
        {
                                                                FibPercentage.Fib_50,
                                                                FibPercentage.Fib_61_8,
                                                                FibPercentage.Fib_66_66,
                                                                FibPercentage.Fib_76_4,
                                                                FibPercentage.Fib_90_02,
                                                                FibPercentage.Fib_94_4,
                                                                FibPercentage.Fib_98_68,
                                                                
                                                                FibPercentage.Fib_100,
                                                                FibPercentage.Fib_105_6,
                                                                FibPercentage.Fib_109_2,
                                                                FibPercentage.Fib_114_6,
                                                                FibPercentage.Fib_123_6,
                                                                FibPercentage.Fib_138_2,
                                                                FibPercentage.Fib_141_4,
                                                                FibPercentage.Fib_150,
                                                                FibPercentage.Fib_161_8,
                                                                FibPercentage.Fib_166_7,
                                                                FibPercentage.Fib_176_4,
                                                                FibPercentage.Fib_185_4,
                                                                FibPercentage.Fib_190_2,
                                                                FibPercentage.Fib_198_4,

                                                                FibPercentage.Fib_200,
                                                                FibPercentage.Fib_205_6,
                                                                FibPercentage.Fib_209_2,
                                                                FibPercentage.Fib_214_6,
                                                                FibPercentage.Fib_223_6,
                                                                FibPercentage.Fib_238_2,
                                                                FibPercentage.Fib_241_4,
                                                                FibPercentage.Fib_250,
                                                                FibPercentage.Fib_261_8,
                                                                FibPercentage.Fib_266_7,
                                                                FibPercentage.Fib_276_4,
                                                                FibPercentage.Fib_285_4,
                                                                FibPercentage.Fib_290_2,
                                                                FibPercentage.Fib_298_4,

                                                                FibPercentage.Fib_300,
                                                                FibPercentage.Fib_305_6,
                                                                FibPercentage.Fib_309_2,
                                                                FibPercentage.Fib_314_6,
                                                                FibPercentage.Fib_323_6,
                                                                FibPercentage.Fib_338_2,
                                                                FibPercentage.Fib_341_4,
                                                                FibPercentage.Fib_350,
                                                                FibPercentage.Fib_361_8,
                                                                FibPercentage.Fib_366_7,
                                                                FibPercentage.Fib_376_4,
                                                                FibPercentage.Fib_385_4,
                                                                FibPercentage.Fib_390_2,
                                                                FibPercentage.Fib_398_4,

                                                                FibPercentage.Fib_400,
                                                                FibPercentage.Fib_405_6,
                                                                FibPercentage.Fib_409_2,
                                                                FibPercentage.Fib_414_6,
                                                                FibPercentage.Fib_423_6,
                                                                FibPercentage.Fib_438_2,
                                                                FibPercentage.Fib_441_4,
                                                                FibPercentage.Fib_450,
                                                                FibPercentage.Fib_461_8,
                                                                FibPercentage.Fib_466_7,
                                                                FibPercentage.Fib_476_4,
                                                                FibPercentage.Fib_485_4,
                                                                FibPercentage.Fib_490_2,
                                                                FibPercentage.Fib_498_4,

                                                                FibPercentage.Fib_500,
                                                                FibPercentage.Fib_505_6,
                                                                FibPercentage.Fib_509_2,
                                                                FibPercentage.Fib_514_6,
                                                                FibPercentage.Fib_523_6,
                                                                FibPercentage.Fib_538_2,
                                                                FibPercentage.Fib_541_4,
                                                                FibPercentage.Fib_550,
                                                                FibPercentage.Fib_561_8,
                                                                FibPercentage.Fib_566_7,
                                                                FibPercentage.Fib_576_4,
                                                                FibPercentage.Fib_585_4,
                                                                FibPercentage.Fib_590_2,
                                                                FibPercentage.Fib_598_4,

                                                                FibPercentage.Fib_600,
                                                                FibPercentage.Fib_605_6,
                                                                FibPercentage.Fib_609_2,
                                                                FibPercentage.Fib_614_6,
                                                                FibPercentage.Fib_623_6,
                                                                FibPercentage.Fib_638_2,
                                                                FibPercentage.Fib_641_4,
                                                                FibPercentage.Fib_650,
                                                                FibPercentage.Fib_661_8,
                                                                FibPercentage.Fib_666_7,
                                                                FibPercentage.Fib_676_4,
                                                                FibPercentage.Fib_685_4,
                                                                FibPercentage.Fib_690_2,
                                                                FibPercentage.Fib_698_4,

                                                                FibPercentage.Fib_700,
                                                                FibPercentage.Fib_705_6,
                                                                FibPercentage.Fib_709_2,
                                                                FibPercentage.Fib_714_6,
                                                                FibPercentage.Fib_723_6,
                                                                FibPercentage.Fib_738_2,
                                                                FibPercentage.Fib_741_4,
                                                                FibPercentage.Fib_750,
                                                                FibPercentage.Fib_761_8,
                                                                FibPercentage.Fib_766_7,
                                                                FibPercentage.Fib_776_4,
                                                                FibPercentage.Fib_785_4,
                                                                FibPercentage.Fib_790_2,
                                                                FibPercentage.Fib_798_4,

                                                                FibPercentage.Fib_800,
                                                                FibPercentage.Fib_805_6,
                                                                FibPercentage.Fib_809_2,
                                                                FibPercentage.Fib_814_6,
                                                                FibPercentage.Fib_823_6,
                                                                FibPercentage.Fib_838_2,
                                                                FibPercentage.Fib_841_4,
                                                                FibPercentage.Fib_850,
                                                                FibPercentage.Fib_861_8,
                                                                FibPercentage.Fib_866_7,
                                                                FibPercentage.Fib_876_4,
                                                                FibPercentage.Fib_885_4,
                                                                FibPercentage.Fib_890_2,
                                                                FibPercentage.Fib_898_4,

                                                                FibPercentage.Fib_900,
                                                                FibPercentage.Fib_905_6,
                                                                FibPercentage.Fib_909_2,
                                                                FibPercentage.Fib_914_6,
                                                                FibPercentage.Fib_923_6,
                                                                FibPercentage.Fib_938_2,
                                                                FibPercentage.Fib_941_4,
                                                                FibPercentage.Fib_950,
                                                                FibPercentage.Fib_961_8,
                                                                FibPercentage.Fib_966_7,
                                                                FibPercentage.Fib_976_4,
                                                                FibPercentage.Fib_985_4,
                                                                FibPercentage.Fib_990_2,
                                                                FibPercentage.Fib_998_4,

                                                                FibPercentage.Fib_1000,
                                                                FibPercentage.Fib_1005_6,
                                                                FibPercentage.Fib_1009_2,
                                                                FibPercentage.Fib_1014_6,
                                                                FibPercentage.Fib_1023_6,
                                                                FibPercentage.Fib_1038_2,
                                                                FibPercentage.Fib_1041_4,
                                                                FibPercentage.Fib_1050,
                                                                FibPercentage.Fib_1061_8,
                                                                FibPercentage.Fib_1066_7,
                                                                FibPercentage.Fib_1076_4,
                                                                FibPercentage.Fib_1085_4,
                                                                FibPercentage.Fib_1090_2,
                                                                FibPercentage.Fib_1098_4,

                                                                FibPercentage.Fib_1100,
                                                                FibPercentage.Fib_1105_6,
                                                                FibPercentage.Fib_1109_2,
                                                                FibPercentage.Fib_1114_6,
                                                                FibPercentage.Fib_1123_6,
                                                                FibPercentage.Fib_1138_2,
                                                                FibPercentage.Fib_1141_4,
                                                                FibPercentage.Fib_1150,
                                                                FibPercentage.Fib_1161_8,
                                                                FibPercentage.Fib_1166_7,
                                                                FibPercentage.Fib_1176_4,
                                                                FibPercentage.Fib_1185_4,
                                                                FibPercentage.Fib_1190_2,
                                                                FibPercentage.Fib_1198_4,

                                                                FibPercentage.Fib_1200,
                                                                FibPercentage.Fib_1205_6,
                                                                FibPercentage.Fib_1209_2,
                                                                FibPercentage.Fib_1214_6,
                                                                FibPercentage.Fib_1223_6,
                                                                FibPercentage.Fib_1238_2,
                                                                FibPercentage.Fib_1241_4,
                                                                FibPercentage.Fib_1250,
                                                                FibPercentage.Fib_1261_8,
                                                                FibPercentage.Fib_1266_7,
                                                                FibPercentage.Fib_1276_4,
                                                                FibPercentage.Fib_1285_4,
                                                                FibPercentage.Fib_1290_2,
                                                                FibPercentage.Fib_1298_4,

                                                                FibPercentage.Fib_1300,
                                                                FibPercentage.Fib_1305_6,
                                                                FibPercentage.Fib_1309_2,
                                                                FibPercentage.Fib_1314_6,
                                                                FibPercentage.Fib_1323_6,
                                                                FibPercentage.Fib_1338_2,
                                                                FibPercentage.Fib_1341_4,
                                                                FibPercentage.Fib_1350,
                                                                FibPercentage.Fib_1361_8,
                                                                FibPercentage.Fib_1366_7,
                                                                FibPercentage.Fib_1376_4,
                                                                FibPercentage.Fib_1385_4,
                                                                FibPercentage.Fib_1390_2,
                                                                FibPercentage.Fib_1398_4,

                                                                FibPercentage.Fib_1400,
                                                                FibPercentage.Fib_1405_6,
                                                                FibPercentage.Fib_1409_2,
                                                                FibPercentage.Fib_1414_6,
                                                                FibPercentage.Fib_1423_6,
                                                                FibPercentage.Fib_1438_2,
                                                                FibPercentage.Fib_1441_4,
                                                                FibPercentage.Fib_1450,
                                                                FibPercentage.Fib_1461_8,
                                                                FibPercentage.Fib_1466_7,
                                                                FibPercentage.Fib_1476_4,
                                                                FibPercentage.Fib_1485_4,
                                                                FibPercentage.Fib_1490_2,
                                                                FibPercentage.Fib_1498_4,

                                                                FibPercentage.Fib_1500,
                                                                FibPercentage.Fib_1505_6,
                                                                FibPercentage.Fib_1509_2,
                                                                FibPercentage.Fib_1514_6,
                                                                FibPercentage.Fib_1523_6,
                                                                FibPercentage.Fib_1538_2,
                                                                FibPercentage.Fib_1541_4,
                                                                FibPercentage.Fib_1550,
                                                                FibPercentage.Fib_1561_8,
                                                                FibPercentage.Fib_1566_7,
                                                                FibPercentage.Fib_1576_4,
                                                                FibPercentage.Fib_1585_4,
                                                                FibPercentage.Fib_1590_2,
                                                                FibPercentage.Fib_1598_4,

                                                                FibPercentage.Fib_1600,
                                                                FibPercentage.Fib_1605_6,
                                                                FibPercentage.Fib_1609_2,
                                                                FibPercentage.Fib_1614_6,
                                                                FibPercentage.Fib_1623_6,
                                                                FibPercentage.Fib_1638_2,
                                                                FibPercentage.Fib_1641_4,
                                                                FibPercentage.Fib_1650,
                                                                FibPercentage.Fib_1661_8,
                                                                FibPercentage.Fib_1666_7,
                                                                FibPercentage.Fib_1676_4,
                                                                FibPercentage.Fib_1685_4,
                                                                FibPercentage.Fib_1690_2,
                                                                FibPercentage.Fib_1698_4,

                                                                FibPercentage.Fib_1700,
                                                                FibPercentage.Fib_1705_6,
                                                                FibPercentage.Fib_1709_2,
                                                                FibPercentage.Fib_1714_6,
                                                                FibPercentage.Fib_1723_6,
                                                                FibPercentage.Fib_1738_2,
                                                                FibPercentage.Fib_1741_4,
                                                                FibPercentage.Fib_1750,
                                                                FibPercentage.Fib_1761_8,
                                                                FibPercentage.Fib_1766_7,
                                                                FibPercentage.Fib_1776_4,
                                                                FibPercentage.Fib_1785_4,
                                                                FibPercentage.Fib_1790_2,
                                                                FibPercentage.Fib_1798_4,

                                                                FibPercentage.Fib_1800,
                                                                FibPercentage.Fib_1805_6,
                                                                FibPercentage.Fib_1809_2,
                                                                FibPercentage.Fib_1814_6,
                                                                FibPercentage.Fib_1823_6,
                                                                FibPercentage.Fib_1838_2,
                                                                FibPercentage.Fib_1841_4,
                                                                FibPercentage.Fib_1850,
                                                                FibPercentage.Fib_1861_8,
                                                                FibPercentage.Fib_1866_7,
                                                                FibPercentage.Fib_1876_4,
                                                                FibPercentage.Fib_1885_4,
                                                                FibPercentage.Fib_1890_2,
                                                                FibPercentage.Fib_1898_4,

                                                                FibPercentage.Fib_1900,
                                                                FibPercentage.Fib_1905_6,
                                                                FibPercentage.Fib_1909_2,
                                                                FibPercentage.Fib_1914_6,
                                                                FibPercentage.Fib_1923_6,
                                                                FibPercentage.Fib_1938_2,
                                                                FibPercentage.Fib_1941_4,
                                                                FibPercentage.Fib_1950,
                                                                FibPercentage.Fib_1961_8,
                                                                FibPercentage.Fib_1966_7,
                                                                FibPercentage.Fib_1976_4,
                                                                FibPercentage.Fib_1985_4,
                                                                FibPercentage.Fib_1990_2,
                                                                FibPercentage.Fib_1998_4,

                                                                FibPercentage.Fib_2000,
                                                                FibPercentage.Fib_2005_6,
                                                                FibPercentage.Fib_2009_2,
                                                                FibPercentage.Fib_2014_6,
                                                                FibPercentage.Fib_2023_6,
                                                                FibPercentage.Fib_2038_2,
                                                                FibPercentage.Fib_2041_4,
                                                                FibPercentage.Fib_2050,
                                                                FibPercentage.Fib_2061_8,
                                                                FibPercentage.Fib_2066_7,
                                                                FibPercentage.Fib_2076_4,
                                                                FibPercentage.Fib_2085_4,
                                                                FibPercentage.Fib_2090_2,
                                                                FibPercentage.Fib_2098_4,

                                                                FibPercentage.Fib_2100,
                                                                FibPercentage.Fib_2105_6,
                                                                FibPercentage.Fib_2109_2,
                                                                FibPercentage.Fib_2114_6,
                                                                FibPercentage.Fib_2123_6,
                                                                FibPercentage.Fib_2138_2,
                                                                FibPercentage.Fib_2141_4,
                                                                FibPercentage.Fib_2150,
                                                                FibPercentage.Fib_2161_8,
                                                                FibPercentage.Fib_2166_7,
                                                                FibPercentage.Fib_2176_4,
                                                                FibPercentage.Fib_2185_4,
                                                                FibPercentage.Fib_2190_2,
                                                                FibPercentage.Fib_2198_4,

                                                                FibPercentage.Fib_2200,
                                                                FibPercentage.Fib_2205_6,
                                                                FibPercentage.Fib_2209_2,
                                                                FibPercentage.Fib_2214_6,
                                                                FibPercentage.Fib_2223_6,
                                                                FibPercentage.Fib_2238_2,
                                                                FibPercentage.Fib_2241_4,
                                                                FibPercentage.Fib_2250,
                                                                FibPercentage.Fib_2261_8,
                                                                FibPercentage.Fib_2266_7,
                                                                FibPercentage.Fib_2276_4,
                                                                FibPercentage.Fib_2285_4,
                                                                FibPercentage.Fib_2290_2,
                                                                FibPercentage.Fib_2298_4,

                                                                FibPercentage.Fib_2300,
                                                                FibPercentage.Fib_2305_6,
                                                                FibPercentage.Fib_2309_2,
                                                                FibPercentage.Fib_2314_6,
                                                                FibPercentage.Fib_2323_6,
                                                                FibPercentage.Fib_2338_2,
                                                                FibPercentage.Fib_2341_4,
                                                                FibPercentage.Fib_2350,
                                                                FibPercentage.Fib_2361_8,
                                                                FibPercentage.Fib_2366_7,
                                                                FibPercentage.Fib_2376_4,
                                                                FibPercentage.Fib_2385_4,
                                                                FibPercentage.Fib_2390_2,
                                                                FibPercentage.Fib_2398_4,

                                                                FibPercentage.Fib_2400,
                                                                FibPercentage.Fib_2405_6,
                                                                FibPercentage.Fib_2409_2,
                                                                FibPercentage.Fib_2414_6,
                                                                FibPercentage.Fib_2423_6,
                                                                FibPercentage.Fib_2438_2,
                                                                FibPercentage.Fib_2441_4,
                                                                FibPercentage.Fib_2450,
                                                                FibPercentage.Fib_2461_8,
                                                                FibPercentage.Fib_2466_7,
                                                                FibPercentage.Fib_2476_4,
                                                                FibPercentage.Fib_2485_4,
                                                                FibPercentage.Fib_2490_2,
                                                                FibPercentage.Fib_2498_4,

                                                                FibPercentage.Fib_2500,
                                                                FibPercentage.Fib_2505_6,
                                                                FibPercentage.Fib_2509_2,
                                                                FibPercentage.Fib_2514_6,
                                                                FibPercentage.Fib_2523_6,
                                                                FibPercentage.Fib_2538_2,
                                                                FibPercentage.Fib_2541_4,
                                                                FibPercentage.Fib_2550,
                                                                FibPercentage.Fib_2561_8,
                                                                FibPercentage.Fib_2566_7,
                                                                FibPercentage.Fib_2576_4,
                                                                FibPercentage.Fib_2585_4,
                                                                FibPercentage.Fib_2590_2,
                                                                FibPercentage.Fib_2598_4,

                                                                FibPercentage.Fib_2600,
                                                                FibPercentage.Fib_2605_6,
                                                                FibPercentage.Fib_2609_2,
                                                                FibPercentage.Fib_2614_6,
                                                                FibPercentage.Fib_2623_6,
                                                                FibPercentage.Fib_2638_2,
                                                                FibPercentage.Fib_2641_4,
                                                                FibPercentage.Fib_2650,
                                                                FibPercentage.Fib_2661_8,
                                                                FibPercentage.Fib_2666_7,
                                                                FibPercentage.Fib_2676_4,
                                                                FibPercentage.Fib_2685_4,
                                                                FibPercentage.Fib_2690_2,
                                                                FibPercentage.Fib_2698_4,

                                                                FibPercentage.Fib_2700,
                                                                FibPercentage.Fib_2705_6,
                                                                FibPercentage.Fib_2709_2,
                                                                FibPercentage.Fib_2714_6,
                                                                FibPercentage.Fib_2723_6,
                                                                FibPercentage.Fib_2738_2,
                                                                FibPercentage.Fib_2741_4,
                                                                FibPercentage.Fib_2750,
                                                                FibPercentage.Fib_2761_8,
                                                                FibPercentage.Fib_2766_7,
                                                                FibPercentage.Fib_2776_4,
                                                                FibPercentage.Fib_2785_4,
                                                                FibPercentage.Fib_2790_2,
                                                                FibPercentage.Fib_2798_4,

                                                                FibPercentage.Fib_2800,
                                                                FibPercentage.Fib_2805_6,
                                                                FibPercentage.Fib_2809_2,
                                                                FibPercentage.Fib_2814_6,
                                                                FibPercentage.Fib_2823_6,
                                                                FibPercentage.Fib_2838_2,
                                                                FibPercentage.Fib_2841_4,
                                                                FibPercentage.Fib_2850,
                                                                FibPercentage.Fib_2861_8,
                                                                FibPercentage.Fib_2866_7,
                                                                FibPercentage.Fib_2876_4,
                                                                FibPercentage.Fib_2885_4,
                                                                FibPercentage.Fib_2890_2,
                                                                FibPercentage.Fib_2898_4,

                                                                FibPercentage.Fib_2900,
                                                                FibPercentage.Fib_2905_6,
                                                                FibPercentage.Fib_2909_2,
                                                                FibPercentage.Fib_2914_6,
                                                                FibPercentage.Fib_2923_6,
                                                                FibPercentage.Fib_2938_2,
                                                                FibPercentage.Fib_2941_4,
                                                                FibPercentage.Fib_2950,
                                                                FibPercentage.Fib_2961_8,
                                                                FibPercentage.Fib_2966_7,
                                                                FibPercentage.Fib_2976_4,
                                                                FibPercentage.Fib_2985_4,
                                                                FibPercentage.Fib_2990_2,
                                                                FibPercentage.Fib_2998_4,


                                                                FibPercentage.Fib_3000,
                                                                FibPercentage.Fib_3005_6,
                                                                FibPercentage.Fib_3009_2,
                                                                FibPercentage.Fib_3014_6,
                                                                FibPercentage.Fib_3023_6,
                                                                FibPercentage.Fib_3038_2,
                                                                FibPercentage.Fib_3041_4,
                                                                FibPercentage.Fib_3050,
                                                                FibPercentage.Fib_3061_8,
                                                                FibPercentage.Fib_3066_7,
                                                                FibPercentage.Fib_3076_4,
                                                                FibPercentage.Fib_3085_4,
                                                                FibPercentage.Fib_3090_2,
                                                                FibPercentage.Fib_3098_4,

                                                                FibPercentage.Fib_3100,
                                                                FibPercentage.Fib_3105_6,
                                                                FibPercentage.Fib_3109_2,
                                                                FibPercentage.Fib_3114_6,
                                                                FibPercentage.Fib_3123_6,
                                                                FibPercentage.Fib_3138_2,
                                                                FibPercentage.Fib_3141_4,
                                                                FibPercentage.Fib_3150,
                                                                FibPercentage.Fib_3161_8,
                                                                FibPercentage.Fib_3166_7,
                                                                FibPercentage.Fib_3176_4,
                                                                FibPercentage.Fib_3185_4,
                                                                FibPercentage.Fib_3190_2,
                                                                FibPercentage.Fib_3198_4,

                                                                FibPercentage.Fib_3200,
                                                                FibPercentage.Fib_3205_6,
                                                                FibPercentage.Fib_3209_2,
                                                                FibPercentage.Fib_3214_6,
                                                                FibPercentage.Fib_3223_6,
                                                                FibPercentage.Fib_3238_2,
                                                                FibPercentage.Fib_3241_4,
                                                                FibPercentage.Fib_3250,
                                                                FibPercentage.Fib_3261_8,
                                                                FibPercentage.Fib_3266_7,
                                                                FibPercentage.Fib_3276_4,
                                                                FibPercentage.Fib_3285_4,
                                                                FibPercentage.Fib_3290_2,
                                                                FibPercentage.Fib_3298_4,

                                                                FibPercentage.Fib_3300,
                                                                FibPercentage.Fib_3305_6,
                                                                FibPercentage.Fib_3309_2,
                                                                FibPercentage.Fib_3314_6,
                                                                FibPercentage.Fib_3323_6,
                                                                FibPercentage.Fib_3338_2,
                                                                FibPercentage.Fib_3341_4,
                                                                FibPercentage.Fib_3350,
                                                                FibPercentage.Fib_3361_8,
                                                                FibPercentage.Fib_3366_7,
                                                                FibPercentage.Fib_3376_4,
                                                                FibPercentage.Fib_3385_4,
                                                                FibPercentage.Fib_3390_2,
                                                                FibPercentage.Fib_3398_4,

                                                                FibPercentage.Fib_3400,
                                                                FibPercentage.Fib_3405_6,
                                                                FibPercentage.Fib_3409_2,
                                                                FibPercentage.Fib_3414_6,
                                                                FibPercentage.Fib_3423_6,
                                                                FibPercentage.Fib_3438_2,
                                                                FibPercentage.Fib_3441_4,
                                                                FibPercentage.Fib_3450,
                                                                FibPercentage.Fib_3461_8,
                                                                FibPercentage.Fib_3466_7,
                                                                FibPercentage.Fib_3476_4,
                                                                FibPercentage.Fib_3485_4,
                                                                FibPercentage.Fib_3490_2,
                                                                FibPercentage.Fib_3498_4,

                                                                FibPercentage.Fib_3500,
                                                                FibPercentage.Fib_3505_6,
                                                                FibPercentage.Fib_3509_2,
                                                                FibPercentage.Fib_3514_6,
                                                                FibPercentage.Fib_3523_6,
                                                                FibPercentage.Fib_3538_2,
                                                                FibPercentage.Fib_3541_4,
                                                                FibPercentage.Fib_3550,
                                                                FibPercentage.Fib_3561_8,
                                                                FibPercentage.Fib_3566_7,
                                                                FibPercentage.Fib_3576_4,
                                                                FibPercentage.Fib_3585_4,
                                                                FibPercentage.Fib_3590_2,
                                                                FibPercentage.Fib_3598_4,

                                                                FibPercentage.Fib_3600,
                                                                FibPercentage.Fib_3605_6,
                                                                FibPercentage.Fib_3609_2,
                                                                FibPercentage.Fib_3614_6,
                                                                FibPercentage.Fib_3623_6,
                                                                FibPercentage.Fib_3638_2,
                                                                FibPercentage.Fib_3641_4,
                                                                FibPercentage.Fib_3650,
                                                                FibPercentage.Fib_3661_8,
                                                                FibPercentage.Fib_3666_7,
                                                                FibPercentage.Fib_3676_4,
                                                                FibPercentage.Fib_3685_4,
                                                                FibPercentage.Fib_3690_2,
                                                                FibPercentage.Fib_3698_4,

                                                                FibPercentage.Fib_3700,
                                                                FibPercentage.Fib_3705_6,
                                                                FibPercentage.Fib_3709_2,
                                                                FibPercentage.Fib_3714_6,
                                                                FibPercentage.Fib_3723_6,
                                                                FibPercentage.Fib_3738_2,
                                                                FibPercentage.Fib_3741_4,
                                                                FibPercentage.Fib_3750,
                                                                FibPercentage.Fib_3761_8,
                                                                FibPercentage.Fib_3766_7,
                                                                FibPercentage.Fib_3776_4,
                                                                FibPercentage.Fib_3785_4,
                                                                FibPercentage.Fib_3790_2,
                                                                FibPercentage.Fib_3798_4,

                                                                FibPercentage.Fib_3800,
                                                                FibPercentage.Fib_3805_6,
                                                                FibPercentage.Fib_3809_2,
                                                                FibPercentage.Fib_3814_6,
                                                                FibPercentage.Fib_3823_6,
                                                                FibPercentage.Fib_3838_2,
                                                                FibPercentage.Fib_3841_4,
                                                                FibPercentage.Fib_3850,
                                                                FibPercentage.Fib_3861_8,
                                                                FibPercentage.Fib_3866_7,
                                                                FibPercentage.Fib_3876_4,
                                                                FibPercentage.Fib_3885_4,
                                                                FibPercentage.Fib_3890_2,
                                                                FibPercentage.Fib_3898_4,

                                                                FibPercentage.Fib_3900,
                                                                FibPercentage.Fib_3905_6,
                                                                FibPercentage.Fib_3909_2,
                                                                FibPercentage.Fib_3914_6,
                                                                FibPercentage.Fib_3923_6,
                                                                FibPercentage.Fib_3938_2,
                                                                FibPercentage.Fib_3941_4,
                                                                FibPercentage.Fib_3950,
                                                                FibPercentage.Fib_3961_8,
                                                                FibPercentage.Fib_3966_7,
                                                                FibPercentage.Fib_3976_4,
                                                                FibPercentage.Fib_3985_4,
                                                                FibPercentage.Fib_3990_2,
                                                                FibPercentage.Fib_3998_4,
        };


        

        
        public static readonly float [ ] Wave3CProjectionLevels = new float [ ]
        {
                                                                76.40f,
                                                                85.40f,
                                                                90.02f,
                                                                94.40f,
                                                                98.40f,

                                                                100.00f,
                                                                105.60f,
                                                                109.20f,
                                                                114.60f,
                                                                123.60f,
                                                                138.20f,
                                                                141.40f,
                                                                150.00f,
                                                                161.80f,
                                                                166.70f,
                                                                176.40f,
                                                                185.40f,
                                                                194.40f,

                                                                200.00f,
                                                                205.60f,
                                                                209.20f,
                                                                214.60f,
                                                                223.60f,
                                                                238.20f,
                                                                241.40f,
                                                                250.00f,
                                                                261.80f,
                                                                266.70f,
                                                                276.40f,
        };

        public static readonly FibPercentage [ ] Wave3CProjectionFibLevelType = new FibPercentage [ ]
        {
                                                                FibPercentage.Fib_76_4,
                                                                FibPercentage.Fib_85_4,
                                                                FibPercentage.Fib_90_02,
                                                                FibPercentage.Fib_94_4,
                                                                FibPercentage.Fib_98_4,

                                                                FibPercentage.Fib_100,
                                                                FibPercentage.Fib_105_6,
                                                                FibPercentage.Fib_109_2,
                                                                FibPercentage.Fib_114_6,
                                                                FibPercentage.Fib_123_6,
                                                                FibPercentage.Fib_138_2,
                                                                FibPercentage.Fib_141_4,
                                                                FibPercentage.Fib_150,
                                                                FibPercentage.Fib_161_8,
                                                                FibPercentage.Fib_166_7,
                                                                FibPercentage.Fib_176_4,
                                                                FibPercentage.Fib_185_4,
                                                                FibPercentage.Fib_194_4,

                                                                FibPercentage.Fib_200,
                                                                FibPercentage.Fib_205_6,
                                                                FibPercentage.Fib_209_2,
                                                                FibPercentage.Fib_214_6,
                                                                FibPercentage.Fib_223_6,
                                                                FibPercentage.Fib_238_2,
                                                                FibPercentage.Fib_241_4,
                                                                FibPercentage.Fib_250,
                                                                FibPercentage.Fib_261_8,
                                                                FibPercentage.Fib_266_7,
                                                                FibPercentage.Fib_276_4,

        };



        public static readonly int [ ] Wave3CProjectionStrength = new int [ ]
        {
                                                                5,              // Fib_76_4
                                                                10,             // Fib_85_4
                                                                10,             // Fib_90_02
                                                                10,             // Fib_94_4
                                                                10,             // Fib_98_4
                                                                
                                                                40,             // Fib_100
                                                                40,             // Fib_105_6
                                                                40,             // Fib_109_2
                                                                10,             // Fib_114_6
                                                                40,             // Fib_123_6
                                                                10,             // Fib_138_2
                                                                30,             // Fib_141_4
                                                                5,              // Fib_150
                                                                5,              // Fib_161_8
                                                                5,              // Fib_166_7
                                                                5,              // Fib_176_4
                                                                20,             // Fib_185_4
                                                                20,             // Fib_194_4

                                                                40,             // Fib_200
                                                                40,             // Fib_205_6
                                                                40,             // Fib_209_2
                                                                10,             // Fib_214_6
                                                                40,             // Fib_223_6
                                                                10,             // Fib_238_2
                                                                30,             // Fib_241_4
                                                                5,              // Fib_250
                                                                5,              // Fib_261_8
                                                                5,              // Fib_266_7
                                                                5,              // Fib_276_4
        };

        

        public static readonly float [ ] Wave5ProjectionLevels = new float [ ]
        {
                                                                33.30f,
                                                                38.20f,
                                                                41.40f,
                                                                50.00f,
                                                                61.80f,
                                                                66.67f,
                                                                76.40f,
                                                                85.40f,
                                                                90.02f,
                                                                94.40f,
                                                                100.00f,
                                                                105.60f
        };

        public static readonly FibPercentage [ ] Wave5ProjectionFibLevelType = new FibPercentage [ ]
        {
                                                                FibPercentage.Fib_33_3,
                                                                FibPercentage.Fib_38_2,
                                                                FibPercentage.Fib_41_4,
                                                                FibPercentage.Fib_50,                                                                
                                                                FibPercentage.Fib_61_8,
                                                                FibPercentage.Fib_66_66,
                                                                FibPercentage.Fib_76_4,
                                                                FibPercentage.Fib_85_4,
                                                                FibPercentage.Fib_90_02,
                                                                FibPercentage.Fib_94_4,
                                                                FibPercentage.Fib_100,
                                                                FibPercentage.Fib_105_6

        };

        public static readonly int [ ] Wave5ProjectionStrength = new int [ ]
        {
                                                                5,
                                                                5,
                                                                30,
                                                                30,
                                                                30,
                                                                20,
                                                                20,
                                                                15,
                                                                5,
                                                                5,
                                                                5,
                                                                5
        };

        public static readonly float [ ] Wave5CProjectionLevels = new float [ ]
        {
                                                                50.00f,
                                                                61.80f,
                                                                66.67f,
                                                                76.40f,
                                                                85.40f,
                                                                90.02f,
                                                                94.40f,
                                                                98.40f,
                                                                100,
                                                                105.60f,
                                                                109.20f,
                                                                114.60f,
                                                                123.60f,
                                                                138.20f,
                                                                161.80f,
                                                                166.70f,
                                                                176.40f,
                                                                185.40f,
                                                                194.40f,
                                                                223.60f,
                                                                261.80f,
                                                                276.40f
        };

        public static readonly FibPercentage [ ] Wave5CProjectionFibLevelType = new FibPercentage [ ]
        {
                                                                FibPercentage.Fib_50,
                                                                FibPercentage.Fib_61_8,
                                                                FibPercentage.Fib_66_66,
                                                                FibPercentage.Fib_76_4,
                                                                FibPercentage.Fib_85_4,
                                                                FibPercentage.Fib_90_02,
                                                                FibPercentage.Fib_94_4,
                                                                FibPercentage.Fib_98_4,
                                                                FibPercentage.Fib_100,
                                                                FibPercentage.Fib_105_6,
                                                                FibPercentage.Fib_109_2,
                                                                FibPercentage.Fib_114_6,
                                                                FibPercentage.Fib_123_6,
                                                                FibPercentage.Fib_138_2,
                                                                FibPercentage.Fib_161_8,
                                                                FibPercentage.Fib_166_7,
                                                                FibPercentage.Fib_176_4,
                                                                FibPercentage.Fib_185_4,
                                                                FibPercentage.Fib_194_4,
                                                                FibPercentage.Fib_223_6,
                                                                FibPercentage.Fib_261_8,
                                                                FibPercentage.Fib_276_4

        };

        public static readonly int [ ] Wave5CProjectionStrength = new int [ ]
        {
            0,
                                                                5,      // Fib_50
                                                                5,      // Fib_61_8
                                                                5,      // Fib_66_66
                                                                10,     // Fib_76_4
                                                                10,     // Fib_85_4
                                                                20,     // Fib_90_02
                                                                25,     // Fib_94_4
                                                                25,     // Fib_98_4
                                                                50,     // Fib_100
                                                                50,     // Fib_105_6
                                                                40,     // Fib_109_2
                                                                40,     // Fib_114_6
                                                                40,     // Fib_123_6
                                                                30,     // Fib_138_2
                                                                10,      // Fib_161_8
                                                                10,
                                                                10,
                                                                10,
                                                                5,
                                                                5,
                                                                5
        };

        #endregion

        #region ABC Projection

        public static readonly float [ ] ABCWaveCProjectionLevels = new float [ ]
        {
                                                                41.40f,
                                                                50.00f,
                                                                61.80f,
                                                                66.67f,

                                                                76.40f,
                                                                85.40f,
                                                                90.02f,
                                                                94.40f,
                                                                100.00f,
                                                                105.60f,
                                                                109.20f,
                                                                114.60f,
                                                                123.60f,
                                                                138.20f,
                                                                150.00f,
                                                                161.80f,
                                                                166.70f,
                                                                176.40f,
                                                                185.40f,
                                                                194.40f,
                                                                223.60f,
                                                                261.80f,
                                                                276.40f,
                                                                285.40f,
                                                                361.80f
        };

        public static readonly FibPercentage [ ] ABCWaveCProjectionFibLevelType = new FibPercentage [ ]
        {
                                                                FibPercentage.Fib_41_4,
                                                                FibPercentage.Fib_50,
                                                                FibPercentage.Fib_61_8,
                                                                FibPercentage.Fib_66_66,
                                                                FibPercentage.Fib_76_4,
                                                                FibPercentage.Fib_85_4,
                                                                FibPercentage.Fib_90_02,
                                                                FibPercentage.Fib_94_4,
                                                                FibPercentage.Fib_100,
                                                                FibPercentage.Fib_105_6,
                                                                FibPercentage.Fib_109_2,
                                                                FibPercentage.Fib_114_6,
                                                                FibPercentage.Fib_123_6,
                                                                FibPercentage.Fib_138_2,
                                                                FibPercentage.Fib_150,
                                                                FibPercentage.Fib_161_8,
                                                                FibPercentage.Fib_166_7,
                                                                FibPercentage.Fib_176_4,
                                                                FibPercentage.Fib_185_4,
                                                                FibPercentage.Fib_194_4,
                                                                FibPercentage.Fib_223_6,
                                                                FibPercentage.Fib_261_8,
                                                                FibPercentage.Fib_276_4,
                                                                FibPercentage.Fib_285_4,
                                                                FibPercentage.Fib_361_8
        };

        public static readonly int [ ] ABCWaveCProjectionStrength = new int [ ]
        {
                                                                5,
                                                                5,
                                                                5,          // Fib_61_8
                                                                5,
                                                                10,         // Fib_76_4
                                                                10,         // Fib_85_4
                                                                10,         // Fib_90_02
                                                                15,         // Fib_94_4
                                                                50,         // Fib_100
                                                                40,         // Fib_105_6
                                                                30,         // Fib_109_2
                                                                20,         // Fib_114_6
                                                                20,         // Fib_123_6
                                                                5,
                                                                10,
                                                                10,         // Fib_161_8
                                                                5,          // Fib_166_7
                                                                5,          // Fib_176_4
                                                                5,          // Fib_185_4
                                                                5,          // Fib_194_4
                                                                50,          // Fib_223_6
                                                                5,
                                                                5,
                                                                5,
                                                                5
        };


       


        public static readonly float[] FirstXProjectionLevels = new float[]
        {
                                                                76.40f,
                                                                90.02f,
                                                                94.40f,
                                                                100.00f,
                                                                105.60f,
                                                                109.20f,
                                                                114.60f,
                                                                123.60f,
                                                                138.20f,
                                                                150.00f,
                                                                161.80f,
                                                                166.70f,
                                                                176.40f,
                                                                185.40f,
                                                                194.40f,
                                                                223.60f,
                                                                238.20f,
                                                                261.80f,
                                                                276.40f,
                                                                285.40f,
                                                                361.80f
        };

        public static readonly FibPercentage [ ] FirstXProjectionFibLevelType = new FibPercentage [ ]
        {
                                                                FibPercentage.Fib_76_4,
                                                                FibPercentage.Fib_90_02,
                                                                FibPercentage.Fib_94_4,
                                                                FibPercentage.Fib_100,
                                                                FibPercentage.Fib_105_6,
                                                                FibPercentage.Fib_109_2,
                                                                FibPercentage.Fib_114_6,
                                                                FibPercentage.Fib_123_6,
                                                                FibPercentage.Fib_138_2,
                                                                FibPercentage.Fib_150,
                                                                FibPercentage.Fib_161_8,
                                                                FibPercentage.Fib_166_7,
                                                                FibPercentage.Fib_176_4,
                                                                FibPercentage.Fib_185_4,
                                                                FibPercentage.Fib_194_4,
                                                                FibPercentage.Fib_223_6,
                                                                FibPercentage.Fib_238_2,
                                                                FibPercentage.Fib_261_8,
                                                                FibPercentage.Fib_276_4,
                                                                FibPercentage.Fib_285_4,
                                                                FibPercentage.Fib_361_8
        };

        public static readonly int[] FirstXProjectionStrength = new int[]
        {
                                                                0,
                                                                0,
                                                                0,
                                                                10,
                                                                10,
                                                                10,
                                                                10,
                                                                10,
                                                                10,
                                                                10,
                                                                10,
                                                                10,
                                                                0,
                                                                0,
                                                                0,
                                                                10,
                                                                10,
                                                                10,
                                                                0,
                                                                0,
                                                                0
        };

        public static readonly float[] SecondXProjectionLevels = new float[]
        {
                                                                76.40f,
                                                                90.02f,
                                                                94.40f,
                                                                100.00f,
                                                                105.60f,
                                                                109.20f,
                                                                114.60f,
                                                                123.60f,
                                                                138.20f,
                                                                150.00f,
                                                                161.80f

        };

        public static readonly FibPercentage [ ] SecondXProjectionFibLevelType = new FibPercentage [ ]
        {
                                                                FibPercentage.Fib_76_4,
                                                                FibPercentage.Fib_90_02,
                                                                FibPercentage.Fib_94_4,
                                                                FibPercentage.Fib_100,
                                                                FibPercentage.Fib_105_6,
                                                                FibPercentage.Fib_109_2,
                                                                FibPercentage.Fib_114_6,
                                                                FibPercentage.Fib_123_6,
                                                                FibPercentage.Fib_138_2,
                                                                FibPercentage.Fib_150,
                                                                FibPercentage.Fib_161_8
                                                                
        };

        public static readonly int[] SecondXProjectionStrength = new int[]
        {
                                                                0,
                                                                10,
                                                                10,
                                                                10,
                                                                10,
                                                                10,
                                                                10,
                                                                10,
                                                                10,
                                                                0,
                                                                0
        };


        
        public static readonly float [ ] ABCWaveBRetracementLevels = new float [ ]
        {
                                                                9.02f,
                                                                14.60f,
                                                                23.60f,
                                                                30.00f,
                                                                33.30f,
                                                                38.20f,
                                                                41.40f,
                                                                50.00f,
                                                                58.60f,
                                                                61.80f,
                                                                66.66f,
                                                                76.40f,
                                                                85.40f,
                                                                90.02f,
                                                                94.40f,
                                                                98.60f
        };

        public static readonly FibPercentage [ ] ABCWaveBRetracementFibLevelType = new FibPercentage [ ]
        {
                                                                FibPercentage.Fib_9_02,
                                                                FibPercentage.Fib_14_6,
                                                                FibPercentage.Fib_23_6,
                                                                FibPercentage.Fib_30,
                                                                FibPercentage.Fib_33_3,
                                                                FibPercentage.Fib_38_2,
                                                                FibPercentage.Fib_41_4,
                                                                FibPercentage.Fib_50,
                                                                FibPercentage.Fib_58_6,
                                                                FibPercentage.Fib_61_8,
                                                                FibPercentage.Fib_66_66,
                                                                FibPercentage.Fib_76_4,
                                                                FibPercentage.Fib_85_4,
                                                                FibPercentage.Fib_90_02,
                                                                FibPercentage.Fib_94_4,
                                                                FibPercentage.Fib_98_68
        };

        public static readonly int [ ] ABCWaveBRetracementStrength = new int [ ]
        {
                                                                0,
                                                                0,
                                                                10,
                                                                20,
                                                                0,
                                                                5,
                                                                10,
                                                                10,
                                                                10,
                                                                20,
                                                                20,
                                                                20,
                                                                20,
                                                                5,
                                                                0,
                                                                5
        };

        public static readonly float [ ] WaveEFBRetracementLevels = new float [ ]
        {
                                                                9.02f,
                                                                14.60f,
                                                                23.60f,
                                                                33.30f,
                                                                38.20f,
                                                                41.40f,
                                                                50.00f,
                                                                58.60f,
                                                                61.80f,
                                                                66.66f,
                                                                76.40f,
                                                                85.40f,
                                                                90.02f,
                                                                94.40f,
                                                                98.68f,
                                                                100.00f,
                                                                109.20f,
                                                                114.60f,
                                                                123.60f,
                                                                133.30f,
                                                                138.20f
        };

        public static readonly FibPercentage [ ] WaveEFBRetracementFibLevelType = new FibPercentage [ ]
        {
                                                                FibPercentage.Fib_9_02,
                                                                FibPercentage.Fib_14_6,
                                                                FibPercentage.Fib_23_6,
                                                                FibPercentage.Fib_33_3,
                                                                FibPercentage.Fib_38_2,
                                                                FibPercentage.Fib_41_4,
                                                                FibPercentage.Fib_50,
                                                                FibPercentage.Fib_58_6,
                                                                FibPercentage.Fib_61_8,
                                                                FibPercentage.Fib_66_66,
                                                                FibPercentage.Fib_76_4,
                                                                FibPercentage.Fib_85_4,
                                                                FibPercentage.Fib_90_02,
                                                                FibPercentage.Fib_94_4,
                                                                FibPercentage.Fib_98_68,
                                                                FibPercentage.Fib_100,                                                                
                                                                FibPercentage.Fib_109_2,
                                                                FibPercentage.Fib_114_6,
                                                                FibPercentage.Fib_123_6,
                                                                FibPercentage.Fib_133_3,
                                                                FibPercentage.Fib_138_2,
        };


        public static readonly int [ ] WaveEFBRetracementStrength = new int [ ]
        {
                                                                0,
                                                                0,
                                                                0,
                                                                0,
                                                                0,
                                                                0,
                                                                0,
                                                                0,
                                                                0,
                                                                0,
                                                                0,
                                                                0,
                                                                0,
                                                                0,
                                                                0,
                                                                0,
                                                                0,
                                                                0,
                                                                0,
                                                                0,
                                                                0,
                                                                0
        };

        public static readonly float [ ] WaveTriBRetracementLevels = new float [ ]
        {
                                                                61.80f,
                                                                66.66f,
                                                                76.40f,
                                                                85.40f,
                                                                90.02f,
                                                                94.40f,
                                                                100.00f,
                                                                109.20f,
                                                                114.60f,
                                                                123.60f,
                                                                133.30f,
                                                                138.20f
        };

        public static readonly FibPercentage [ ] WaveTriBRetracementFibLevelType = new FibPercentage [ ]
        {
                                                                FibPercentage.Fib_61_8,
                                                                FibPercentage.Fib_66_66,
                                                                FibPercentage.Fib_76_4,
                                                                FibPercentage.Fib_85_4,
                                                                FibPercentage.Fib_90_02,
                                                                FibPercentage.Fib_94_4,                                                                
                                                                FibPercentage.Fib_100,
                                                                FibPercentage.Fib_109_2,
                                                                FibPercentage.Fib_114_6,
                                                                FibPercentage.Fib_123_6,
                                                                FibPercentage.Fib_133_3,
                                                                FibPercentage.Fib_138_2,
        };

        public static readonly int [ ] WaveTriBRetracementStrength = new int [ ]
        {
                                                                0,
                                                                10,
                                                                10,
                                                                10,
                                                                0,
                                                                0,
                                                                0,
                                                                0,
                                                                0,
                                                                0,
                                                                0,
                                                                0
        };

        public static readonly float [ ] WaveTriCRetracementLevels = new float [ ]
        {
                                                                61.80f,
                                                                66.66f,
                                                                76.40f,
                                                                85.40f,
                                                                90.20f,
                                                                94.40f,
                                                                100.00f
        };

        public static readonly FibPercentage [ ] WaveTriCRetracementFibLevelType = new FibPercentage [ ]
        {
                                                                FibPercentage.Fib_61_8,
                                                                FibPercentage.Fib_66_66,
                                                                FibPercentage.Fib_76_4,
                                                                FibPercentage.Fib_85_4,
                                                                FibPercentage.Fib_90_02,
                                                                FibPercentage.Fib_94_4,
                                                                FibPercentage.Fib_100
        };

        public static readonly int [ ] WaveTriCRetracementStrength = new int [ ]
        {
                                                                0,
                                                                10,
                                                                10,
                                                                10,
                                                                0,
                                                                0,
                                                                0
        };

        public static readonly float [ ] WaveTriDRetracementLevels = new float [ ]
        {
                                                                61.80f,
                                                                66.66f,
                                                                76.40f,
                                                                85.40f,
                                                                90.02f,
                                                                94.40f
        };

        public static readonly FibPercentage [ ] WaveTriDRetracementFibLevelType = new FibPercentage [ ]
        {
                                                                FibPercentage.Fib_61_8,
                                                                FibPercentage.Fib_66_66,
                                                                FibPercentage.Fib_76_4,
                                                                FibPercentage.Fib_85_4,
                                                                FibPercentage.Fib_90_02,
                                                                FibPercentage.Fib_94_4
                                                                
        };

        public static readonly int [ ] WaveTriDRetracementStrength = new int [ ]
        {
                                                                0,
                                                                10,
                                                                10,
                                                                10,
                                                                0,
                                                                0
        };

        public static readonly float [ ] WaveTriERetracementLevels = new float [ ]
        {
                                                                38.20f,
                                                                41.40f,
                                                                50.00f,
                                                                58.60f,
                                                                61.80f,
                                                                66.60f,
                                                                76.40f,
                                                                85.40f
        };

        public static readonly FibPercentage [ ] WaveTriERetracementFibLevelType = new FibPercentage [ ]
        {
                                                                FibPercentage.Fib_38_2,
                                                                FibPercentage.Fib_41_4,
                                                                FibPercentage.Fib_50,
                                                                FibPercentage.Fib_58_6,
                                                                FibPercentage.Fib_61_8,
                                                                FibPercentage.Fib_66_66,
                                                                FibPercentage.Fib_76_4,
                                                                FibPercentage.Fib_85_4
        };

        public static readonly int [ ] WaveTriERetracementStrength = new int [ ]
        {
                                                                0,
                                                                0,
                                                                10,
                                                                10,
                                                                10,
                                                                10,
                                                                0,
                                                                0
        };
        #endregion
    }

    public enum TrendDirection : byte
    {
        NoTrend   = 0,
        [Description( "Up" )]
        Uptrend   = 1,
        [Description( "Down" )]
        DownTrend = 2
    }

    public enum PriceActionThruTime : byte
    {
        NonDirectional = 0,
        Directional    = 1
    }

    public enum BrokenLevel : byte
    {
        NONE = 0,
        BrokenUp = 1,
        BrokenDown = 2
    }

    public static class DataBarHelper
    {
        public static string ToDescription( this Enum en ) //ext method
        {
            Type type = en.GetType( );

            MemberInfo [ ] memInfo = type.GetMember( en.ToString( ) );

            if ( memInfo != null && memInfo.Length > 0 )
            {

                object [ ] attrs = memInfo [ 0 ].GetCustomAttributes(
                                                                    typeof( DescriptionAttribute ),
                                                                    false
                                                                  );

                if ( attrs != null && attrs.Length > 0 )

                    return ( ( DescriptionAttribute ) attrs [ 0 ] ).Description;

            }

            return en.ToString( );
        }

        public static int TechnicalAnalysisSignalCount( TASignal input, ref IList<TASignal> signalList )
        {
            int count = 0;

            if ( input.HasFlag( TASignal.HAS_BOTTOMING_SIGNAL ) )
            {
                signalList.Add( TASignal.HAS_BOTTOMING_SIGNAL );
                count++;
            }
            else if ( input.HasFlag( TASignal.HAS_TOPPING_SIGNAL ) )
            {
                signalList.Add( TASignal.HAS_TOPPING_SIGNAL );
                count++;
            }

            

            if ( input.HasFlag( TASignal.GANN_PEAK ) )
            {
                signalList.Add( TASignal.GANN_PEAK );
                count++;
            }
            else if ( input.HasFlag( TASignal.GANN_TROUGH ) )
            {
                signalList.Add( TASignal.GANN_TROUGH );
                count++;
            }
            

            if ( input.HasFlag( TASignal.HAS_DIVERGENCE ) )
            {
                signalList.Add( TASignal.HAS_DIVERGENCE );
                count++;
            }
                        
            if ( input.HasFlag( TASignal.ExitOverBought ) )
            {
                signalList.Add( TASignal.ExitOverBought );
                count++;
            }
            else if ( input.HasFlag( TASignal.ExitOverSold ) )
            {
                signalList.Add( TASignal.ExitOverSold );
                count++;
            }


            return count;
        }

        public static int TechnicalAnalysisSignalCount( TASignal input, ref PooledList<TASignal> signalList )
        {
            int count = 0;

            if ( input.HasFlag( TASignal.HAS_BOTTOMING_SIGNAL ) )
            {
                signalList.Add( TASignal.HAS_BOTTOMING_SIGNAL );
                count++;
            }
            else if ( input.HasFlag( TASignal.HAS_TOPPING_SIGNAL ) )
            {
                signalList.Add( TASignal.HAS_TOPPING_SIGNAL );
                count++;
            }

            
            if ( input.HasFlag( TASignal.HAS_DIVERGENCE ) )
            {
                signalList.Add( TASignal.HAS_DIVERGENCE );
                count++;
            }            

            if ( input.HasFlag( TASignal.ExitOverBought ) )
            {
                signalList.Add( TASignal.ExitOverBought );
                count++;
            }
            else if ( input.HasFlag( TASignal.ExitOverSold ) )
            {
                signalList.Add( TASignal.ExitOverSold );
                count++;
            }


            return count;
        }

        public static string GetCandlestickHtml( TACandle input )
        {
            string result = "";

            int count = 0;

            if ( input.HasFlag( TACandle.CdlMorningStar ) )
            {
                count++;
                result += String.Format( "Morning Start\r\n", count );
            }

            if ( input.HasFlag( TACandle.CdlDoji ) )
            {
                count++;
                result += String.Format( "Doji\r\n", count );
            }

            if ( input.HasFlag( TACandle.CdlEveningStar ) )
            {
                count++;
                result += String.Format( "Evening Star\r\n", count );
            }

            if ( input.HasFlag( TACandle.CdlBreakAwayBull ) )
            {
                count++;
                result += String.Format( "Bullish Breakaway\r\n", count );
            }

            if ( input.HasFlag( TACandle.CdlBreakAwayBear ) )
            {
                count++;
                result += String.Format( "Bearish Breakaway\r\n", count );
            }

            if ( input.HasFlag( TACandle.CdlAdvanceBlock ) )
            {
                count++;
                result += String.Format( "Advance Block\r\n", count );
            }

            if ( input.HasFlag( TACandle.CdlPiercing ) )
            {
                count++;
                result += String.Format( "Piercing\r\n", count );
            }


            if ( input.HasFlag( TACandle.CdlDarkCloudCover ) )
            {
                count++;
                result += String.Format( "Dark Cloud Cover\r\n", count );
            }


            if ( input.HasFlag( TACandle.CdlEngulfingBull ) )
            {
                count++;
                result += String.Format( "Bullish Engulfing\r\n", count );
            }


            if ( input.HasFlag( TACandle.CdlEngulfingBear ) )
            {
                count++;
                result += String.Format( "Bearish Engulfing\r\n", count );
            }


            if ( input.HasFlag( TACandle.Cdl3BlackCrows ) )
            {
                count++;
                result += String.Format( "Three Black Crows\r\n", count );
            }


            if ( input.HasFlag( TACandle.Cdl3WhiteSoldiers ) )
            {
                count++;
                result += String.Format( "Three White Soldiers\r\n", count );
            }


            if ( input.HasFlag( TACandle.CdlDojiStarBear ) )
            {
                count++;
                result += String.Format( "Bearish Doji Star\r\n", count );
            }


            if ( input.HasFlag( TACandle.CdlDojiStarBull ) )
            {
                count++;
                result += String.Format( "Bullish Doji Star\r\n", count );
            }


            if ( input.HasFlag( TACandle.CdlHammer ) )
            {
                count++;
                result += String.Format( "Hammer\r\n", count );
            }


            if ( input.HasFlag( TACandle.CdlHangingMan ) )
            {
                count++;
                result += String.Format( "Hanging Man\r\n", count );
            }





            if ( input.HasFlag( TACandle.CdlShootingStar ) )
            {
                count++;
                result += String.Format( "Shooting Star\r\n", count );
            }


            if ( input.HasFlag( TACandle.CdlInvertedHammer ) )
            {
                count++;
                result += String.Format( "Inverted Hammer\r\n", count );
            }


            if ( input.HasFlag( TACandle.CdlRising3Methods ) )
            {
                count++;
                result += String.Format( "Rising Three Methods\r\n", count );
            }


            if ( input.HasFlag( TACandle.CdlFalling3Methods ) )
            {
                count++;
                result += String.Format( "Falling Three Methods\r\n", count );
            }

            if ( input.HasFlag( TACandle.CdlLadderBottom ) )
            {
                count++;
                result += String.Format( "Ladder Bottom\r\n", count );
            }


            if ( input.HasFlag( TACandle.Cdl3LineStrikeBear ) )
            {
                count++;
                result += String.Format( "Bearish Three Line Strike\r\n", count );
            }


            if ( input.HasFlag( TACandle.Cdl3LineStrikeBull ) )
            {
                count++;
                result += String.Format( "Bullish Three Line Strike\r\n", count );
            }


            if ( input.HasFlag( TACandle.CdlMeetingLinesBear ) )
            {
                count++;
                result += String.Format( "Bearish Meeting Lines\r\n", count );
            }

            if ( input.HasFlag( TACandle.CdlTriStarBull ) )
            {
                count++;
                result += String.Format( "Tri Star Bullish\r\n", count );
            }

            if ( input.HasFlag( TACandle.CdlTriStarBear ) )
            {
                count++;
                result += String.Format( "Tri Star Bearish\r\n", count );
            }

            return result;
        }

        public static int GetCandlestickCount( TACandle input )
        {
            int count = 0;

            if ( input.HasFlag( TACandle.CdlMorningStar ) )
            {
                count++;
            }

            if ( input.HasFlag( TACandle.CdlDoji ) )
            {
                count++;
            }

            if ( input.HasFlag( TACandle.CdlEveningStar ) )
            {
                count++;
            }

            if ( input.HasFlag( TACandle.CdlBreakAwayBull ) )
            {
                count++;
            }

            if ( input.HasFlag( TACandle.CdlBreakAwayBear ) )
            {
                count++;
            }

            if ( input.HasFlag( TACandle.CdlAdvanceBlock ) )
            {
                count++;
            }

            if ( input.HasFlag( TACandle.CdlPiercing ) )
            {
                count++;
            }


            if ( input.HasFlag( TACandle.CdlDarkCloudCover ) )
            {
                count++;
            }


            if ( input.HasFlag( TACandle.CdlEngulfingBull ) )
            {
                count++;             
            }


            if ( input.HasFlag( TACandle.CdlEngulfingBear ) )
            {
                count++;
                
            }


            if ( input.HasFlag( TACandle.Cdl3BlackCrows ) )
            {
                count++;
                
            }


            if ( input.HasFlag( TACandle.Cdl3WhiteSoldiers ) )
            {
                count++;
                
            }


            if ( input.HasFlag( TACandle.CdlDojiStarBear ) )
            {
                count++;
                
            }


            if ( input.HasFlag( TACandle.CdlDojiStarBull ) )
            {
                count++;
                
            }


            if ( input.HasFlag( TACandle.CdlHammer ) )
            {
                count++;
                
            }


            if ( input.HasFlag( TACandle.CdlHangingMan ) )
            {
                count++;
                
            }





            if ( input.HasFlag( TACandle.CdlShootingStar ) )
            {
                count++;
                
            }


            if ( input.HasFlag( TACandle.CdlInvertedHammer ) )
            {
                count++;
                
            }


            if ( input.HasFlag( TACandle.CdlRising3Methods ) )
            {
                count++;
                
            }


            if ( input.HasFlag( TACandle.CdlFalling3Methods ) )
            {
                count++;
                
            }

            if ( input.HasFlag( TACandle.CdlLadderBottom ) )
            {
                count++;
                
            }


            if ( input.HasFlag( TACandle.Cdl3LineStrikeBear ) )
            {
                count++;
                
            }


            if ( input.HasFlag( TACandle.Cdl3LineStrikeBull ) )
            {
                count++;
                
            }


            if ( input.HasFlag( TACandle.CdlMeetingLinesBear ) )
            {
                count++;
                
            }



            return count;
        }

        public static string GetDataBarSignalHtml( TASignal input )
        {
            string result = "";

            int count = 0;

            if ( input.HasFlag( TASignal.HAS_BOTTOMING_SIGNAL ) )
            {
                count++;
                result += String.Format( "MACD crossing UP\r\n", count );
            }
            else if ( input.HasFlag( TASignal.HAS_TOPPING_SIGNAL ) )
            {
                count++;
                result += String.Format( "MACD crossing DOWN\r\n", count );
            }

                        

            
            if ( input.HasFlag( TASignal.HAS_DIVERGENCE ) )
            {
                count++;
                result += String.Format( "MACD and Price Divergence\r\n", count );
            }
            

            
            if ( input.HasFlag( TASignal.ExitOverBought ) )
            {
                count++;
                result += String.Format( "Exit Overbought\r\n", count );
            }
            else if ( input.HasFlag( TASignal.ExitOverSold ) )
            {
                count++;
                result += String.Format( "Exit Oversold\r\n", count );
            }

            if ( input.HasFlag( TASignal.GANN_PEAK ) )
            {
                count++;
                result += String.Format( "Gann Swing Peak\r\n", count );
            }
            else if ( input.HasFlag( TASignal.GANN_TROUGH ) )
            {
                count++;
                result += String.Format( "Gann Swing Trough\r\n", count );
            }

            if ( input.HasFlag( TASignal.WAVE_PEAK ) )
            {
                count++;
                result += String.Format( "Zig Zag Wave Peak\r\n", count );
            }
            else if ( input.HasFlag( TASignal.WAVE_TROUGH ) )
            {
                count++;
                result += String.Format( "Zig Zag Wave Trough\r\n", count );
            }

            return result;
        }

        public static string GetDataBarSignalText( TASignal input )
        {
            string result = "";

            int count = 0;

            if ( input.HasFlag( TASignal.HAS_BOTTOMING_SIGNAL ) )
            {
                count++;
                result += String.Format( "MACD crossing UP", count );
            }
            else if ( input.HasFlag( TASignal.HAS_TOPPING_SIGNAL ) )
            {
                count++;
                result += String.Format( "MACD crossing DOWN", count );
            }

            
            
            if ( input.HasFlag( TASignal.HAS_DIVERGENCE ) )
            {
                count++;
                result += String.Format( "MACD and Price Divergence", count );
            }
                        
            if ( input.HasFlag( TASignal.ExitOverBought ) )
            {
                count++;
                result += String.Format( "Exit Overbought", count );
            }
            else if ( input.HasFlag( TASignal.ExitOverSold ) )
            {
                count++;
                result += String.Format( "Exit Oversold", count );
            }

            if ( input.HasFlag( TASignal.GANN_PEAK ) )
            {
                count++;
                result += String.Format( "Gann Swing Peak", count );
            }
            else if ( input.HasFlag( TASignal.GANN_TROUGH ) )
            {
                count++;
                result += String.Format( "Gann Swing Trough", count );
            }

            if ( input.HasFlag( TASignal.WAVE_PEAK ) )
            {
                count++;
                result += String.Format( "Zig Zag Wave Peak", count );
            }
            else if ( input.HasFlag( TASignal.WAVE_TROUGH ) )
            {
                count++;
                result += String.Format( "Zig Zag Wave Trough", count );
            }

            return result;
        }


        public static string ToDescription( this TASignal signals )
        {
            return ( GetDataBarSignalHtml( signals ) );
        }

        public static string ToDescription( this TACandle signals )
        {
            return ( GetCandlestickHtml( signals ) );
        }

        public static int Count( this TACandle signals )
        {
            return ( GetCandlestickCount( signals ) );
        }

        public static string ToText( this TASignal signals )
        {
            return ( GetDataBarSignalText( signals ) );
        }

        //public static SymbolsEnum ToTradableSymbol( this string input )
        //{
        //    SymbolsEnum mysmbol;

        //    switch( input )
        //    {
        //        case "EUR/USD":
        //        mysmbol =  SymbolsEnum.EURUSD;                    
        //            break;

        //        case "CHF/JPY":
        //        mysmbol =  SymbolsEnum.CHFJPY;                    
        //            break;

        //        case "GBP/CHF":
        //            mysmbol =  SymbolsEnum.GBPCHF;                    
        //            break;

        //        case "EUR/AUD":
        //            mysmbol =  SymbolsEnum.EURAUD;                    
        //            break;

        //        case "EUR/CAD":
        //        mysmbol =  SymbolsEnum.EURCAD;                    
        //            break;
        //        
        //        case "AUD/CAD":
        //            mysmbol =  SymbolsEnum.AUDCAD;                    
        //            break;

        //        case "CAD/JPY":
        //        mysmbol =  SymbolsEnum.CADJPY;                    
        //            break;
        //        case "NZD/JPY":
        //            mysmbol =  SymbolsEnum.NZDJPY;                    
        //            break;

        //        case "GBP/CAD":
        //        mysmbol =  SymbolsEnum.GBPCAD;                    
        //            break;

        //        case "AUD/NZD":
        //            mysmbol =  SymbolsEnum.AUDNZD;
        //            
        //            break;
        //        case "USD/SEK":
        //            mysmbol =  SymbolsEnum.USDSEK;
        //            
        //            break;
        //        case "USD/DDK":
        //            mysmbol =  SymbolsEnum.USDDDK;
        //            
        //            break;
        //        case "EUR/SEK":
        //            mysmbol =  SymbolsEnum.EURSEK;                    
        //            break;

        //        case "EUR/NOK - Norway Krone":
        //            mysmbol =  SymbolsEnum.EURNOK;
        //            
        //            break;

        //        case "USD/NOK":
        //            mysmbol =  SymbolsEnum.USDNOK;
        //            
        //            break;
        //        case "USD/MXN":
        //            mysmbol =  SymbolsEnum.USDMXN;
        //            
        //            break;
        //        case "AUD/CHF":
        //            mysmbol =  SymbolsEnum.AUDCHF;
        //            
        //            break;
        //        case "EUR/NZD":
        //            mysmbol =  SymbolsEnum.EURNZD;
        //            
        //            break;
        //        case "EUR/PLN":
        //            mysmbol =  SymbolsEnum.EURPLN;
        //            
        //            break;
        //        case "USD/PLN":
        //            mysmbol =  SymbolsEnum.USDPLN;
        //            
        //            break;
        //        case "EUR/CZK":
        //            mysmbol =  SymbolsEnum.EURCZK;
        //            
        //            break;
        //        case "USD/CZK":
        //            mysmbol =  SymbolsEnum.USDCZK;
        //            
        //            break;
        //        case "USD/ZAR":
        //            mysmbol =  SymbolsEnum.USDZAR;                    
        //            break;

        //        mysmbol =  SymbolsEnum.USDSGD;
        //            case "USD/SGD";
        //            break;
        //        mysmbol =  SymbolsEnum.USDHKD;
        //            case "USD/HKD";
        //            break;
        //        mysmbol =  SymbolsEnum.EURDKK;
        //            case "EUR/DKK";
        //            break;
        //        mysmbol =  SymbolsEnum.GBPSEK;
        //            case "GBP/SEK";
        //            break;
        //        mysmbol =  SymbolsEnum.NOKJPY;
        //            case "NOK/JPY";
        //            break;
        //        mysmbol =  SymbolsEnum.SEKJPY;
        //            case "SEK/JPY";
        //            break;
        //        mysmbol =  SymbolsEnum.SGDJPY;
        //            case "SGD/JPY";
        //            break;
        //        mysmbol =  SymbolsEnum.HKDJPY;
        //            case "HKD/JPY";
        //            break;
        //        mysmbol =  SymbolsEnum.ZARJPY;
        //            case "ZAR/JPY";
        //            break;
        //        mysmbol =  SymbolsEnum.USDTRY;
        //            case "USD/TRY - Turkey Lira";
        //            break;
        //        mysmbol =  SymbolsEnum.EURTRY;
        //            case "EUR/TRY - Turkey Lira";
        //            break;
        //        mysmbol =  SymbolsEnum.NZDCHF;
        //            case "NZD/CHF";
        //            break;
        //        mysmbol =  SymbolsEnum.CADCHF;
        //            case "CAD/CHF";
        //            break;
        //        mysmbol =  SymbolsEnum.NZDCAD;
        //            case "NZD/CAD";
        //            break;
        //        mysmbol =  SymbolsEnum.CHFSEK;
        //            case "CHF/SEK";
        //            break;
        //        mysmbol =  SymbolsEnum.CHFNOK;
        //            case "CHF/NOK";
        //            break;
        //        mysmbol =  SymbolsEnum.EURHUF;
        //            case "EUR/HUF - Hungary Forint";
        //            break;
        //        mysmbol =  SymbolsEnum.USDHUF;
        //            case "USD/HUF - Hungary Forint";
        //            break;
        //        mysmbol =  SymbolsEnum.TRYJPY;
        //            case "TRY/JPY";
        //            break;
        //        mysmbol =  SymbolsEnum.GBPUSD;
        //            case "GBP/USD";
        //            break;
        //        mysmbol =  SymbolsEnum.USDCNH;
        //            case "USD/CNH";
        //            break;
        //        mysmbol =  SymbolsEnum.EURJPY;
        //            case "EUR/JPY";
        //            break;
        //        mysmbol =  SymbolsEnum.USDJPY;
        //            case "USD/JPY";
        //            break;
        //        mysmbol =  SymbolsEnum.GBPJPY;
        //            case "GBP/JPY";
        //            break;
        //        mysmbol =  SymbolsEnum.AUDJPY;
        //            case "AUD/JPY";
        //            break;
        //        mysmbol =  SymbolsEnum.USDCHF;
        //            case "USD/CHF";
        //            break;
        //        mysmbol =  SymbolsEnum.AUDUSD;
        //            case "AUD/USD";
        //            break;
        //        mysmbol =  SymbolsEnum.EURCHF;
        //            case "EUR/CHF";
        //            break;
        //        mysmbol =  SymbolsEnum.EURGBP;
        //            case "EUR/GBP";
        //            break;
        //        mysmbol =  SymbolsEnum.NZDUSD;
        //            case "NZD/USD";
        //            break;
        //        mysmbol =  SymbolsEnum.USDCAD;
        //            case "USD/CAD";
        //            break;
        //        mysmbol =  SymbolsEnum.GPBAUD;
        //            case "GPB/AUD";
        //            break;
        //        mysmbol =  SymbolsEnum.XAGUSD;
        //            case "Silver to USD";
        //            break;
        //        mysmbol =  SymbolsEnum.XAUUSD;
        //            case "Gold to USD";
        //            break;
        //        mysmbol =  SymbolsEnum.UK100;
        //            case "UK FTSE 100 index";
        //            break;
        //        mysmbol =  SymbolsEnum.USDOLLAR;
        //            case "US Dollar";
        //            break;
        //        mysmbol =  SymbolsEnum.GER30;
        //            case "Germany DAX index";
        //            break;
        //        mysmbol =  SymbolsEnum.FRA40;
        //            case "France CAC 40";
        //            break;
        //        mysmbol =  SymbolsEnum.AUS200;
        //            case "Australia ASX 200";
        //            break;
        //        mysmbol =  SymbolsEnum.ESP35;
        //            case "Spain IBEX 35";
        //            break;
        //        mysmbol =  SymbolsEnum.HKG33;
        //            case "Hong Kong Heng Seng Index";
        //            break;
        //        mysmbol =  SymbolsEnum.ITA40;
        //            case "Italy FTSE MIB";
        //            break;
        //        mysmbol =  SymbolsEnum.JPN225;
        //            case "Japanese Nikkei Index";
        //            break;
        //        mysmbol =  SymbolsEnum.NAS100;
        //            case "Nasdaq 100";
        //            break;
        //        mysmbol =  SymbolsEnum.SPX500;
        //            case "S&P 500";
        //            break;
        //        mysmbol =  SymbolsEnum.SUI20;
        //            case "Swiss Market Index";
        //            break;
        //        mysmbol =  SymbolsEnum.COPPER;
        //            case "Copper";
        //            break;
        //        mysmbol =  SymbolsEnum.EUSTX50;
        //            case "Euro STOXX 50";
        //            break;
        //        mysmbol =  SymbolsEnum.US30;
        //            case "USA Dow Jones Industrial Average";
        //            break;
        //        mysmbol =  SymbolsEnum.USOIL;
        //            case "USA Oil";
        //            break;
        //        mysmbol =  SymbolsEnum.UKOIL;
        //            case "London Oil";
        //            break;
        //        mysmbol =  SymbolsEnum.NGAS;
        //            case "Natural Gas";
        //            break;
        //        mysmbol =  SymbolsEnum.XPDUSD;
        //            case "Palladium";
        //            break;
        //        mysmbol =  SymbolsEnum.XPTUSD;
        //            case "Platinum";
        //            break;
        //        mysmbol =  SymbolsEnum.BUND;
        //            case "Germany Bond Index Fund";
        //            break;
        //    }

        //    return tradableSymbol;
        //
        //}


        public static BarPeriod ToPeriodEnum( this TimeSpan period )
        {
            BarPeriod sPeriodId = BarPeriod.m5;

            if ( period.Days == 30 )
            {
                sPeriodId = BarPeriod.M1;
            }
            else if ( period.Days == 7 )
            {
                sPeriodId = BarPeriod.W1;
            }
            else if ( period.Days == 1 )
            {
                sPeriodId = BarPeriod.D1;
            }
            else if ( period.Hours == 4 )
            {
                sPeriodId = BarPeriod.H4;
            }
            else if ( period.Hours == 3 )
            {
                sPeriodId = BarPeriod.H3;
            }
            else if ( period.Hours == 2 )
            {
                sPeriodId = BarPeriod.H2;
            }
            else if ( period.Hours == 1 )
            {
                sPeriodId = BarPeriod.H1;
            }
            else if ( period.Minutes == 1 )
            {
                sPeriodId = BarPeriod.m1;
            }
            else if ( period.Minutes == 5 )
            {
                sPeriodId = BarPeriod.m5;
            }
            else if ( period.Minutes == 15 )
            {
                sPeriodId = BarPeriod.m15;
            }
            else if ( period.Minutes == 30 )
            {
                sPeriodId = BarPeriod.m30;
            }
            else if ( period.Ticks == 1 )
            {
                sPeriodId = BarPeriod.t1;
            }

            return sPeriodId;
        }

        public static string ToDescription( this SymbolsEnum forex )
        {
            string tradableSymbol = "N/A";

            switch ( forex )
            {
                case SymbolsEnum.EURUSD:
                    tradableSymbol = "EUR/USD";
                    break;
                case SymbolsEnum.CHFJPY:
                    tradableSymbol = "CHF/JPY";
                    break;
                case SymbolsEnum.GBPCHF:
                    tradableSymbol = "GBP/CHF";
                    break;
                case SymbolsEnum.EURAUD:
                    tradableSymbol = "EUR/AUD";
                    break;
                case SymbolsEnum.EURCAD:
                    tradableSymbol = "EUR/CAD";
                    break;
                case SymbolsEnum.AUDCAD:
                    tradableSymbol = "AUD/CAD";
                    break;
                case SymbolsEnum.CADJPY:
                    tradableSymbol = "CAD/JPY";
                    break;
                case SymbolsEnum.NZDJPY:
                    tradableSymbol = "NZD/JPY";
                    break;
                case SymbolsEnum.GBPCAD:
                    tradableSymbol = "GBP/CAD";
                    break;
                case SymbolsEnum.AUDNZD:
                    tradableSymbol = "AUD/NZD";
                    break;
                case SymbolsEnum.USDSEK:
                    tradableSymbol = "USD/SEK - Swenden Krona";
                    break;
                case SymbolsEnum.USDDDK:
                    tradableSymbol = "USD/DDK";
                    break;
                case SymbolsEnum.EURSEK:
                    tradableSymbol = "EUR/SEK - Swenden Krona";
                    break;
                case SymbolsEnum.EURNOK:
                    tradableSymbol = "EUR/NOK - Norway Krone";
                    break;
                case SymbolsEnum.USDNOK:
                    tradableSymbol = "USD/NOK - Norway Krone";
                    break;
                case SymbolsEnum.USDMXN:
                    tradableSymbol = "USD/MXN - Mexican Peso";
                    break;
                case SymbolsEnum.AUDCHF:
                    tradableSymbol = "AUD/CHF";
                    break;
                case SymbolsEnum.EURNZD:
                    tradableSymbol = "EUR/NZD";
                    break;
                case SymbolsEnum.EURPLN:
                    tradableSymbol = "EUR/PLN - Poland Zloty";
                    break;
                case SymbolsEnum.USDPLN:
                    tradableSymbol = "USD/PLN - Poland Zloty";
                    break;
                case SymbolsEnum.EURCZK:
                    tradableSymbol = "EUR/CZK - Czech Republic Koruna";
                    break;
                case SymbolsEnum.USDCZK:
                    tradableSymbol = "USD/CZK - Czech Republic Koruna";
                    break;
                case SymbolsEnum.USDZAR:
                    tradableSymbol = "USD/ZAR - South Africa Rand";
                    break;
                case SymbolsEnum.USDSGD:
                    tradableSymbol = "USD/SGD";
                    break;
                case SymbolsEnum.USDHKD:
                    tradableSymbol = "USD/HKD";
                    break;
                case SymbolsEnum.EURDKK:
                    tradableSymbol = "EUR/DKK";
                    break;
                case SymbolsEnum.GBPSEK:
                    tradableSymbol = "GBP/SEK";
                    break;
                case SymbolsEnum.NOKJPY:
                    tradableSymbol = "NOK/JPY";
                    break;
                case SymbolsEnum.SEKJPY:
                    tradableSymbol = "SEK/JPY";
                    break;
                case SymbolsEnum.SGDJPY:
                    tradableSymbol = "SGD/JPY";
                    break;
                case SymbolsEnum.HKDJPY:
                    tradableSymbol = "HKD/JPY";
                    break;
                case SymbolsEnum.ZARJPY:
                    tradableSymbol = "ZAR/JPY";
                    break;
                case SymbolsEnum.USDTRY:
                    tradableSymbol = "USD/TRY - Turkey Lira";
                    break;
                case SymbolsEnum.EURTRY:
                    tradableSymbol = "EUR/TRY - Turkey Lira";
                    break;
                case SymbolsEnum.NZDCHF:
                    tradableSymbol = "NZD/CHF";
                    break;
                case SymbolsEnum.CADCHF:
                    tradableSymbol = "CAD/CHF";
                    break;
                case SymbolsEnum.NZDCAD:
                    tradableSymbol = "NZD/CAD";
                    break;
                case SymbolsEnum.CHFSEK:
                    tradableSymbol = "CHF/SEK";
                    break;
                case SymbolsEnum.CHFNOK:
                    tradableSymbol = "CHF/NOK";
                    break;
                case SymbolsEnum.EURHUF:
                    tradableSymbol = "EUR/HUF - Hungary Forint";
                    break;
                case SymbolsEnum.USDHUF:
                    tradableSymbol = "USD/HUF - Hungary Forint";
                    break;
                case SymbolsEnum.TRYJPY:
                    tradableSymbol = "TRY/JPY";
                    break;
                case SymbolsEnum.GBPUSD:
                    tradableSymbol = "GBP/USD";
                    break;
                case SymbolsEnum.USDCNH:
                    tradableSymbol = "USD/CNH";
                    break;
                case SymbolsEnum.EURJPY:
                    tradableSymbol = "EUR/JPY";
                    break;
                case SymbolsEnum.USDJPY:
                    tradableSymbol = "USD/JPY";
                    break;
                case SymbolsEnum.GBPJPY:
                    tradableSymbol = "GBP/JPY";
                    break;
                case SymbolsEnum.AUDJPY:
                    tradableSymbol = "AUD/JPY";
                    break;
                case SymbolsEnum.USDCHF:
                    tradableSymbol = "USD/CHF";
                    break;
                case SymbolsEnum.AUDUSD:
                    tradableSymbol = "AUD/USD";
                    break;
                case SymbolsEnum.EURCHF:
                    tradableSymbol = "EUR/CHF";
                    break;
                case SymbolsEnum.EURGBP:
                    tradableSymbol = "EUR/GBP";
                    break;
                case SymbolsEnum.NZDUSD:
                    tradableSymbol = "NZD/USD";
                    break;
                case SymbolsEnum.USDCAD:
                    tradableSymbol = "USD/CAD";
                    break;
                case SymbolsEnum.GBPAUD:
                    tradableSymbol = "GBP/AUD";
                    break;
                case SymbolsEnum.XAGUSD:
                    tradableSymbol = "Silver to USD";
                    break;
                case SymbolsEnum.XAUUSD:
                    tradableSymbol = "Gold to USD";
                    break;
                case SymbolsEnum.UK100:
                    tradableSymbol = "UK FTSE 100 index";
                    break;
                case SymbolsEnum.USDOLLAR:
                    tradableSymbol = "US Dollar";
                    break;
                case SymbolsEnum.GER30:
                    tradableSymbol = "Germany DAX index";
                    break;
                case SymbolsEnum.FRA40:
                    tradableSymbol = "France CAC 40";
                    break;
                case SymbolsEnum.AUS200:
                    tradableSymbol = "Australia ASX 200";
                    break;
                case SymbolsEnum.ESP35:
                    tradableSymbol = "Spain IBEX 35";
                    break;
                case SymbolsEnum.HKG33:
                    tradableSymbol = "Hong Kong Heng Seng Index";
                    break;
                case SymbolsEnum.ITA40:
                    tradableSymbol = "Italy FTSE MIB";
                    break;
                case SymbolsEnum.JPN225:
                    tradableSymbol = "Japanese Nikkei Index";
                    break;
                case SymbolsEnum.NAS100:
                    tradableSymbol = "Nasdaq 100";
                    break;
                case SymbolsEnum.SPX500:
                    tradableSymbol = "S&P 500";
                    break;
                case SymbolsEnum.SUI20:
                    tradableSymbol = "Swiss Market Index";
                    break;
                case SymbolsEnum.COPPER:
                    tradableSymbol = "Copper";
                    break;
                case SymbolsEnum.EUSTX50:
                    tradableSymbol = "Euro STOXX 50";
                    break;
                case SymbolsEnum.US30:
                    tradableSymbol = "USA Dow Jones Industrial Average";
                    break;
                case SymbolsEnum.USOIL:
                    tradableSymbol = "USA Oil";
                    break;
                case SymbolsEnum.UKOIL:
                    tradableSymbol = "London Oil";
                    break;
                case SymbolsEnum.NGAS:
                    tradableSymbol = "Natural Gas";
                    break;
                case SymbolsEnum.XPDUSD:
                    tradableSymbol = "Palladium";
                    break;
                case SymbolsEnum.XPTUSD:
                    tradableSymbol = "Platinum";
                    break;
                case SymbolsEnum.BUND:
                    tradableSymbol = "Germany Bond Index Fund";
                    break;
            }

            return tradableSymbol;
        }

        public static string ToTradeSymbol( this SymbolsEnum forex )
        {
            string tradableSymbol = "N/A";

            switch ( forex )
            {
                case SymbolsEnum.EURUSD:
                    tradableSymbol = "EUR/USD";
                    break;
                case SymbolsEnum.CHFJPY:
                    tradableSymbol = "CHF/JPY";
                    break;
                case SymbolsEnum.GBPCHF:
                    tradableSymbol = "GBP/CHF";
                    break;
                case SymbolsEnum.EURAUD:
                    tradableSymbol = "EUR/AUD";
                    break;
                case SymbolsEnum.EURCAD:
                    tradableSymbol = "EUR/CAD";
                    break;
                case SymbolsEnum.AUDCAD:
                    tradableSymbol = "AUD/CAD";
                    break;
                case SymbolsEnum.CADJPY:
                    tradableSymbol = "CAD/JPY";
                    break;
                case SymbolsEnum.NZDJPY:
                    tradableSymbol = "NZD/JPY";
                    break;
                case SymbolsEnum.GBPCAD:
                    tradableSymbol = "GBP/CAD";
                    break;
                case SymbolsEnum.AUDNZD:
                    tradableSymbol = "AUD/NZD";
                    break;
                case SymbolsEnum.USDSEK:
                    tradableSymbol = "USD/SEK";
                    break;
                case SymbolsEnum.USDDDK:
                    tradableSymbol = "USD/DDK";
                    break;
                case SymbolsEnum.EURSEK:
                    tradableSymbol = "EUR/SEK";
                    break;
                case SymbolsEnum.EURNOK:
                    tradableSymbol = "EUR/NOK";
                    break;
                case SymbolsEnum.USDNOK:
                    tradableSymbol = "USD/NOK";
                    break;
                case SymbolsEnum.USDMXN:
                    tradableSymbol = "USD/MXN";
                    break;
                case SymbolsEnum.AUDCHF:
                    tradableSymbol = "AUD/CHF";
                    break;
                case SymbolsEnum.EURNZD:
                    tradableSymbol = "EUR/NZD";
                    break;
                case SymbolsEnum.EURPLN:
                    tradableSymbol = "EUR/PLN";
                    break;
                case SymbolsEnum.USDPLN:
                    tradableSymbol = "USD/PLN";
                    break;
                case SymbolsEnum.EURCZK:
                    tradableSymbol = "EUR/CZK";
                    break;
                case SymbolsEnum.USDCZK:
                    tradableSymbol = "USD/CZK";
                    break;
                case SymbolsEnum.USDZAR:
                    tradableSymbol = "USD/ZAR";
                    break;
                case SymbolsEnum.USDSGD:
                    tradableSymbol = "USD/SGD";
                    break;
                case SymbolsEnum.USDHKD:
                    tradableSymbol = "USD/HKD";
                    break;
                case SymbolsEnum.EURDKK:
                    tradableSymbol = "EUR/DKK";
                    break;
                case SymbolsEnum.GBPSEK:
                    tradableSymbol = "GBP/SEK";
                    break;
                case SymbolsEnum.NOKJPY:
                    tradableSymbol = "NOK/JPY";
                    break;
                case SymbolsEnum.SEKJPY:
                    tradableSymbol = "SEK/JPY";
                    break;
                case SymbolsEnum.SGDJPY:
                    tradableSymbol = "SGD/JPY";
                    break;
                case SymbolsEnum.HKDJPY:
                    tradableSymbol = "HKD/JPY";
                    break;
                case SymbolsEnum.ZARJPY:
                    tradableSymbol = "ZAR/JPY";
                    break;
                case SymbolsEnum.USDTRY:
                    tradableSymbol = "USD/TRY";
                    break;
                case SymbolsEnum.EURTRY:
                    tradableSymbol = "EUR/TRY";
                    break;
                case SymbolsEnum.NZDCHF:
                    tradableSymbol = "NZD/CHF";
                    break;
                case SymbolsEnum.CADCHF:
                    tradableSymbol = "CAD/CHF";
                    break;
                case SymbolsEnum.NZDCAD:
                    tradableSymbol = "NZD/CAD";
                    break;
                case SymbolsEnum.CHFSEK:
                    tradableSymbol = "CHF/SEK";
                    break;
                case SymbolsEnum.CHFNOK:
                    tradableSymbol = "CHF/NOK";
                    break;
                case SymbolsEnum.EURHUF:
                    tradableSymbol = "EUR/HUF";
                    break;
                case SymbolsEnum.USDHUF:
                    tradableSymbol = "USD/HUF";
                    break;
                case SymbolsEnum.TRYJPY:
                    tradableSymbol = "TRY/JPY";
                    break;
                case SymbolsEnum.GBPUSD:
                    tradableSymbol = "GBP/USD";
                    break;
                case SymbolsEnum.USDCNH:
                    tradableSymbol = "USD/CNH";
                    break;
                case SymbolsEnum.EURJPY:
                    tradableSymbol = "EUR/JPY";
                    break;
                case SymbolsEnum.USDJPY:
                    tradableSymbol = "USD/JPY";
                    break;
                case SymbolsEnum.GBPJPY:
                    tradableSymbol = "GBP/JPY";
                    break;
                case SymbolsEnum.AUDJPY:
                    tradableSymbol = "AUD/JPY";
                    break;
                case SymbolsEnum.USDCHF:
                    tradableSymbol = "USD/CHF";
                    break;
                case SymbolsEnum.AUDUSD:
                    tradableSymbol = "AUD/USD";
                    break;
                case SymbolsEnum.EURCHF:
                    tradableSymbol = "EUR/CHF";
                    break;
                case SymbolsEnum.EURGBP:
                    tradableSymbol = "EUR/GBP";
                    break;
                case SymbolsEnum.NZDUSD:
                    tradableSymbol = "NZD/USD";
                    break;
                case SymbolsEnum.USDCAD:
                    tradableSymbol = "USD/CAD";
                    break;
                case SymbolsEnum.GBPAUD:
                    tradableSymbol = "GBP/AUD";
                    break;
                case SymbolsEnum.XAGUSD:
                    tradableSymbol = "XAG/USD";
                    break;
                case SymbolsEnum.XAUUSD:
                    tradableSymbol = "XAU/USD";
                    break;
                case SymbolsEnum.UK100:
                    tradableSymbol = "UK100";
                    break;
                case SymbolsEnum.USDOLLAR:
                    tradableSymbol = "USDOLLAR";
                    break;
                case SymbolsEnum.GER30:
                    tradableSymbol = "GER30";
                    break;
                case SymbolsEnum.FRA40:
                    tradableSymbol = "FRA40";
                    break;
                case SymbolsEnum.AUS200:
                    tradableSymbol = "AUS200";
                    break;
                case SymbolsEnum.ESP35:
                    tradableSymbol = "ESP35";
                    break;
                case SymbolsEnum.HKG33:
                    tradableSymbol = "HKG33";
                    break;
                case SymbolsEnum.ITA40:
                    tradableSymbol = "ITA40";
                    break;
                case SymbolsEnum.JPN225:
                    tradableSymbol = "JPN225";
                    break;
                case SymbolsEnum.NAS100:
                    tradableSymbol = "NAS100";
                    break;
                case SymbolsEnum.SPX500:
                    tradableSymbol = "SPX500";
                    break;
                case SymbolsEnum.SUI20:
                    tradableSymbol = "SUI20";
                    break;
                case SymbolsEnum.COPPER:
                    tradableSymbol = "COPPER";
                    break;
                case SymbolsEnum.EUSTX50:
                    tradableSymbol = "EUSTX50";
                    break;
                case SymbolsEnum.US30:
                    tradableSymbol = "US30";
                    break;
                case SymbolsEnum.USOIL:
                    tradableSymbol = "USOIL";
                    break;
                case SymbolsEnum.UKOIL:
                    tradableSymbol = "UKOIL";
                    break;
                case SymbolsEnum.NGAS:
                    tradableSymbol = "NGAS";
                    break;
                case SymbolsEnum.XPDUSD:
                    tradableSymbol = "XPD/USD";
                    break;
                case SymbolsEnum.XPTUSD:
                    tradableSymbol = "XPT/USD";
                    break;
                case SymbolsEnum.BUND:
                    tradableSymbol = "BUND";
                    break;
            }

            return tradableSymbol;
        }


        public static bool IsForexPair( this SymbolsEnum forex )
        {
            bool isForex = false;

            switch ( forex )
            {
                case SymbolsEnum.EURUSD:
                    isForex = true;
                    break;
                case SymbolsEnum.CHFJPY:
                    isForex = true;
                    break;
                case SymbolsEnum.GBPCHF:
                    isForex = true;
                    break;
                case SymbolsEnum.EURAUD:
                    isForex = true;
                    break;
                case SymbolsEnum.EURCAD:
                    isForex = true;
                    break;
                case SymbolsEnum.AUDCAD:
                    isForex = true;
                    break;
                case SymbolsEnum.CADJPY:
                    isForex = true;
                    break;
                case SymbolsEnum.NZDJPY:
                    isForex = true;
                    break;
                case SymbolsEnum.GBPCAD:
                    isForex = true;
                    break;
                case SymbolsEnum.AUDNZD:
                    isForex = true;
                    break;
                case SymbolsEnum.USDSEK:
                    isForex = true;
                    break;
                case SymbolsEnum.USDDDK:
                    isForex = true;
                    break;
                case SymbolsEnum.EURSEK:
                    isForex = true;
                    break;
                case SymbolsEnum.EURNOK:
                    isForex = true;
                    break;
                case SymbolsEnum.USDNOK:
                    isForex = true;
                    break;
                case SymbolsEnum.USDMXN:
                    isForex = true;
                    break;
                case SymbolsEnum.AUDCHF:
                    isForex = true;
                    break;
                case SymbolsEnum.EURNZD:
                    isForex = true;
                    break;
                case SymbolsEnum.EURPLN:
                    isForex = true;
                    break;
                case SymbolsEnum.USDPLN:
                    isForex = true;
                    break;
                case SymbolsEnum.EURCZK:
                    isForex = true;
                    break;
                case SymbolsEnum.USDCZK:
                    isForex = true;
                    break;
                case SymbolsEnum.USDZAR:
                    isForex = true;
                    break;
                case SymbolsEnum.USDSGD:
                    isForex = true;
                    break;
                case SymbolsEnum.USDHKD:
                    isForex = true;
                    break;
                case SymbolsEnum.EURDKK:
                    isForex = true;
                    break;
                case SymbolsEnum.GBPSEK:
                    isForex = true;
                    break;
                case SymbolsEnum.NOKJPY:
                    isForex = true;
                    break;
                case SymbolsEnum.SEKJPY:
                    isForex = true;
                    break;
                case SymbolsEnum.SGDJPY:
                    isForex = true;
                    break;
                case SymbolsEnum.HKDJPY:
                    isForex = true;
                    break;
                case SymbolsEnum.ZARJPY:
                    isForex = true;
                    break;
                case SymbolsEnum.USDTRY:
                    isForex = true;
                    break;
                case SymbolsEnum.EURTRY:
                    isForex = true;
                    break;
                case SymbolsEnum.NZDCHF:
                    isForex = true;
                    break;
                case SymbolsEnum.CADCHF:
                    isForex = true;
                    break;
                case SymbolsEnum.NZDCAD:
                    isForex = true;
                    break;
                case SymbolsEnum.CHFSEK:
                    isForex = true;
                    break;
                case SymbolsEnum.CHFNOK:
                    isForex = true;
                    break;
                case SymbolsEnum.EURHUF:
                    isForex = true;
                    break;
                case SymbolsEnum.USDHUF:
                    isForex = true;
                    break;
                case SymbolsEnum.TRYJPY:
                    isForex = true;
                    break;
                case SymbolsEnum.GBPUSD:
                    isForex = true;
                    break;
                case SymbolsEnum.USDCNH:
                    isForex = true;
                    break;
                case SymbolsEnum.EURJPY:
                    isForex = true;
                    break;
                case SymbolsEnum.USDJPY:
                    isForex = true;
                    break;
                case SymbolsEnum.GBPJPY:
                    isForex = true;
                    break;
                case SymbolsEnum.AUDJPY:
                    isForex = true;
                    break;
                case SymbolsEnum.USDCHF:
                    isForex = true;
                    break;
                case SymbolsEnum.AUDUSD:
                    isForex = true;
                    break;
                case SymbolsEnum.EURCHF:
                    isForex = true;
                    break;
                case SymbolsEnum.EURGBP:
                    isForex = true;
                    break;
                case SymbolsEnum.NZDUSD:
                    isForex = true;
                    break;
                case SymbolsEnum.USDCAD:
                    isForex = true;
                    break;
                case SymbolsEnum.GBPAUD:
                    isForex = true;
                    break;
                case SymbolsEnum.XAGUSD:
                    isForex = false;
                    break;
                case SymbolsEnum.XAUUSD:
                    isForex = false;
                    break;
                case SymbolsEnum.UK100:
                    isForex = false;
                    break;
                case SymbolsEnum.USDOLLAR:
                    isForex = false;
                    break;
                case SymbolsEnum.GER30:
                    isForex = false;
                    break;
                case SymbolsEnum.FRA40:
                    isForex = false;
                    break;
                case SymbolsEnum.AUS200:
                    isForex = false;
                    break;
                case SymbolsEnum.ESP35:
                    isForex = false;
                    break;
                case SymbolsEnum.HKG33:
                    isForex = false;
                    break;
                case SymbolsEnum.ITA40:
                    isForex = false;
                    break;
                case SymbolsEnum.JPN225:
                    isForex = false;
                    break;
                case SymbolsEnum.NAS100:
                    isForex = false;
                    break;
                case SymbolsEnum.SPX500:
                    isForex = false;
                    break;
                case SymbolsEnum.SUI20:
                    isForex = false;
                    break;
                case SymbolsEnum.COPPER:
                    isForex = false;
                    break;
                case SymbolsEnum.EUSTX50:
                    isForex = false;
                    break;
                case SymbolsEnum.US30:
                    isForex = false;
                    break;
                case SymbolsEnum.USOIL:
                    isForex = false;
                    break;
                case SymbolsEnum.UKOIL:
                    isForex = false;
                    break;
                case SymbolsEnum.NGAS:
                    isForex = false;
                    break;
                case SymbolsEnum.XPDUSD:
                    isForex = false;
                    break;
                case SymbolsEnum.XPTUSD:
                    isForex = false;
                    break;
                case SymbolsEnum.BUND:
                    isForex = false;
                    break;
            }

            return isForex;
        }


    }

}
