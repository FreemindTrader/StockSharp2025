//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Media.Imaging;

//namespace FreemindAITrade.Helpers
//{
//    public static class SymbolImageHelper
//    {
//        public static BitmapImage GetImageSourece( string symbol )
//        {
//            var secWithout = symbol.Replace( @"/", "" );

//            BitmapImage bitmap = null;

//            switch ( secWithout )
//            {
//                case "AUDCAD":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/AUDCAD.png", UriKind.Absolute ) );
//                }
//                break;

//                case "AUDCHF":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/AUDCHF.png", UriKind.Absolute ) );
//                }
//                break;

//                case "AUDJPY":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/AUDJPY.png", UriKind.Absolute ) );
//                }
//                break;

//                case "AUDNZD":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/AUDNZD.png", UriKind.Absolute ) );
//                }
//                break;

//                case "AUDUSD":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/AUDUSD.png", UriKind.Absolute ) );
//                }
//                break;

//                case "AUS200":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/AUS200.png", UriKind.Absolute ) );
//                }
//                break;

//                case "BTCUSD":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/BTCUSD.png", UriKind.Absolute ) );
//                }
//                break;

//                case "BUND":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/BUND.png", UriKind.Absolute ) );
//                }
//                break;

//                case "CADCHF":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/CADCHF.png", UriKind.Absolute ) );
//                }
//                break;

//                case "CADJPY":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/CADJPY.png", UriKind.Absolute ) );
//                }
//                break;

//                case "CHFJPY":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/CHFJPY.png", UriKind.Absolute ) );
//                }
//                break;

//                case "CHN50":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/CHN50.png", UriKind.Absolute ) );
//                }
//                break;

//                case "COPPER":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/COPPER.png", UriKind.Absolute ) );
//                }
//                break;

//                case "ESP35":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/ESP35.png", UriKind.Absolute ) );
//                }
//                break;

//                case "EURAUD":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/EURAUD.png", UriKind.Absolute ) );
//                }
//                break;

//                case "EURCAD":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/EURCAD.png", UriKind.Absolute ) );
//                }
//                break;

//                case "EURCHF":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/EURCHF.png", UriKind.Absolute ) );
//                }
//                break;

//                case "EURCZK":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/EURCZK.png", UriKind.Absolute ) );
//                }
//                break;

//                case "EURGBP":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/EURGBP.png", UriKind.Absolute ) );
//                }
//                break;

//                case "EURJPY":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/EURJPY.png", UriKind.Absolute ) );
//                }
//                break;

//                case "EURNOK":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/EURNOK.png", UriKind.Absolute ) );
//                }
//                break;

//                case "EURNZD":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/EURNZD.png", UriKind.Absolute ) );
//                }
//                break;

//                case "EURSEK":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/EURSEK.png", UriKind.Absolute ) );
//                }
//                break;

//                case "EURTRY":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/EURTRY.png", UriKind.Absolute ) );
//                }
//                break;

//                case "EURUSD":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/EURUSD.png", UriKind.Absolute ) );
//                }
//                break;

//                case "EUSTX50":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/EUSTX50.png", UriKind.Absolute ) );
//                }
//                break;

//                case "FRA40":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/FRA40.png", UriKind.Absolute ) );
//                }
//                break;

//                case "GBPAUD":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/GBPAUD.png", UriKind.Absolute ) );
//                }
//                break;

//                case "GBPCAD":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/GBPCAD.png", UriKind.Absolute ) );
//                }
//                break;

//                case "GBPCHF":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/GBPCHF.png", UriKind.Absolute ) );
//                }
//                break;

//                case "GBPJPY":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/GBPJPY.png", UriKind.Absolute ) );
//                }
//                break;

//                case "GBPNZD":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/GBPNZD.png", UriKind.Absolute ) );
//                }
//                break;

//                case "GBPUSD":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/GBPUSD.png", UriKind.Absolute ) );
//                }
//                break;

//                case "GER30":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/GER30.png", UriKind.Absolute ) );
//                }
//                break;

//                case "HKG33":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/HKG33.png", UriKind.Absolute ) );
//                }
//                break;

//                case "ITA40":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/ITA40.png", UriKind.Absolute ) );
//                }
//                break;

//                case "JPN225":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/JPN225.png", UriKind.Absolute ) );
//                }
//                break;

//                case "NAS100":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/NAS100.png", UriKind.Absolute ) );
//                }
//                break;

//                case "NGAS":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/NGAS.png", UriKind.Absolute ) );
//                }
//                break;

//                case "NZDCAD":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/NZDCAD.png", UriKind.Absolute ) );
//                }
//                break;

//                case "NZDCHF":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/NZDCHF.png", UriKind.Absolute ) );
//                }
//                break;

//                case "NZDUSD":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/NZDUSD.png", UriKind.Absolute ) );
//                }
//                break;

//                case "SPX500":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/SPX500.png", UriKind.Absolute ) );
//                }
//                break;

//                case "SUI30":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/SUI30.png", UriKind.Absolute ) );

//                }
//                break;

//                case "TRYJPY":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/TRYJPY.png", UriKind.Absolute ) );
//                }
//                break;

//                case "UK100":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/UK100.png", UriKind.Absolute ) );
//                }
//                break;

//                case "UKOIL":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/UKOIL.png", UriKind.Absolute ) );
//                }
//                break;

//                case "US30":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/US30.png", UriKind.Absolute ) );
//                }
//                break;

//                case "USDCAD":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/USDCAD.png", UriKind.Absolute ) );
//                }
//                break;

//                case "USDCHF":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/USDCHF.png", UriKind.Absolute ) );
//                }
//                break;

//                case "USDCNH":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/USDCNH.png", UriKind.Absolute ) );
//                }
//                break;

//                case "USDCZK":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/USDCZK.png", UriKind.Absolute ) );
//                }
//                break;

//                case "USDHKD":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/USDHKD.png", UriKind.Absolute ) );
//                }
//                break;

//                case "USDJPY":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/USDJPY.png", UriKind.Absolute ) );
//                }
//                break;

//                case "USDMXN":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/USDMXN.png", UriKind.Absolute ) );

//                }
//                break;

//                case "USDNOK":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/USDNOK.png", UriKind.Absolute ) );
//                }
//                break;

//                case "USDOLLAR":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/USDOLLAR.png", UriKind.Absolute ) );
//                }
//                break;

//                case "USDSEK":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/USDSEK.png", UriKind.Absolute ) );
//                }
//                break;

//                case "USDTRY":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/USDTRY.png", UriKind.Absolute ) );
//                }
//                break;

//                case "USDZAR":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/USDZAR.png", UriKind.Absolute ) );
//                }
//                break;

//                case "USOIL":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/USOIL.png", UriKind.Absolute ) );
//                }
//                break;

//                case "XAGUSD":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/XAGUSD.png", UriKind.Absolute ) );
//                }
//                break;

//                case "XAUUSD":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/XAUUSD.png", UriKind.Absolute ) );
//                }
//                break;

//                case "XPDUSD":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/XPDUSD.png", UriKind.Absolute ) );
//                }
//                break;

//                case "XPTUSD":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/XPTUSD.png", UriKind.Absolute ) );
//                }
//                break;

//                case "ZARJPY":
//                {
//                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/FreemindAITrade;component/Images/ZARJPY.png", UriKind.Absolute ) );
//                }
//                break;
//            }


//            return bitmap;
//        }
//    }
//}
