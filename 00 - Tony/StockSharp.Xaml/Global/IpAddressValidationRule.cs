using Ecng.Common;
using MoreLinq;
using StockSharp.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Windows.Controls;

namespace Ecng.Xaml
{
    internal sealed class IpAddressValidationRule : ValidationRule
    {
        
        public bool SplitString
        {
            get => _splitString;
            set => _splitString = value;
        }
        

        bool _splitString;
        

        public override ValidationResult Validate( object ip, CultureInfo culture )
        {
            try
            {
                if ( !StringHelper.IsEmpty( ( string )ip ) )
                {
                    if ( this.SplitString )
                    {
                        ( ( IEnumerable<string> )StringHelper.SplitBySep( ( string )Converter.To<string>( ip ), ".", true ) ).ForEach<string>( a => Converter.To<IPAddress>( ( object )a ) );
                    }
                        
                    else
                        Converter.To<IPAddress>( ip );
                }
                return ValidationResult.ValidResult;
            }
            catch ( Exception ex )
            {
                return new ValidationResult( false, ( object )( LocalizedStrings.Str1459 + Environment.NewLine + ex.Message ) );
            }
        }        
    }
}
