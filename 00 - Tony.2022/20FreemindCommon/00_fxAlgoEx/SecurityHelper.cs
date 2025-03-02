using StockSharp.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockSharp.AlgoEx
{
    /// <summary>
    /// 
    /// </summary>
    public static class SecurityHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="security"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        public static bool IsRelatedNews( this Security security, string country )
        {
            if ( country == "USD" ) return true;

            switch ( security.Type )
            {
                case Messages.SecurityTypes.Currency:
                {
                    var secString = security.Code;

                    var pairs = secString.Split( '/' );

                    foreach ( string pair in pairs )
                    {
                        if ( pair == country )
                        {
                            return true;
                        }
                    }

                }
                break;

                case Messages.SecurityTypes.Cfd:
                {
                    switch ( security.Code )
                    {
                        case string us when us.Contains( ".US" ):
                        {
                            if ( country == "USD" ) return true;
                        }
                        break;

                        case string hk when hk.Contains( ".HK" ):
                        {
                            if ( country == "HKD" ) return true;
                        }
                        break;

                        case string de when de.Contains( ".DE" ):
                        {
                            if ( country == "EUR" ) return true;
                        }
                        break;

                        case string uk when uk.Contains( ".UK" ):
                        {
                            if ( country == "GBP" ) return true;
                        }
                        break;

                        case string fr when fr.Contains( ".FR" ):
                        {
                            if ( country == "EUR" ) return true;
                        }
                        break;

                        default:
                        {
                            
                        }
                        break;
                    }
                }
                break;

                case Messages.SecurityTypes.Index:
                {
                    switch ( security.Code )
                    {
                        case "AUS200":
                        {
                            if ( country == "AUD" || country == "USD" ) return true;
                        }
                        break;

                        case "ESP35":
                        {
                            if ( country == "EUR" || country == "USD" ) return true;
                        }
                        break;

                        case "FRA40":
                        {
                            if ( country == "EUR" || country == "USD" ) return true;
                        }
                        break;

                        case "GER30":
                        {
                            if ( country == "EUR" || country == "USD" ) return true;
                        }
                        break;

                        case "HKG33":
                        {
                            if ( country == "HKD" || country == "USD" ) return true;
                        }
                        break;

                        case "JNP225":
                        {
                            if ( country == "JPY" || country == "USD" ) return true;
                        }
                        break;

                        case "NAS100":
                        {
                            if ( country == "USD" ) return true;
                        }
                        break;

                        case "SPX500":
                        {
                            if ( country == "USD" ) return true;
                        }
                        break;

                        case "UK100":
                        {
                            if ( country == "GBP" || country == "USD" ) return true;
                        }
                        break;

                        case "US30":
                        {
                            if ( country == "USD" ) return true;
                        }
                        break;
                    }
                }
                break;

                case Messages.SecurityTypes.Commodity:
                {
                    switch ( security.Code )
                    {
                        case "USOIL":
                        case "USOILSPOT":
                        case "UKOIL":
                        case "UKOILSPOT":
                        {
                            if ( country == "GBP" || country == "USD" ) return true;
                        }
                        break;

                        case "SOYF":
                        {
                            if ( country == "USD" ) return true;
                        }
                        break;

                        case "WHEATF":
                        {
                            return true;
                        }                        

                        case "CORNF":
                        {
                            return true;
                        }                        
                    }
                }
                break;

                case Messages.SecurityTypes.CryptoCurrency:
                {
                    return true;
                }                
            }

            return false;
        }
    }
}
