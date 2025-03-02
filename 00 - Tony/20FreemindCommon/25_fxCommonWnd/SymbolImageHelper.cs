using StockSharp.BusinessEntities;
using StockSharp.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace fx.Common
{
    public static class SymbolImageHelper
    {
        /*
         * 
         * Tony ： Uncheck Indent case content ( when Block ) for Code Formating will line up the open and close Braces for Case Block.
         * 
         * 
         */
        public static BitmapImage GetImageSourece( Security symbol )
        {
            BitmapImage bitmap = null;

            switch ( symbol.Code )
            {
                case "AUD/CAD":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/AUDCAD.png", UriKind.Absolute ) );
                }
                break;

                case "AUD/CHF":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/AUDCHF.png", UriKind.Absolute ) );
                }
                break;

                case "AUD/JPY":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/AUDJPY.png", UriKind.Absolute ) );
                }
                break;

                case "AUD/NZD":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/AUDNZD.png", UriKind.Absolute ) );
                }
                break;

                case "AUD/USD":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/AUDUSD.png", UriKind.Absolute ) );
                }
                break;

                case "AUS200":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/AUS200.png", UriKind.Absolute ) );
                }
                break;

                case "BTC/USD":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/BTCUSD.png", UriKind.Absolute ) );
                }
                break;

                case "BUND":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/BUND.png", UriKind.Absolute ) );
                }
                break;

                case "CAD/CHF":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/CADCHF.png", UriKind.Absolute ) );
                }
                break;

                case "CAD/JPY":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/CADJPY.png", UriKind.Absolute ) );
                }
                break;

                case "CHF/JPY":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/CHFJPY.png", UriKind.Absolute ) );
                }
                break;

                case "CHN50":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/CHN50.png", UriKind.Absolute ) );
                }
                break;

                case "COPPER":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/COPPER.png", UriKind.Absolute ) );
                }
                break;

                case "ESP35":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/ESP35.png", UriKind.Absolute ) );
                }
                break;

                case "EUR/AUD":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/EURAUD.png", UriKind.Absolute ) );
                }
                break;

                case "EUR/CAD":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/EURCAD.png", UriKind.Absolute ) );
                }
                break;

                case "EUR/CHF":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/EURCHF.png", UriKind.Absolute ) );
                }
                break;

                case "EUR/CZK":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/EURCZK.png", UriKind.Absolute ) );
                }
                break;

                case "EUR/GBP":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/EURGBP.png", UriKind.Absolute ) );
                }
                break;

                case "EUR/JPY":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/EURJPY.png", UriKind.Absolute ) );
                }
                break;

                case "EUR/NOK":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/EURNOK.png", UriKind.Absolute ) );
                }
                break;

                case "EUR/NZD":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/EURNZD.png", UriKind.Absolute ) );
                }
                break;

                case "EUR/SEK":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/EURSEK.png", UriKind.Absolute ) );
                }
                break;

                case "EUR/TRY":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/EURTRY.png", UriKind.Absolute ) );
                }
                break;

                case "EUR/USD":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/EURUSD.png", UriKind.Absolute ) );
                }
                break;

                case "EUSTX50":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/EUSTX50.png", UriKind.Absolute ) );
                }
                break;

                case "FRA40":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/FRA40.png", UriKind.Absolute ) );
                }
                break;

                case "GBP/AUD":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/GBPAUD.png", UriKind.Absolute ) );
                }
                break;

                case "GBP/CAD":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/GBPCAD.png", UriKind.Absolute ) );
                }
                break;

                case "GBP/CHF":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/GBPCHF.png", UriKind.Absolute ) );
                }
                break;

                case "GBP/JPY":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/GBPJPY.png", UriKind.Absolute ) );
                }
                break;

                case "GBP/NZD":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/GBPNZD.png", UriKind.Absolute ) );
                }
                break;

                case "GBP/USD":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/GBPUSD.png", UriKind.Absolute ) );
                }
                break;

                case "GER30":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/GER30.png", UriKind.Absolute ) );
                }
                break;

                case "HKG33":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/HKG33.png", UriKind.Absolute ) );
                }
                break;

                case "ITA40":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/ITA40.png", UriKind.Absolute ) );
                }
                break;

                case "JPN225":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/JPN225.png", UriKind.Absolute ) );
                }
                break;

                case "NAS100":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/NAS100.png", UriKind.Absolute ) );
                }
                break;

                case "NGAS":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/NGAS.png", UriKind.Absolute ) );
                }
                break;

                case "NZD/CAD":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/NZDCAD.png", UriKind.Absolute ) );
                }
                break;

                case "NZD/CHF":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/NZDCHF.png", UriKind.Absolute ) );
                }
                break;

                case "NZD/USD":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/NZDUSD.png", UriKind.Absolute ) );
                }
                break;

                case "SPX500":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/SPX500.png", UriKind.Absolute ) );
                }
                break;

                case "SUI30":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/SUI30.png", UriKind.Absolute ) );

                }
                break;

                case "TRY/JPY":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/TRYJPY.png", UriKind.Absolute ) );
                }
                break;

                case "UK100":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/UK100.png", UriKind.Absolute ) );
                }
                break;

                case "UKOIL":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/UKOIL.png", UriKind.Absolute ) );
                }
                break;

                case "US30":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/US30.png", UriKind.Absolute ) );
                }
                break;

                case "USD/CAD":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/USDCAD.png", UriKind.Absolute ) );
                }
                break;

                case "USD/CHF":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/USDCHF.png", UriKind.Absolute ) );
                }
                break;

                case "USD/CNH":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/USDCNH.png", UriKind.Absolute ) );
                }
                break;

                case "USD/CZK":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/USDCZK.png", UriKind.Absolute ) );
                }
                break;

                case "USD/HKD":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/USDHKD.png", UriKind.Absolute ) );
                }
                break;

                case "USD/JPY":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/USDJPY.png", UriKind.Absolute ) );
                }
                break;

                case "USD/MXN":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/USDMXN.png", UriKind.Absolute ) );

                }
                break;

                case "USD/NOK":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/USDNOK.png", UriKind.Absolute ) );
                }
                break;

                case "USDOLLAR":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/USDOLLAR.png", UriKind.Absolute ) );
                }
                break;

                case "USD/SEK":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/USDSEK.png", UriKind.Absolute ) );
                }
                break;

                case "USD/TRY":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/USDTRY.png", UriKind.Absolute ) );
                }
                break;

                case "USD/ZAR":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/USDZAR.png", UriKind.Absolute ) );
                }
                break;

                case "USOIL":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/USOIL.png", UriKind.Absolute ) );
                }
                break;

                case "XAG/USD":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/XAGUSD.png", UriKind.Absolute ) );
                }
                break;

                case "XAU/USD":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/XAUUSD.png", UriKind.Absolute ) );
                }
                break;

                case "XPD/USD":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/XPDUSD.png", UriKind.Absolute ) );
                }
                break;

                case "XPT/USD":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/XPTUSD.png", UriKind.Absolute ) );
                }
                break;

                case "ZAR/JPY":
                {
                    bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/ZARJPY.png", UriKind.Absolute ) );
                }
                break;

                default:
                {
                    if ( symbol.Type.HasValue )
                    {
                        var symType = symbol.Type.Value;
                        switch ( symType )
                        {
                            /// <summary>
                            /// Shares.
                            /// </summary>
                            case SecurityTypes.Stock:
                            {
                                bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/Stock.png", UriKind.Absolute ) );
                            }
                            break;

                            /// <summary>
                            /// Future contract.
                            /// </summary>
                            case SecurityTypes.Future:
                            {
                                bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/Future.png", UriKind.Absolute ) );
                            }
                            break;

                            /// <summary>
                            /// Options contract.
                            /// </summary>
                            case SecurityTypes.Option:
                            {
                                bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/Option.png", UriKind.Absolute ) );
                            }
                            break;

                            /// <summary>
                            /// Index.
                            /// </summary>
                            case SecurityTypes.Index:
                            {
                                bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/Index.png", UriKind.Absolute ) );
                            }
                            break;

                            /// <summary>
                            /// Currency.
                            /// </summary>
                            case SecurityTypes.Currency:
                            {
                                bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/Currency.png", UriKind.Absolute ) );
                            }
                            break;

                            /// <summary>
                            /// Bond.
                            /// </summary>
                            case SecurityTypes.Bond:
                            {
                                bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/Bond.png", UriKind.Absolute ) );
                            }
                            break;

                            /// <summary>
                            /// Warrant.
                            /// </summary>
                            case SecurityTypes.Warrant:
                            {
                                bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/Warrant.png", UriKind.Absolute ) );
                            }
                            break;

                            /// <summary>
                            /// Forward.
                            /// </summary>
                            case SecurityTypes.Forward:
                            {
                                bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/Forward.png", UriKind.Absolute ) );
                            }
                            break;

                            /// <summary>
                            /// Swap.
                            /// </summary>
                            case SecurityTypes.Swap:
                            {
                                bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/Swap.png", UriKind.Absolute ) );
                            }
                            break;

                            /// <summary>
                            /// Commodity.
                            /// </summary>
                            case SecurityTypes.Commodity:
                            {
                                bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/Commodity.png", UriKind.Absolute ) );
                            }
                            break;

                            /// <summary>
                            /// CFD.
                            /// </summary>
                            case SecurityTypes.Cfd:
                            {
                                bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/Cfd.png", UriKind.Absolute ) );
                            }
                            break;

                            /// <summary>
                            /// News.
                            /// </summary>
                            case SecurityTypes.News:
                            {
                                bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/News.png", UriKind.Absolute ) );
                            }
                            break;

                            /// <summary>
                            /// Weather.
                            /// </summary>
                            case SecurityTypes.Weather:
                            {
                                bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/Weather.png", UriKind.Absolute ) );
                            }
                            break;

                            /// <summary>
                            /// Mutual funds.
                            /// </summary>
                            case SecurityTypes.Fund:
                            {
                                bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/Fund.png", UriKind.Absolute ) );
                            }
                            break;

                            /// <summary>
                            /// American Depositary Receipts.
                            /// </summary>
                            case SecurityTypes.Adr:
                            {
                                bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/Adr.png", UriKind.Absolute ) );
                            }
                            break;

                            /// <summary>
                            /// Cryptocurrency.
                            /// </summary>
                            case SecurityTypes.CryptoCurrency:
                            {
                                bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/CryptoCurrency.png", UriKind.Absolute ) );
                            }
                            break;

                            /// <summary>
                            /// ETF.
                            /// </summary>
                            case SecurityTypes.Etf:
                            {
                                bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/Etf.png", UriKind.Absolute ) );
                            }
                            break;

                            /// <summary>
                            /// Multi leg.
                            /// </summary>
                            case SecurityTypes.MultiLeg:
                            {
                                bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/MultiLeg.png", UriKind.Absolute ) );
                            }
                            break;

                            /// <summary>
                            /// Loan.
                            /// </summary>
                            case SecurityTypes.Loan:
                            {
                                bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/Loan.png", UriKind.Absolute ) );
                            }
                            break;

                            /// <summary>
                            /// Spread.
                            /// </summary>
                            case SecurityTypes.Spread:
                            {
                                bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/Spread.png", UriKind.Absolute ) );
                            }
                            break;

                            /// <summary>
                            /// Global Depositary Receipts.
                            /// </summary>
                            case SecurityTypes.Gdr:
                            {
                                bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/Gdr.png", UriKind.Absolute ) );
                            }
                            break;

                            /// <summary>
                            /// Receipt.
                            /// </summary>
                            case SecurityTypes.Receipt:
                            {
                                bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/Receipt.png", UriKind.Absolute ) );
                            }
                            break;

                            /// <summary>
                            /// Indicator.
                            /// </summary>
                            case SecurityTypes.Indicator:
                            {
                                bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/Indicator.png", UriKind.Absolute ) );
                            }
                            break;

                            /// <summary>
                            /// Strategy.
                            /// </summary>
                            case SecurityTypes.Strategy:
                            {
                                bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/Strategy.png", UriKind.Absolute ) );
                            }
                            break;

                            /// <summary>
                            /// Volatility.
                            /// </summary>
                            case SecurityTypes.Volatility:
                            {
                                bitmap = new BitmapImage( new Uri( "pack://application:,,,/fx.AITrade;component/Images/Volatility.png", UriKind.Absolute ) );
                            }
                            break;
                        }

                    }

                }
                break;
            }


            return bitmap;
        }
    }
}
